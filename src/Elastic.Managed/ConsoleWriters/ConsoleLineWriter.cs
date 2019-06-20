using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	public class ConsoleLineWriter : IConsoleLineHandler
	{
		public void Handle(LineOut lineOut)
		{
			var w = lineOut.Error ? Console.Error : Console.Out;
			w.WriteLine(lineOut);
		}

		public void Handle(Exception e) => Console.Error.WriteLine(e);
	}
}
