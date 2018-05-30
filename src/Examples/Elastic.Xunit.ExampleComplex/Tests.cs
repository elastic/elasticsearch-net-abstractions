using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;

namespace Elastic.Xunit.ExampleComplex
{
//	public class MyTestClass : ClusterTestClassBase<TestCluster>
//	{
//		public MyTestClass(TestCluster cluster) : base(cluster) { }
//
//		[I] public void SomeTest()
//		{
//			var info = this.Client.RootNodeInfo();
//
//			info.IsValid.Should().BeTrue();
//		}
//	}
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
