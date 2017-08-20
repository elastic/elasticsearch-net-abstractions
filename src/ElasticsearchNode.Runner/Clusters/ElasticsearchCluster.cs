using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Elastic.Net.Abstractions.Clusters
{
	public class ElasticsearchCluster : ClusterBase
	{
		public ElasticsearchCluster(ElasticsearchVersion version, int instanceCount = 1, string clusterName = null)
			: base(version, instanceCount, clusterName, CreateNodeSettings(instanceCount).ToArray()) { }

		protected override ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings) => connectionSettings;

		private static IEnumerable<string> CreateNodeSettings(int instanceCount)
		{
			yield return $"node.max_local_storage_nodes={instanceCount}";
			yield return $"discovery.zen.minimum_master_nodes={Quorem(instanceCount)}";
		}

		private static int Quorem(int instanceCount) => Math.Max(1, (int) Math.Floor((double) instanceCount / 2) + 1);
	}
}
