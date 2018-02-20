using System.IO;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(EphimeralClusterBase cluster, INodeFileSystem fs) =>
			WriteFileIfNotExist(Path.Combine(fs.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
