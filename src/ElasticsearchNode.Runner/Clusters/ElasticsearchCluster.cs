using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.ManagedNode.Configuration;
using Nest;

namespace Elastic.Net.Abstractions.Clusters
{
	public class ElasticsearchCluster : ClusterBase
	{
		public ElasticsearchCluster(ElasticsearchVersion version, int instanceCount = 1, string clusterName = null)
			: base(version, instanceCount, clusterName) { }

		protected override ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings) => connectionSettings;

	}
}
