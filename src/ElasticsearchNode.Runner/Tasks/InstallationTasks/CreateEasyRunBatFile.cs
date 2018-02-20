using System.IO;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(EphimeralClusterBase cluster, INodeFileSystem fs) =>
			WriteFileIfNotExist(Path.Combine(fs.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
