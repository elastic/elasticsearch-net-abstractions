using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace Elastic.ProcessManagement.Functional
{
	public abstract class ConfirmedStartedStateProcessBase : IDisposable
	{
		public int? LastExitCode => this._process?.LastExitCode;
		private readonly object _lock = new object();
		private readonly ManualResetEvent _completedHandle = new ManualResetEvent(false);
		private readonly ManualResetEvent _startedHandle = new ManualResetEvent(false);
		private readonly IConsoleOutHandler _handler;
		private readonly IObservableProcess _process;

		private bool _started;
		private CompositeDisposable _disposables = new CompositeDisposable();


		protected ConfirmedStartedStateProcessBase(
			IObservableProcess observableProcess,
			IConsoleOutHandler consoleOutHandler
			)
		{
			this._process = observableProcess ?? throw new ArgumentNullException(nameof(observableProcess));
			this._handler = consoleOutHandler ?? new ConsoleOutHandler();
		}

		protected abstract string PrintableName { get; }
		protected abstract bool HandleMessage(ConsoleOut message);

		public virtual void Start(TimeSpan waitForStarted = default(TimeSpan), bool subscribeToMessagesAfterStartedConfirmation = false)
		{
			var timeout = waitForStarted == default(TimeSpan) ? TimeSpan.FromMinutes(2) : waitForStarted;
			this.Stop();
			lock (_lock)
			{
				this._startedHandle.Reset();

				var observable = this._process.Start().Publish();
				if (subscribeToMessagesAfterStartedConfirmation)
				{
					//subscribe to all messages and write them to console
					this._disposables.Add(observable.Subscribe(this._handler.Write, delegate { }, delegate { }));
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
			throw new StartupException($"Could not start process within ({timeout}): {PrintableName}");
		}

		public void WaitForCompletion(TimeSpan timeout)
		{
			if (!this._completedHandle.WaitOne(timeout))
				throw new StartupException($"Could not run process to completion within ({timeout}): {PrintableName}");
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
			this._handler.Handle(message);
			if (this.HandleMessage(message))
				this.ConfirmProcessStarted();
		}

		public void Dispose() => this.Stop();
	}
}
