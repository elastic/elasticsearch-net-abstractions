using System;
using System.IO;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.InstallationTasks
{
	public class DownloadCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(EphimeralClusterBase cluster, INodeFileSystem fs)
		{
			var v = fs.Version;
			var from = v.DownloadUrl;
			var to = Path.Combine(fs.LocalFolder, fs.Version.Zip);
			if (File.Exists(to)) return;
			Console.WriteLine($"Download elasticsearch: {v} from {from} to {to}");
			DownloadFile(from, to);
			Console.WriteLine($"Downloaded elasticsearch: {v} to {to}");
		}
	}
}
