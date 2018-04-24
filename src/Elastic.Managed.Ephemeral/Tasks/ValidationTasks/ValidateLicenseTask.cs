using System;
using System.Linq;
using Elastic.Managed.FileSystem;
using Nest;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateLicenseTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;

			var license = cluster.Client().GetLicense();
			if (license.IsValid && license.License.Status == LicenseStatus.Active) return;

			var exceptionMessageStart = "Server has license plugin installed, ";
			var licenseFile = Environment.GetEnvironmentVariable("ES_LICENSE_FILE");
			if (!string.IsNullOrWhiteSpace(licenseFile))
			{
				var putLicense = cluster.Client().PostLicense(new PostLicenseRequest
				{
					License = License.LoadFromDisk(licenseFile)
				});
				if (!putLicense.IsValid)
					throw new Exception("Server has invalid license and the ES_LICENSE_FILE failed to register\r\n" + putLicense.DebugInformation);

				license = cluster.Client().GetLicense();
				if (license.IsValid && license.License.Status == LicenseStatus.Active) return;
				exceptionMessageStart += " but the installed license is invalid and we attempted to register ES_LICENSE_FILE ";
			}

			Exception exception = null;
			if (!license.IsValid)
			{
				exception = license.ApiCall.HttpStatusCode == 404
					? new Exception($"{exceptionMessageStart} but the license was not found! Details: {license.DebugInformation}")
					: new Exception($"{exceptionMessageStart} but a {license.ApiCall.HttpStatusCode} was returned! Details: {license.DebugInformation}");
			}
			else if (license.License == null)
				exception = new Exception($"{exceptionMessageStart}  but the license was deleted!");

			else if (license.License.Status == LicenseStatus.Expired)
				exception = new Exception($"{exceptionMessageStart} but the license has expired!");

			else if (license.License.Status == LicenseStatus.Invalid)
				exception = new Exception($"{exceptionMessageStart} but the license is invalid!");

			if (exception != null)
				throw exception;
		}
	}
}
