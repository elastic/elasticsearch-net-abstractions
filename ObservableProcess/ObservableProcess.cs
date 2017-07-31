using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Elastic.ProcessManagement
{
	public class ObservableProcessArguments
	{
		public string Binary { get; }
		public IEnumerable<string> Args { get; }

		public ObservableProcessArguments(string binary, IEnumerable<string> args)
			: this(binary, args?.ToArray()) { }

		public ObservableProcessArguments(string binary, params string[] args)
		{
			this.Binary = binary;
			this.Args = args;
		}

		public Action OnBeforeStop { get; set; }
		public Action<ProcessStartInfo> AlterProcessStartInfo { get; set; }
	}


	public class ObservableProcess : IObservableProcess
	{
		private readonly ObservableProcessArguments _arguments;

		public ObservableProcess(ObservableProcessArguments arguments)
		{
			this._arguments = arguments;
			this.Binary = this._arguments.Binary;
			this.Process = CreateProcess();
		}

		private readonly object _lock = new object();

		private string Binary { get; }
		private Process Process { get; }
		private bool Started { get; set; }
		public int? LastExitCode { get; private set; }

		private IObservable<ConsoleOut> OutStream { get; set; } = Observable.Empty<ConsoleOut>();

		public IObservable<ConsoleOut> Start(TimeSpan waitForStarted = default(TimeSpan))
		{
			if (this._isDisposed) throw new ObjectDisposedException(nameof(ObservableProcess));
			if (this.Started) return OutStream;
			lock (_lock)
			{
				if (this.Started) return OutStream;

				this.OutStream = Observable.Create<ConsoleOut>(observer =>
				{
					// listen to stdout and stderr
					var stdOut = this.Process.CreateStandardOutputObservable();
					var stdErr = this.Process.CreateStandardErrorObservable();

					var stdOutSubscription = stdOut.Subscribe(observer);
					var stdErrSubscription = stdErr.Subscribe(observer);

					var processExited = Observable.FromEventPattern(h => this.Process.Exited += h, h => this.Process.Exited -= h);
					var processError = CreateProcessExitSubscription(this.Process, processExited, observer);

					if (!this.Process.Start())
						throw new StartupException($"Failed to start observable process: {this.Binary}");

					this.Process.BeginOutputReadLine();
					this.Process.BeginErrorReadLine();
					this.Process.Exited += (sender, eventArgs) => observer.OnCompleted();
					this.Started = true;

					return new CompositeDisposable(stdOutSubscription, stdErrSubscription, processError);
				});
				return this.OutStream;
			}
		}

		private IDisposable CreateProcessExitSubscription(Process process, IObservable<EventPattern<object>> processExited,
			IObserver<ConsoleOut> observer)
		{
			return processExited.Subscribe(args =>
			{
				try
				{
					this.LastExitCode = process?.ExitCode ?? 0;

					//if process does not terminated with 0 (no errors) or 130 (cancellation requested on java.exe)
					//throw an exception that bubbles back out to Program (elasticsearch.exe)
					if (this.LastExitCode > 0 && this.LastExitCode != 130)
					{
						observer.OnError(new StartupException(
							$"Process '{process.StartInfo.FileName}' terminated with error code {process.ExitCode}"));
					}
					else observer.OnCompleted();
				}
				finally
				{
					this.Stop();
				}
			}, e => { observer.OnCompleted(); }, observer.OnCompleted);
		}

		private Process CreateProcess()
		{
			var args = this._arguments.Args;
			var processStartInfo = new ProcessStartInfo
			{
				FileName = this._arguments.Binary,
				Arguments =  args != null ? string.Join(" ", args) : string.Empty,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				RedirectStandardInput = false,
			};
			this._arguments.AlterProcessStartInfo?.Invoke(processStartInfo);

			var p = new Process
			{
				EnableRaisingEvents = true,
				StartInfo = processStartInfo
			};
			return p;
		}

		public void Stop()
		{
			try
			{
				if (this.Process == null) return;
				if (this.Started)
					this._arguments.OnBeforeStop?.Invoke();

				this.Process?.Kill();
				this.Process?.WaitForExit(2000);
				this.Process?.Dispose();
			}
			catch (InvalidOperationException)
			{
			}
			finally
			{
				this.Started = false;
				this.OutStream = null;
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
