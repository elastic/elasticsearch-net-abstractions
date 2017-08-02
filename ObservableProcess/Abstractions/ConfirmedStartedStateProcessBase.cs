using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement.Abstractions
{
	public abstract class ConfirmedStartedStateProcessBase : IDisposable
	{
		public int? LastExitCode => this._process?.ExitCode;
		private readonly object _lock = new object();
		private readonly ManualResetEvent _completedHandle = new ManualResetEvent(false);
		private readonly ManualResetEvent _startedHandle = new ManualResetEvent(false);
		private readonly IConsoleOutWriter _writer;
		private readonly IObservableProcess _process;

		private bool _started;
		private CompositeDisposable _disposables = new CompositeDisposable();

		protected ConfirmedStartedStateProcessBase(
			IObservableProcess observableProcess,
			IConsoleOutWriter consoleOutWriter
			)
		{
			this._process = observableProcess ?? throw new ArgumentNullException(nameof(observableProcess));
			this._writer = consoleOutWriter ?? new ConsoleOutWriter();
		}

		/// <summary>
		/// A human readable string that can be used to identify the process in exception messages
		/// </summary>
		protected abstract string PrintableName { get; }

		/// <summary>
		/// Handle the messages coming out of your process, return true to signal the process was confirmed to be in started state.
		/// </summary>
		/// <returns>Return true to signal you've confirmed the process has really started</returns>
		protected abstract bool HandleMessage(ConsoleOut message);

		/// <summary>
		/// Start the observable process and monitor its std out and error for a functional started message
		/// </summary>
		/// <param name="waitTimeout">How long we want to wait for the started confirmation before bailing</param>
		/// <param name="subscribeToMessagesAfterStartedConfirmation">
		/// By default we no longer subscribe after we recieve the started confirmation. Set this to true to
		/// keep receiving the console out messages wrapped in <see cref="ConsoleOut"/>. This boolean has no effect on the running state.
		/// The process will keep running until it either decides to stop or <see cref="Stop"/> is called.
		/// </param>
		/// <exception cref="EarlyExitException">an exception that indicates a problem early in the pipeline</exception>
		public virtual void Start(TimeSpan waitTimeout = default(TimeSpan), bool subscribeToMessagesAfterStartedConfirmation = false)
		{
			var timeout = waitTimeout == default(TimeSpan) ? TimeSpan.FromMinutes(2) : waitTimeout;
			this.Stop();
			lock (_lock)
			{
				this._startedHandle.Reset();
				this._completedHandle.Reset();

				var observable = this._process.Publish();
				if (subscribeToMessagesAfterStartedConfirmation)
				{
					//subscribe to all messages and write them to console
					this._disposables.Add(observable.Subscribe(c=>this._writer.Write(c), delegate { }, delegate { }));
				}

				//subscribe as long we are not in started state and attempt to read console
				//out for this confirmation
				this._disposables.Add(observable
					.TakeWhile(c => !this._started)
					.Subscribe(this.Handle, delegate { }, delegate { })
				);
				this._disposables.Add(observable.Subscribe(delegate { }, HandleException, HandleCompleted));
				this._disposables.Add(observable.Connect());

				if (this._startedHandle.WaitOne(timeout)) return;
			}
			this.Stop();
			throw new EarlyExitException($"Could not start process within ({timeout}): {PrintableName}");
		}

		/// <summary>
		/// Block until the process completes.
		/// </summary>
		/// <param name="timeout">The maximum time span we are willing to wait</param>
		/// <exception cref="EarlyExitException">an exception that indicates a problem early in the pipeline</exception>
		public void WaitForCompletion(TimeSpan timeout)
		{
			if (!this._completedHandle.WaitOne(timeout))
				throw new EarlyExitException($"Could not run process to completion within ({timeout}): {PrintableName}");
		}

		public virtual void Stop()
		{
			lock (_lock)
			{
				this._completedHandle.Reset();
				this._startedHandle.Reset();
				this._process?.Dispose();
				this._disposables?.Dispose();
				this._disposables = new CompositeDisposable();
			}
		}

		private void ConfirmProcessStarted()
		{
			this._started = true;
			this._startedHandle.Set();
		}

		private void HandleException(Exception e)
		{
			this._completedHandle.Set();
			this._startedHandle.Set();
			throw e;
		}

		private void HandleCompleted()
		{
			this._startedHandle.Set();
			this._completedHandle.Set();
		}

		private void Handle(ConsoleOut message)
		{
			if (this.HandleMessage(message))
				this.ConfirmProcessStarted();
		}

		public void Dispose() => this.Stop();
	}
}
