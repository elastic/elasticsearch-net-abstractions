using Elastic.Managed;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.Sdk;
using Nest;

namespace Elastic.Xunit
{
	public abstract class ClusterTestClassBase<TCluster> : IClusterFixture<TCluster>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, new()
	{
		public TCluster Cluster { get; }
		public IElasticClient Client { get; }

		protected ClusterTestClassBase(TCluster cluster)
		{
			this.Cluster = cluster;
			this.Client = cluster.Client;
		}
	}
}
