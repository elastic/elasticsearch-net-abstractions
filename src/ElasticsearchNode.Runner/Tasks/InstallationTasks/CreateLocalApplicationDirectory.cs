using System.IO;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.InstallationTasks
{
	public class CreateLocalApplicationDirectory : InstallationTaskBase
	{
		public override void Run(EphimeralClusterBase cluster, INodeFileSystem fs)
		{
			if (!Directory.Exists(fs.LocalFolder))
				Directory.CreateDirectory(fs.LocalFolder);
		}
	}
}
