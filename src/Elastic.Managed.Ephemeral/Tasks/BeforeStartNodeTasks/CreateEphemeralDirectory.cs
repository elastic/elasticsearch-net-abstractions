using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;
using static System.IO.Directory;
using static System.IO.SearchOption;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateEphemeralDirectory : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var fs = cluster.FileSystem;
			if (!(fs is EphemeralFileSystem f))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} unexpected IFileSystem implementation {{{fs.GetType()}}}");
				return;
			}

			cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} creating {{{f.TempFolder}}}");

			CreateDirectory(f.TempFolder);

			if (!Exists(f.ConfigPath))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} creating config folder {{{f.ConfigPath}}}");
				CreateDirectory(f.ConfigPath);

			}
			var target = f.ConfigPath;

			var source = Path.Combine(f.ElasticsearchHome, "config");
			if (!Exists(source))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} source config {{{source}}} does not exist nothing to copy");
				return;
			}

			foreach (var sourceDir in GetDirectories(source, "*", AllDirectories))
			{
				var targetDir = sourceDir.Replace(source, target);
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} creating config folder {{{targetDir}}}");
				CreateDirectory(targetDir);
			}

			foreach (var sourcePath in GetFiles(source, "*.*", AllDirectories))
			{
				var targetPath = sourcePath.Replace(source, target);
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} copying config file to {{{targetPath}}}");
				File.Copy(sourcePath, targetPath, true);
			}
		}

	}
}
