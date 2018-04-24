using System.Collections.Concurrent;
using Elastic.Managed.Ephemeral;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Xunit.Example
{
	internal static class EphemeralClusterExtensions
	{
		private static readonly ConcurrentDictionary<IEphemeralCluster, IElasticClient> Clients = new ConcurrentDictionary<IEphemeralCluster, IElasticClient>();

		public static IElasticClient GetOrAddClient(this IEphemeralCluster cluster)
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

	public interface IMyCluster
	{
		IElasticClient Client { get; }
	}

	public abstract class MyClusterBase : XunitClusterBase, IMyCluster
	{
		protected MyClusterBase() : base(new XunitClusterConfiguration("6.0.0")) { }

		public IElasticClient Client => this.GetOrAddClient();
	}

	public class TestCluster : MyClusterBase
	{
		protected override void SeedCluster()
		{
			var pluginsResponse = this.Client.CatPlugins();
		}
	}

	public class TestGenericCluster : XunitClusterBase<XunitClusterConfiguration>, IMyCluster
	{
		public TestGenericCluster() : base(new XunitClusterConfiguration("6.0.0")) { }

		public IElasticClient Client => this.GetOrAddClient();

		protected override void SeedCluster()
		{
			var aliasesResponse = this.Client.CatAliases();
		}
	}
}
