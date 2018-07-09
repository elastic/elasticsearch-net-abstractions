using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Managed.ConsoleWriters;
using ProcNet;
using ProcNet.Std;

namespace Elastic.Managed.Ephemeral.Tasks
{
	public interface IClusterComposeTask
	{
		void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster);
	}
	public interface IClusterTeardownTask
	{
		/// <summary>
		/// Called when the cluster disposes, used to clean up after itself.
		/// </summary>
		/// <param name="cluster">The cluster configuration of the node that was started</param>
		/// <param name="nodeStarted">Whether the cluster composer was successful in starting the node</param>
		void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster, bool nodeStarted);
	}

	public abstract class ClusterComposeTask: IClusterComposeTask
	{
		public abstract void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster);

		protected static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
		protected static string BinarySuffix => IsMono || Path.DirectorySeparatorChar == '/' ? "" : ".bat";

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
		protected string GetResponseString(HttpResponseMessage m) => m.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

		protected HttpResponseMessage Get(IEphemeralCluster<EphemeralClusterConfiguration> cluster, string path, string query) =>
			Call(cluster, path, query, (c, u, t) => c.GetAsync(u, t));

		protected HttpResponseMessage Post(IEphemeralCluster<EphemeralClusterConfiguration> cluster, string path, string query, string json) =>
			Call(cluster, path, query, (c, u, t) => c.PostAsync(u, new StringContent(json, Encoding.UTF8, "application/json" ), t));

		private HttpResponseMessage Call(
			IEphemeralCluster<EphemeralClusterConfiguration> cluster,
			string path,
			string query,
			Func<HttpClient, Uri, CancellationToken, Task<HttpResponseMessage>> verb)
		{
			var q = string.IsNullOrEmpty(query) ? "pretty=true" : (query + "&pretty=true");
			var statusUrl = new UriBuilder(cluster.NodesUris().First()) { Path = path, Query = q }.Uri;

			var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(20));
			var handler = new HttpClientHandler
			{
				AutomaticDecompression =  DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
			};
			if (cluster.ClusterConfiguration.EnableSsl)
			{
				handler.ServerCertificateCustomValidationCallback += (m, c,cn, p) => true;
			}
			using (var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(20)})
			{
				if (cluster.ClusterConfiguration.EnableSecurity)
				{
                    var byteArray = Encoding.ASCII.GetBytes($"{ClusterAuthentication.Admin.Username}:{ClusterAuthentication.Admin.Password}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
				}

				try
				{
					var response = verb(client, statusUrl, tokenSource.Token).ConfigureAwait(false).GetAwaiter().GetResult();
					if (response.StatusCode == HttpStatusCode.OK) return response;
					cluster.Writer.WriteDiagnostic($"{{{nameof(Call)}}} [{statusUrl.PathAndQuery}] Bad status code: [{(int)response.StatusCode}]");
					var body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
					foreach(var l in (body ?? string.Empty).Split('\n','\r'))
						cluster.Writer.WriteDiagnostic($"{{{nameof(Call)}}} [{statusUrl.PathAndQuery}] returned [{l}]");
				}
				catch
				{
					// ignored
				}
			}
			return null;
		}

		protected static void WriteFileIfNotExist(string fileLocation, string contents)
		{
			if (!File.Exists(fileLocation)) File.WriteAllText(fileLocation, contents);
		}

		protected static void ExecuteBinary(EphemeralClusterConfiguration config, IConsoleLineWriter writer, string binary, string description, params string[] arguments)
		{
			var command = $"{{{binary}}} {{{string.Join(" ", arguments)}}}";
			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} starting process [{description}] {command}");

			var timeout = TimeSpan.FromSeconds(420);
			var processStartArguments = new StartArguments(binary, arguments)
			{
				Environment = new Dictionary<string, string>
				{
					{ config.FileSystem.ConfigEnvironmentVariableName, config.FileSystem.ConfigPath },
					{"ES_HOME", config.FileSystem.ElasticsearchHome}
				}
			};
			var result = Proc.Start(processStartArguments, timeout, new ConsoleOutColorWriter());

			if (!result.Completed)
				throw new Exception($"Timeout while executing {description} exceeded {timeout}");

			if (result.ExitCode != 0)
				throw new Exception($"Expected exit code 0 but recieved ({result.ExitCode}) while executing {description}: {command}");

			var errorOut = result.ConsoleOut.Where(c => c.Error).ToList();
			if (errorOut.Any())
				throw new Exception($"Recieved error out with exitCode ({result.ExitCode}) while executing {description}: {command}");

			writer?.WriteDiagnostic($"{{{nameof(ExecuteBinary)}}} finished process [{description}] {{{result.ExitCode}}}");
		}

		protected static void CopyFolder(string source, string target, bool overwrite = true)
		{
			foreach (var sourceDir in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
			{
				var targetDir = sourceDir.Replace(source, target);
				Directory.CreateDirectory(targetDir);
			}

			foreach (var sourcePath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
			{
				var targetPath = sourcePath.Replace(source, target);
				if (!overwrite && File.Exists(targetPath)) continue;
				File.Copy(sourcePath, targetPath, overwrite);
			}
		}
	}
}
