using System.IO;
using System.IO.Compression;
using System.Linq;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack
{
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

			this.GenerateCertificates(config, silentModeConfigFile, cluster.Writer);
			this.GenerateUnusedCertificates(config, silentModeConfigFile, cluster.Writer);
			this.AddClientCertificateUser(config);
		}

		private void AddClientCertificateUser(EphemeralClusterConfiguration config)
		{
			var file = Path.Combine(config.FileSystem.ConfigPath, "x-pack", "role_mapping") + ".yml";
			var name = config.FileSystem.ClientCertificateName;
			if (!File.Exists(file) || !File.ReadAllLines(file).Any(f => f.Contains(name)))
				File.WriteAllLines(file, new[]
				{
					"admin:",
					$"    - \"{name}\""
				});
		}

		private void GenerateCertificates(EphemeralClusterConfiguration config, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var name = config.FileSystem.CertificateFolderName;
			var zipLocation = Path.Combine(config.FileSystem.ConfigPath, "x-pack", name) + ".zip";
			var @out = config.Version.Major < 6 ? $"{name}.zip" : zipLocation;
			if (!File.Exists(config.FileSystem.CaCertificate))
				ExecuteBinary(config, writer, config.FileSystem.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", @out);

			if (Directory.Exists(config.FileSystem.CertificatesPath)) return;
			Directory.CreateDirectory(config.FileSystem.CertificatesPath);
			ZipFile.ExtractToDirectory(zipLocation, config.FileSystem.CertificatesPath);
		}

		private void GenerateUnusedCertificates(EphemeralClusterConfiguration config, string silentModeConfigFile, IConsoleLineWriter writer)
		{
			var name = config.FileSystem.UnusedCertificateFolderName;
			var zipLocation = Path.Combine(config.FileSystem.ConfigPath, "x-pack", name) + ".zip";
			var @out = config.Version.Major < 6 ? $"{name}.zip" : zipLocation;
			if (!File.Exists(config.FileSystem.UnusedCaCertificate))
				ExecuteBinary(config, writer, config.FileSystem.CertGenBinary, "generating ssl certificates for this session",
					"-in", silentModeConfigFile, "-out", @out);

			if (Directory.Exists(config.FileSystem.UnusedCertificatesPath)) return;
			Directory.CreateDirectory(config.FileSystem.UnusedCertificatesPath);
			ZipFile.ExtractToDirectory(zipLocation, config.FileSystem.UnusedCertificatesPath);
		}

	}
}
