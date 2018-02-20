using System.IO;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.FileSystem
{
	public class LocalAppDataFileSystem : NodeFileSystem
	{
		public LocalAppDataFileSystem(ElasticsearchVersion version, string clusterName = null) : base(version, clusterName, LocalAppDataHome(version)) { }

		public override string ConfigPath => Path.Combine(ElasticsearchHome, "config");
		public override string LogsPath => Path.Combine(ElasticsearchHome, "logs");
		public override string RepositoryPath => Path.Combine(LocalFolder, "repositories");
		public override string DataPath => Path.Combine(ElasticsearchHome, "data");
	}
}