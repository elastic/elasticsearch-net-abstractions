using System;
using Nest;

namespace Elastic.Net.Abstractions.Tasks.ValidationTasks
{
	public class ValidateRunningVersion : NodeValidationTaskBase
	{
		public override void Validate(IElasticClient client, NodeConfiguration configuration)
		{
			var alreadyUp = client.RootNodeInfo();
			if (!alreadyUp.IsValid) return;
			var v = configuration.ElasticsearchVersion;

			var alreadyUpVersion = new ElasticsearchVersion(alreadyUp.Version.Number);
			var alreadyUpSnapshotVersion = new ElasticsearchVersion(alreadyUp.Version.Number + "-SNAPSHOT");
			if (v != alreadyUpVersion && v != alreadyUpSnapshotVersion)
				throw new Exception($"running elasticsearch is version {alreadyUpVersion} but the test config dictates {v}");
		}
	}
}
