// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Clients.Elasticsearch;
using Elastic.Stack.ArtifactsApi;
using Elastic.Transport;
using Elastic.Xunitv3.Elasticsearch.Core;

namespace Elastic.Xunitv3.Elasticsearch;

/// <summary>
///     A convenience non-generic Elasticsearch cluster base class.
///     Use with a primary constructor for one-liner cluster definitions:
///     <code>public class MyCluster() : ElasticsearchCluster("latest-9");</code>
///     <para>
///         Provides a default <see cref="Client" /> that connects to the cluster
///         with debug mode enabled and per-request diagnostics routed to the
///         current xUnit v3 test's output. Override to customize.
///     </para>
///     <para>
///         When connected to an external cluster (via <c>TEST_ELASTICSEARCH_URL</c> or
///         <see cref="ElasticsearchCluster{TConfiguration}.TryUseExternalCluster" />),
///         the client is automatically configured with the external API key if provided.
///     </para>
/// </summary>
public class ElasticsearchCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
	public ElasticsearchCluster(string version)
		: base(new ElasticsearchConfiguration(version))
	{
	}

	public ElasticsearchCluster(ElasticVersion version)
		: base(new ElasticsearchConfiguration(version))
	{
	}

	public ElasticsearchCluster(ElasticsearchConfiguration configuration)
		: base(configuration)
	{
	}

	/// <summary>
	///     A default <see cref="ElasticsearchClient" /> configured with debug mode
	///     and per-request diagnostics routed to the current xUnit v3 test's output.
	///     The client is cached for the cluster's lifetime.
	///     When using an external cluster with an API key, authentication is
	///     configured automatically.
	///     Override to customize connection settings, authentication, serialization, etc.
	/// </summary>
	public virtual ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
	{
		var settings = new ElasticsearchClientSettings(new StaticNodePool(c.NodesUris()))
			.WireXunitOutput(output);

		if (ExternalApiKey != null)
			settings = settings.Authentication(new ApiKey(ExternalApiKey));

		return new ElasticsearchClient(settings);
	});
}
