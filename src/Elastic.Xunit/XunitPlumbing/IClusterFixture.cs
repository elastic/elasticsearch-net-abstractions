using Elastic.Managed;
using Xunit;

namespace Elastic.Xunit.Sdk
{
	public interface IClusterFixture<TFixture> : IClassFixture<EndpointUsage>
		where TFixture : ClusterBase, new()
	{ }
}
