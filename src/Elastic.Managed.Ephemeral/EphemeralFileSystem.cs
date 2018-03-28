using System;
using System.IO;
using Elastic.Managed.Configuration;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralFileSystem : NodeFileSystem
	{

		public EphemeralFileSystem(ElasticsearchVersion version) : base(version, LocalAppDataHome(version)) { }

		public string TempFolder => Path.Combine(Path.GetTempPath(), SubFolder, this.Version.FolderInZip, this.ClusterName);

		public override string ConfigPath => Path.Combine(TempFolder, "config");
		public override string LogsPath => Path.Combine(TempFolder, "logs");
		public override string RepositoryPath => Path.Combine(TempFolder, "repositories");
		public override string DataPath => Path.Combine(TempFolder, "data");

	}
}
