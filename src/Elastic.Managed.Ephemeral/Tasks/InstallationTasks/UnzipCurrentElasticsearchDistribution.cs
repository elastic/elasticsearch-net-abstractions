using System;
using System.IO;
using System.IO.Compression;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class UnzipCurrentElasticsearchDistribution : InstallationTaskBase
	{
		public override void Run(EphemeralClusterBase cluster, INodeFileSystem fs)
		{
			var v = fs.Version;
			if (Directory.Exists(fs.ElasticsearchHome)) return;
			Console.WriteLine($"Unzipping elasticsearch: {v} ...");
			var from = Path.Combine(fs.LocalFolder, fs.Version.Zip);
			ZipFile.ExtractToDirectory(from, fs.LocalFolder);
		}
	}
}
