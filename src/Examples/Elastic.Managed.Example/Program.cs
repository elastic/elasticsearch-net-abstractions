using System;
using Elastic.Managed.Configuration;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Example
{
	//TODO ship with a simple cluster implementation
	class Program
	{
		static void Main(string[] args)
		{
			var version = "6.2.0";
			var esHome = Environment.ExpandEnvironmentVariables($@"%LOCALAPPDATA%\ElasticManaged\{version}\elasticsearch-{version}");

//			var clusterConfiguration = new ClusterConfiguration(version, esHome);
//			var nodeConfiguration = new NodeConfiguration(clusterConfiguration, 9200)
//			{
//				ShowElasticsearchOutputAfterStarted = false,
//				Settings =
//				{
//					"node.attr.x", "y"
//				}
//			};

//			using (var node = new ElasticsearchNode(version, esHome))
//			{
//				node.Start();
//			}
//			using (var node = new ElasticsearchNode(version, esHome))
//			{
//				node.SubscribeLines(new LineHighlightWriter());
//				if (!node.WaitForStarted(TimeSpan.FromMinutes(2))) throw new Exception();
//			}

//			using (var node = new ElasticsearchNode(nodeConfiguration))
//			{
//				node.Start();
//			}

			var clusterConfiguration = new ClusterConfiguration(version, esHome, numberOfNodes: 2);
			using (var cluster = new ElasticsearchCluster(clusterConfiguration))
			{
				cluster.Start();
			}

			Console.WriteLine("Program ended");
		}
	}
}
