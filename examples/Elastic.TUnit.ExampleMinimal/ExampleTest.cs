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

/// <summary> Declare our cluster that we want to inject into our test classes. </summary>
public class MyTestCluster() : ElasticsearchCluster("latest-9");

[ClassDataSource<MyTestCluster>(Shared = SharedType.Keyed, Key = nameof(MyTestCluster))]
public class ExampleTest(MyTestCluster cluster)
{
	[Test]
	public async Task SomeTest()
	{
		// GetOrAddClient with TextWriter routes per-request diagnostics to the
		// current test's output. The client is cached per-cluster, but the writer
		// dynamically resolves to whichever test is running.
		var client = cluster.GetOrAddClient((c, output) =>
		{
			var nodes = c.NodesUris();
			var connectionPool = new StaticNodePool(nodes);
			var settings = new ElasticsearchClientSettings(connectionPool)
				.EnableDebugMode()
				.OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
			return new ElasticsearchClient(settings);
		});

		var rootNodeInfo = client.Info();

		await Assert.That(rootNodeInfo.Name).IsNotNull();
	}
}
