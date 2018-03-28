using System;
using Elastic.Managed;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elasticsearch.Net;
using ProcNet.Std;

namespace ScratchPad
{
	public static class Program
	{
		public static int Main()
		{
//			var clusterConfiguration = new EphemeralClusterConfiguration("6.0.0", 2);
//			var ephemeralCluster = new EphemeralCluster(clusterConfiguration);
//			var nodeConfiguration = new NodeConfiguration(clusterConfiguration, 9200);
//			var elasticsearchNode = new ElasticsearchNode(nodeConfiguration);
//

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

			using (var cluster = new EphemeralCluster("6.0.0", numberOfNodes: 1))
			{
				cluster.Start();
			}

			return 0;
		}

	}

}
