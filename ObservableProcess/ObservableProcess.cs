using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.ProcessManagement.Extensions;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	public class BufferedObservableProcess : ObservableProcess
	{
		public BufferedObservableProcess(ObservableProcessArguments arguments) : base(arguments) { }

		protected override IObservable<ConsoleOut> CreateConsoleOutObservable()
		{
			return Observable.Create<ConsoleOut>(observer =>
			{
				if (!this.StartProcess(observer))
					return Disposable.Empty;

				this.Started = true;

				if (this.Process.HasExited)
				{
					OnExit(observer);
					return Disposable.Empty;
				}
				//var processExited = Observable.FromEventPattern(h => this.Process.Exited += h, h => this.Process.Exited -= h);
				//var processError = CreateProcessExitSubscription(processExited, observer);

				var stdOutSubscription = this.Process.ObserveStandardOutBuffered(observer);
				var stdErrSubscription = this.Process.ObserveErrorOutBuffered(observer);

				Task.WhenAll(stdOutSubscription, stdErrSubscription)
					.ContinueWith((t) => { OnExit(observer); });

				this.Process.Exited += (o, s) =>
				{
					Task.WaitAll(stdOutSubscription, stdErrSubscription);
					OnExit(observer);
				};


				return Disposable.Empty;
			});
		}
		public override IDisposable Subscribe(IConsoleOutWriter writer) =>
			this.OutStream.Subscribe(c => writer.Write(c), writer.Write);

	}


	public class ObservableProcess : IObservableProcess
	{
		private readonly ObservableProcessArguments _arguments;
		private readonly ManualResetEvent _completedHandle = new ManualResetEvent(false);

		public ObservableProcess(ObservableProcessArguments arguments)
		{
			this._arguments = arguments;
			this.Binary = this._arguments.Binary;
			this.Process = CreateProcess();
			this.Start();
		}
		public IDisposable Subscribe(IObserver<ConsoleOut> observer)
		{
			return this.OutStream.Subscribe(observer);
		}
		public virtual IDisposable Subscribe(IConsoleOutWriter writer)
		{
			return this.OutStream.Subscribe(c => writer.Write(c, finishLine: true), writer.Write);
		}

		private readonly object _lock = new object();

		private string Binary { get; }
		protected Process Process { get; }
		protected bool Started { get; set; }

		public int? LastExitCode { get; private set; }

		protected IObservable<ConsoleOut> OutStream { get; private set; } = Observable.Empty<ConsoleOut>();

		private void Start()
		{
			if (this._isDisposed) throw new ObjectDisposedException(nameof(ObservableProcess));
			if (this.Started) return;
			lock (_lock)
			{
				if (this.Started) return;
				this._completedHandle.Reset();
				this.OutStream = CreateConsoleOutObservable();
			}
		}

		protected bool StartProcess(IObserver<ConsoleOut> observer)
		{
			try
			{
				if (this.Process.Start()) return true;

				observer.OnError(new EarlyExitException($"Failed to start observable process: {this.Binary}"));
				this._completedHandle.Set();
				return false;
			}
			catch (Exception e)
			{
				observer.OnError(new EarlyExitException($"Exception while starting observable process: {this.Binary}", e.Message, e));
				this._completedHandle.Set();
			}
			return false;
		}

		protected virtual IObservable<ConsoleOut> CreateConsoleOutObservable()
		{
			return Observable.Create<ConsoleOut>(observer =>
			{
				var stdOut = this.Process.ObserveStandardOutLineByLine();
				var stdErr = this.Process.ObserveErrorOutLineByLine();

				var stdOutSubscription = stdOut.Subscribe(observer);
				var stdErrSubscription = stdErr.Subscribe(observer);

				var processExited = Observable.FromEventPattern(h => this.Process.Exited += h, h => this.Process.Exited -= h);
				var processError = CreateProcessExitSubscription(processExited, observer);

				if (!this.StartProcess(observer))
					return new CompositeDisposable(processError);

				this.Process.BeginOutputReadLine();
				this.Process.BeginErrorReadLine();

				this.Started = true;

				return new CompositeDisposable(stdOutSubscription, stdErrSubscription, processError);

			});
		}

		protected IDisposable CreateProcessExitSubscription(IObservable<EventPattern<object>> processExited, IObserver<ConsoleOut> observer)
		{
			return processExited.Subscribe(args => { OnExit(observer); }, e => observer.OnCompleted(), observer.OnCompleted);
		}

		private readonly object _exitLock = new object();
		protected void OnExit(IObserver<ConsoleOut> observer)
		{
			if (!this.Started) return;
			lock (_exitLock)
			{
				if (!this.Started) return;
				try
				{
					var c = this.Process.ExitCode;
					this.LastExitCode = c;
					var validExitCode = this._arguments.ValidExitCodePredicate?.Invoke(c) ?? c == 0;
					if (!validExitCode)
						observer.OnError(new EarlyExitException($"Process '{this._arguments.Binary}' terminated with error code {c}"));
					else observer.OnCompleted();
				}
				finally
				{
					this.Stop();
				}
			}
		}

		private Process CreateProcess()
		{
			var args = this._arguments.Args;
			var processStartInfo = new ProcessStartInfo
			{
				FileName = this._arguments.Binary,
				Arguments = args != null ? string.Join(" ", args) : string.Empty,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				RedirectStandardInput = false
			};
			this._arguments.AlterProcessStartInfo?.Invoke(processStartInfo);

			var p = new Process
			{
				EnableRaisingEvents = true,
				StartInfo = processStartInfo
			};
			return p;
		}

		/// <summary>
		/// Block until the process completes.
		/// </summary>
		/// <param name="timeout">The maximum time span we are willing to wait</param>
		/// <exception cref="EarlyExitException">an exception that indicates a problem early in the pipeline</exception>
		public bool WaitForCompletion(TimeSpan timeout)
		{
			if (this._completedHandle.WaitOne(timeout)) return true;
			this.Stop();
			return false;
		}

		private void Stop()
		{
			try
			{
				if (this.Process == null) return;
				if (this.Started)
				{
					this._arguments.OnBeforeStop?.Invoke();
					this.Process?.WaitForExit(10000);
				}

				if (this.Started && !this.Process.HasExited)
					this.Process?.Kill();

				if (this.Started)
				{
					this.Process?.WaitForExit();
				}

				this.Process?.Dispose();
			}
			catch (InvalidOperationException)
			{
			}
			finally
			{
				this.Started = false;
				//this.OutStream = null;
				this._completedHandle.Set();
			}
		}

		private bool _isDisposed = false;
		public void Dispose()
		{
			this.Stop();
			this._isDisposed = true;
		}
	}
}
