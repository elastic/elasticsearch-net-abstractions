using System;
using System.Security.Cryptography.X509Certificates;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elasticsearch.Net;
using Nest;
using static Elastic.Managed.Ephemeral.ClusterFeatures;

namespace ScratchPad
{
	public static class Program
	{
		public static int Main()
		{
			ElasticsearchVersion v = null;


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

			var config = new EphemeralClusterConfiguration("6.2.3", XPack | Security | SSL, null, 1)
			{
				ShowElasticsearchOutputAfterStarted = true,
				CacheEsHomeInstallation = true
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
			}
//
//			Console.WriteLine($".. DONE ...");
			return 0;
		}

	}

}
