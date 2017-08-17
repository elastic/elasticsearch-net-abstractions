using System.IO;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem) =>
			WriteFileIfNotExist(Path.Combine(fileSystem.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
