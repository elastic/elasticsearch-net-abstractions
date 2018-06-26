using System.IO;
using Elastic.Managed.Configuration;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack
{
	public class PathXPackInBatFile : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;

			var config = cluster.ClusterConfiguration;
			var fileSystem = cluster.FileSystem;
			var v = config.Version;

			if (v.Major != 5) return;
			if (IsMono || Path.DirectorySeparatorChar == '/') return;

			cluster.Writer?.WriteDiagnostic($"{{{nameof(PathXPackInBatFile)}}} patching x-pack .in.bat to accept CONF_DIR");
			PatchPlugin(v, fileSystem);
		}

		private static void PatchPlugin(ElasticsearchVersion v, INodeFileSystem fileSystem)
		{
			var h = fileSystem.ElasticsearchHome;
			var file = Path.Combine(h, "bin", "x-pack", ".in.bat");
			var contents = File.ReadAllText(file);
			contents = contents.Replace("set ES_PARAMS=-Des.path.home=\"%ES_HOME%\"", "set ES_PARAMS=-Des.path.home=\"%ES_HOME%\" -Des.path.conf=\"%CONF_DIR%\"");
			File.WriteAllText(file, contents);
		}
	}
}