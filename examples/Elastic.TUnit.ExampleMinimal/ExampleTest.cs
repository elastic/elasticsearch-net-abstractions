// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Elasticsearch.TUnit;
using Elastic.Transport;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Elastic.TUnit.ExampleMinimal;

/// <summary>
///     Declare the cluster once with its client configuration.
///     The client is cached per-cluster and per-request diagnostics are routed
///     to whichever TUnit test is currently executing.
/// </summary>
public class MyTestCluster() : ElasticsearchCluster("latest-9")
{
	public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
	{
		var pool = new StaticNodePool(c.NodesUris());
		var settings = new ElasticsearchClientSettings(pool)
			.EnableDebugMode()
			.OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
		return new ElasticsearchClient(settings);
	});
}

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
