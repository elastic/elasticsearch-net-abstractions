// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Managed.FileSystem;
using Elastic.Stack.ArtifactsApi;
using Elastic.Stack.ArtifactsApi.Products;

namespace Elastic.Elasticsearch.Ephemeral
{
	public class EphemeralFileSystem : NodeFileSystem
	{
		public EphemeralFileSystem(ElasticVersion version, string clusterName) : base(version,
			EphemeralHome(version, clusterName)) => ClusterName = clusterName;

		private string ClusterName { get; }

		public string TempFolder => Path.Combine(Path.GetTempPath(), SubFolder, Artifact.LocalFolderName, ClusterName);

		public override string ConfigPath => Path.Combine(TempFolder, "config");
		public override string LogsPath => Path.Combine(TempFolder, "logs");
		public override string RepositoryPath => Path.Combine(TempFolder, "repositories");
		public override string DataPath => Path.Combine(TempFolder, "data");

		//certificates
		public string CertificateFolderName => "node-certificates";
		public string CertificateNodeName => "node01";
		public string ClientCertificateName => "cn=John Doe,ou=example,o=com";
		public string ClientCertificateFilename => "john_doe";

		public string CertificatesPath => Path.Combine(ConfigPath, CertificateFolderName);

		public string CaCertificate => Path.Combine(CertificatesPath, "ca", "ca") + ".crt";
		public string CaPrivateKey => Path.Combine(CertificatesPath, "ca", "ca") + ".key";

		public string NodePrivateKey =>
			Path.Combine(CertificatesPath, CertificateNodeName, CertificateNodeName) + ".key";

		public string NodeCertificate =>
			Path.Combine(CertificatesPath, CertificateNodeName, CertificateNodeName) + ".crt";

		public string ClientCertificate =>
			Path.Combine(CertificatesPath, ClientCertificateFilename, ClientCertificateFilename) + ".crt";

		public string ClientPrivateKey =>
			Path.Combine(CertificatesPath, ClientCertificateFilename, ClientCertificateFilename) + ".key";

		public string UnusedCertificateFolderName => $"unused-{CertificateFolderName}";
		public string UnusedCertificatesPath => Path.Combine(ConfigPath, UnusedCertificateFolderName);
		public string UnusedCaCertificate => Path.Combine(UnusedCertificatesPath, "ca", "ca") + ".crt";

		public string UnusedClientCertificate =>
			Path.Combine(UnusedCertificatesPath, ClientCertificateFilename, ClientCertificateFilename) + ".crt";


		protected static string EphemeralHome(ElasticVersion version, string clusterName)
		{
			var temp = Path.Combine(Path.GetTempPath(), SubFolder,
				version.Artifact(Product.Elasticsearch).LocalFolderName, clusterName);
			return Path.Combine(temp, "home");
		}
	}
}
