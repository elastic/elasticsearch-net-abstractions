using System;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidateRunningVersion : NodeValidationTaskBase
	{
		public override void Validate(EphemeralClusterBase cluster, INodeFileSystem fileSystem)
		{
			var alreadyUp = cluster.Client.RootNodeInfo();
			if (!alreadyUp.IsValid) return;
			var v = fileSystem.Version;

			var alreadyUpVersion = ElasticsearchVersion.From(alreadyUp.Version.Number);
			var alreadyUpSnapshotVersion = ElasticsearchVersion.From(alreadyUp.Version.Number + "-SNAPSHOT");
			if (v != alreadyUpVersion && v != alreadyUpSnapshotVersion)
				throw new Exception($"running elasticsearch is version {alreadyUpVersion} but the test config dictates {v}");
		}
	}
}
