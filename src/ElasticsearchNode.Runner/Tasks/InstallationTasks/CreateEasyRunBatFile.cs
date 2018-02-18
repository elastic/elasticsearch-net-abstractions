using System.IO;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class CreateEasyRunBatFile : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem, ElasticsearchPlugin[] requiredPlugins) =>
			WriteFileIfNotExist(Path.Combine(fileSystem.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");
	}
}
