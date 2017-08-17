using System;
using System.IO;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class DownloadCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var v = config.ElasticsearchVersion;
			var from = v.DownloadUrl;
			var to = fileSystem.ZipDownloadTarget;
			if (File.Exists(to)) return;
			Console.WriteLine($"Download elasticsearch: {v} from {from} to {to}");
			DownloadFile(from, to);
			Console.WriteLine($"Downloaded elasticsearch: {v} to {to}");
		}
	}
}
