using System;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elastic.Net.Abstractions.Plugins;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Net.Abstractions.Tasks.ValidationTasks
{
	public class ValidateClusterStateTask : NodeValidationTaskBase
	{
		private static TimeSpan ClusterHealthTimeout { get; } = TimeSpan.FromSeconds(20);

		public override void Validate(EphimeralClusterBase cluster, INodeFileSystem fs)
		{
			var healthyCluster = cluster.Client.ClusterHealth(g => g
				.WaitForStatus(WaitForStatus.Yellow)
				.Timeout(ClusterHealthTimeout)
			);
			if(!healthyCluster.IsValid)
				throw new Exception($"Did not see a healhty cluster before calling onNodeStarted handler." + healthyCluster.DebugInformation);
		}
	}
}
