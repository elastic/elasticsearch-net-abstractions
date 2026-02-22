// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elastic.Xunitv3.Elasticsearch.Core;

/// <summary>
///     Optional convenience base class for test classes that use an Elasticsearch cluster.
///     Implements <see cref="IClusterFixture{TCluster}" /> and exposes the cluster via
///     a protected property. Not required — users can implement <see cref="IClusterFixture{TCluster}" />
///     directly and use primary constructor injection.
/// </summary>
public abstract class ElasticsearchTestBase<TCluster>(TCluster cluster)
	: IClusterFixture<TCluster>
	where TCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
	protected TCluster Cluster { get; } = cluster;
}
