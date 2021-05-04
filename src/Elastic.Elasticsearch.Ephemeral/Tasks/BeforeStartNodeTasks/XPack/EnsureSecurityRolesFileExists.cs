// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Linq;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.BeforeStartNodeTasks.XPack
{
	public class EnsureSecurityRolesFileExists : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSecurity) return;
			if (!cluster.ClusterConfiguration.EnableSecurity) return;


			//2.x tests only use prebaked roles
			var v = cluster.ClusterConfiguration.Version;
			if (v.Major < 5) return;
			var folder = v.Major >= 5 ? "x-pack" : "shield";
			var rolesConfig = v >= "6.3.0"
				? Path.Combine(cluster.FileSystem.ConfigPath, "roles.yml")
				: Path.Combine(cluster.FileSystem.ConfigPath, folder, "roles.yml");

			cluster.Writer.WriteDiagnostic(
				$"{{{nameof(EnsureSecurityRolesFileExists)}}} adding roles to {rolesConfig}");
			if (!File.Exists(rolesConfig))
			{
				cluster.Writer.WriteDiagnostic(
					$"{{{nameof(EnsureSecurityRolesFileExists)}}} {rolesConfig} does not exist");
				Directory.CreateDirectory(Path.Combine(cluster.FileSystem.ConfigPath, folder));
				File.WriteAllText(rolesConfig, string.Empty);
			}

			var lines = File.ReadAllLines(rolesConfig).ToList();
			var saveFile = false;

			if (!lines.Any(line => line.StartsWith("user:")))
			{
				lines.InsertRange(0,
					new[]
					{
						"# Read-only operations on indices", "user:", "  indices:", "    - names: '*'",
						"      privileges:", "        - read", string.Empty
					});

				saveFile = true;
			}

			if (!lines.Any(line => line.StartsWith("power_user:")))
			{
				lines.InsertRange(0,
					new[]
					{
						"# monitoring cluster privileges", "# All operations on all indices", "power_user:",
						"  cluster:", "    - monitor", "  indices:", "    - names: '*'", "      privileges:",
						"        - all", string.Empty
					});

				saveFile = true;
			}

			if (!lines.Any(line => line.StartsWith("admin:")))
			{
				lines.InsertRange(0,
					new[]
					{
						"# All cluster rights", "# All operations on all indices", "admin:", "  cluster:",
						"    - all", "  indices:", "    - names: '*'", "      privileges:", "        - all",
						string.Empty
					});

				saveFile = true;
			}

			if (saveFile) File.WriteAllLines(rolesConfig, lines);
			cluster.Writer.WriteDiagnostic(
				$"{{{nameof(EnsureSecurityRolesFileExists)}}} {(saveFile ? "saved" : "skipped saving")} roles to [{rolesConfig}]");
		}
	}
}
