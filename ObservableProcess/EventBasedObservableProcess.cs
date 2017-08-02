using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Elastic.ProcessManagement.Extensions;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	/// <summary>
	/// This implementation wraps over <see cref="Process.OutputDataReceived"/> and see <see cref="Process.ErrorDataReceived"/>
	/// and even though it uses the calling <see cref="Process.WaitForExit()"/> twice technique (once with timeout, once without),
	/// it still likely to complete before all the buffers have been read. This is especially common for processes that finish within
	/// a second such e.g `ipconfig /all`. For this reason its marked as obsolete but still included to play around with.
	/// Use <see cref="BufferedObservableProcess"/> or <see cref="LineByLineObservableProcess"/> instead.
	/// </summary>
	[Obsolete("Using events can lead to dropped messages, only included for educational purposes. See this class's <summary>")]
	public class EventBasedObservableProcess: ObservableProcessBase<LineOut>
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
	}
}
