using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	public static class LineWriterExtensions
	{
		public static void WriteDiagnostic(this IConsoleLineHandler writer, string message) => writer.Handle(Info(message));

		public static void WriteDiagnostic(this IConsoleLineHandler writer, string message, string node) => writer?.Handle(Info(node != null ? $"[{node}] {message}" : message));

		public static void WriteError(this IConsoleLineHandler writer, string message) => writer.Handle(Error(message));

		public static void WriteError(this IConsoleLineHandler writer, string message, string node) => writer?.Handle(Error(node != null ? $"[{node}] {message}" : message));

		private static string Format(bool error, string message) => $"[{DateTime.UtcNow:yyyy-MM-ddThh:mm:ss,fff}][{(error ? "ERROR" : "INFO ")}][Managed Elasticsearch\t] {message}";
		private static LineOut Info(string message) => ConsoleOut.Out(Format(false, message));
		private static LineOut Error(string message) => ConsoleOut.ErrorOut(Format(true, message));

	}

}
