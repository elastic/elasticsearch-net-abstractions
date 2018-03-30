using System.Globalization;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Configuration
{
	public class NodeConfiguration
	{
		public NodeConfiguration(ElasticsearchVersion version, int? port = null) : this(new ClusterConfiguration(version), port) { }

		public NodeConfiguration(ClusterConfiguration clusterConfiguration, int? port = null)
		{
			this.ClusterConfiguration = clusterConfiguration;
			this.DesiredPort = port;
			this.DesiredNodeName = clusterConfiguration.CreateNodeName(port);
			this.Settings = new NodeSettings(clusterConfiguration.ClusterNodeSettings);

			if (!string.IsNullOrWhiteSpace(this.DesiredNodeName)) this.Settings.Add("node.name", this.DesiredNodeName);
			if (this.DesiredPort.HasValue) this.Settings.Add("http.port", this.DesiredPort.Value.ToString(CultureInfo.InvariantCulture));
		}

		public ClusterConfiguration ClusterConfiguration { get; }
		public int? DesiredPort { get; }
		public string DesiredNodeName { get; }
		private NodeSettings Settings { get; }

		public INodeFileSystem FileSystem => this.ClusterConfiguration.FileSystem;
		public ElasticsearchVersion Version => this.ClusterConfiguration.Version;
		public string[] CommandLineArguments => this.Settings.ToCommandLineArguments(this.Version);

		public string AttributeKey(string attribute)
		{
			var attr = this.Version.Major >= 5 ? "attr." : "";
			return $"node.{attr}.{attribute}";
		}

		public void Add(string key, string value) => this.Settings.Add(key,value);
	}
}
