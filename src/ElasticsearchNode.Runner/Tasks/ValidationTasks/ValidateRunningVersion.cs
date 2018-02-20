using System;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.ValidationTasks
{
	public class ValidateRunningVersion : NodeValidationTaskBase
	{
		public override void Validate(EphimeralClusterBase cluster, INodeFileSystem fileSystem)
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
