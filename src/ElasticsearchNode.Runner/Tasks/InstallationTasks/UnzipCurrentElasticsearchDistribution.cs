using System;
using System.IO;
using System.IO.Compression;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class UnzipCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(EphimeralClusterBase cluster, INodeFileSystem fs)
		{
			var v = fs.Version;
			if (Directory.Exists(fs.ElasticsearchHome)) return;
			Console.WriteLine($"Unzipping elasticsearch: {v} ...");
			var from = Path.Combine(fs.LocalFolder, fs.Version.Zip);
			ZipFile.ExtractToDirectory(from, fs.LocalFolder);
		}
	}
}
