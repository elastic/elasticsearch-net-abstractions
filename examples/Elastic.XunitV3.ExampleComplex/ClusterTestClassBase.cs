// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Clients.Elasticsearch;
using Elastic.XunitV3.Elasticsearch.Core;
using Nullean.Xunit.Partitions.v3.Sdk;

namespace Elastic.XunitV3.ExampleComplex;

public abstract class ClusterTestClassBase<TCluster>(TCluster cluster)
	: IClusterFixture<TCluster>
	where TCluster : ElasticsearchCluster<ElasticsearchConfiguration>, IPartitionLifetime
{
	protected TCluster Cluster { get; } = cluster;
}
