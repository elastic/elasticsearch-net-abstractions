using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateLocalApplicationDirectory : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var fs = cluster.FileSystem;
			if (Directory.Exists(fs.LocalFolder)) return;

			cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateLocalApplicationDirectory)}}} creating {{{fs.LocalFolder}}}");

			Directory.CreateDirectory(fs.LocalFolder);
		}
	}
}
