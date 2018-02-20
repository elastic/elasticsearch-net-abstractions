using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly IList<string> _nodes;
		public ElasticsearchConsoleOutWriter() { }

		private readonly Dictionary<string, ConsoleColor> _nodeColors;
		private readonly ConsoleColor[] _availableNodeColors =
		{
			ConsoleColor.DarkGreen,
			ConsoleColor.DarkBlue,
			ConsoleColor.DarkRed,
			ConsoleColor.DarkCyan,
			ConsoleColor.DarkYellow,
			ConsoleColor.Blue,
		};


		public ElasticsearchConsoleOutWriter(IList<string> nodes)
		{
			_nodes = nodes;
			var colors = new Dictionary<string, ConsoleColor>();
			for (var i = 0; i < nodes.Count; i++)
			{
				var color = i % _availableNodeColors.Length;
				colors.Add(nodes[i], _availableNodeColors[color]);
			}

			this._nodeColors = colors;
		}

		public void Write(LineOut lineOut)
		{
			var parsed = ElasticsearchConsoleOutParser.TryParse(lineOut.Line, out var date, out var level, out var section, out var node, out var message, out _);
			if (parsed)
				LineOutElasticsearchHighlighter.Write(lineOut.Error, date, level, section, node, message, NodeColor);
			else if (ElasticsearchExceptionLogParser.TryParseCause(lineOut.Line, out var cause, out var causeMessage))
				LineOutElasticsearchHighlighter.WriteCausedBy(lineOut.Error, cause, causeMessage);

			else if (ElasticsearchExceptionLogParser.TryParseStackTrace(lineOut.Line, out var at, out var method, out var file, out var jar))
				LineOutElasticsearchHighlighter.WriteStackTraceLine(lineOut.Error, at, method, file, jar);

			else LineOutElasticsearchHighlighter.Write(lineOut.Error, lineOut.Line);
		}

		public ConsoleColor NodeColor(string node) => (_nodeColors != null && _nodeColors.TryGetValue(node, out var color)) ? color : _availableNodeColors[0];

		public void Write(Exception e)
		{
			Console.Error.WriteLine(e.Message);
		}
	}
}
