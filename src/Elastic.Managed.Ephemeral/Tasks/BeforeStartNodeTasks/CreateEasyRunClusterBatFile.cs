using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks
{
	public class CreateEasyRunClusterBatFile : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var fs = cluster.FileSystem;
			var clusterMoniker = cluster.ClusterMoniker;
			var v = cluster.ClusterConfiguration.Version;
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;

			var easyRunBat = Path.Combine(fs.LocalFolder, $"run-{clusterMoniker}.bat");
			if (File.Exists(easyRunBat)) return;
			cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEasyRunClusterBatFile)}}} node count [{cluster.Nodes.Count}]");

			var n = cluster.Nodes.First().NodeConfiguration;

			var badSettings = new[] {"node.name", "cluster.name", "http.port"};
			var batSettings = string.Join(" ", n.CommandLineArguments.Where(s => !badSettings.Any(s.Contains)));
			File.WriteAllText(easyRunBat, $@"elasticsearch-{v}\bin\elasticsearch.bat {batSettings}");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEasyRunClusterBatFile)}}} created easy run bat for [{clusterMoniker}]: {{{easyRunBat}}}");
		}
	}
}

