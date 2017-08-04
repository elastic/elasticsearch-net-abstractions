using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	public abstract class ObservableProcessBase<TConsoleOut> : IObservableProcess<TConsoleOut>
		where TConsoleOut : ConsoleOut
	{
		private readonly ObservableProcessArguments _arguments;
		private readonly ManualResetEvent _completedHandle = new ManualResetEvent(false);

		protected ObservableProcessBase(string binary, params string[] arguments)
			: this(new ObservableProcessArguments(binary, arguments)) { }

		protected ObservableProcessBase(ObservableProcessArguments arguments)
		{
			this._arguments = arguments;
			this.Binary = this._arguments.Binary;
			this.Process = CreateProcess();
			this.Start();
		}

		public virtual IDisposable Subscribe(IObserver<TConsoleOut> observer) => this.OutStream.Subscribe(observer);

		public IDisposable Subscribe(IConsoleOutWriter writer) => this.OutStream.Subscribe(writer.Write, writer.Write, delegate { });

		private readonly object _lock = new object();

		private string Binary { get; }
		protected Process Process { get; }
		protected bool Started { get; set; }

		public int? ExitCode { get; private set; }

		protected IObservable<TConsoleOut> OutStream { get; private set; } = Observable.Empty<TConsoleOut>();

		private void Start()
		{
			if (this._isDisposed) throw new ObjectDisposedException(nameof(ObservableProcessBase<TConsoleOut>));
			if (this.Started) return;
			lock (_lock)
			{
				if (this.Started) return;
				this._completedHandle.Reset();
				this.OutStream = CreateConsoleOutObservable();
			}
		}

		protected abstract IObservable<TConsoleOut> CreateConsoleOutObservable();

		protected bool StartProcess(IObserver<TConsoleOut> observer)
		{
			try
			{
				if (this.Process.Start()) return true;

				OnError(observer, new CleanExitException($"Failed to start observable process: {this.Binary}"));
				this._completedHandle.Set();
				return false;
			}
			catch (Exception e)
			{
				OnError(observer, new CleanExitException($"Exception while starting observable process: {this.Binary}", e.Message, e));
				this._completedHandle.Set();
			}
			return false;
		}

		protected IDisposable CreateProcessExitSubscription(IObservable<EventPattern<object>> processExited, IObserver<TConsoleOut> observer)
		{
			return processExited.Subscribe(args => { OnExit(observer); }, e => OnCompleted(observer), ()=> OnCompleted(observer));
		}

		protected virtual void OnError(IObserver<TConsoleOut> observer, Exception e) => observer.OnError(e);
		protected virtual void OnCompleted(IObserver<TConsoleOut> observer) => observer.OnCompleted();

		private readonly object _exitLock = new object();

		protected void OnExit(IObserver<TConsoleOut> observer)
		{
			if (!this.Started || this.ExitCode.HasValue) return;
			lock (_exitLock)
			{
				if (!this.Started || this.ExitCode.HasValue) return;
				try
				{
					var c = this.Process.ExitCode;
					this.ExitCode = c;
					var validExitCode = this._arguments.ValidExitCodePredicate?.Invoke(c) ?? c == 0;
					if (!validExitCode)
						OnError(observer, new CleanExitException($"Process '{this._arguments.Binary}' terminated with error code {c}"));
					else OnCompleted(observer);
				}
				catch (Exception e)
				{

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
		/// <exception cref="CleanExitException">an exception that indicates a problem early in the pipeline</exception>
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
