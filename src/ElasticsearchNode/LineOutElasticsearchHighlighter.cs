using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Elastic.ManagedNode
{
	public static class LineOutElasticsearchHighlighter
	{
		private static readonly object _lock = new object();
		public static void Write(bool error, string date, string level, string section, string node, string message, Func<string, ConsoleColor> getNodeColor = null)
		{
			lock (_lock)
			{
				var w = error ? Console.Error : Console.Out;
				WriteBlock(w, ConsoleColor.DarkGray, date);
				WriteBlock(w, LevelColor(level), level, 5);

				if (!string.IsNullOrWhiteSpace(section))
				{
					WriteBlock(w, ConsoleColor.DarkCyan, section, 25);
					WriteSpace(w);
				}

				WriteBlock(w, getNodeColor?.Invoke(node) ?? ConsoleColor.DarkGreen, node);
				WriteSpace(w);

				var messageColor = error || level == "ERROR" ? ConsoleColor.Red : ConsoleColor.White;
				WriteMessage(w, messageColor, message);

				Console.ResetColor();
				w.Flush();
			}
		}

		public static void WriteCausedBy(bool error, string cause, string causeMessage)
		{
			lock (_lock)
			{
				var w = error ? Console.Error : Console.Out;
				Write(w, ConsoleColor.DarkRed, cause);
				WriteSpace(w);
				Write(w, ConsoleColor.Red, causeMessage);
				w.WriteLine();
				Console.ResetColor();
				w.Flush();

			}
		}

		public static void WriteStackTraceLine(bool error, string at, string method, string file, string jar)
		{
			lock (_lock)
			{
				var w = error ? Console.Error : Console.Out;
				Write(w, ConsoleColor.DarkGray, at);
				Write(w, ConsoleColor.DarkBlue, method);
				Write(w, ConsoleColor.DarkGray, "(");
				Write(w, ConsoleColor.Blue, file);
				Write(w, ConsoleColor.DarkGray, ")");
				WriteSpace(w);
				Write(w, ConsoleColor.Gray, jar);
				w.WriteLine();

				Console.ResetColor();
				w.Flush();

			}
		}


		public static void Write(bool error, string message)
		{
			lock (_lock)
			{
				var w = error ? Console.Error : Console.Out;
				var messageColor = error ? ConsoleColor.Red : ConsoleColor.White;
				WriteMessage(w, messageColor, message);
				Console.ResetColor();
				w.Flush();
			}
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

		private static readonly char[] Anchors = {'[', ']', '{', '}'};
		private static IEnumerable<string> Parts(string s)
		{
			int start = 0, index;
			while ((index = s.IndexOfAny(Anchors, start)) != -1)
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
			var insideSquareBracket = 0;
			var insideCurlyBracket = 0;
			foreach (var p in Parts(message))
			{
				if (p.Length == 0) continue;
				if (p[0] == '[') insideSquareBracket++;
				else if (p[0] == ']') insideSquareBracket--;
				else if (p[0] == '{') insideCurlyBracket++;
				else if (p[0] == '}') insideCurlyBracket--;

				if (Anchors.Contains(p[0])) Console.ForegroundColor = ConsoleColor.DarkGray;
				else if (insideSquareBracket > 0) Console.ForegroundColor = ConsoleColor.Yellow;
				else if (insideCurlyBracket > 0) Console.ForegroundColor = ConsoleColor.Blue;
				else Console.ForegroundColor = color;

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
		private static void Write(TextWriter w, ConsoleColor color, string block, int? pad = null)
		{
			var b = pad != null ? block.PadRight(pad.Value) : block;
			var original = Console.ForegroundColor;
			Console.ForegroundColor = color;
			w.Write(b);
			Console.ForegroundColor = original;
		}

	}
}
