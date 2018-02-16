using System;
using System.Collections.Generic;
using System.Linq;

namespace Elastic.ManagedNode.Configuration
{
	public class NodeConfiguration
	{
		public bool EnableSsl { get; }
		public bool XPackEnabled { get; }
		public int DesiredPort { get;  }

		public ElasticsearchVersion ElasticsearchVersion { get; }
		public NodeFileSystem FileSystem { get; }

		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		private readonly string _clusterName;
		private readonly string _nodeName;

		public string ClusterMoniker => "default";
		public string ClusterName => this._clusterName ?? $"{this.ClusterMoniker}-cluster-{_uniqueSuffix}";
		public string NodeName => this._nodeName ?? $"{this.ClusterMoniker}-node-{_uniqueSuffix}";

		public string[] CommandLineArguments { get; }

		public NodeConfiguration(ElasticsearchVersion version, int desiredPort = 9200, bool enableXPack = false, bool enableSsl = false)
			: this(version, null, null, null, desiredPort, enableXPack, enableSsl) { }

		public NodeConfiguration(
			ElasticsearchVersion version,
			string clusterName,
			string nodeName,
			IDictionary<string, string> additionalNodeSettings = null,
			int desiredPort = 9200,
			bool enableXPack = false,
			bool enableSsl = false)
		{
			this._clusterName = clusterName;
			this._nodeName = nodeName;
			this.FileSystem = new NodeFileSystem(version, this.ClusterName);
			this.XPackEnabled = enableXPack;
			this.EnableSsl = enableSsl;

			var v = version;
			this.ElasticsearchVersion = v;
			this.DesiredPort = desiredPort;

			var attr = v.Major >= 5 ? "attr." : "";

			var settings = ElasticsearchSettings(desiredPort, attr);
			AddXpackSettings(v, settings);
			if (v >= VersionThatIntroducedCrossClusterSearch) settings.Add($"search.remote.connect=true");
			foreach (var kv in (additionalNodeSettings ?? new Dictionary<string, string>()))
				settings.Add($"{kv.Key}={kv.Value}");


			this.CommandLineArguments = this.CreateSettings(settings);
		}

		private void AddXpackSettings(ElasticsearchVersion v, List<string> settings)
		{
			var b = this.XPackEnabled.ToString().ToLowerInvariant();
			var shieldOrSecurity = v > LastVersionThatAcceptedShieldSettings ? "xpack.security" : "shield";
			var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
			if (this.XPackEnabled)
				settings.AddRange(new List<string>
				{
					$"{shieldOrSecurity}.enabled={b}",
					$"{shieldOrSecurity}.http.ssl.enabled={sslEnabled}",
					$"{shieldOrSecurity}.authc.realms.pki1.enabled={sslEnabled}",
				});
		}

		private List<string> ElasticsearchSettings(int desiredPort, string attr)
		{
			//TODO Keep this to a minimum
			var indexedOrStored = this.ElasticsearchVersion > LastVersionThatTookIndexedScripts ? "stored" : "indexed";
			return new List<string>
			{
				$"cluster.name={this.ClusterName}",
				$"node.name={this.NodeName}",
				$"path.repo={this.FileSystem.RepositoryPath}",
				$"path.data={this.FileSystem.DataPath}",
				$"script.inline=true",
				$"script.max_compilations_per_minute=10000",
				$"script.{indexedOrStored}=true",
				$"node.{attr}testingcluster=true",
				$"node.{attr}gateway=true",
				$"http.port={desiredPort}"
			};
		}

		private static ElasticsearchVersion VersionThatIntroducedCrossClusterSearch = new ElasticsearchVersion("5.4.0");
		private static ElasticsearchVersion LastVersionThatTookIndexedScripts = new ElasticsearchVersion("5.0.0-alpha1");
		private static ElasticsearchVersion LastVersionThatAcceptedShieldSettings = new ElasticsearchVersion("5.0.0-alpha1");
		private static ElasticsearchVersion LastVersionWithoutPrefixForSettings = new ElasticsearchVersion("5.0.0-alpha2");

		private string[] CreateSettings(IEnumerable<string> elasticsearchSettings)
		{
			var settingsPrefix = this.ElasticsearchVersion >  LastVersionWithoutPrefixForSettings ? "" : "es.";
			var settingArgument = this.ElasticsearchVersion.Major >= 5 ? "-E " : "-D";
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
