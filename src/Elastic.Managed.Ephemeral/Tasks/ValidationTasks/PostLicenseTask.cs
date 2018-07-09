using System;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class PostLicenseTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;
			if (string.IsNullOrWhiteSpace(cluster.ClusterConfiguration.XPackLicenseJson))
			{
				cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} no license file available to post");
				return;
			}

			cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} attempting to post license json");

			var postResponse = this.Post(cluster, "_xpack/license", "", cluster.ClusterConfiguration.XPackLicenseJson);
			if (postResponse != null && postResponse.IsSuccessStatusCode) return;

			var details = postResponse != null ? this.GetResponseString(postResponse) : "";
			throw new Exception($"The license that was posted was not accepted: {details}");
		}
	}
}
