// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Stack.ArtifactsApi;

namespace Elastic.Elasticsearch.TUnit;

/// <summary>
///     A convenience non-generic Elasticsearch cluster base class.
///     Use with a primary constructor for one-liner cluster definitions:
///     <code>public class MyCluster : ElasticsearchCluster("latest-9");</code>
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
}
