using System;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elastic.Net.Abstractions.Plugins;
using Nest;

namespace Elastic.Net.Abstractions.Tasks.ValidationTasks
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
