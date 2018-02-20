using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;

namespace Elastic.Net.Abstractions.Tasks.BeforeStartNodeTasks
{
	public class CreateEasyRunClusterBatFile : BeforeStartNodeTaskBase
	{
		public override void Run(EphimeralClusterBase cluster, INodeFileSystem fs)
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

