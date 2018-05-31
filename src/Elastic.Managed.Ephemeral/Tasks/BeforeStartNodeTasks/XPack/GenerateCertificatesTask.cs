using System.IO;
using System.IO.Compression;
using System.Linq;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack
{
	public class AddClientCertificateRoleMappingTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.EnableSsl) return;

			var config = cluster.ClusterConfiguration;

			var file = Path.Combine(config.FileSystem.ConfigPath, "x-pack", "role_mapping") + ".yml";
			var name = config.FileSystem.ClientCertificateName;
			if (!File.Exists(file) || !File.ReadAllLines(file).Any(f => f.Contains(name)))
				File.WriteAllLines(file, new[]
				{
					"admin:",
					$"    - \"{name}\""
				});
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

		private static void GenerateCertGenConfigFiles(IEphemeralCluster<EphemeralClusterConfiguration> cluster, INodeFileSystem fileSystem, string silentModeConfigFile, EphemeralClusterConfiguration config)
		{
			var silentModeConfigFileDuplicate = Path.Combine(fileSystem.ConfigPath, "x-pack", "certgen") + ".yml";

			cluster.Writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} creating config files");

			foreach (var file in new[] {silentModeConfigFile, silentModeConfigFileDuplicate})
				if (!File.Exists(file))
					File.WriteAllLines(file, new[]
					{
						"instances:",
						$"    - name : \"{config.FileSystem.CertificateNodeName}\"",
						$"    - name : \"{config.FileSystem.ClientCertificateName}\"",
						$"      filename : \"{config.FileSystem.ClientCertificateFilename}\"",
					});
		}

		private static void GenerateCertificates(IEphemeralCluster<EphemeralClusterConfiguration> cluster, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var config = cluster.ClusterConfiguration;
			var name = config.FileSystem.CertificateFolderName;
			var path = config.FileSystem.CertificatesPath;
			NewOrCachedCertificates(cluster, name, path, silentModeConfigFile, writer);
		}

		private static void GenerateUnusedCertificates(IEphemeralCluster<EphemeralClusterConfiguration> cluster, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var config = cluster.ClusterConfiguration;
			var name = config.FileSystem.UnusedCertificateFolderName;
			var path = config.FileSystem.UnusedCertificatesPath;
			NewOrCachedCertificates(cluster, name, path, silentModeConfigFile, writer);
		}

		private static void NewOrCachedCertificates(IEphemeralCluster<EphemeralClusterConfiguration> cluster, string name, string path, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var config = cluster.ClusterConfiguration;
			var cachedEsHomeFolder = Path.Combine(config.FileSystem.LocalFolder, cluster.GetCacheFolderName());
			var zipLocationCache = Path.Combine(cachedEsHomeFolder, name) + ".zip";

			if (File.Exists(zipLocationCache))
			{
				writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} using cached certificates from {zipLocationCache}");
				UnpackCertificatesZip(zipLocationCache, path, writer);
				return;
			}

			var zipLocation = Path.Combine(config.FileSystem.ConfigPath, "x-pack", name) + ".zip";
			GenerateCertificate(config, name, path, zipLocation, silentModeConfigFile, writer);

			if (!File.Exists(zipLocationCache))
			{
				writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} caching {zipLocation} in ES_HOME {zipLocationCache}");
				File.Copy(zipLocation, zipLocationCache);
			}

			UnpackCertificatesZip(zipLocation, path, writer);
		}

		private static void GenerateCertificate(EphemeralClusterConfiguration config, string name, string path, string zipLocation, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var @out = config.Version.Major < 6 ? $"{name}.zip" : zipLocation;
			if (!Directory.Exists(path))
				ExecuteBinary(config, writer, config.FileSystem.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", @out);
			if (config.Version.Major < 6)
			{
				var badLocation = Path.Combine(config.FileSystem.ElasticsearchHome, "config", "x-pack", @out);
				writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} moving {badLocation} to {@out}");
				File.Move(badLocation, zipLocation);
			}
		}


		private static void UnpackCertificatesZip(string zipLocation, string outFolder, IConsoleLineWriter writer)
		{
			if (Directory.Exists(outFolder)) return;

			writer.WriteDiagnostic($"{{{nameof(GenerateCertificatesTask)}}} unzipping certificates to {outFolder}");
			Directory.CreateDirectory(outFolder);
			ZipFile.ExtractToDirectory(zipLocation, outFolder);
		}

	}
}
