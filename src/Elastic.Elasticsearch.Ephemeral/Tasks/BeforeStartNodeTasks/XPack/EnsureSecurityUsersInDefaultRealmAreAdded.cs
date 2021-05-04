// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.BeforeStartNodeTasks.XPack
{
	public class EnsureSecurityUsersInDefaultRealmAreAdded : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSecurity) return;

			var config = cluster.ClusterConfiguration;
			var fileSystem = cluster.FileSystem;
			var v = config.Version;

			var xpackConfigFolder =
				v >= "6.3.0" ? fileSystem.ConfigPath : Path.Combine(fileSystem.ConfigPath, "x-pack");
			;
			var xpackConfigFolderCached = v >= "6.3.0"
				? Path.Combine(fileSystem.LocalFolder, cluster.GetCacheFolderName(), "config")
				: Path.Combine(fileSystem.LocalFolder, cluster.GetCacheFolderName(), "config", "x-pack");

			var usersFile = Path.Combine(xpackConfigFolder, "users");
			var usersFileCached = usersFile.Replace(xpackConfigFolder, xpackConfigFolderCached);
			var usersRolesFile = Path.Combine(xpackConfigFolder, "users_roles");
			var usersRolesFileCached = usersRolesFile.Replace(xpackConfigFolder, xpackConfigFolderCached);
			var userCachedFileInfo = new FileInfo(usersFileCached);

			if (userCachedFileInfo.Exists && userCachedFileInfo.Length > 0 &&
			    cluster.ClusterConfiguration.CacheEsHomeInstallation)
			{
				cluster.Writer?.WriteDiagnostic(
					$"{{{nameof(EnsureSecurityUsersInDefaultRealmAreAdded)}}} using cached users and users_roles files from {{{xpackConfigFolderCached}}}");
				if (!Directory.Exists(xpackConfigFolder)) Directory.CreateDirectory(xpackConfigFolder);
				if (!File.Exists(usersFile)) File.Copy(usersFileCached, usersFile);
				if (!File.Exists(usersRolesFile)) File.Copy(usersRolesFileCached, usersRolesFile);
			}
			else
			{
				var folder = v.Major >= 5 ? v >= "6.3.0" ? string.Empty : "x-pack" : "shield";
				var binary = v.Major >= 5 ? v >= "6.3.0" ? "elasticsearch-users" : "users" : "esusers";

				var h = fileSystem.ElasticsearchHome;
				var pluginFolder = v >= "6.3.0" ? Path.Combine(h, "bin") : Path.Combine(h, "bin", folder);
				var pluginBat = Path.Combine(pluginFolder, binary) + BinarySuffix;

				foreach (var cred in ClusterAuthentication.AllUsers)
					ExecuteBinary(cluster.ClusterConfiguration, cluster.Writer, pluginBat,
						$"adding user {cred.Username}", $"useradd {cred.Username} -p {cred.Password} -r {cred.Role}");

				if (!Directory.Exists(xpackConfigFolderCached)) Directory.CreateDirectory(xpackConfigFolderCached);

				if (!File.Exists(usersFileCached)) File.Copy(usersFile, usersFileCached);
				if (!File.Exists(usersRolesFileCached)) File.Copy(usersRolesFile, usersRolesFileCached);
			}
		}
	}
}
