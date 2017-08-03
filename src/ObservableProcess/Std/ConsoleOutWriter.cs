using System;

namespace Elastic.ProcessManagement.Std
{
	public class ConsoleOutWriter : IConsoleOutWriter
	{
		public virtual void Write(Exception e)
		{
			var ee = e as CleanExitException;
			if (ee == null) throw e;
			Console.WriteLine(e.Message);
			if (!string.IsNullOrEmpty(ee.HelpText))
			{
				Console.WriteLine(ee.HelpText);
			}

		}
		public virtual void Write(ConsoleOut consoleOut)
		{
			if (consoleOut.Error)
				consoleOut.CharsOrString(ErrorCharacters, ErrorLine);
			else
				consoleOut.CharsOrString(OutCharacters, OutLine);
		}

		protected static void ErrorCharacters(char[] data) => Console.Error.Write(data);
		protected static void ErrorLine(string data) => Console.Error.WriteLine(data);
		protected static void OutCharacters(char[] data) => Console.Write(data);
		protected static void OutLine(string data) => Console.WriteLine(data);

	}
}
