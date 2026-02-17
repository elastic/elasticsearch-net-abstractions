// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.TUnit.Elasticsearch.Core;

namespace Elastic.TUnit.ExampleComplex;

public abstract class ClusterTestClassBase<TCluster>(TCluster cluster)
	where TCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
	protected TCluster Cluster { get; } = cluster;
}
