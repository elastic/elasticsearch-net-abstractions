using System;

namespace Elastic.ProcessManagement
{
	public class ConsoleOutHandler : IConsoleOutHandler
	{
		public void Handle(ConsoleOut consoleOut) { }

		public virtual void Write(ConsoleOut consoleOut)
		{
			if (consoleOut.Error)
				Console.Error.WriteLine(consoleOut.Data);
			else
				Console.WriteLine(consoleOut.Data);
		}
	}
}