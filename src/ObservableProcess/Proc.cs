using System;
using System.Collections.Generic;
using System.Text;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	public static class Proc
	{
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);

		public static ProcessResult Start(string bin, params string[] arguments) => Start(bin, DefaultTimeout, arguments);
		public static ProcessResult Start(string bin, TimeSpan timeout, params string[] arguments) => Start(new ObservableProcessArguments(bin, arguments), timeout);
		public static ProcessResult Start(ObservableProcessArguments arguments) => Start(arguments, DefaultTimeout);
		public static ProcessResult Start(ObservableProcessArguments arguments, TimeSpan timeout)
		{
			var process = new ObservableProcess(arguments);
			var consoleOut = new List<LineOut>();
			process.SubscribeLines(consoleOut.Add);
			var completed = process.WaitForCompletion(timeout);
			return new ProcessResult
			{
				Completed = completed,
				ConsoleOut = consoleOut,
				ExitCode = process.ExitCode
			};
		}
	}

	public class ProcessResult
	{
		public bool Completed { get; set; }
		public int? ExitCode { get; set; }
		public ICollection<LineOut> ConsoleOut { get; set; }
	}
}
