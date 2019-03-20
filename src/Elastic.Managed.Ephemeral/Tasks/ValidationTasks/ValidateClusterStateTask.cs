using System;
using System.Net;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateClusterStateTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			cluster.Writer.WriteDiagnostic($"{{{nameof(ValidateClusterStateTask)}}} waiting cluster to go into yellow health state");
			var healthyResponse = this.Get(cluster, "_cluster/health", "wait_for_status=yellow&timeout=20s");
			if (healthyResponse == null || healthyResponse.StatusCode != HttpStatusCode.OK)
				throw new Exception($"Cluster health waiting for status yellow failed after 20s {GetResponseException(healthyResponse)}");
		}
	}
}
