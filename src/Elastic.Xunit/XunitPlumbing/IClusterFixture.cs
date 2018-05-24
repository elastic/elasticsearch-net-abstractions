using Elastic.Managed;
using Elastic.Managed.Ephemeral;

namespace Elastic.Xunit.XunitPlumbing
{
	// ReSharper disable once UnusedTypeParameter
	// used by the runner to new() the proper cluster
	public interface IClusterFixture<out TCluster>
		where TCluster : ICluster<EphemeralClusterConfiguration>, new()
	{
	}
}
