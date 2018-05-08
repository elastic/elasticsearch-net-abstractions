using System.IO;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack
{
	public class EnsureSecurityUsersInDefaultRealmAreAdded : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSecurity) return;

			var config = cluster.ClusterConfiguration;
			var v = config.Version;
			var fileSystem = cluster.FileSystem;
			var folder = v.Major >= 5 ? "x-pack" : "shield";
			var plugin = v.Major >= 5 ? "users" : "esusers";

			var pluginBat = Path.Combine(fileSystem.ElasticsearchHome, "bin", folder, plugin) + BinarySuffix;
			foreach (var cred in ClusterAuthentication.AllUsers)
				ExecuteBinary(cluster.ClusterConfiguration, cluster.Writer, pluginBat, $"adding user {cred.Username}",$"useradd {cred.Username} -p {cred.Password} -r {cred.Role}");
		}
	}
}
