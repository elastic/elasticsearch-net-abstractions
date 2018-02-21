using System.IO;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(EphemeralCluster cluster, INodeFileSystem fs) =>
			WriteFileIfNotExist(Path.Combine(fs.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
