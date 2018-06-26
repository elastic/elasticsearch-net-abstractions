using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks
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

			Directory.CreateDirectory(f.TempFolder);

			if (!Directory.Exists(f.ConfigPath))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} creating config folder {{{f.ConfigPath}}}");
				Directory.CreateDirectory(f.ConfigPath);

			}

			CopyHomeConfigToEphemeralConfig(cluster, f, fs);
		}

		private static void CopyHomeConfigToEphemeralConfig(IEphemeralCluster<EphemeralClusterConfiguration> cluster, EphemeralFileSystem ephemeralFileSystem, INodeFileSystem fs)
		{
			var target = ephemeralFileSystem.ConfigPath;
			var cachedEsHomeFolder = Path.Combine(fs.LocalFolder, cluster.GetCacheFolderName());
			var cachedElasticsearchYaml = Path.Combine(cachedEsHomeFolder, "config", "elasticsearch.yaml");

			var homeSource = cluster.ClusterConfiguration.CacheEsHomeInstallation && File.Exists(cachedElasticsearchYaml) ? cachedEsHomeFolder : fs.ElasticsearchHome;
			var source = Path.Combine(homeSource, "config");
			if (!Directory.Exists(source))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} source config {{{source}}} does not exist nothing to copy");
				return;
			}

			cluster.Writer?.WriteDiagnostic($"{{{nameof(CreateEphemeralDirectory)}}} copying cached {{{source}}} as to [{target}]");
			CopyFolder(source, target);
		}
	}
}
