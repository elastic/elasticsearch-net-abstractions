using Elastic.Managed.Clusters;
using Xunit;

namespace Elastic.Xunit.Xunit
{
	public interface IClusterFixture<TFixture> : IClassFixture<EndpointUsage>
		where TFixture : ClusterBase, new()
	{ }
}
