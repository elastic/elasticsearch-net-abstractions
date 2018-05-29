using System;
using System.IO;
using System.Runtime.InteropServices;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.FileSystem
{
	/// <inheritdoc />
	public class NodeFileSystem : INodeFileSystem
	{
		protected const string SubFolder = "ElasticManaged";

		protected ElasticsearchVersion Version { get; }

		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;

		protected static string BinarySuffix => IsMono || Path.DirectorySeparatorChar == '/' ? "" : ".bat";

		/// <inheritdoc />
		public string Binary => Path.Combine(this.ElasticsearchHome, "bin", "elasticsearch") + BinarySuffix;

		/// <inheritdoc />
		public string PluginBinary => Path.Combine(this.ElasticsearchHome, "bin", (this.Version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + BinarySuffix;

		/// <inheritdoc />
		public string ElasticsearchHome { get; }
		/// <inheritdoc />
		public string LocalFolder { get; }
		/// <inheritdoc />
		public virtual string ConfigPath => null;
		/// <inheritdoc />
		public virtual string DataPath => null;
		/// <inheritdoc />
		public virtual string LogsPath => null;
		/// <inheritdoc />
		public virtual string RepositoryPath => null;

		public NodeFileSystem(ElasticsearchVersion version, string elasticsearchHome = null)
		{
			this.Version = version;
			this.LocalFolder = AppDataFolder(version);
			this.ElasticsearchHome = elasticsearchHome ?? GetEsHomeVariable() ?? throw new ArgumentNullException(nameof(elasticsearchHome));
		}

		protected static string AppDataFolder(ElasticsearchVersion version)
		{
			var appData = GetApplicationDataDirectory();
			return Path.Combine(appData, SubFolder, version.FullyQualifiedVersion);
		}

		protected static string GetEsHomeVariable() => Environment.GetEnvironmentVariable("ES_HOME");

		protected static string GetApplicationDataDirectory() =>
			RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Environment.GetEnvironmentVariable("LocalAppData") : "/tmp";
	}
}
