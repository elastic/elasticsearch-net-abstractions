using System;

namespace Elastic.ProcessManagement.Std
{
	public class ConsoleOutColorWriter : ConsoleOutWriter
	{
		private readonly object _lock = new object();
		public override void Write(ConsoleOut consoleOut, bool finishLine = false)
		{
			var data = finishLine ? consoleOut.Data + Environment.NewLine : consoleOut.Data;
			lock (_lock)
			{
				if (consoleOut.Error)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Error.Write(data);
				}
				else
				{
					Console.ResetColor();
					Console.Write(data);
				}
				Console.ResetColor();
			}
		}
		public override void Write(Exception e)
		{
			var ee = e as EarlyExitException;
			if (ee == null) throw e;
			lock (_lock)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write(e.Message);
				if (!string.IsNullOrEmpty(ee.HelpText))
				{
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.Write(ee.HelpText);
				}
				Console.ResetColor();
			}
		}
	}
}
