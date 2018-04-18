using System;
using Elastic.Managed.Configuration;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateRunningVersion : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var infoResponse = cluster.Client().RootNodeInfo();
			if (!infoResponse.IsValid) return;
			var v = cluster.ClusterConfiguration.Version;

			var runningVersion = ElasticsearchVersion.From(infoResponse.Version.Number);
			if (v == runningVersion) return;

			var unsnapShot = v.ToString().Replace("-SNAPSHOT", "");
			if (unsnapShot == runningVersion) return;

			throw new Exception($"running elasticsearch is version {runningVersion} but the test config dictates {v}");
		}
	}
}
