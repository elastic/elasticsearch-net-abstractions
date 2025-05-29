// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Concurrent;
using Elastic.Clients.Elasticsearch;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit;
using Elastic.Transport;

namespace Elastic.Xunit.ExampleComplex
{
	internal static class EphemeralClusterExtensions
	{
		private static readonly ConcurrentDictionary<IEphemeralCluster, ElasticsearchClient> Clients = new();

		public static ElasticsearchClient GetOrAddClient(this IEphemeralCluster cluster) =>
			Clients.GetOrAdd(cluster, c =>
			{
				var connectionPool = new StaticNodePool(c.NodesUris());
				var settings = new ElasticsearchClientSettings(connectionPool);
				var client = new ElasticsearchClient(settings);
				return client;
			});
	}

	public interface IMyCluster
	{
		ElasticsearchClient Client { get; }
	}

	public abstract class MyClusterBase() : XunitClusterBase(new XunitClusterConfiguration(MyRunOptions.TestVersion)
	{
		ShowElasticsearchOutputAfterStarted = false,
	}), IMyCluster
	{
		public ElasticsearchClient Client => this.GetOrAddClient();
	}

	public class TestCluster : MyClusterBase
	{
		protected override void SeedCluster()
		{
			var response = Client.Info();
		}
	}

	public class TestGenericCluster : XunitClusterBase<XunitClusterConfiguration>, IMyCluster
	{
		public TestGenericCluster() : base(new XunitClusterConfiguration(MyRunOptions.TestVersion))
		{
		}

		public ElasticsearchClient Client => this.GetOrAddClient();

		protected override void SeedCluster()
		{
			var response = Client.Info();
		}
	}
}
