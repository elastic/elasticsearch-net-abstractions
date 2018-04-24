using Elastic.Managed;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.Sdk;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Xunit.Example
{
	public abstract class ClusterTestClassBase<TCluster> : IClusterFixture<TCluster>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, IMyCluster, new()
	{
		public TCluster Cluster { get; }
		public IElasticClient Client => this.Cluster.Client;

		protected ClusterTestClassBase(TCluster cluster)
		{
			this.Cluster = cluster;
		}
	}
}
