using Elastic.Managed;
using Elastic.Managed.Ephemeral;
using Nest;

namespace Elastic.Xunit.Sdk
{
	// ReSharper disable once UnusedTypeParameter
	// used by the runner to new() the proper cluster
	public interface IClusterFixture<out TCluster>
		where TCluster : ICluster<EphemeralClusterConfiguration>, new()
	{
	}
}
