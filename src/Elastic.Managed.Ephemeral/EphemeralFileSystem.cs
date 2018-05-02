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

		protected static string EphemeralHome(ElasticsearchVersion version, string clusterName)
		{
		 	var temp = Path.Combine(Path.GetTempPath(), SubFolder, version.FolderInZip, clusterName);
			return Path.Combine(temp, "home");
		}
	}

}
