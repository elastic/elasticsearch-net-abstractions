using System;
using System.Security.Cryptography.X509Certificates;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elasticsearch.Net;
using Nest;
using ScratchPad;
using static Elastic.Managed.Ephemeral.ClusterFeatures;

namespace ScratchPad
{
	public static class Program
	{
		public static int Main()
		{
			//ResolveVersions();
			//ManualConfigRun();
			ValidateCombinations.Run();
			return 0;
		}

		private static void ManualConfigRun()
		{
			ElasticsearchVersion version = "7.0.0-beta1";

			var plugins =
				new ElasticsearchPlugins(ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.AnalysisKuromoji);
			var config = new EphemeralClusterConfiguration(version, XPack | Security, plugins, numberOfNodes: 1)
			{
				HttpFiddlerAware = true,
				ShowElasticsearchOutputAfterStarted = false,
				PrintYamlFilesInConfigFolder = true,
				CacheEsHomeInstallation = false,
				TrialMode = XPackTrialMode.Trial,
				NoCleanupAfterNodeStopped = false,
			};

			using (var cluster = new EphemeralCluster(config))
			{
				cluster.Start();

				var nodes = cluster.NodesUris();
				var connectionPool = new StaticConnectionPool(nodes);
				var settings = new ConnectionSettings(connectionPool).EnableDebugMode();
				if (config.EnableSecurity)
					settings = settings.BasicAuthentication(ClusterAuthentication.Admin.Username, ClusterAuthentication.Admin.Password);
				if (config.EnableSsl)
					settings = settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

				var client = new ElasticClient(settings);

				Console.Write(client.XPackInfo().DebugInformation);
				Console.WriteLine("Press any key to exit");
				Console.ReadKey();
				Console.WriteLine("Exitting..");
			}

			Console.WriteLine("Done!");
		}

		private static void ResolveVersions()
		{
			var versions = new[] {"7.0.0-beta1", "6.6.1", "7.0.0-alpha1-SNAPSHOT", "latest", "7.0.0-beta1"};
			foreach (var v in versions)
			{
				var r = ElasticsearchVersion.From(v);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(v);
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine($"\t{r}");
				Console.WriteLine($"\t{r.Zip}");
				Console.WriteLine($"\t{r.ReleaseState}");
				Console.WriteLine($"\t{r.FolderInZip}");
				Console.WriteLine($"\tfolder: {r.ExtractFolderName}");
				Console.WriteLine($"\t{r.FullyQualifiedVersion}");
				Console.WriteLine($"\t{r.DownloadLocations.ElasticsearchDownloadUrl}");
				Console.WriteLine($"\t{r.DownloadLocations.PluginDownloadUrl("analysis-icu")}");
			}
		}
	}
}
