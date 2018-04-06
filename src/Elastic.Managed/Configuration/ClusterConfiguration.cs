using System;
using System.Globalization;
using System.IO;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Configuration
{
	public class ClusterConfiguration
	{
		public ClusterConfiguration(ElasticsearchVersion version, int numberOfNodes = 1, string clusterName = null, Func<ElasticsearchVersion, string, INodeFileSystem> fileSystem = null)
		{
			this.ClusterName = clusterName;
			this.Version = version;
			fileSystem = fileSystem ?? ((v, s) => new NodeFileSystem(v));
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
		public NodeSettings ClusterNodeSettings { get; } = new NodeSettings();

		public virtual string CreateNodeName(int? node) => null;

		static int Quorum(int instanceCount) => Math.Max(1, (int) Math.Floor((double) instanceCount / 2) + 1);

		public void Add(string key, string value)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
			this.ClusterNodeSettings.Add(key,value);
		}
		public void Add(string key, string value, string range)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
			if (string.IsNullOrWhiteSpace(range) || this.Version.InRange(range))
				this.ClusterNodeSettings.Add(key, value, range);
		}
	}
}
