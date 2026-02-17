// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ProcNet.Std;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     A console line handler that emits periodic progress heartbeats during cluster
///     bootstrap. Every <paramref name="interval" /> seconds, writes a single summary
///     line showing the cluster name, elapsed time, and the last received log line.
///     <para>
///         Designed for interactive terminal sessions where full verbose output would
///         be noisy, but silence during a 60+ second bootstrap is unhelpful.
///     </para>
///     <para>
///         Errors and exceptions are always written immediately to stderr.
///     </para>
/// </summary>
internal sealed class BootstrapProgressWriter : IConsoleLineHandler
{
	private static readonly bool UseColor;

	private readonly string _clusterName;
	private readonly string _location;
	private readonly TextWriter _out;
	private readonly TextWriter _err;
	private readonly Timer _timer;
	private readonly Stopwatch _stopwatch;
	private volatile string _lastLine;

	static BootstrapProgressWriter()
	{
		if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NO_COLOR")))
		{
			UseColor = false;
			return;
		}

		UseColor = Environment.UserInteractive;
	}

	public BootstrapProgressWriter(string clusterName, string location, TimeSpan interval)
	{
		_clusterName = clusterName;
		_location = location;
		_out = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
		_err = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };
		_stopwatch = Stopwatch.StartNew();
		// Fire immediately, then repeat at interval
		_timer = new Timer(WriteProgress, null, TimeSpan.Zero, interval);
	}

	public void Handle(LineOut lineOut) => _lastLine = lineOut.Line;

	public void Handle(Exception e)
	{
		_err.Write(Color("\x1b[91m"));
		_err.WriteLine(e);
		_err.Write(Reset);
	}

	internal void Stop()
	{
		_timer.Change(Timeout.Infinite, Timeout.Infinite);
		_timer.Dispose();
		// Two newlines — TUnit's progress line overwrites the first
		_out.WriteLine();
		_out.WriteLine();
	}

	private void WriteProgress(object state)
	{
		var last = _lastLine;
		if (last == null) return;

		var elapsed = _stopwatch.Elapsed;
		var seconds = (int)elapsed.TotalSeconds;

		_out.Write(Color("\x1b[36m")); // Cyan
		_out.Write($"Bootstrapping {_clusterName}");
		_out.Write(Color("\x1b[90m")); // Dark Gray
		_out.Write($" in {_location}");
		_out.Write(Color("\x1b[33m")); // Yellow
		_out.Write($" [{seconds}s] ");
		_out.Write(Color("\x1b[37m")); // White
		_out.WriteLine(Truncate(last, 120));
		_out.WriteLine();
		_out.WriteLine();
	}

	private static string Truncate(string value, int maxLength) =>
		value.Length <= maxLength ? value : string.Concat(value.AsSpan(0, maxLength - 3), "...");

	private static string Color(string ansi) => UseColor ? ansi : "";
	private static string Reset => UseColor ? "\x1b[0m" : "";
}
