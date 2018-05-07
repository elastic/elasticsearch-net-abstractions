using System;
using System.Threading;
using Elastic.Managed;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elasticsearch.Net;
using Nest;
using ProcNet.Std;

namespace ScratchPad
{
	public static class Program
	{
		public static int Main()
		{
//			var clusterConfiguration = new EphemeralClusterConfiguration("6.0.0", numberOfNodes: 2);
//			var ephemeralCluster = new EphemeralCluster(clusterConfiguration);
//			var nodeConfiguration = new NodeConfiguration(clusterConfiguration, 9200);
//			var elasticsearchNode = new ElasticsearchNode(nodeConfiguration);
////

//			using (var node = new ElasticsearchNode("5.5.1"))
//			{
//				node.Subscribe(new ConsoleOutColorWriter());
//				node.WaitForStarted(TimeSpan.FromMinutes(2));
//			}
//
//			using (var node = new ElasticsearchNode("6.0.0-beta2", @"c:\Data\elasticsearch-6.0.0-beta2"))
//			{
//				node.Subscribe();
//				node.WaitForStarted(TimeSpan.FromMinutes(2));
//				Console.ReadKey();
//			}

			using (var cluster = new EphemeralCluster("6.0.0"))
			{
				cluster.Start();
			}

			var clusterStarted = false;
			var plugins = new ElasticsearchPlugins(ElasticsearchPlugin.RepositoryAzure, ElasticsearchPlugin.IngestAttachment);
			var config = new EphemeralClusterConfiguration("6.2.3", ClusterFeatures.XPack, plugins, numberOfNodes: 2)
			{
				ShowElasticsearchOutputAfterStarted = false
			};
			using (var cluster = new EphemeralCluster(config))
			{
				cluster.Start();

				var nodes = cluster.NodesUris();
				var connectionPool = new StaticConnectionPool(nodes);
				var settings = new ConnectionSettings(connectionPool).EnableDebugMode();
				var client = new ElasticClient(settings);

				Console.Write(client.RootNodeInfo().DebugInformation);
				Thread.Sleep(1000 * 10);
				Console.Write(client.CatNodes().DebugInformation);

				clusterStarted = true;
			}

			//Console.WriteLine($"Clusterstarted:{clusterStarted}");
			return 0;
		}

	}

}
