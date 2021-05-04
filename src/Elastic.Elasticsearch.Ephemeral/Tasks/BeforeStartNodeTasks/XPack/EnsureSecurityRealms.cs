// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.BeforeStartNodeTasks.XPack
{
	public class EnsureSecurityRealms : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSecurity) return;

			var configFile = Path.Combine(cluster.FileSystem.ConfigPath, "elasticsearch.yml");
			cluster.Writer.WriteDiagnostic(
				$"{{{nameof(EnsureSecurityRealms)}}} attempting to add xpack realms to [{configFile}]");
			var lines = File.ReadAllLines(configFile).ToList();
			var saveFile = false;

			var major = cluster.ClusterConfiguration.Version.Major;
			saveFile = major >= 6
				? major >= 7 ? Write7XAndUpRealms(lines, cluster.ClusterConfiguration.EnableSsl) :
				Write6XAndUpRealms(lines)
				: Write5XAndUpRealms(lines);

			if (saveFile) File.WriteAllLines(configFile, lines);
			cluster.Writer.WriteDiagnostic(
				$"{{{nameof(EnsureSecurityRealms)}}} {(saveFile ? "saved" : "skipped saving")} xpack realms to [{configFile}]");
		}

		private static bool Write7XAndUpRealms(List<string> lines, bool sslEnabled)
		{
			if (lines.Any(line => line.Contains("file1"))) return false;
			lines.AddRange(new[]
			{
				string.Empty, "xpack:", "  security:", "    authc:", "      realms:", "        file:",
				$"         {SecurityRealms.FileRealm}:", "            order: 0", string.Empty
			});
			if (sslEnabled)
				lines.AddRange(new[]
				{
					"        pki:", $"         {SecurityRealms.PkiRealm}:", "            order: 1", string.Empty
				});
			return true;
		}

		private static bool Write6XAndUpRealms(List<string> lines)
		{
			if (lines.Any(line => line.Contains("file1"))) return false;
			lines.AddRange(new[]
			{
				string.Empty, "xpack:", "  security:", "    authc:", "      realms:",
				$"        {SecurityRealms.FileRealm}:", "          type: file", "          order: 0",
				$"        {SecurityRealms.PkiRealm}:", "          type: pki", "          order: 1", string.Empty
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
					string.Empty, "xpack:", "  security:", "    authc:", "      realms:",
					$"        {SecurityRealms.FileRealm}:", "          type: file", "          order: 0",
					string.Empty
				});
				saveFile = true;
			}

			if (!lines.Any(line => line.Contains("pki1")))
			{
				lines.AddRange(new[]
				{
					string.Empty, "xpack:", "  security:", "    authc:", "      realms:",
					$"        {SecurityRealms.PkiRealm}:", "          type: pki", "          order: 1", string.Empty
				});
				saveFile = true;
			}

			return saveFile;
		}
	}
}
