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
			var v = ElasticsearchVersion.From("6.2.0");
			var esHome = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\ElasticManaged\6.2.0\elasticsearch-6.2.0");

			var clusterConfiguration = new ClusterConfiguration("6.2.0", esHome, 2);
			var nodeConfiguration = new NodeConfiguration(clusterConfiguration, 9200);

//			using (var node = new ElasticsearchNode(v, esHome))
//			{
//				node.Start();
//			}
//			using (var node = new ElasticsearchNode(v, esHome))
//			{
//				node.Subscribe(new HighlightWriter());
//
//				node.WaitForStarted(TimeSpan.FromSeconds(30));
//			}
//			using (var node = new ElasticsearchNode(nodeConfiguration))
//			{
//				node.Subscribe(new HighlightWriter());
//
//				node.WaitForStarted(TimeSpan.FromSeconds(30));
//			}

			using (var cluster = new ElasticsearchCluster(clusterConfiguration))
			{
				cluster.Start();
			}

			Console.WriteLine("Program ended");
		}
	}
}
