using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Elastic.Managed.ConsoleWriters;
using ProcNet;
using ProcNet.Std;

namespace Elastic.Managed.Ephemeral.Tasks
{
	public interface IClusterComposeTask { }

	public interface IClusterComposeTask<in TConfiguration> : IClusterComposeTask
		where TConfiguration : EphemeralClusterConfiguration
	{
		void Run(IEphemeralCluster<TConfiguration> cluster);
	}

	public abstract class ClusterComposeTaskBase<TConfiguration> : IClusterComposeTask<TConfiguration>
		where TConfiguration : EphemeralClusterConfiguration
	{
		public abstract void Run(IEphemeralCluster<TConfiguration> cluster);

		private bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
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
			var command = $"{{{binary}}} {{{string.Join(" ", arguments)}}}";
			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} starting process [{description}] {command}");

			var timeout = TimeSpan.FromSeconds(420);
			var result = Proc.Start(binary, timeout, new ConsoleOutColorWriter(), arguments);

			if (!result.Completed)
				throw new Exception($"Timeout while executing {description} exceeded {timeout}");

			if (result.ExitCode != 0)
				throw new Exception($"Expected exit code 0 but recieved ({result.ExitCode}) while executing {description}: {command}");

			var errorOut = result.ConsoleOut.Where(c => c.Error).ToList();
			if (errorOut.Any())
				throw new Exception($"Recieved error out with exitCode ({result.ExitCode}) while executing {description}: {command}");

			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} finished process [{description}] {{{result.ExitCode}}}");
		}
	}

	public abstract class ClusterComposeTask : ClusterComposeTaskBase<EphemeralClusterConfiguration> { }

	public abstract class ClusterComposeTask<TConfiguration> : ClusterComposeTaskBase<TConfiguration>
		where TConfiguration : EphemeralClusterConfiguration { }
}
