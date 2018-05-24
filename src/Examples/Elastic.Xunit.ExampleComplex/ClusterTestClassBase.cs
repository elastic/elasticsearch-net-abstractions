using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Elastic.Xunit.ExampleComplex
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
