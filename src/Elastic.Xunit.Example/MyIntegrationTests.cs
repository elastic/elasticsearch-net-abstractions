using Elastic.Managed.Configuration;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
namespace Elastic.Xunit.Example
{
	public class TestCluster : XunitClusterBase
	{
		public TestCluster() : base(new XunitClusterConfiguration("6.0.0")) { }

		protected override void SeedCluster()
		{
			var infoResult = this.Client.RootNodeInfo();
		}
	}

	public class MyTestClass : ClusterTestClassBase<TestCluster>
	{
		public MyTestClass(TestCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = this.Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();
		}
	}
}
