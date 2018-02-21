using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Configuration
{
	public class NodeConfiguration
	{
		public INodeFileSystem FileSystem { get; }
		public NodeFeatures Features { get; }
		public int? DesiredPort { get; }
		public string DesiredNodeName { get; }

		private List<string> Settings { get; }

		public ElasticsearchVersion Version => this.FileSystem.Version;
		public string[] CommandLineArguments => this.CreateCommandLineArgs(this.Settings);

		public bool XpackEnabled => this.Features.HasFlag(NodeFeatures.XPack);
		private bool EnableSsl => this.Features.HasFlag(NodeFeatures.SSL);
		private bool EnableSecurity => this.Features.HasFlag(NodeFeatures.Security);

		public NodeConfiguration(ElasticsearchVersion version, NodeFeatures features = NodeFeatures.None, string nodeName = null, string clusterName = null, int? port = null)
			: this(new NodeFileSystem(version, clusterName), features, nodeName, port) { }

		public NodeConfiguration(INodeFileSystem fileSystem, NodeFeatures features = NodeFeatures.None, string nodeName = null, int? port = null)
		{
			this.FileSystem = fileSystem;
			this.Features = features;
			this.DesiredPort = port;
			this.DesiredNodeName = nodeName;

			var v = this.Version;
			this.Settings = new List<string> ();
			if (!string.IsNullOrWhiteSpace(nodeName)) this.Settings.Add($"node.name={nodeName}");
			if (this.DesiredPort.HasValue) this.Settings.Add($"http.port={this.DesiredPort}");
			if (!string.IsNullOrWhiteSpace(this.FileSystem.ClusterName)) this.Settings.Add($"cluster.name={this.FileSystem.ClusterName}");
			if (!string.IsNullOrWhiteSpace(this.FileSystem.RepositoryPath)) this.Settings.Add($"path.repo={this.FileSystem.RepositoryPath}");
			if (!string.IsNullOrWhiteSpace(this.FileSystem.DataPath)) this.Settings.Add($"path.data={this.FileSystem.DataPath}");
			var logsPathDefault = Path.Combine(this.FileSystem.ElasticsearchHome, "logs");
			if (logsPathDefault != this.FileSystem.LogsPath)
				if (!string.IsNullOrWhiteSpace(this.FileSystem.LogsPath)) this.Settings.Add($"path.logs={this.FileSystem.LogsPath}");
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
