using System;
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

//			using (var cluster = new EphemeralCluster("6.0.0"))
//			{
//				cluster.Start();
//			}

			var clusterStarted = false;
			var plugins = new ElasticsearchPlugins(ElasticsearchPlugin.RepositoryAzure, ElasticsearchPlugin.IngestAttachment);
			var config = new EphemeralClusterConfiguration("6.0.0", ClusterFeatures.XPack, plugins);
			using (var cluster = new EphemeralCluster(config))
			{
				cluster.Start();
				var client = new ElasticClient(new ConnectionSettings(new StaticConnectionPool(cluster.NodesUris())).EnableDebugMode());
				Console.Write(client.CatPlugins().DebugInformation);
				clusterStarted = true;
			}

			Console.WriteLine($"Clusterstarted:{clusterStarted}");
			return 0;
		}

	}

}
