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
				this.StartTrial(cluster);
				return;
			}

			cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} attempting to post license json");

			var postResponse = this.Post(cluster, "_xpack/license", "", cluster.ClusterConfiguration.XPackLicenseJson);
			if (postResponse != null && postResponse.IsSuccessStatusCode) return;

			var details = postResponse != null ? this.GetResponseString(postResponse) : "";
			throw new Exception($"The license that was posted was not accepted: {details}");
		}

		private void StartTrial(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var w = cluster.Writer;
			var c = cluster.ClusterConfiguration;
			if (c.Version < "6.3.0" || c.TrialMode == XPackTrialMode.None)
			{
				cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} {c.Version} < 6.3.0 or opting out of explicit basic/trial license");
				return;
			}

			if (c.TrialMode == XPackTrialMode.Trial)
			{
				//TODO make this configurable for either trial or basic
				cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} attempt to start trial license");
				var postResponse = this.Post(cluster, "_xpack/license/start_trial", "acknowledge=true", string.Empty);
				if (postResponse != null && postResponse.IsSuccessStatusCode) return;
			}

			if (c.TrialMode == XPackTrialMode.Basic)
			{
				//TODO make this configurable for either trial or basic
				cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} attempt to start basic license");
				var postResponse = this.Post(cluster, "_xpack/license/start_basic", "acknowledge=true", string.Empty);
				if (postResponse != null && postResponse.IsSuccessStatusCode) return;
			}

		}
	}
}
