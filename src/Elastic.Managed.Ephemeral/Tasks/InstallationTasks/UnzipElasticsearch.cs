using System;
using System.IO;
using System.IO.Compression;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class UnzipElasticsearch : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var fs = cluster.FileSystem;
			var v = cluster.ClusterConfiguration.Version;
			if (Directory.Exists(fs.ElasticsearchHome)) return;
			var from = Path.Combine(fs.LocalFolder, v.Zip);
			cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} unzipping version [{v}] {{{from}}}");
			ZipFile.ExtractToDirectory(from, fs.LocalFolder);
			cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} extracted version [{v}] to {{{fs.LocalFolder}}}");
		}
	}
}
