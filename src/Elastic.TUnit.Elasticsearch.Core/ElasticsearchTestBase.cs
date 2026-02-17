// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     Optional convenience base class for test classes that use an Elasticsearch cluster.
///     Exposes the cluster via a protected property.
///     Not required - users can use primary constructor injection directly.
/// </summary>
public abstract class ElasticsearchTestBase<TCluster>(TCluster cluster)
	where TCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
	protected TCluster Cluster { get; } = cluster;
}
