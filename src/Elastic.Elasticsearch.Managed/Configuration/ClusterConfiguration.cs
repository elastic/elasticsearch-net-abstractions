// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.IO;
using Elastic.Elasticsearch.Managed.FileSystem;
using Elastic.Stack.ArtifactsApi;
using Elastic.Stack.ArtifactsApi.Products;

namespace Elastic.Elasticsearch.Managed.Configuration
{
	public interface IClusterConfiguration<out TFileSystem> where TFileSystem : INodeFileSystem
	{
		TFileSystem FileSystem { get; }

		string ClusterName { get; }
		NodeSettings DefaultNodeSettings { get; }
		ElasticVersion Version { get; }
		int NumberOfNodes { get; }
		int StartingPortNumber { get; set; }
		bool NoCleanupAfterNodeStopped { get; set; }

		bool ShowElasticsearchOutputAfterStarted { get; set; }
		bool CacheEsHomeInstallation { get; set; }

		string CreateNodeName(int? node);
	}

	public class ClusterConfiguration : ClusterConfiguration<NodeFileSystem>
	{
		public ClusterConfiguration(ElasticVersion version, string esHome, int numberOfNodes = 1)
			: base(version, (v, s) => new NodeFileSystem(v, esHome), numberOfNodes , null) { }

		public ClusterConfiguration(ElasticVersion version, Func<ElasticVersion, string, NodeFileSystem> fileSystem = null, int numberOfNodes = 1, string clusterName = null)
			: base(version, fileSystem ?? ((v, s) => new NodeFileSystem(v, s)), numberOfNodes , clusterName) { }
	}

	public class ClusterConfiguration<TFileSystem> : IClusterConfiguration<TFileSystem>
		where TFileSystem : INodeFileSystem
	{
		/// <summary>
		/// Creates a new instance of a configuration for an Elasticsearch cluster.
		/// </summary>
		/// <param name="version">The version of Elasticsearch</param>
		/// <param name="fileSystem">A delegate to create the instance of <typeparamref name="TFileSystem"/>.
		/// Passed the Elasticsearch version and the Cluster name</param>
		/// <param name="numberOfNodes">The number of nodes in the cluster</param>
		/// <param name="clusterName">The name of the cluster</param>
		public ClusterConfiguration(ElasticVersion version, Func<ElasticVersion, string, TFileSystem> fileSystem, int numberOfNodes = 1, string clusterName = null)
		{
			if (fileSystem == null) throw new ArgumentException(nameof(fileSystem));

			ClusterName = clusterName;
			Version = version;
			Artifact = version.Artifact(Product.Elasticsearch);
			FileSystem = fileSystem(Version, ClusterName);
			NumberOfNodes = numberOfNodes;

			var fs = FileSystem;
			Add("node.max_local_storage_nodes", numberOfNodes.ToString(CultureInfo.InvariantCulture), "<8.0.0");

			if (version < "7.0.0-beta1")
				Add("discovery.zen.minimum_master_nodes", Quorum(numberOfNodes).ToString(CultureInfo.InvariantCulture));

			Add("cluster.name", clusterName);
			Add("path.repo", fs.RepositoryPath);
			Add("path.data", fs.DataPath);
			var logsPathDefault = Path.Combine(fs.ElasticsearchHome, "logs");
			if (logsPathDefault != fs.LogsPath) Add("path.logs", fs.LogsPath);

			if (version.Major < 6) Add("path.conf", fs.ConfigPath);

		}

		public string ClusterName { get; }
		public ElasticVersion Version { get; }
		public Artifact Artifact { get; }
		public TFileSystem FileSystem { get; }
		public int NumberOfNodes { get; }
		public int StartingPortNumber { get; set; } = 9200;
		public bool NoCleanupAfterNodeStopped { get; set; }

		public string JavaHomeEnvironmentVariable => Version.InRange("<7.12.0") ? "JAVA_HOME" : "ES_JAVA_HOME";

		/// <summary> Will print the contents of all the yaml files when starting the cluster up, great for debugging purposes</summary>
		public bool PrintYamlFilesInConfigFolder { get; set; }

		/// <summary>
		/// Whether <see cref="ElasticsearchNode" /> should continue to write output to console after it has started.
		/// <para>Defaults to <c>true</c></para>
		/// </summary>
		public bool ShowElasticsearchOutputAfterStarted { get; set; } = true;

		public bool CacheEsHomeInstallation { get; set; }

		/// <summary>The node settings to apply to each started node</summary>
		public NodeSettings DefaultNodeSettings { get; } = new NodeSettings();

		/// <summary>
		/// Creates a node name
		/// </summary>
		public virtual string CreateNodeName(int? node) => node.HasValue ? $"managed-elasticsearch-{node}" : " managed-elasticsearch";

		/// <summary>
		/// Calculates the quorum given the number of instances
		/// </summary>
		private static int Quorum(int instanceCount) => Math.Max(1, (int) Math.Floor((double) instanceCount / 2) + 1);

		/// <summary>
		/// Creates a node attribute for the version of Elasticsearch
		/// </summary>
		public string AttributeKey(string attribute)
		{
			var attr = Version.Major >= 5 ? "attr." : "";
			return $"node.{attr}{attribute}";
		}

		/// <summary>
		/// Adds a node setting to the default node settings
		/// </summary>
		protected void Add(string key, string value)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
			DefaultNodeSettings.Add(key,value);
		}

		/// <summary>
		/// Adds a node setting to the default node settings only if the Elasticsearch
		/// version is in the range.
		/// </summary>
		protected void Add(string key, string value, string range)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
			if (string.IsNullOrWhiteSpace(range) || Version.InRange(range))
				DefaultNodeSettings.Add(key, value, range);
		}
	}
}
