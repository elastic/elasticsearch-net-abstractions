using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	internal class NoopConsoleLineWriter : IConsoleLineHandler
	{
		public static NoopConsoleLineWriter Instance { get; } = new NoopConsoleLineWriter();
		public void Handle(LineOut lineOut) { }

		public void Handle(Exception e) { }
	}
}