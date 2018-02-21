using System;
using System.IO;
using System.IO.Compression;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class UnzipElasticsearch : InstallationTaskBase
	{
		public override void Run(EphemeralCluster cluster, INodeFileSystem fs)
		{
			var v = fs.Version;
			if (Directory.Exists(fs.ElasticsearchHome)) return;
			var from = Path.Combine(fs.LocalFolder, fs.Version.Zip);
			cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} unzipping version [{v}] {{{from}}}");
			ZipFile.ExtractToDirectory(from, fs.LocalFolder);
			cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} extracted version [{v}] to {{{fs.LocalFolder}}}");
		}
	}
}
