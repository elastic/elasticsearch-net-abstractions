using System;
using System.Threading;

namespace Elastic.ProcessManagement.Tests.Binary
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.Error.WriteLine("no testcase specified");
				return 1;
			}

			var testCase = args[0].ToLowerInvariant();

			if (testCase == nameof(SingleLineNoEnter).ToLowerInvariant())
				return SingleLineNoEnter();

			if (testCase == nameof(SingleLine).ToLowerInvariant())
				return SingleLine();

			if (testCase == nameof(DelayedWriter).ToLowerInvariant())
				return DelayedWriter();

			return 1;
		}
		private static int DelayedWriter()
		{
			Thread.Sleep(1000);
			Console.Write(nameof(DelayedWriter));
			return 20;
		}
		private static int SingleLineNoEnter()
		{
			Console.Write(nameof(SingleLineNoEnter));
			return 0;
		}
		private static int SingleLine()
		{
			Console.WriteLine(nameof(SingleLine));
			return 0;
		}
	}
}
