using System;

namespace Elastic.ProcessManagement.Std
{
	public class ConsoleOutWriter : IConsoleOutWriter
	{
		public virtual void Write(Exception e)
		{
			var ee = e as EarlyExitException;
			if (ee == null) throw ee;
			Console.WriteLine(e.Message);
			if (!string.IsNullOrEmpty(ee.HelpText))
			{
				Console.WriteLine(ee.HelpText);
			}

		}
		public virtual void Write(ConsoleOut consoleOut, bool finishLine = false)
		{
			var data = finishLine ? consoleOut.Data + Environment.NewLine : consoleOut.Data;

			if (consoleOut.Error)
				Console.Error.Write(data);
			else
				Console.Write(data);
		}
	}
}
