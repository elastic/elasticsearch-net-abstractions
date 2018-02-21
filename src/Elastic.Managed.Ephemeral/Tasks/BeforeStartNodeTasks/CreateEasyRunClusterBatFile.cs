using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks
{
	public class CreateEasyRunClusterBatFile : BeforeStartNodeTaskBase
	{
		public override void Run(EphemeralClusterBase cluster, INodeFileSystem fs)
		{
			var clusterMoniker = cluster.ClusterMoniker;
			var v = fs.Version;
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;

			var easyRunBat = Path.Combine(fs.LocalFolder, $"run-{clusterMoniker}.bat");
			if (File.Exists(easyRunBat)) return;
			var n = cluster.Nodes.First().NodeConfiguration;

			var badSettings = new[] {"node.name", "cluster.name", "http.port"};
			var batSettings = string.Join(" ", n.CommandLineArguments.Where(s => !badSettings.Any(s.Contains)));
			File.WriteAllText(easyRunBat, $@"elasticsearch-{v}\bin\elasticsearch.bat {batSettings}");
		}
	}
}

