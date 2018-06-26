using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks.XPack
{
	public class EnsureSecurityRealms : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSecurity) return;

			var configFile = Path.Combine(cluster.FileSystem.ConfigPath, "elasticsearch.yml");
			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureSecurityRealms)}}} attempting to add xpack realms to [{configFile}]");
			var lines = File.ReadAllLines(configFile).ToList();
			var saveFile = false;

			saveFile = cluster.ClusterConfiguration.Version.Major >= 6 ? Write6XAndUpRealms(lines) : Write5XAndUpRealms(lines);

			if (saveFile) File.WriteAllLines(configFile, lines);
			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureSecurityRealms)}}} {(saveFile ? "saved" : "skipped saving")} xpack realms to [{configFile}]");
		}

		private static bool Write6XAndUpRealms(List<string> lines)
		{
			if (lines.Any(line => line.Contains("file1"))) return false;
			lines.AddRange(new[]
			{
				string.Empty,
				"xpack:",
				"  security:",
				"    authc:",
				"      realms:",
				$"        {SecurityRealms.FileRealm}:",
				"          type: file",
				"          order: 0",
				$"        {SecurityRealms.PkiRealm}:",
				"          type: pki",
				"          order: 1",
				string.Empty
			});

			return true;
		}

		private static bool Write5XAndUpRealms(List<string> lines)
		{
			var saveFile = false;
			if (!lines.Any(line => line.Contains("file1")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack:",
					"  security:",
					"    authc:",
					"      realms:",
					$"        {SecurityRealms.FileRealm}:",
					"          type: file",
					"          order: 0",
					string.Empty
				});
				saveFile = true;
			}

			if (!lines.Any(line => line.Contains("pki1")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack:",
					"  security:",
					"    authc:",
					"      realms:",
					$"        {SecurityRealms.PkiRealm}:",
					"          type: pki",
					"          order: 1",
					string.Empty
				});
				saveFile = true;
			}

			return saveFile;
		}
	}
}
