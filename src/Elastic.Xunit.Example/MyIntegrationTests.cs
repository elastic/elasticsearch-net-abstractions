using Elastic.Xunit.Example;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: Elastic.Xunit.ElasticXunitConfiguration(typeof(MyRunOptions))]

namespace Elastic.Xunit.Example
{
	public class MyRunOptions : ElasticXunitRunOptions
	{
		public MyRunOptions()
		{
			this.ClusterFilter = "test";
		}
	}

	public class TestCluster : XunitClusterBase
	{
		public static string Filter => "x";
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
