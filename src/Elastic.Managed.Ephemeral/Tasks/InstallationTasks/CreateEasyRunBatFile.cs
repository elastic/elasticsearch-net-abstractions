using System.IO;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(EphemeralClusterBase cluster, INodeFileSystem fs) =>
			WriteFileIfNotExist(Path.Combine(fs.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
