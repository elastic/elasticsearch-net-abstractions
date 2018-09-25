using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Elastic.Xunit.ExampleComplex
{
	public class MyTestClass : ClusterTestClassBase<TestCluster>
	{
		public MyTestClass(TestCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = this.Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();

			this.Client.CreateIndex("INASda");


			this.Client.LowLevel.Search<StringResponse>(PostData.Serializable(new
			{
				query = new { query_string = 1 }
			}));

		}
	}
//
//	public class MyGenericTestClass : ClusterTestClassBase<TestGenericCluster>
//	{
//		public MyGenericTestClass(TestGenericCluster cluster) : base(cluster) { }
//
//		[I] public void SomeTest()
//		{
//			var info = this.Client.RootNodeInfo();
//
//			info.IsValid.Should().BeTrue();
//		}
//		[U] public void UnitTest()
//		{
//			(1 + 1).Should().Be(2);
//		}
//	}

	[SkipVersion("<6.2.0", "")]
	public class SkipTestClass : ClusterTestClassBase<TestGenericCluster>
	{
		public SkipTestClass(TestGenericCluster cluster) : base(cluster) { }

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
