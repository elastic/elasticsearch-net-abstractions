using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Configuration
{
	public struct NodeSetting
	{
		public string Key { get; }
		public string Value { get; }

		public NodeSetting(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		public override string ToString() => $"{Key}={Value}";
	}

	public class NodeSettings : List<NodeSetting>
	{
		public NodeSettings() { }
		public NodeSettings(NodeSettings settings) : base(settings) { }

		public void Add(string setting)
		{
			var s = setting.Split(new[] {'='}, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length != 2)
				throw new ArgumentException($"Can only add node settings in key=value from but received: {setting}");
			this.Add(new NodeSetting(s[0], s[1]));

		}
		public void Add(string key, string value) => this.Add(new NodeSetting(key, value));

		private static readonly ElasticsearchVersion LastVersionWithoutPrefixForSettings = ElasticsearchVersion.From("5.0.0-alpha2");

		public string[] ToCommandLineArguments(ElasticsearchVersion version)
		{
			var settingsPrefix = version > LastVersionWithoutPrefixForSettings ? "" : "es.";
			var settingArgument = version.Major >= 5 ? "-E " : "-D";
			return this

				//allow additional settings to take precedence over already DefaultNodeSettings
				//without relying on elasticsearch to dedup, 5.4.0 no longer allows passing the same setting twice
				//on the command with the latter taking precedence
				.GroupBy(setting => setting.Key)
				.Select(g => g.Last())
				.Select(s => s.Key.StartsWith(settingArgument) ? s.ToString() : $"{settingArgument}{settingsPrefix}{s}")
				.ToArray();
		}
	}

	public class ClusterConfiguration
	{
		public ClusterConfiguration(INodeFileSystem fileSystem) : this(clusterName: null, fileSystem: fileSystem) {}
		public ClusterConfiguration(string clusterName, INodeFileSystem fileSystem, int numberOfNodes = 1)
		{
			this.ClusterName = clusterName;
			this.FileSystem = fileSystem;
			this.NumberOfNodes = numberOfNodes;
			this.Features = ClusterFeatures.None;

			var fs = this.FileSystem;
			if (!string.IsNullOrWhiteSpace(clusterName)) this.ClusterNodeSettings.Add("cluster.name", clusterName);
			if (!string.IsNullOrWhiteSpace(fs.RepositoryPath)) this.ClusterNodeSettings.Add("path.repo", fs.RepositoryPath);
			if (!string.IsNullOrWhiteSpace(fs.DataPath)) this.ClusterNodeSettings.Add("path.data", fs.DataPath);
			var logsPathDefault = Path.Combine(fs.ElasticsearchHome, "logs");
			if (logsPathDefault != fs.LogsPath && !string.IsNullOrWhiteSpace(fs.LogsPath))
				this.ClusterNodeSettings.Add("path.logs", fs.LogsPath);

			this.AddXpackSettings();

			this.ClusterNodeSettings.Add("node.max_local_storage_nodes", numberOfNodes.ToString(CultureInfo.InvariantCulture));
			this.ClusterNodeSettings.Add("discovery.zen.minimum_master_nodes", Quorum(numberOfNodes).ToString(CultureInfo.InvariantCulture));
		}

		public string ClusterName { get; }
		public INodeFileSystem FileSystem { get; }
		public int NumberOfNodes { get; }
		public ClusterFeatures Features { get; }

		public NodeSettings ClusterNodeSettings { get; } = new NodeSettings();

		public ElasticsearchVersion Version => this.FileSystem.Version;
		public bool XpackEnabled => this.Features.HasFlag(ClusterFeatures.XPack);
		private bool EnableSsl => this.Features.HasFlag(ClusterFeatures.SSL);
		private bool EnableSecurity => this.Features.HasFlag(ClusterFeatures.Security);

		public virtual string CreateNodeName(int? node) => null;

		private static int Quorum(int instanceCount) => Math.Max(1, (int) Math.Floor((double) instanceCount / 2) + 1);

		private static readonly ElasticsearchVersion LastVersionThatAcceptedShieldSettings = "5.0.0-alpha1";
		private void AddXpackSettings()
		{
			if (!EnableSecurity) return;
			var b = this.EnableSecurity.ToString().ToLowerInvariant();
			var shieldOrSecurity = this.Version > LastVersionThatAcceptedShieldSettings ? "xpack.security" : "shield";
			var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
			this.Add($"{shieldOrSecurity}.enabled", b);
			this.Add($"{shieldOrSecurity}.http.ssl.enabled", sslEnabled);
			this.Add($"{shieldOrSecurity}.authc.realms.pki1.enabled", sslEnabled);
		}

		public void Add(string key, string value)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;
			this.ClusterNodeSettings.Add(key,value);
		}
	}

	public class NodeConfiguration
	{
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
		public ElasticsearchVersion Version => this.FileSystem.Version;
		public string[] CommandLineArguments => this.Settings.ToCommandLineArguments(this.Version);

		public string AttributeKey(string attribute)
		{
			var attr = this.Version.Major >= 5 ? "attr." : "";
			return $"node.{attr}.{attribute}";
		}

		public void Add(string key, string value) => this.Settings.Add(key,value);


		//TODO move these to XUnit project once we start on that
		//if (v >= VersionThatIntroducedCrossClusterSearch) settings.Add($"search.remote.connect=true");
		//private static readonly ElasticsearchVersion VersionThatIntroducedCrossClusterSearch = ElasticsearchVersionResolver.From("5.4.0");
		//var indexedOrStored = this.Version > LastVersionThatTookIndexedScripts ? "stored" : "indexed";
		//private static readonly ElasticsearchVersion LastVersionThatTookIndexedScripts = ElasticsearchVersion.From("5.0.0-alpha1");
	}
}
