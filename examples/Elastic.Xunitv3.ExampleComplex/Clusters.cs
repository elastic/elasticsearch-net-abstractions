// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Elastic.Xunitv3.Elasticsearch;
using Elastic.Xunitv3.Elasticsearch.Core;

namespace Elastic.Xunitv3.ExampleComplex;

/// <summary>
///     Shared base — inherits the default Client from <see cref="ElasticsearchCluster" />.
///     Env-var detection (TEST_ELASTICSEARCH_URL / TEST_ELASTICSEARCH_API_KEY) is handled
///     automatically by the base class — no code changes needed.
/// </summary>
public abstract class MyClusterBase : ElasticsearchCluster
{
	protected MyClusterBase() : base(new ElasticsearchConfiguration("latest-9")
	{
		ShowElasticsearchOutputAfterStarted = false,
	})
	{
	}
}

public class TestCluster : MyClusterBase
{
	protected override void SeedCluster()
	{
		var response = Client.Info();
	}
}

/// <summary>
///     Uses the generic base directly — needs its own Client property since
///     <see cref="ElasticsearchCluster{TConfiguration}" /> does not provide one.
/// </summary>
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

		if (ExternalApiKey != null)
			settings = settings.Authentication(new ApiKey(ExternalApiKey));

		return new ElasticsearchClient(settings);
	});

	protected override void SeedCluster()
	{
		var response = Client.Info();
	}
}

/// <summary>
///     Demonstrates the programmatic hook — override
///     <see cref="ElasticsearchCluster{TConfiguration}.TryUseExternalCluster" />
///     to point at a specific cluster from code (e.g. read from a config file, service
///     discovery, etc.). Falls through to ephemeral startup when returning null.
/// </summary>
public class ProgrammaticExternalCluster : ElasticsearchCluster
{
	public ProgrammaticExternalCluster() : base(new ElasticsearchConfiguration("latest-9"))
	{
	}

	protected override ExternalClusterConfiguration TryUseExternalCluster()
	{
		var url = Environment.GetEnvironmentVariable("MY_DEV_CLUSTER_URL");
		if (string.IsNullOrEmpty(url))
			return null;

		return new ExternalClusterConfiguration(
			new Uri(url),
			Environment.GetEnvironmentVariable("MY_DEV_CLUSTER_KEY")
		);
	}
}
