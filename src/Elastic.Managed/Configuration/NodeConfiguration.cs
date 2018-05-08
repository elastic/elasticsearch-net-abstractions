using System.Globalization;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Configuration
{
	public class NodeConfiguration
	{
		public NodeConfiguration(ElasticsearchVersion version, int? port = null) : this(new ClusterConfiguration(version), port) { }

		public NodeConfiguration(IClusterConfiguration<NodeFileSystem> clusterConfiguration, int? port = null)
		{
			this.ClusterConfiguration = clusterConfiguration;
			this.DesiredPort = port;
			this.DesiredNodeName = clusterConfiguration.CreateNodeName(port);
			this.Settings = new NodeSettings(clusterConfiguration.DefaultNodeSettings);

			if (!string.IsNullOrWhiteSpace(this.DesiredNodeName)) this.Settings.Add("node.name", this.DesiredNodeName);
			if (this.DesiredPort.HasValue) this.Settings.Add("http.port", this.DesiredPort.Value.ToString(CultureInfo.InvariantCulture));
		}

		private IClusterConfiguration<NodeFileSystem> ClusterConfiguration { get; }

		public int? DesiredPort { get; }
		public string DesiredNodeName { get; }

		/// <summary>
		/// Wheter <see cref="ElasticsearchNode" /> should continue to write output to console after it has started.
		/// <para>Defaults to true but useful to turn of if it proofs to be too noisy </para>
		/// </summary>
		public bool ShowElasticsearchOutputAfterStarted { get; set; } = true;

		/// <summary>
		/// Copy of <see cref="IClusterConfiguration{TFileSystem}.DefaultNodeSettings" />. Add individual node settings here.
		/// </summary>
		public NodeSettings Settings { get; }

		public INodeFileSystem FileSystem => this.ClusterConfiguration.FileSystem;
		public ElasticsearchVersion Version => this.ClusterConfiguration.Version;
		public string[] CommandLineArguments => this.Settings.ToCommandLineArguments(this.Version);

		public string AttributeKey(string attribute)
		{
			var attr = this.Version.Major >= 5 ? "attr." : "";
			return $"node.{attr}{attribute}";
		}

		public void Add(string key, string value) => this.Settings.Add(key,value);
	}
}
