using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Stack.Artifacts;
using Elastic.Stack.Artifacts.Products;
using Elastic.Stack.Artifacts.Resolvers;
using Elasticsearch.Net;
using Nest;
using static Elastic.Managed.Ephemeral.ClusterFeatures;
using HttpMethod = Elasticsearch.Net.HttpMethod;

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
			ElasticVersion version = "latest";

			var plugins =
				new ElasticsearchPlugins(ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.IngestAttachment);
			var features = Security | XPack | SSL;
			var config = new EphemeralClusterConfiguration(version, features, plugins, numberOfNodes: 1)
			{
				HttpFiddlerAware = true,
				ShowElasticsearchOutputAfterStarted = true,
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
					settings = settings.BasicAuthentication(ClusterAuthentication.Admin.Username,
						ClusterAuthentication.Admin.Password);
				if (config.EnableSsl)
					settings = settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

				var client = new ElasticClient(settings);

				var clusterConfiguration = new Dictionary<string, object>()
				{
					{"cluster.routing.use_adaptive_replica_selection", true},
					{"cluster.remote.remote-cluster.seeds", "127.0.0.1:9300"}
				};


				var putSettingsResponse = client.ClusterPutSettings(new ClusterPutSettingsRequest
				{
					Transient = clusterConfiguration
				});


				Console.Write(client.XPackInfo().DebugInformation);
				Console.WriteLine("Press any key to exit");
				Console.ReadKey();
				Console.WriteLine("Exitting..");
			}

			Console.WriteLine("Done!");
		}

		private static void ResolveVersions()
		{
			var versions = new[]
			{
				"8.0.0-SNAPSHOT", "7.0.0-beta1", "6.6.1", "latest-7", "latest", "7.0.0", "7.4.0-SNAPSHOT",
				"957e3089:7.2.0", "latest-6"
			};
			//versions = new[] {"latest-7"};
			var products = new Product[]
			{
				Product.Elasticsearch,
				Product.Kibana,
				Product.ElasticsearchPlugin(ElasticsearchPlugin.AnalysisIcu)
			};
			var x = SnapshotApiResolver.AvailableVersions.Value;

			foreach (var v in versions)
			{
				foreach (var p in products)
				{
					var r = ElasticVersion.From(v);
					var a = r.Artifact(p);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(v);
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write($"\t{p.Moniker}");
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write($"\t\t{r.ArtifactBuildState.GetStringValue()}");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine($"\t{a?.BuildHash}");
					Console.ForegroundColor = ConsoleColor.Blue;
//                    Console.WriteLine($"\t{a.Archive}");
//                    Console.WriteLine($"\t{r.ArtifactBuildState}");
//                    Console.WriteLine($"\t{a.FolderInZip}");
//                    Console.WriteLine($"\tfolder: {a.LocalFolderName}");
					if (a == null)
					{
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\tArtifact not resolved");
                        continue;
					}
					Console.WriteLine($"\t{a.DownloadUrl}");
					var found = false;
					try
					{
						found = HeadReturns200OnDownloadUrl(a.DownloadUrl);
					}
					catch
					{
						// ignored, best effort but does not take into account proxies or other bits that might prevent the check
					}

					if (found) continue;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\tArtifact not found");
				}
			}
		}

		private static HttpClient HttpClient { get; } = new HttpClient() { };

		public static bool HeadReturns200OnDownloadUrl(string url)
		{
			var message = new HttpRequestMessage
			{
				Method = System.Net.Http.HttpMethod.Head,
				RequestUri = new Uri(url)
			};

			using (var response = HttpClient.SendAsync(message).GetAwaiter().GetResult())
				return response.StatusCode == HttpStatusCode.OK;
		}
	}
}
