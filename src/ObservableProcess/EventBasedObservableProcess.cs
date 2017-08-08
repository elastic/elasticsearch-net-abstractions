using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Elastic.ProcessManagement.Extensions;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	/// <summary>
	/// This implementation wraps over <see cref="Process.OutputDataReceived"/> and <see cref="Process.ErrorDataReceived"/>
	/// it utilizes a double call to <see cref="Process.WaitForExit()"/> once with timeout and once without to ensure all events are
	/// received.
	/// </summary>
	public class EventBasedObservableProcess: ObservableProcessBase<LineOut>, ISubscribeLines
	{
		public EventBasedObservableProcess(string binary, params string[] arguments) : base(binary, arguments) { }

		public EventBasedObservableProcess(ObservableProcessArguments arguments) : base(arguments) { }

		protected override IObservable<LineOut> CreateConsoleOutObservable()
		{
			return Observable.Create<LineOut>(observer =>
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

		private IDisposable CreateProcessExitSubscription(IObservable<EventPattern<object>> processExited, IObserver<LineOut> observer) =>
			processExited.Subscribe(args => { OnExit(observer); }, e => OnCompleted(observer), ()=> OnCompleted(observer));
	}
}
