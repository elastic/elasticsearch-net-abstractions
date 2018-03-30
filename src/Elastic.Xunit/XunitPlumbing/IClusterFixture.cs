using Elastic.Managed;
using Elastic.Managed.Ephemeral;
using Nest;
using Xunit;

namespace Elastic.Xunit.Sdk
{
	public interface IClusterFixture<out TCluster> : IClassFixture<EndpointUsage>
		where TCluster : ICluster<EphemeralClusterConfiguration>, new()
	{
		TCluster Cluster { get; }
		IElasticClient Client { get; }
	}
}
