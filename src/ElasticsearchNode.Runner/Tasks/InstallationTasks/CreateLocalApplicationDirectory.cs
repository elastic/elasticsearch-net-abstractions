using System.IO;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
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
