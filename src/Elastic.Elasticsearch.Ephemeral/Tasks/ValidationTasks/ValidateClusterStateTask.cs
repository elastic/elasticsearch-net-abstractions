// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Net;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateClusterStateTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			cluster.Writer.WriteDiagnostic($"{{{nameof(ValidateClusterStateTask)}}} waiting cluster to go into yellow health state");
			var healthyResponse = Get(cluster, "_cluster/health", "wait_for_status=yellow&timeout=20s");
			if (healthyResponse == null || healthyResponse.StatusCode != HttpStatusCode.OK)
				throw new Exception($"Cluster health waiting for status yellow failed after 20s {GetResponseException(healthyResponse)}");
		}
	}
}
