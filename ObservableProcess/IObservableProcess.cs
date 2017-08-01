using System;
using System.Collections.Generic;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	public interface IObservableProcess : IDisposable, IObservable<ConsoleOut>
	{
		IDisposable Subscribe(IConsoleOutWriter writer);

		int? LastExitCode { get; }
	}
}
