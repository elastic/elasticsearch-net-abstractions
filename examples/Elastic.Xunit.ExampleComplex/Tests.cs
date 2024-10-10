// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;

namespace Elastic.Xunit.ExampleComplex
{
	public class MyTestClass : ClusterTestClassBase<TestCluster>
	{
		public MyTestClass(TestCluster cluster) : base(cluster) { }

		[I]
		public void SomeTest()
		{
			var info = Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();

			Client.CreateIndex("INASda");
			Client.LowLevel.Search<StringResponse>(PostData.Serializable(new {query = new {query_string = 1}}));
		}
	}

	public class Tests1 : ClusterTestClassBase<TestCluster>
	{
		public Tests1(TestCluster cluster) : base(cluster) { }

		[U] public void Unit1Test() => (1 + 1).Should().Be(2);
		[U] public void Unit1Test1() => (1 + 1).Should().Be(2);
		[U] public void Unit1Test2() => (1 + 1).Should().Be(2);
		[U] public void Unit1Test3() => (1 + 1).Should().Be(2);
		[U] public void Unit1Test4() => (1 + 1).Should().Be(2);
		[U] public void Unit1Test5() => (1 + 1).Should().Be(2);
		[U] public void Unit1Test6() => (1 + 1).Should().Be(2);
	}

	public class Tests3
	{
		[U] public void Unit3Test() => (1 + 1).Should().Be(2);
		[U] public void Unit3Test1() => (1 + 1).Should().Be(2);
		[U] public void Unit3Test2() => (1 + 1).Should().Be(2);
		[U] public void Unit3Test3() => (1 + 1).Should().Be(2);
		[U] public void Unit3Test4() => (1 + 1).Should().Be(2);
		[U] public void Unit3Test5() => (1 + 1).Should().Be(2);
		[U] public void Unit3Test6() => (1 + 1).Should().Be(2);
	}

	public class Tests2 : ClusterTestClassBase<TestCluster>
	{
		public Tests2(TestCluster cluster) : base(cluster) { }

		[U] public void Unit2Test() => (1 + 1).Should().Be(2);
		[U] public void Unit2Test1() => (1 + 1).Should().Be(2);
		[U] public void Unit2Test2() => (1 + 1).Should().Be(2);
		[U] public void Unit2Test3() => (1 + 1).Should().Be(2);
		[U] public void Unit2Test4() => (1 + 1).Should().Be(2);
		[U] public void Unit2Test5() => (1 + 1).Should().Be(2);
		[U] public void Unit2Test6() => (1 + 1).Should().Be(2);
	}

	public class MyGenericTestClass : ClusterTestClassBase<TestGenericCluster>
	{
		public MyGenericTestClass(TestGenericCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();
		}
		[U] public void MyGenericUnitTest() => (1 + 1).Should().Be(2);
		[U] public void MyGenericUnitTest1() => (1 + 1).Should().Be(2);
		[U] public void MyGenericUnitTest2() => (1 + 1).Should().Be(2);
		[U] public void MyGenericUnitTest3() => (1 + 1).Should().Be(2);
		[U] public void MyGenericUnitTest4() => (1 + 1).Should().Be(2);
		[U] public void MyGenericUnitTest5() => (1 + 1).Should().Be(2);
		[U] public void MyGenericUnitTest6() => (1 + 1).Should().Be(2);
	}

	[SkipVersion("<6.2.0", "")]
	public class SkipTestClass : ClusterTestClassBase<TestGenericCluster>
	{
		public SkipTestClass(TestGenericCluster cluster) : base(cluster) { }

		[I]
		public void SomeTest()
		{
			var info = Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();
		}

		[U]
		public void UnitTest() => (1 + 1).Should().Be(2);
	}

	public class DirectInterfaceTests : IClusterFixture<TestGenericCluster>
	{
		public DirectInterfaceTests(TestGenericCluster cluster) { }

		[U]
		public void DirectUnitTest() => (1 + 1).Should().Be(2);
	}
}
