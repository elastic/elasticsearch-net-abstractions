// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.TUnit.Elasticsearch.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Elastic.TUnit.ExampleComplex;

[ClassDataSource<TestCluster>(Shared = SharedType.Keyed, Key = nameof(TestCluster))]
[ParallelLimiter<ElasticsearchParallelLimit>]
public class MyTestClass(TestCluster cluster)
	: ClusterTestClassBase<TestCluster>(cluster)
{
	[Test]
	public async Task SomeTest()
	{
		var info = Cluster.Client.Info();

		await Assert.That(info.IsValidResponse).IsTrue();
	}
}

[ClassDataSource<TestCluster>(Shared = SharedType.Keyed, Key = nameof(TestCluster))]
public class Tests1(TestCluster cluster)
	: ClusterTestClassBase<TestCluster>(cluster)
{
	private static int Add(int a, int b) => a + b;

	[Test] public async Task Unit1Test() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit1Test1() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit1Test2() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit1Test3() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit1Test4() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit1Test5() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit1Test6() => await Assert.That(Add(1, 1)).IsEqualTo(2);
}

public class Tests3
{
	private static int Add(int a, int b) => a + b;

	[Test] public async Task Unit3Test() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit3Test1() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit3Test2() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit3Test3() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit3Test4() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit3Test5() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit3Test6() => await Assert.That(Add(1, 1)).IsEqualTo(2);
}

[ClassDataSource<TestCluster>(Shared = SharedType.Keyed, Key = nameof(TestCluster))]
public class Tests2(TestCluster cluster)
	: ClusterTestClassBase<TestCluster>(cluster)
{
	private static int Add(int a, int b) => a + b;

	[Test] public async Task Unit2Test() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit2Test1() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit2Test2() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit2Test3() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit2Test4() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit2Test5() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task Unit2Test6() => await Assert.That(Add(1, 1)).IsEqualTo(2);
}

[ClassDataSource<TestGenericCluster>(Shared = SharedType.Keyed, Key = nameof(TestGenericCluster))]
public class MyGenericTestClass(TestGenericCluster cluster)
	: ClusterTestClassBase<TestGenericCluster>(cluster)
{
	private static int Add(int a, int b) => a + b;

	[Test]
	public async Task SomeTest()
	{
		var info = Cluster.Client.Info();

		await Assert.That(info.IsValidResponse).IsTrue();
	}

	[Test] public async Task MyGenericUnitTest() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task MyGenericUnitTest1() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task MyGenericUnitTest2() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task MyGenericUnitTest3() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task MyGenericUnitTest4() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task MyGenericUnitTest5() => await Assert.That(Add(1, 1)).IsEqualTo(2);
	[Test] public async Task MyGenericUnitTest6() => await Assert.That(Add(1, 1)).IsEqualTo(2);
}

[SkipVersion("<6.2.0", "")]
[ClassDataSource<TestGenericCluster>(Shared = SharedType.Keyed, Key = nameof(TestGenericCluster))]
public class SkipTestClass(TestGenericCluster cluster)
	: ClusterTestClassBase<TestGenericCluster>(cluster)
{
	private static int Add(int a, int b) => a + b;

	[Test]
	public async Task SomeTest()
	{
		var info = await Cluster.Client.InfoAsync();

		await Assert.That(info.IsValidResponse).IsTrue();
	}

	[Test]
	public async Task UnitTest() => await Assert.That(Add(1, 1)).IsEqualTo(2);
}
