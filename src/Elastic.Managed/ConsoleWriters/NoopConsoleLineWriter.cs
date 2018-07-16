using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	internal class NoopConsoleLineWriter : IConsoleLineWriter
	{
		public static NoopConsoleLineWriter Instance { get; } = new NoopConsoleLineWriter();
		public void Write(LineOut lineOut) { }

		public void Write(Exception e) { }
	}
}