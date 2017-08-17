using System.IO;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class CreateLocalApplicationDirectory : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			if (!Directory.Exists(fileSystem.LocalFolder))
				Directory.CreateDirectory(fileSystem.LocalFolder);
		}
	}
}
