using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Elastic.ManagedNode.Configuration
{
	public class NodeFileSystem : INodeFileSystem
	{
		public ElasticsearchVersion Version { get; }

		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;
		private static string BinarySuffix => IsMono || Path.PathSeparator == '/' ? "" : ".bat";

		public string Binary => Path.Combine(this.ElasticsearchHome, "bin", "elasticsearch") + BinarySuffix;
		public string PluginBinary => Path.Combine(this.ElasticsearchHome, "bin", (this.Version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + BinarySuffix;

		public string ElasticsearchHome { get; }
		public string LocalFolder { get; }

		public virtual string ConfigPath => null;
		public virtual string DataPath => null;
		public virtual string LogsPath => null;
		public virtual string RepositoryPath => null;

		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		private readonly string _clusterName;
		public string ClusterName => this._clusterName ?? $"managed-cluster-{_uniqueSuffix}";

		public NodeFileSystem(ElasticsearchVersion version) : this(version, null, null) { }
		public NodeFileSystem(ElasticsearchVersion version, string clusterName, string elasticsearchHome = null)
		{
			this.Version = version;
			this._clusterName = clusterName;
			this.LocalFolder = AppDataFolder(version);
			this.ElasticsearchHome = elasticsearchHome ?? GetEsHomeVariable() ?? throw new ArgumentNullException(nameof(elasticsearchHome));

		}
		protected static string AppDataFolder(ElasticsearchVersion version)
		{
			var appData = GetApplicationDataDirectory();
			return Path.Combine(appData, "ElasticsearchRunner", version.FullyQualifiedVersion);
		}

		protected static string LocalAppDataHome(ElasticsearchVersion version)
		{
			var localFolder = AppDataFolder(version);
			return Path.Combine(localFolder, version.FolderInZip);
		}

		private static string GetEsHomeVariable() => Environment.GetEnvironmentVariable("ES_HOME");

		private static string GetApplicationDataDirectory() =>
			RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Environment.GetEnvironmentVariable("LocalAppData") : "/tmp";
	}
}
