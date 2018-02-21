using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	public interface IConsoleLineWriter
	{
		void Write(LineOut lineOut);
		void Write(Exception e);
	}

	public static class LineWriterExtensions
	{
		public static void WriteDiagnostic(this IConsoleLineWriter writer, string message) => writer.Write(Info(message));

		public static void WriteDiagnostic(this IConsoleLineWriter writer, string message, string node) => writer?.Write(Info(node != null ? $"[{node}] {message}" : message));

		public static void WriteError(this IConsoleLineWriter writer, string message) => writer.Write(Error(message));

		public static void WriteError(this IConsoleLineWriter writer, string message, string node) => writer?.Write(Error(node != null ? $"[{node}] {message}" : message));

		private static string Format(bool error, string message) => $"[{DateTime.UtcNow:yyyy-MM-ddThh:mm:ss,fff}][{(error ? "ERROR" : "INFO ")}][Managed Elasticsearch\t] {message}";
		private static LineOut Info(string message) => ConsoleOut.Out(Format(false, message));
		private static LineOut Error(string message) => ConsoleOut.ErrorOut(Format(true, message));

	}

}
