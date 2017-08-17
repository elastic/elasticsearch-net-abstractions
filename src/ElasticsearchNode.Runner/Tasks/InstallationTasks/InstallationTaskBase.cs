using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Elastic.ProcessManagement;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public abstract class InstallationTaskBase
	{
		public abstract void Run(NodeConfiguration config, NodeFileSystem fileSystem);

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
			using (var p = new ObservableProcess(binary, arguments))
			{
				Console.WriteLine($"Executing: {binary} {string.Join(" ", arguments)}");
				p.Subscribe(c => Console.Write(c.Characters),
					(e) =>
					{
						Console.WriteLine($"Failed executing: {description} {e.Message} {e.StackTrace}");
						throw e;
					},
					() => Console.WriteLine($"Finished executing {description} exit code: {p.ExitCode}"));
				if (!p.WaitForCompletion(timeout))
					throw new Exception($"Timeout while executing {description} exceeded {timeout}");
			}
		}
	}
}
