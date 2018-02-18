using System;
using System.IO;
using System.IO.Compression;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class UnzipCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem, ElasticsearchPlugin[] requiredPlugins)
		{
			var v = config.ElasticsearchVersion;
			if (Directory.Exists(fileSystem.ElasticsearchHome)) return;
			Console.WriteLine($"Unzipping elasticsearch: {v} ...");
			ZipFile.ExtractToDirectory(fileSystem.ZipDownloadTarget, fileSystem.LocalFolder);
		}
	}
}
