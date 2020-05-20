// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Elastic.Xunit.ExampleComplex
{
	public abstract class ClusterTestClassBase<TCluster> : IClusterFixture<TCluster>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, IMyCluster, new()
	{
		public TCluster Cluster { get; }
		public IElasticClient Client => Cluster.Client;

		protected ClusterTestClassBase(TCluster cluster) => Cluster = cluster;
	}
}
