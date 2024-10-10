// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Concurrent;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Xunit.ExampleComplex
{
	internal static class EphemeralClusterExtensions
	{
		private static readonly ConcurrentDictionary<IEphemeralCluster, IElasticClient> Clients = new();

		public static IElasticClient GetOrAddClient(this IEphemeralCluster cluster) =>
			Clients.GetOrAdd(cluster, c =>
			{
				var connectionPool = new StaticConnectionPool(c.NodesUris());
				var settings = new ConnectionSettings(connectionPool);
				var client = new ElasticClient(settings);
				return client;
			});
	}

	public interface IMyCluster
	{
		IElasticClient Client { get; }
	}

	public abstract class MyClusterBase() : XunitClusterBase(new XunitClusterConfiguration(MyRunOptions.TestVersion)
	{
		ShowElasticsearchOutputAfterStarted = false,
	}), IMyCluster
	{
		public IElasticClient Client => this.GetOrAddClient();
	}

	public class TestCluster : MyClusterBase
	{
		protected override void SeedCluster()
		{
			var pluginsResponse = Client.CatPlugins();
		}
	}

	public class TestGenericCluster : XunitClusterBase<XunitClusterConfiguration>, IMyCluster
	{
		public TestGenericCluster() : base(new XunitClusterConfiguration(MyRunOptions.TestVersion))
		{
		}

		public IElasticClient Client => this.GetOrAddClient();

		protected override void SeedCluster()
		{
			var aliasesResponse = Client.CatAliases();
		}
	}
}
