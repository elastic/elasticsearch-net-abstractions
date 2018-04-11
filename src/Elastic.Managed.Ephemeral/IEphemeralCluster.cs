using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Elastic.Managed.Ephemeral.Plugins;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Managed.Ephemeral
{
	public interface IEphemeralCluster
	{
		ICollection<Uri> NodesUris(string hostName = "localhost");
		ElasticsearchPlugins Plugins { get; }
	}

	public interface IEphemeralCluster<out TConfiguration> : IEphemeralCluster, ICluster<TConfiguration>
		where TConfiguration : EphemeralClusterConfiguration { }

	internal static class EphemeralClusterExtensions
	{
		private static readonly ConcurrentDictionary<IEphemeralCluster, IElasticClient> Clients = new ConcurrentDictionary<IEphemeralCluster, IElasticClient>();

		public static IElasticClient Client(this IEphemeralCluster cluster)
		{
			return Clients.GetOrAdd(cluster, (c) =>
			{
				var connectionPool = new StaticConnectionPool(c.NodesUris());
				var settings = new ConnectionSettings(connectionPool);
				var client = new ElasticClient(settings);
				return client;
			});

		}
	}

}
