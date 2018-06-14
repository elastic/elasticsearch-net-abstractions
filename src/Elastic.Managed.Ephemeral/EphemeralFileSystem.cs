using System;
using System.IO;
using Elastic.Managed.Configuration;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralFileSystem : NodeFileSystem
	{
		public EphemeralFileSystem(ElasticsearchVersion version, string clusterName) : base(version, EphemeralHome(version, clusterName))
		{
			this.ClusterName = clusterName;
		}

		private string ClusterName { get; }

		public string TempFolder => Path.Combine(Path.GetTempPath(), SubFolder, this.Version.FolderInZip, this.ClusterName);

		public override string ConfigPath => Path.Combine(TempFolder, "config");
		public override string LogsPath => Path.Combine(TempFolder, "logs");
		public override string RepositoryPath => Path.Combine(TempFolder, "repositories");
		public override string DataPath => Path.Combine(TempFolder, "data");

		//certificates
		public string CertificateFolderName => "node-certificates";
		public string CertificateNodeName => "node01";
		public string ClientCertificateName => "cn=John Doe,ou=example,o=com";
		public string ClientCertificateFilename => "john_doe";

		public string CertificatesPath => Path.Combine(this.ConfigPath, this.CertificateFolderName);

		public string CaCertificate => Path.Combine(this.CertificatesPath, "ca", "ca") + ".crt";
		public string NodePrivateKey => Path.Combine(this.CertificatesPath, this.CertificateNodeName, this.CertificateNodeName) + ".key";
		public string NodeCertificate => Path.Combine(this.CertificatesPath, this.CertificateNodeName, this.CertificateNodeName) + ".crt";
		public string ClientCertificate => Path.Combine(this.CertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".crt";
		public string ClientPrivateKey => Path.Combine(this.CertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".key";

		public string UnusedCertificateFolderName => $"unused-{CertificateFolderName}";
		public string UnusedCertificatesPath => Path.Combine(this.ConfigPath, this.UnusedCertificateFolderName);
		public string UnusedCaCertificate => Path.Combine(this.UnusedCertificatesPath, "ca", "ca") + ".crt";
		public string UnusedClientCertificate => Path.Combine(this.UnusedCertificatesPath, this.ClientCertificateFilename, this.ClientCertificateFilename) + ".crt";


		protected static string EphemeralHome(ElasticsearchVersion version, string clusterName)
		{
		 	var temp = Path.Combine(Path.GetTempPath(), SubFolder, version.FolderInZip, clusterName);
			return Path.Combine(temp, "home");
		}
	}

}
