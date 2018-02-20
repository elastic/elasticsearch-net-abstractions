using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Configuration
{
	public class NodeConfiguration
	{
		public INodeFileSystem FileSystem { get; }
		public NodeFeatures Features { get; }
		public int DesiredPort { get; }

		private List<string> Settings { get; }

		private readonly string _nodeName;
		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		public string NodeName => this._nodeName ?? $"managed-node-{_uniqueSuffix}";

		public ElasticsearchVersion Version => this.FileSystem.Version;
		public string[] CommandLineArguments => this.CreateCommandLineArgs(this.Settings);

		public bool XpackEnabled => this.Features.HasFlag(NodeFeatures.XPack);
		private bool EnableSsl => this.Features.HasFlag(NodeFeatures.SSL);
		private bool EnableSecurity => this.Features.HasFlag(NodeFeatures.Security);

		public NodeConfiguration(ElasticsearchVersion version, NodeFeatures features = NodeFeatures.None, int port = 9200, string nodeName = null, string clusterName = null)
			: this(new NodeFileSystem(version, clusterName), features, port, nodeName) { }

		public NodeConfiguration(INodeFileSystem fileSystem, NodeFeatures features, int port = 9200, string nodeName = null)
		{
			this._nodeName = nodeName;
			this.FileSystem = fileSystem;
			this.Features = features;
			this.DesiredPort = port;

			var v = this.Version;
			this.Settings = new List<string>
			{
				$"cluster.name={this.FileSystem.ClusterName}",
				$"node.name={this.NodeName}",
				$"http.port={this.DesiredPort}",
			};
			if (!string.IsNullOrWhiteSpace(this.FileSystem.RepositoryPath)) this.Settings.Add($"path.repo={this.FileSystem.RepositoryPath}");
			if (!string.IsNullOrWhiteSpace(this.FileSystem.DataPath)) this.Settings.Add($"path.data={this.FileSystem.DataPath}");
			AddXpackSettings(v, this.Settings);
		}

		public string AttributeKey(string attribute)
		{
			var attr = this.Version.Major >= 5 ? "attr." : "";
			return $"node.{attr}.{attribute}";
		}

		public void Add(string key, string value) => this.Settings.Add($"{key}={value}");

		private void AddXpackSettings(ElasticsearchVersion v, List<string> settings)
		{
			if (!EnableSecurity) return;
			var b = this.EnableSecurity.ToString().ToLowerInvariant();
			var shieldOrSecurity = v > LastVersionThatAcceptedShieldSettings ? "xpack.security" : "shield";
			var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
			settings.AddRange(new List<string>
			{
				$"{shieldOrSecurity}.enabled={b}",
				$"{shieldOrSecurity}.http.ssl.enabled={sslEnabled}",
				$"{shieldOrSecurity}.authc.realms.pki1.enabled={sslEnabled}",
			});
		}

		//TODO move these to XUnit project once we start on that
		//if (v >= VersionThatIntroducedCrossClusterSearch) settings.Add($"search.remote.connect=true");
		//private static readonly ElasticsearchVersion VersionThatIntroducedCrossClusterSearch = ElasticsearchVersionResolver.From("5.4.0");
		//var indexedOrStored = this.Version > LastVersionThatTookIndexedScripts ? "stored" : "indexed";
		//private static readonly ElasticsearchVersion LastVersionThatTookIndexedScripts = ElasticsearchVersion.From("5.0.0-alpha1");
		private static readonly ElasticsearchVersion LastVersionThatAcceptedShieldSettings = ElasticsearchVersion.From("5.0.0-alpha1");
		private static readonly ElasticsearchVersion LastVersionWithoutPrefixForSettings = ElasticsearchVersion.From("5.0.0-alpha2");

		private string[] CreateCommandLineArgs(IEnumerable<string> elasticsearchSettings)
		{
			var settingsPrefix = this.Version > LastVersionWithoutPrefixForSettings ? "" : "es.";
			var settingArgument = this.Version.Major >= 5 ? "-E " : "-D";
			return elasticsearchSettings

				//allow additional settings to take precedence over already DefaultNodeSettings
				//without relying on elasticsearch to dedup, 5.4.0 no longer allows passing the same setting twice
				//on the command with the latter taking precedence
				.GroupBy(setting => setting.Split(new[] {'='}, 2, StringSplitOptions.RemoveEmptyEntries)[0])
				.Select(g => g.Last())
				.Select(s => s.StartsWith(settingArgument) ? s : $"{settingArgument}{settingsPrefix}{s}")
				.ToArray();
		}
	}
}
