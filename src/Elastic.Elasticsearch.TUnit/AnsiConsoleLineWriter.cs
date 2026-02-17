// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using ProcNet.Std;

namespace Elastic.Elasticsearch.TUnit;

/// <summary>
///     A console line handler that writes colorized output using ANSI escape codes
///     directly to the process's stdout/stderr streams, bypassing any
///     <see cref="Console.SetOut" /> interception by test frameworks.
///     <para>
///     This ensures cluster bootstrap output is visible on the terminal
///     rather than captured per-test by TUnit.
///     </para>
/// </summary>
internal class AnsiConsoleLineWriter : IConsoleLineHandler
{
	private static readonly object Lock = new();
	private static readonly bool UseColor;

	private readonly TextWriter _out;
	private readonly TextWriter _err;
	private readonly Dictionary<string, string> _nodeColors;

	private static readonly string[] NodeAnsiColors =
	[
		"\x1b[32m",  // Green
		"\x1b[34m",  // Blue
		"\x1b[31m",  // Red
		"\x1b[36m",  // Cyan
		"\x1b[33m",  // Yellow
		"\x1b[94m",  // Bright Blue
	];

	static AnsiConsoleLineWriter()
	{
		// Respect NO_COLOR convention (https://no-color.org/)
		if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NO_COLOR")))
		{
			UseColor = false;
			return;
		}

		var term = Environment.GetEnvironmentVariable("TERM") ?? "";
		UseColor = term.Contains("color", StringComparison.OrdinalIgnoreCase)
		           || term.Contains("xterm", StringComparison.OrdinalIgnoreCase)
		           || term.Contains("screen", StringComparison.OrdinalIgnoreCase)
		           || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"))
		           || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS"))
		           || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"))
		           || Environment.UserInteractive;
	}

	public AnsiConsoleLineWriter(IList<string> nodes)
	{
		_out = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
		_err = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };

		_nodeColors = new Dictionary<string, string>();
		if (nodes != null)
		{
			for (var i = 0; i < nodes.Count; i++)
				_nodeColors[nodes[i]] = NodeAnsiColors[i % NodeAnsiColors.Length];
		}
	}

	public void Handle(LineOut lineOut)
	{
		var parsed = LineOutParser.TryParse(lineOut.Line,
			out var date, out var level, out var section, out var node, out var message, out _);

		lock (Lock)
		{
			var w = lineOut.Error ? _err : _out;

			if (parsed)
				WriteParsedLine(w, date, level, section, node, message);
			else if (ExceptionLineParser.TryParseCause(lineOut.Line, out var cause, out var causeMessage))
				WriteCausedBy(w, cause, causeMessage);
			else if (ExceptionLineParser.TryParseStackTrace(lineOut.Line, out var at, out var method,
				         out var file, out var jar))
				WriteStackTrace(w, at, method, file, jar);
			else
				WriteRaw(w, lineOut.Error, lineOut.Line);
		}
	}

	public void Handle(Exception e)
	{
		lock (Lock)
		{
			_err.Write(Color("\x1b[91m")); // Bright Red
			_err.WriteLine(e);
			_err.Write(Reset);
		}
	}

	private void WriteParsedLine(TextWriter w, string date, string level, string section, string node, string message)
	{
		WriteBlock(w, "\x1b[90m", date);        // Dark Gray
		WriteBlock(w, LevelColor(level), level, 5);

		if (!string.IsNullOrWhiteSpace(section))
		{
			WriteBlock(w, "\x1b[36m", section, 25); // Cyan
			w.Write(' ');
		}

		var nodeColor = _nodeColors.TryGetValue(node, out var nc) ? nc : "\x1b[32m";
		WriteBlock(w, nodeColor, node);
		w.Write(' ');

		var messageColor = level == "ERROR" ? "\x1b[31m" : "\x1b[37m"; // Red or White
		w.Write(Color(messageColor));
		w.WriteLine(message);
		w.Write(Reset);
	}

	private static void WriteCausedBy(TextWriter w, string cause, string message)
	{
		w.Write(Color("\x1b[31m")); // Red
		w.Write(cause);
		w.Write(Reset);
		w.Write(' ');
		w.Write(Color("\x1b[91m")); // Bright Red
		w.WriteLine(message);
		w.Write(Reset);
	}

	private static void WriteStackTrace(TextWriter w, string at, string method, string file, string jar)
	{
		w.Write(Color("\x1b[90m")); // Dark Gray
		w.Write(at);
		w.Write(Color("\x1b[34m")); // Blue
		w.Write(method);
		w.Write(Color("\x1b[90m")); // Dark Gray
		w.Write('(');
		w.Write(Color("\x1b[94m")); // Bright Blue
		w.Write(file);
		w.Write(Color("\x1b[90m")); // Dark Gray
		w.Write(')');
		w.Write(' ');
		w.Write(Color("\x1b[37m")); // White
		w.WriteLine(jar);
		w.Write(Reset);
	}

	private static void WriteRaw(TextWriter w, bool error, string line)
	{
		w.Write(Color(error ? "\x1b[31m" : "\x1b[37m"));
		w.WriteLine(line);
		w.Write(Reset);
	}

	private static void WriteBlock(TextWriter w, string color, string text, int? pad = null)
	{
		if (string.IsNullOrEmpty(text)) return;
		var padded = pad.HasValue ? text.PadRight(pad.Value) : text;
		w.Write(Color("\x1b[90m")); // Dark Gray for brackets
		w.Write('[');
		w.Write(Color(color));
		w.Write(padded);
		w.Write(Color("\x1b[90m"));
		w.Write(']');
	}

	private static string LevelColor(string level) =>
		level switch
		{
			"WARN" => "\x1b[33m",   // Yellow
			"FATAL" => "\x1b[31m",  // Red
			"ERROR" => "\x1b[31m",  // Red
			"DEBUG" => "\x1b[90m",  // Dark Gray
			"TRACE" => "\x1b[90m",  // Dark Gray
			_ => "\x1b[36m",        // Cyan
		};

	private static string Color(string ansi) => UseColor ? ansi : "";
	private static string Reset => UseColor ? "\x1b[0m" : "";
}
