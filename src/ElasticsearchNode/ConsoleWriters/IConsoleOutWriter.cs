using System;
using System.Linq;
using ProcNet.Std;

namespace Elastic.ManagedNode
{
	public interface IConsoleOutWriter
	{
		void Write(LineOut lineOut);
		void Write(Exception e);
	}
}
