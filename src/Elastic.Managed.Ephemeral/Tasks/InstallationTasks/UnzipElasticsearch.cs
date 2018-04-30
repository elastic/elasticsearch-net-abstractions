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
			if (Directory.Exists(fs.ElasticsearchHome))
			{
                cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} skipping [{fs.ElasticsearchHome}] already exists");
				return;
			}

			var from = Path.Combine(fs.LocalFolder, v.Zip);
			var extractedFolder = Path.Combine(fs.LocalFolder, v.FolderInZip);
			if (!Directory.Exists(extractedFolder))
			{
                cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} unzipping version [{v}] {{{from}}}");
                ZipFile.ExtractToDirectory(from, fs.LocalFolder);
                cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} extracted version [{v}] to {{{fs.LocalFolder}}}");

			}

			if (extractedFolder == fs.ElasticsearchHome) return;
			cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} Copying extracted folder {{{extractedFolder}}} => {fs.ElasticsearchHome}");
			Copy(new DirectoryInfo(extractedFolder), new DirectoryInfo(fs.ElasticsearchHome));
		}

		private static void Copy(DirectoryInfo source, DirectoryInfo target)
		{
			foreach (var dir in source.GetDirectories()) Copy(dir, target.CreateSubdirectory(dir.Name));
			foreach (var file in source.GetFiles()) file.CopyTo(Path.Combine(target.FullName, file.Name));
		}
	}
}
