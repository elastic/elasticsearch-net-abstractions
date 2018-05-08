using System.IO;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack
{
	public class EnsureXPackEnvBinaryExists : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;

			var config = cluster.ClusterConfiguration;
			var v = config.Version;
			if (v.Major != 6 || File.Exists(config.FileSystem.XPackEnvBinary)) return;

			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureSecurityUsersInDefaultRealmAreAdded)}}} {config.FileSystem.XPackEnvBinary} does not exist, patching now.");
			File.WriteAllText(config.FileSystem.XPackEnvBinary, "set ES_CLASSPATH=!ES_CLASSPATH!;!ES_HOME!/plugins/x-pack/*");
		}
	}
}
