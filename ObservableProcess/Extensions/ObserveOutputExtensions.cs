using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement.Extensions
{
	internal static class ObserveOutputExtensions
	{
		public static IObservable<ConsoleOut> ObserveErrorOutLineByLine(this Process process)
		{
			var receivedStdErr =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.ErrorDataReceived += h, h => process.ErrorDataReceived -= h)
					.Select(e => ConsoleOut.ErrorOut(e.EventArgs.Data));

			return Observable.Create<ConsoleOut>(observer =>
			{
				var cancel = Disposable.Create(process.CancelErrorRead);
				return new CompositeDisposable(cancel, receivedStdErr.Subscribe(observer));
			});
		}

		public static IObservable<ConsoleOut> ObserveStandardOutLineByLine(this Process process)
		{
			var receivedStdOut =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.OutputDataReceived += h, h => process.OutputDataReceived -= h)
					.Select(e => ConsoleOut.Out(e.EventArgs.Data));

			return Observable.Create<ConsoleOut>(observer =>
			{
				var cancel = Disposable.Create(process.CancelOutputRead);
				return new CompositeDisposable(cancel, receivedStdOut.Subscribe(observer));
			});
		}

		public static Task ObserveErrorOutBuffered(this Process process, IObserver<ConsoleOut> observer)
		{
			var reader = process.StandardError;
			return Task.Run(async () => await BufferedRead(reader, observer, ConsoleOut.ErrorOut));
		}

		public static Task ObserveStandardOutBuffered(this Process process, IObserver<ConsoleOut> observer)
		{
			var reader = process.StandardOutput;
			return Task.Run(async () => await BufferedRead(reader, observer, ConsoleOut.Out));
		}

		private static async Task BufferedRead(StreamReader reader, IObserver<ConsoleOut> observer, Func<char[], ConsoleOut> map)
		{
			while (!reader.EndOfStream)
			{
				var buffer = new char[256];
				var read = await reader.ReadAsync(buffer, 0, buffer.Length);
				if (read > 0)
				{
					observer.OnNext(map(buffer));
				}
				else
					await Task.Delay(10);
			}
		}


	}
}
