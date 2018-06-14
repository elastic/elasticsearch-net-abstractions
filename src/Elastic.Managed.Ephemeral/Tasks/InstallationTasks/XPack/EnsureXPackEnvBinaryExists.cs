using System.IO;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack
{
	public class EnsureXPackEnvBinaryExists : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;
			if (cluster.CachingAndCachedHomeExists()) return;

			var config = cluster.ClusterConfiguration;
			var v = config.Version;

			var envBinary = Path.Combine(config.FileSystem.ElasticsearchHome, "bin", "x-pack", "x-pack-env") + BinarySuffix;

			if (v.Major != 6 || File.Exists(envBinary)) return;
			if (v >= "6.3.0") return;

			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureSecurityUsersInDefaultRealmAreAdded)}}} {envBinary} does not exist, patching now.");
			File.WriteAllText(envBinary, "set ES_CLASSPATH=!ES_CLASSPATH!;!ES_HOME!/plugins/x-pack/*");
		}
	}
}
