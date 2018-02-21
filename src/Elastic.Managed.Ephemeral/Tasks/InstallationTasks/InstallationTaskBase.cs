using System;
using System.IO;
using System.Net.Http;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;
using ProcNet;
using ProcNet.Std;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public abstract class InstallationTaskBase
	{
		public abstract void Run(EphemeralCluster cluster, INodeFileSystem fs);

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

		protected static void ExecuteBinary(IConsoleLineWriter writer, string binary, string description, params string[] arguments)
		{
			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} starting process [{description}] {{{binary}}} {{{string.Join(" ", arguments)}}}");

			var timeout = TimeSpan.FromSeconds(420);
			var result = Proc.Start(binary, timeout, new ConsoleOutColorWriter(), arguments);

			if (!result.Completed)
				throw new Exception($"Timeout while executing {description} exceeded {timeout}");

			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} finished process [{description}] {{{result.ExitCode}}}");
		}

	}
}
