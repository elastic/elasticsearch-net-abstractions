using System.IO;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateLocalApplicationDirectory : InstallationTaskBase
	{
		public override void Run(EphemeralClusterBase cluster, INodeFileSystem fs)
		{
			if (!Directory.Exists(fs.LocalFolder))
				Directory.CreateDirectory(fs.LocalFolder);
		}
	}
}
