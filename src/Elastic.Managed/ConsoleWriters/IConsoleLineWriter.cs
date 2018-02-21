using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	public interface IConsoleLineWriter
	{
		void Write(LineOut lineOut);
		void Write(Exception e);
	}
}
