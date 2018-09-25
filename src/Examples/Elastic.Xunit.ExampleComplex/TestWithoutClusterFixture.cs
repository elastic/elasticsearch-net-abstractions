using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;

namespace Elastic.Xunit.ExampleComplex
{
	[IntegrationTestCluster(typeof(TestCluster))]
	[SkipVersion("<6.3.0", "")]
	public class TestWithoutClusterFixture
	{
		[I] public void Test()
		{
			(1 + 1).Should().Be(2);
			var info = ElasticXunitRunner.CurrentCluster.GetOrAddClient().RootNodeInfo();
			info.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
