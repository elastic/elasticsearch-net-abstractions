using System;
using Elastic.Managed.FileSystem;
using Elasticsearch.Net;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateClusterStateTask : ClusterComposeTask
	{
		private static TimeSpan ClusterHealthTimeout { get; } = TimeSpan.FromSeconds(20);

		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
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
