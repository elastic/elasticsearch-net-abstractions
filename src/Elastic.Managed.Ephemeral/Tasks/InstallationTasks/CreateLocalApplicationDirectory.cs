using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateLocalApplicationDirectory : InstallationTaskBase
	{
		public override void Run(EphemeralClusterBase cluster, INodeFileSystem fs)
		{
			if (Directory.Exists(fs.LocalFolder)) return;

			cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateLocalApplicationDirectory)}}} creating {{{fs.LocalFolder}}}");

			Directory.CreateDirectory(fs.LocalFolder);
		}
	}
}
