using System;
using ProcNet.Std;

namespace Elastic.ManagedNode
{
	public interface IElasticsearchConsoleOutWriter
	{
		void Write(LineOut lineOut);
		void Write(Exception e);
	}

	public class ElasticsearchConsoleOutWriter : IElasticsearchConsoleOutWriter
	{
		public void Write(LineOut lineOut)
		{
			var parsed = ElasticsearchConsoleOutParser.TryParse(lineOut.Line, out var date, out var level, out var section, out var node, out var message, out _);
			if (parsed)
				LineOutElasticsearchHighlighter.Write(lineOut.Error, date, level, section, node, message);
			else Console.WriteLine(lineOut.Line);
		}

		public void Write(Exception e)
		{
			Console.Error.WriteLine(e.Message);
		}
	}
}
