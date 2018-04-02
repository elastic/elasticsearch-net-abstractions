using Elastic.Managed;
using Elastic.Managed.Ephemeral;
using Nest;

namespace Elastic.Xunit.Sdk
{
	public interface IClusterFixture<out TCluster>
		where TCluster : ICluster<EphemeralClusterConfiguration>, new()
	{
		TCluster Cluster { get; }
		IElasticClient Client { get; }
	}
}
