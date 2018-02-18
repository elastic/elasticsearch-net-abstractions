using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Elastic.ManagedNode.Configuration
{
	public class NodeFileSystem
	{
		private readonly ElasticsearchVersion _version;
		private readonly string _clusterName;
		private readonly string _tempFolderName;
		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;

		private string BinarySuffix => IsMono || Path.PathSeparator == '/' ? "" : ".bat";
		public string Binary => Path.Combine(this.ElasticsearchHome, "bin", "elasticsearch") + BinarySuffix;
		public string PluginBinary =>
			Path.Combine(this.ElasticsearchHome, "bin", (this._version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + BinarySuffix;

		public string ElasticsearchHome { get; }
		public string ConfigPath => Path.Combine(ElasticsearchHome, "config");
		public string DataPath => Path.Combine(ElasticsearchHome, "data", this._clusterName);
		public string LogsPath => Path.Combine(ElasticsearchHome, "logs");
		public string RepositoryPath => Path.Combine(LocalFolder, "repositories");

		public string LocalFolder { get; }
		public string AnalysisFolder => Path.Combine(this.ConfigPath, "analysis");

		public string ZipDownloadTarget => Path.Combine(this.LocalFolder, this._version.Zip);
		public string TaskRunnerFile => Path.Combine(this.LocalFolder, "taskrunner.log");

		public NodeFileSystem(ElasticsearchVersion version, string clusterName, string tempFolderName = "ElasticsearchRunner")
		{
			this._version = version;
			this._clusterName = clusterName;
			this._tempFolderName = tempFolderName;
			var appData = GetApplicationDataDirectory();
			this.LocalFolder = Path.Combine(appData, tempFolderName, this._version.FullyQualifiedVersion);
			this.ElasticsearchHome = Path.Combine(this.LocalFolder, this._version.FolderInZip);
		}

		private static string GetApplicationDataDirectory() =>
			RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Environment.GetEnvironmentVariable("LocalAppData") : "/tmp";
	}
}
