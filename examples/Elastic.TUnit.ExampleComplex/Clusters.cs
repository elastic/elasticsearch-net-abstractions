// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Clients.Elasticsearch;
using Elastic.Elasticsearch.TUnit;
using Elastic.Transport;

namespace Elastic.TUnit.ExampleComplex;

/// <summary>
///     Shared base that configures the cluster and exposes a typed client.
///     All clusters in this project inherit from here.
/// </summary>
public abstract class MyClusterBase : ElasticsearchCluster
{
	protected MyClusterBase() : base(new ElasticsearchConfiguration("latest-9")
	{
		ShowElasticsearchOutputAfterStarted = false,
	})
	{
	}

	public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
	{
		var pool = new StaticNodePool(c.NodesUris());
		var settings = new ElasticsearchClientSettings(pool)
			.EnableDebugMode()
			.OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
		return new ElasticsearchClient(settings);
	});
}

public class TestCluster : MyClusterBase
{
	protected override void SeedCluster()
	{
		var response = Client.Info();
	}
}

public class TestGenericCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
	public TestGenericCluster() : base(new ElasticsearchConfiguration("latest-9"))
	{
	}

	public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
	{
		var pool = new StaticNodePool(c.NodesUris());
		var settings = new ElasticsearchClientSettings(pool)
			.EnableDebugMode()
			.OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
		return new ElasticsearchClient(settings);
	});

	protected override void SeedCluster()
	{
		var response = Client.Info();
	}
}
