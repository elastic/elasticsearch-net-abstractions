using System;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Elastic.ProcessManagement.Extensions;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	public class BufferedObservableProcess : ObservableProcessBase
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
				var stdOutSubscription = this.Process.ObserveStandardOutBuffered(observer);
				var stdErrSubscription = this.Process.ObserveErrorOutBuffered(observer);

				Task.WhenAll(stdOutSubscription, stdErrSubscription)
					.ContinueWith((t) => { OnExit(observer); });

				this.Process.Exited += (o, s) =>
				{
					var timeout = TimeSpan.FromSeconds(20000);
					if (!Task.WaitAll(new [] { stdOutSubscription, stdErrSubscription }, timeout))
						OnError(observer, new EarlyExitException(
							$"Waited {timeout} unsuccesfully for stdout/err subscriptions to complete after the the process exited"
						));

					OnExit(observer);
				};

				return Disposable.Empty;
			});
		}
	}
}
