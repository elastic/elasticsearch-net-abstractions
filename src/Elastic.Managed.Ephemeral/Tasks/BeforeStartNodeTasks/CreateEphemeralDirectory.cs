using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks;
using Elastic.Managed.FileSystem;
using static System.IO.Directory;
using static System.IO.SearchOption;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class CreateEphemeralDirectory : BeforeStartNodeTaskBase
	{
		public override void Run(EphemeralCluster cluster, INodeFileSystem fs)
		{
			if (!(fs is EphemeralFileSystem f)) return;

			if (Exists(f.TempFolder)) return;

			cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} creating {{{f.TempFolder}}}");

			CreateDirectory(f.TempFolder);

			if (!Exists(f.ConfigPath))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} creating config folder {{{f.ConfigPath}}}");
				CreateDirectory(f.ConfigPath);

			}
			var target = f.ConfigPath;
			var source = Path.Combine(f.ElasticsearchHome, "config");

			if (Exists(source))
			{
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
}
