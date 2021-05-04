// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.IO.Compression;
using System.Linq;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Elastic.Elasticsearch.Managed.FileSystem;
using ProcNet.Std;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.BeforeStartNodeTasks.XPack
{
	public class AddClientCertificateRoleMappingTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSsl) return;

			var config = cluster.ClusterConfiguration;
			var v = config.Version;

			var file = v >= "6.3.0"
				? Path.Combine(config.FileSystem.ConfigPath, "role_mapping") + ".yml"
				: Path.Combine(config.FileSystem.ConfigPath, "x-pack", "role_mapping") + ".yml";

			var name = config.FileSystem.ClientCertificateName;
			if (!File.Exists(file) || !File.ReadAllLines(file).Any(f => f.Contains(name)))
				File.WriteAllLines(file, new[] {"admin:", $"    - \"{name}\""});
		}
	}


	/// <inheritdoc />
	public class GenerateCertificatesTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSsl) return;

			var config = cluster.ClusterConfiguration;

			var fileSystem = cluster.FileSystem;
			//due to a bug in certgen this file needs to live in two places
			var silentModeConfigFile = Path.Combine(fileSystem.ElasticsearchHome, "certgen") + ".yml";
			GenerateCertGenConfigFiles(cluster, fileSystem, silentModeConfigFile, config);

			GenerateCertificates(cluster, silentModeConfigFile, cluster.Writer);
			GenerateUnusedCertificates(cluster, silentModeConfigFile, cluster.Writer);
		}

		private static void GenerateCertGenConfigFiles(IEphemeralCluster<EphemeralClusterConfiguration> cluster,
			INodeFileSystem fileSystem, string silentModeConfigFile, EphemeralClusterConfiguration config)
		{
			var silentModeConfigFileDuplicate = Path.Combine(fileSystem.ConfigPath, "x-pack", "certgen") + ".yml";
			var files = cluster.ClusterConfiguration.Version >= "6.3.0"
				? new[] {silentModeConfigFile}
				: new[] {silentModeConfigFile, silentModeConfigFileDuplicate};

			cluster.Writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} creating config files");

			foreach (var file in files)
				if (!File.Exists(file))
					File.WriteAllLines(file,
						new[]
						{
							"instances:", $"    - name : \"{config.FileSystem.CertificateNodeName}\"", $"      ip:",
							$"          - \"127.0.0.1\"", $"      dns:", $"          - \"127.0.0.1\"",
							$"          - \"localhost\"", $"          - \"ipv4.fiddler\"",
							$"    - name : \"{config.FileSystem.ClientCertificateName}\"", $"      ip:",
							$"          - \"127.0.0.1\"", $"      dns:", $"          - \"localhost\"",
							$"          - \"ipv4.fiddler\"",
							$"      filename : \"{config.FileSystem.ClientCertificateFilename}\"",
						});
		}

		private static void GenerateCertificates(IEphemeralCluster<EphemeralClusterConfiguration> cluster,
			string silentModeConfigFile, IConsoleLineHandler writer)
		{
			var config = cluster.ClusterConfiguration;
			var name = config.FileSystem.CertificateFolderName;
			var path = config.FileSystem.CertificatesPath;
			NewOrCachedCertificates(cluster, name, path, silentModeConfigFile, writer);
		}

		private static void GenerateUnusedCertificates(IEphemeralCluster<EphemeralClusterConfiguration> cluster,
			string silentModeConfigFile, IConsoleLineHandler writer)
		{
			var config = cluster.ClusterConfiguration;
			var name = config.FileSystem.UnusedCertificateFolderName;
			var path = config.FileSystem.UnusedCertificatesPath;
			NewOrCachedCertificates(cluster, name, path, silentModeConfigFile, writer);
		}

		private static void NewOrCachedCertificates(IEphemeralCluster<EphemeralClusterConfiguration> cluster,
			string name, string path, string silentModeConfigFile, IConsoleLineHandler writer)
		{
			var config = cluster.ClusterConfiguration;
			var cachedEsHomeFolder = Path.Combine(config.FileSystem.LocalFolder, cluster.GetCacheFolderName());
			var zipLocationCache = Path.Combine(cachedEsHomeFolder, name) + ".zip";

			if (File.Exists(zipLocationCache))
			{
				writer.WriteDiagnostic(
					$"{{{nameof(GenerateCertificatesTask)}}} using cached certificates from {zipLocationCache}");
				UnpackCertificatesZip(zipLocationCache, path, writer);
				return;
			}

			var zipLocation = config.Version >= "6.3.0"
				? Path.Combine(config.FileSystem.ConfigPath, name) + ".zip"
				: Path.Combine(config.FileSystem.ConfigPath, "x-pack", name) + ".zip";
			GenerateCertificate(config, name, path, zipLocation, silentModeConfigFile, writer);

			if (!File.Exists(zipLocationCache))
			{
				writer.WriteDiagnostic(
					$"{{{nameof(GenerateCertificatesTask)}}} caching {zipLocation} in ES_HOME {zipLocationCache}");
				File.Copy(zipLocation, zipLocationCache);
			}

			UnpackCertificatesZip(zipLocation, path, writer);
		}

		private static void GenerateCertificate(EphemeralClusterConfiguration config, string name, string path,
			string zipLocation, string silentModeConfigFile, IConsoleLineHandler writer)
		{
			var @out = config.Version.Major < 6 ? $"{name}.zip" : zipLocation;
			var fs = config.FileSystem;
			var binary = config.Version >= "6.3.0"
				? config.Version < "8.0.0"
					? Path.Combine(fs.ElasticsearchHome, "bin", "elasticsearch-certgen") + BinarySuffix
					: Path.Combine(fs.ElasticsearchHome, "bin", "elasticsearch-certutil") + BinarySuffix
				: Path.Combine(fs.ElasticsearchHome, "bin", "x-pack", "certgen") + BinarySuffix;


			if (!Directory.Exists(path))
			{
				if (config.Version < "7.0.0")
					ExecuteBinary(config, writer, binary, "generating ssl certificates for this session",
						"-in", silentModeConfigFile, "-out", @out);
				else
					ExecuteBinary(config, writer, binary, "generating ssl certificates for this session",
						"cert",
						"-in", silentModeConfigFile, "-out", @out);
			}

			var badLocation = Path.Combine(config.FileSystem.ElasticsearchHome, "config", "x-pack", @out);
			//not necessary anymore now that we patch .in.bat i think
			if (config.Version.Major < 6 && File.Exists(badLocation))
			{
				writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} moving {badLocation} to {@out}");
				File.Move(badLocation, zipLocation);
			}
		}


		private static void UnpackCertificatesZip(string zipLocation, string outFolder, IConsoleLineHandler writer)
		{
			if (Directory.Exists(outFolder)) return;

			writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} unzipping certificates to {outFolder}");
			Directory.CreateDirectory(outFolder);
			ZipFile.ExtractToDirectory(zipLocation, outFolder);
		}
	}
}
