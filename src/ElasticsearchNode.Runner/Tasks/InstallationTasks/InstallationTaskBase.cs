using System;
using System.IO;
using System.Net.Http;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;
using ProcNet;
using ProcNet.Std;

namespace Elastic.Managed.Ephimeral.Tasks.InstallationTasks
{
	public abstract class InstallationTaskBase
	{
		public abstract void Run(EphimeralClusterBase cluster, INodeFileSystem fs);

		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
		protected string BinarySuffix => IsMono || Path.PathSeparator == '/' ? "" : ".bat";

		protected static void DownloadFile(string from, string to)
		{
			if (File.Exists(to)) return;
			var http = new HttpClient();
			using (var stream = http.GetStreamAsync(new Uri(from)).GetAwaiter().GetResult())
			using (var fileStream = File.Create(to))
			{
				stream.CopyTo(fileStream);
				fileStream.Flush();
			}
		}

		protected static void WriteFileIfNotExist(string fileLocation, string contents)
		{
			if (!File.Exists(fileLocation)) File.WriteAllText(fileLocation, contents);
		}

		protected static void ExecuteBinary(string binary, string description, params string[] arguments)
		{
			Console.WriteLine($"Preparing to execute: {description} ...");

			var timeout = TimeSpan.FromSeconds(420);
			var result = Proc.Start(binary, timeout, new ConsoleOutColorWriter(), arguments);

			if (!result.Completed)
				throw new Exception($"Timeout while executing {description} exceeded {timeout}");

			Console.WriteLine($"Finished executing {description} exit code: {result.ExitCode}");
		}

	}
}
