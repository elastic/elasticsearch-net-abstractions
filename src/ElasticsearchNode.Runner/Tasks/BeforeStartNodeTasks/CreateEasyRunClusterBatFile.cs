using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Elastic.Net.Abstractions.Tasks.BeforeStartNodeTasks
{
	public class CreateEasyRunClusterBatFile : BeforeStartNodeTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fs, string[] serverSettings)
		{
			var clusterMoniker = config.ClusterMoniker;
			var v = config.ElasticsearchVersion;
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;

			var easyRunBat = Path.Combine(fs.LocalFolder, $"run-{clusterMoniker}.bat");
			if (File.Exists(easyRunBat)) return;
			var badSettings = new[] {"node.name", "cluster.name"};
			var batSettings = string.Join(" ", serverSettings.Where(s => !badSettings.Any(s.Contains)));
			File.WriteAllText(easyRunBat, $@"elasticsearch-{v}\bin\elasticsearch.bat {batSettings}");
		}
	}
}

