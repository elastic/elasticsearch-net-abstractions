using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	public class NoColorWriter : IConsoleOutWriter
	{
		public void Write(LineOut lineOut)
		{
			var w = lineOut.Error ? Console.Error : Console.Out;
			w.WriteLine(lineOut);
		}

		public void Write(Exception e) => Console.Error.WriteLine(e);
	}
}