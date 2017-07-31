using System;
using System.Collections.Generic;

namespace Elastic.ProcessManagement
{
	public interface IObservableProcess : IDisposable
	{
		IObservable<ConsoleOut> Start(TimeSpan waitForStarted = default(TimeSpan));
		void Stop();

		int? LastExitCode { get; }
	}
}
