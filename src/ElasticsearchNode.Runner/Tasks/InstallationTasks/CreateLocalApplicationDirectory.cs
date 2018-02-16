using System.IO;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class CreateLocalApplicationDirectory : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem, ElasticsearchPlugin[] requiredPlugins)
		{
			if (!Directory.Exists(fileSystem.LocalFolder))
				Directory.CreateDirectory(fileSystem.LocalFolder);
		}
	}
}
