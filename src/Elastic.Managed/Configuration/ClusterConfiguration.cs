using System;
using System.Globalization;
using System.IO;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Configuration
{
	public class ClusterConfiguration
	{
		public ClusterConfiguration(ElasticsearchVersion version, string esHome, int numberOfNodes = 1)
			: this(version, (v, s) => new NodeFileSystem(v, esHome), numberOfNodes , null)
		{

		}

		public ClusterConfiguration(ElasticsearchVersion version, Func<ElasticsearchVersion, string, INodeFileSystem> fileSystem = null, int numberOfNodes = 1, string clusterName = null)
		{
			this.ClusterName = clusterName;
			this.Version = version;
			fileSystem = fileSystem ?? ((v, s) => new NodeFileSystem(v, s));
			this.FileSystem = fileSystem(this.Version, this.ClusterName);
			this.NumberOfNodes = numberOfNodes;

			var fs = this.FileSystem;
			this.Add("node.max_local_storage_nodes", numberOfNodes.ToString(CultureInfo.InvariantCulture));
			this.Add("discovery.zen.minimum_master_nodes", Quorum(numberOfNodes).ToString(CultureInfo.InvariantCulture));

			this.Add("cluster.name", clusterName);
			this.Add("path.repo", fs.RepositoryPath);
			this.Add("path.data", fs.DataPath);
			var logsPathDefault = Path.Combine(fs.ElasticsearchHome, "logs");
			if (logsPathDefault != fs.LogsPath) this.Add("path.logs", fs.LogsPath);
		}

		public string ClusterName { get; }
		public ElasticsearchVersion Version { get; }
		public INodeFileSystem FileSystem { get; }
		public int NumberOfNodes { get; }
		public int StartingPortNumber { get; set; } = 9200;

		/// <summary>
		/// Wheter <see cref="ElasticsearchNode" /> should continue to write output to console after it has started.
		/// <para>Defaults to true but useful to turn of if it proofs to be too noisy </para>
		/// </summary>
		public bool ShowElasticsearchOutputAfterStarted { get; set; } = true;

		/// <summary> The global node settings that apply to each started node, can be added to.</summary>
		public NodeSettings DefaultNodeSettings { get; } = new NodeSettings();

		public virtual string CreateNodeName(int? node) => node.HasValue ? $"managed-elasticsearch-{node}" : " managed-elasticsearch";

		private static int Quorum(int instanceCount) => Math.Max(1, (int) Math.Floor((double) instanceCount / 2) + 1);

		public string AttributeKey(string attribute)
		{
			var attr = this.Version.Major >= 5 ? "attr." : "";
			return $"node.{attr}{attribute}";
		}
		protected void Add(string key, string value)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
			this.DefaultNodeSettings.Add(key,value);
		}

		protected void Add(string key, string value, string range)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
			if (string.IsNullOrWhiteSpace(range) || this.Version.InRange(range))
				this.DefaultNodeSettings.Add(key, value, range);
		}
	}
}
