using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Elastic.Managed.Configuration;
using Elastic.Managed.Configuration.Artifacts;
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
			ManualConfigRun();
			//ValidateCombinations.Run();
			return 0;
		}

		private static void ManualConfigRun()
		{
			ElasticsearchVersion version = "latest";

			var plugins = new ElasticsearchPlugins(ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.AnalysisKuromoji);
			var features = Security | XPack | SSL;
			var config = new EphemeralClusterConfiguration(version, features, null, numberOfNodes: 1)
			{
				HttpFiddlerAware = true,
				ShowElasticsearchOutputAfterStarted = true,
				CacheEsHomeInstallation = true,
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
			Console.WriteLine(string.Join(",", ArtifactsResolver.AvailableVersions.Value.Select(v=>v.ToString())));
			Console.WriteLine(ArtifactsResolver.LatestReleaseOrSnapshot);
			Console.WriteLine(ArtifactsResolver.LatestSnapshotForMajor(7));
			Console.WriteLine(ArtifactsResolver.LatestReleaseOrSnapshotForMajor(6));
			Console.WriteLine(ArtifactsResolver.LatestSnapshotForMajor(6));
			
			var versions = new[] {"8.0.0-SNAPSHOT",  "7.0.0-beta1", "6.6.1", "latest-7", "latest", "7.0.0-beta1"};
			//var versions = new[] {"latest", "latest-7", "latest-6", "latest-8"};
			foreach (var v in versions)
			{
				var r = ElasticsearchVersion.From(v);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(v);
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine($"\t{r}");
				Console.WriteLine($"\t{r.Archive}");
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
