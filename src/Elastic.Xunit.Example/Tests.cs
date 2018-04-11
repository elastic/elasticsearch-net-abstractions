using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Elastic.Xunit.Example
{
	public class MyTestClass : ClusterTestClassBase<TestCluster>
	{
		public MyTestClass(TestCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = this.Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();
		}
	}

	public class MyGenericTestClass : ClusterTestClassBase<TestGenericCluster>
	{
		public MyGenericTestClass(TestGenericCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = this.Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();
		}
		[U] public void UnitTest()
		{
			(1 + 1).Should().Be(2);
		}
	}

}
