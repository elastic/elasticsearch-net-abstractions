// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Plugins;
using Elastic.Stack.ArtifactsApi.Products;
using Elasticsearch.Net;
using Nest;

namespace ScratchPad
{
	public static class ValidateCombinations
	{
		public static void Run()
		{
			var plugins = new ElasticsearchPlugins(ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.AnalysisKuromoji);
			var versions = new string[] {"7.0.0-beta1", "latest", "latest-7", "latest-6", "957e3089:7.2.0", "6.6.1", "5.6.15" };
			var features = new[]
			{
				ClusterFeatures.None,
//				ClusterFeatures.XPack,
//				ClusterFeatures.XPack | ClusterFeatures.Security,
				ClusterFeatures.XPack | ClusterFeatures.SSL | ClusterFeatures.Security
			};

			foreach (var v in versions)
			{
				foreach (var f in features)
				{
					Console.Clear();
					var reset = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine($"{v} {f}");

					Console.ForegroundColor = reset;
					var config = new EphemeralClusterConfiguration(v, f, plugins, numberOfNodes: 1)
					{
						HttpFiddlerAware = true,
					};

					using (var cluster = new EphemeralCluster(config))
					{
						try
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
                              Console.WriteLine(client.RootNodeInfo().Version.Number);
                              cluster.Dispose();
                              cluster.WaitForExit(TimeSpan.FromMinutes(1));
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
							Console.ForegroundColor = ConsoleColor.Cyan;
							Console.WriteLine($"{v} {f}");

							Console.ForegroundColor = reset;

							throw;
						}
					}
				}
			}
			Console.WriteLine("Done!");
		}
	}
}
