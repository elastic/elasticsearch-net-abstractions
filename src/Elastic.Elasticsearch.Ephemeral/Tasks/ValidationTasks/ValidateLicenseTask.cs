// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Threading;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateLicenseTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;

			cluster.Writer.WriteDiagnostic(
				$"{{{nameof(ValidateLicenseTask)}}} validating the x-pack license is active");
			var licenseType = GetLicenseType(cluster);

			var licenseStatus = GetLicenseStatus(cluster);
			if (licenseStatus == "active") return;
			throw new Exception(
				$"x-pack is installed but the {licenseType} license was expected to be active but is {licenseStatus}");
		}

		private string GetLicenseStatus(IEphemeralCluster<EphemeralClusterConfiguration> cluster) =>
			LicenseInfo(cluster, "license.status");

		private string GetLicenseType(IEphemeralCluster<EphemeralClusterConfiguration> cluster) =>
			LicenseInfo(cluster, "license.type");

		private string LicenseInfo(IEphemeralCluster<EphemeralClusterConfiguration> cluster, string filter,
			int retries = 0)
		{
			var licenseUrl = cluster.ClusterConfiguration.Version.Major < 8 ? "_xpack/license" : "_license";
			var getLicense = Get(cluster, licenseUrl, "filter_path=" + filter);
			if ((getLicense == null || !getLicense.IsSuccessStatusCode) && retries >= 5)
				throw new Exception(
					$"Calling GET _xpack/license did not result in an OK response after trying {retries} {GetResponseException(getLicense)}");

			if (getLicense == null || !getLicense.IsSuccessStatusCode)
			{
				Thread.Sleep(TimeSpan.FromSeconds(10));
				return LicenseInfo(cluster, filter, ++retries);
			}

			var licenseStatusString = GetResponseString(getLicense)
				.Where(c => !new[] {' ', '\n', '{', '"', '}'}.Contains(c))
				.ToArray();
			var status = new string(licenseStatusString).Replace($"{filter.Replace(".", ":")}:", "");
			return status;
		}
	}
}
