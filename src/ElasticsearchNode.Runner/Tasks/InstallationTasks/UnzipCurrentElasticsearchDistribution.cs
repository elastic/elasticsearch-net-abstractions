using System;
using System.IO;
using System.IO.Compression;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.InstallationTasks
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
