// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

			cluster.Writer?.WriteDiagnostic(
				$"{{{nameof(ValidateRunningVersion)}}} validating the cluster is running the requested version: {requestedVersion}");

			var catNodes = this.Get(cluster, "_cat/nodes", "h=version");
			if (catNodes == null || !catNodes.IsSuccessStatusCode)
				throw new Exception(
					$"Calling _cat/nodes for version checking did not result in an OK response {GetResponseException(catNodes)}");

			var nodeVersions = GetResponseString(catNodes).Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries)
				.ToList();
			var allOnRequestedVersion = false;

			// fully qualified name not returned anymore, so beta1 rc1 etcetera is no longer returned in the version number
			if (requestedVersion.Major >= 7)
			{
				var anchorVersion = $"{requestedVersion.Major}.{requestedVersion.Minor}.{requestedVersion.Patch}";
				allOnRequestedVersion = nodeVersions.All(v => v.Trim() == anchorVersion);
				if (!allOnRequestedVersion)
					throw new Exception(
						$"Not all the running nodes in the cluster are on requested version: {anchorVersion} received: {string.Join(", ", nodeVersions)}");
			}
			else
			{
				var requestedVersionNoSnapShot =
					cluster.ClusterConfiguration.Version.ToString().Replace("-SNAPSHOT", "");
				allOnRequestedVersion = nodeVersions.All(v => v.Trim() == requestedVersion || v.Trim() == requestedVersionNoSnapShot);

				if (!allOnRequestedVersion)
					throw new Exception(
						$"Not all the running nodes in the cluster are on requested version: {requestedVersion} received: {string.Join(", ", nodeVersions)}");
			}
		}
	}
}
