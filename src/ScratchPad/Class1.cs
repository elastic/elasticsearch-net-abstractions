using System;
using Elastic.ManagedNode;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elasticsearch.Net;
using ProcNet.Std;

namespace ScratchPad
{
	public static class Program
	{
		public static int Main()
		{

//			using (var node = new ElasticsearchNode(new NodeConfiguration("5.5.1", "cluster-x", "node-y")))
//			{
//				node.Start(new ElasticsearchConsoleOutWriter());
//				node.WaitForStarted(TimeSpan.FromMinutes(2));
//			}

			using (var cluster = new ElasticsearchCluster("5.5.1", instanceCount: 2))
			{
				cluster.Start(new ElasticsearchConsoleOutWriter());

				Console.ReadKey();
			}

			return 0;
		}

	}
}
