// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.ValidationTasks
{
	public class PostLicenseTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;
			if (string.IsNullOrWhiteSpace(cluster.ClusterConfiguration.XPackLicenseJson))
			{
				cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} no license file available to post");
				StartTrial(cluster);
				return;
			}

			cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} attempting to post license json");

			var licenseUrl = cluster.ClusterConfiguration.Version.Major < 8 ? "_xpack/license" : "_license";
			var postResponse = Post(cluster, licenseUrl, "", cluster.ClusterConfiguration.XPackLicenseJson);
			if (postResponse != null && postResponse.IsSuccessStatusCode) return;

			var details = postResponse != null ? GetResponseException(postResponse) : "";
			throw new Exception($"The license that was posted was not accepted: {details}");
		}

		private void StartTrial(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var w = cluster.Writer;
			var c = cluster.ClusterConfiguration;
			if (c.Version < "6.3.0" || c.TrialMode == XPackTrialMode.None)
			{
				cluster.Writer.WriteDiagnostic(
					$"{{{nameof(PostLicenseTask)}}} {c.Version} < 6.3.0 or opting out of explicit basic/trial license");
				return;
			}

			var licenseUrl = cluster.ClusterConfiguration.Version.Major < 8 ? "_xpack/license" : "_license";
			if (c.TrialMode == XPackTrialMode.Trial)
			{
				//TODO make this configurable for either trial or basic
				cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} attempt to start trial license");
				var postResponse = Post(cluster, $"{licenseUrl}/start_trial", "acknowledge=true", string.Empty);
				if (postResponse != null && postResponse.IsSuccessStatusCode) return;
			}

			if (c.TrialMode == XPackTrialMode.Basic)
			{
				//TODO make this configurable for either trial or basic
				cluster.Writer.WriteDiagnostic($"{{{nameof(PostLicenseTask)}}} attempt to start basic license");
				var postResponse = Post(cluster, $"{licenseUrl}/start_basic", "acknowledge=true", string.Empty);
				if (postResponse != null && postResponse.IsSuccessStatusCode) return;
			}
		}
	}
}
