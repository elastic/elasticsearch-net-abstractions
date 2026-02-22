// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.XunitV3.Elasticsearch;
using Elastic.XunitV3.Elasticsearch.Core;
using Xunit;

[assembly: TestFramework(typeof(ElasticTestFramework))]

namespace Elastic.XunitV3.ExampleMinimal;

/// <summary> One-liner cluster — the default Client is provided by the base class. </summary>
public class MyTestCluster() : ElasticsearchCluster("latest-9");

public class ExampleTest(MyTestCluster cluster) : IClusterFixture<MyTestCluster>
{
	[Fact]
	public async Task SomeTest()
	{
		var info = await cluster.Client.InfoAsync();

		Assert.NotNull(info.Name);
	}
}
