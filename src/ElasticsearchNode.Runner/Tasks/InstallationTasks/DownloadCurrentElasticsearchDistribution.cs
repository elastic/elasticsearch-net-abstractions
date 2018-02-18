using System;
using System.IO;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class DownloadCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem, ElasticsearchPlugin[] requiredPlugins)
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
