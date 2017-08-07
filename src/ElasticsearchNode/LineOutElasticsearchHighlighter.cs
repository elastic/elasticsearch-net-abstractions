using System;
using System.Collections.Generic;
using System.IO;

namespace Elastic.ProcessManagement
{
	public static class LineOutElasticsearchHighlighter
	{
		public static void Write(bool error, string date, string level, string section, string node, string message)
		{
			var w = error ? Console.Error : Console.Out;
			WriteBlock(w, ConsoleColor.DarkGray, date);
			WriteBlock(w, LevelColor(level), level, 5);

			if (!string.IsNullOrWhiteSpace(section))
			{
				WriteBlock(w, ConsoleColor.DarkCyan, section, 25);
				WriteSpace(w);
			}
			WriteBlock(w, ConsoleColor.DarkGreen, node);
			WriteSpace(w);

			var messageColor = error || level == "ERROR" ? ConsoleColor.Red : ConsoleColor.White;
			WriteMessage(w, messageColor, message);

			Console.ResetColor();
			w.Flush();
		}

		private static ConsoleColor LevelColor(string level)
		{
			switch (level ?? "")
			{
				case "WARN": return ConsoleColor.Yellow;
				case "FATAL":
				case "ERROR":
					return ConsoleColor.Red;
				case "DEBUG":
				case "TRACE":
					return ConsoleColor.DarkGray;
				default:
					return ConsoleColor.Cyan;
			}
		}

		private static readonly char[] _anchors = {'[', ']'};
		private static IEnumerable<string> Parts(string s)
		{
			int start = 0, index;
			while ((index = s.IndexOfAny(_anchors, start)) != -1)
			{
				if(index-start > 0)
					yield return s.Substring(start, index - start);

				yield return s.Substring(index, 1);
				start = index + 1;
			}
			if (start < s.Length)
				yield return s.Substring(start);
		}

		private static void WriteMessage(TextWriter w, ConsoleColor color, string message)
		{
			var inside = 0;
			foreach (var p in Parts(message))
			{
				if (p.Length == 0) continue;
				if (p[0] == '[') inside++;
				if (p[0] == ']') inside--;
				Console.ForegroundColor =
					(p[0] == '[' && inside > 1)
					|| (p[0] != '[' && inside > 0)? ConsoleColor.Yellow : color;
				w.Write(p);
			}
			Console.ResetColor();
			w.WriteLine();

		}

		private static void WriteSpace(TextWriter w) => w.Write(" ");
		private static void WriteBlock(TextWriter w, ConsoleColor color, string block, int? pad = null)
		{
			var b = pad != null ? block.PadRight(pad.Value) : block;
			Console.ForegroundColor = ConsoleColor.DarkGray;
			w.Write("[");
			Console.ForegroundColor = color;
			w.Write(b);
			Console.ForegroundColor = ConsoleColor.DarkGray;
			w.Write("]");

		}
	}
}
