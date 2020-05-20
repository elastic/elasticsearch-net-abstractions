// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Runtime.InteropServices;
using Elastic.Stack.ArtifactsApi;
using Elastic.Stack.ArtifactsApi.Products;

namespace Elastic.Elasticsearch.Managed.FileSystem
{
	/// <inheritdoc />
	public class NodeFileSystem : INodeFileSystem
	{
		protected const string SubFolder = "ElasticManaged";

		protected ElasticVersion Version { get; }
		protected Artifact Artifact { get; }

		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;

		protected static string BinarySuffix => IsMono || Path.DirectorySeparatorChar == '/' ? "" : ".bat";

		/// <inheritdoc />
		public string Binary => Path.Combine(ElasticsearchHome, "bin", "elasticsearch") + BinarySuffix;

		/// <inheritdoc />
		public string PluginBinary => Path.Combine(ElasticsearchHome, "bin", (Version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + BinarySuffix;

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

		public string ConfigEnvironmentVariableName { get; }

		public NodeFileSystem(ElasticVersion version, string elasticsearchHome = null)
		{
			Version = version;
			Artifact = version.Artifact(Product.Elasticsearch);
			LocalFolder = AppDataFolder(version);
			ElasticsearchHome = elasticsearchHome ?? GetEsHomeVariable() ?? throw new ArgumentNullException(nameof(elasticsearchHome));

			ConfigEnvironmentVariableName = version.Major >= 6 ? "ES_PATH_CONF" : "CONF_DIR";
		}

		protected static string AppDataFolder(ElasticVersion version)
		{
			var appData = GetApplicationDataDirectory();
			return Path.Combine(appData, SubFolder, version.Artifact(Product.Elasticsearch).LocalFolderName);
		}

		protected static string GetEsHomeVariable() => Environment.GetEnvironmentVariable("ES_HOME");

		protected static string GetApplicationDataDirectory() =>
			RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
				? Environment.GetEnvironmentVariable("LocalAppData")
				: Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
	}
}
