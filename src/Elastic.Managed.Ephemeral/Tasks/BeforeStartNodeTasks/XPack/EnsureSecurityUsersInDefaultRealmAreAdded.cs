using System.IO;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack
{
	public class EnsureSecurityUsersInDefaultRealmAreAdded : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSecurity) return;

			var config = cluster.ClusterConfiguration;
			var fileSystem = cluster.FileSystem;

			var xpackConfigFolder = Path.Combine(fileSystem.ConfigPath, "x-pack");
			var xpackConfigFolderCached = Path.Combine(fileSystem.LocalFolder, cluster.GetCacheFolderName(), "config", "x-pack");

			var usersFile = Path.Combine(xpackConfigFolder, "users");
			var usersFileCached = usersFile.Replace(xpackConfigFolder, xpackConfigFolderCached);
			var usersRolesFile = Path.Combine(xpackConfigFolder, "users_roles");
			var usersRolesFileCached = usersRolesFile.Replace(xpackConfigFolder, xpackConfigFolderCached);

			if (File.Exists(usersFileCached))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureSecurityUsersInDefaultRealmAreAdded)}}} using cached users and users_roles files from {{{xpackConfigFolderCached}}}");
				if (!Directory.Exists(xpackConfigFolder)) Directory.CreateDirectory(xpackConfigFolder);
				if (!File.Exists(usersFile)) File.Copy(usersFileCached, usersFile);
				if (!File.Exists(usersRolesFile)) File.Copy(usersRolesFileCached, usersRolesFile);

			}
			else
			{
				var v = config.Version;
				var folder = v.Major >= 5 ? "x-pack" : "shield";
				var plugin = v.Major >= 5 ? "users" : "esusers";

				var pluginBat = Path.Combine(fileSystem.ElasticsearchHome, "bin", folder, plugin) + BinarySuffix;
				foreach (var cred in ClusterAuthentication.AllUsers)
					ExecuteBinary(cluster.ClusterConfiguration, cluster.Writer, pluginBat, $"adding user {cred.Username}", $"useradd {cred.Username} -p {cred.Password} -r {cred.Role}");

				if (!Directory.Exists(xpackConfigFolderCached)) Directory.CreateDirectory(xpackConfigFolderCached);

				File.Copy(usersFile, usersFileCached);
				File.Copy(usersRolesFile, usersRolesFileCached);
			}
		}
	}
}
