// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.TUnit;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Elastic.TUnit.ExampleMinimal;

/// <summary> One-liner cluster — the default Client is provided by the base class. </summary>
public class MyTestCluster() : ElasticsearchCluster("latest-9");

[ClassDataSource<MyTestCluster>(Shared = SharedType.Keyed, Key = nameof(MyTestCluster))]
public class ExampleTest(MyTestCluster cluster)
{
	[Test]
	public async Task SomeTest()
	{
		var info = await cluster.Client.InfoAsync();

		await Assert.That(info.Name).IsNotNull();
	}
}
