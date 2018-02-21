using System;
using System.IO;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.FileSystem
{
	public class EphemeralFileSystem : NodeFileSystem
	{
		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		public override string ClusterName => $"ephemeral-cluster-{_uniqueSuffix}";

		public EphemeralFileSystem(ElasticsearchVersion version) : base(version, null, LocalAppDataHome(version)) { }

		public override string ConfigPath => Path.Combine(ElasticsearchHome, "config");
		public override string LogsPath => Path.Combine(ElasticsearchHome, "logs");
		public override string RepositoryPath => Path.Combine(LocalFolder, "repositories");
		public override string DataPath => Path.Combine(ElasticsearchHome, "data");
	}
}
