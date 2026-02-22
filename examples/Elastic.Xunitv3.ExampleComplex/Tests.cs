// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Xunitv3.Elasticsearch.Core;
using FluentAssertions;
using Xunit;

namespace Elastic.Xunitv3.ExampleComplex;

public class MyTestClass(TestCluster cluster)
	: ClusterTestClassBase<TestCluster>(cluster)
{
	[Fact]
	public void SomeTest()
	{
		var info = Cluster.Client.Info();

		info.IsValidResponse.Should().BeTrue();
	}
}

public class Tests1(TestCluster cluster)
	: ClusterTestClassBase<TestCluster>(cluster)
{
	[Fact] public void Unit1Test() => (1 + 1).Should().Be(2);
	[Fact] public void Unit1Test1() => (1 + 1).Should().Be(2);
	[Fact] public void Unit1Test2() => (1 + 1).Should().Be(2);
	[Fact] public void Unit1Test3() => (1 + 1).Should().Be(2);
	[Fact] public void Unit1Test4() => (1 + 1).Should().Be(2);
	[Fact] public void Unit1Test5() => (1 + 1).Should().Be(2);
	[Fact] public void Unit1Test6() => (1 + 1).Should().Be(2);
}

public class Tests3
{
	[Fact] public void Unit3Test() => (1 + 1).Should().Be(2);
	[Fact] public void Unit3Test1() => (1 + 1).Should().Be(2);
	[Fact] public void Unit3Test2() => (1 + 1).Should().Be(2);
	[Fact] public void Unit3Test3() => (1 + 1).Should().Be(2);
	[Fact] public void Unit3Test4() => (1 + 1).Should().Be(2);
	[Fact] public void Unit3Test5() => (1 + 1).Should().Be(2);
	[Fact] public void Unit3Test6() => (1 + 1).Should().Be(2);
}

public class Tests2(TestCluster cluster)
	: ClusterTestClassBase<TestCluster>(cluster)
{
	[Fact] public void Unit2Test() => (1 + 1).Should().Be(2);
	[Fact] public void Unit2Test1() => (1 + 1).Should().Be(2);
	[Fact] public void Unit2Test2() => (1 + 1).Should().Be(2);
	[Fact] public void Unit2Test3() => (1 + 1).Should().Be(2);
	[Fact] public void Unit2Test4() => (1 + 1).Should().Be(2);
	[Fact] public void Unit2Test5() => (1 + 1).Should().Be(2);
	[Fact] public void Unit2Test6() => (1 + 1).Should().Be(2);
}

public class MyGenericTestClass(TestGenericCluster cluster)
	: ClusterTestClassBase<TestGenericCluster>(cluster)
{
	[Fact]
	public void SomeTest()
	{
		var info = Cluster.Client.Info();

		info.IsValidResponse.Should().BeTrue();
	}

	[Fact] public void MyGenericUnitTest() => (1 + 1).Should().Be(2);
	[Fact] public void MyGenericUnitTest1() => (1 + 1).Should().Be(2);
	[Fact] public void MyGenericUnitTest2() => (1 + 1).Should().Be(2);
	[Fact] public void MyGenericUnitTest3() => (1 + 1).Should().Be(2);
	[Fact] public void MyGenericUnitTest4() => (1 + 1).Should().Be(2);
	[Fact] public void MyGenericUnitTest5() => (1 + 1).Should().Be(2);
	[Fact] public void MyGenericUnitTest6() => (1 + 1).Should().Be(2);
}

[SkipVersion("<6.2.0", "")]
public class SkipTestClass(TestGenericCluster cluster)
	: ClusterTestClassBase<TestGenericCluster>(cluster)
{
	[Fact]
	public async Task SomeTest()
	{
		var info = await Cluster.Client.InfoAsync();

		info.IsValidResponse.Should().BeTrue();
	}

	[Fact]
	public void UnitTest() => (1 + 1).Should().Be(2);
}
