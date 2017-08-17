using System;
using System.IO;
using System.IO.Compression;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class UnzipCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var v = config.ElasticsearchVersion;
			if (Directory.Exists(fileSystem.ElasticsearchHome)) return;
			Console.WriteLine($"Unzipping elasticsearch: {v} ...");
			ZipFile.ExtractToDirectory(fileSystem.ZipDownloadTarget, fileSystem.LocalFolder);
		}
	}
}
