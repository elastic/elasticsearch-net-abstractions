using System;
using System.Linq;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateRunningVersion : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var requestedVersion = cluster.ClusterConfiguration.Version;

			cluster.Writer.WriteDiagnostic($"{{{nameof(ValidateRunningVersion)}}} validating the cluster is running the requested version: {requestedVersion}");

			var catNodes = this.Get(cluster, "_cat/nodes", "h=version");
			if (catNodes == null || !catNodes.IsSuccessStatusCode) throw new Exception($"Calling _cat/nodes for version checking did not result in an OK response");

			var requestedVersionNoSnapShot = cluster.ClusterConfiguration.Version.ToString().Replace("-SNAPSHOT", "");
			var nodeVersions = GetResponseString(catNodes).Split(new [] {'\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();

			var allOnRequestedVersion = nodeVersions.All(v => v == requestedVersion || v == requestedVersionNoSnapShot);
			if (!allOnRequestedVersion)
				throw new Exception($"Not all the running nodes in the cluster are on requested version: {requestedVersion}");
		}
	}
}
