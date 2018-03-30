using System.IO;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster) =>
			WriteFileIfNotExist(Path.Combine(cluster.FileSystem.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
