using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Net.Abstractions.Plugins;
using Nest;

namespace Elastic.Net.Abstractions
{
	public class NodeConfiguration
	{
		public bool EnableSsl { get; set; }
		public int DesiredPort { get; set; }
		public ElasticsearchPlugin[] RequiredPlugins { get; set; } = { };

		public ElasticsearchVersion ElasticsearchVersion { get; }
		public NodeFileSystem FileSystem { get; }
		public bool XPackEnabled => this.RequiredPlugins.Contains(OfficialPlugin.XPack);

		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		private readonly string _clusterName;
		private readonly string _nodeName;

		public string ClusterMoniker => "default";
		public string ClusterName => this._clusterName ?? $"{this.ClusterMoniker}-cluster-{_uniqueSuffix}";
		public string NodeName => this._nodeName ?? $"{this.ClusterMoniker}-node-{_uniqueSuffix}";

		private List<string> ElasticsearchArguments { get; }

		public NodeConfiguration(ElasticsearchVersion version, int desiredPort = 9200)
			: this(version, null, null, desiredPort)
		{
		}

		public NodeConfiguration(ElasticsearchVersion version, string clusterName, string nodeName, int desiredPort = 9200)
		{
			this._clusterName = clusterName;
			this._nodeName = nodeName;
			this.FileSystem = new NodeFileSystem(version, this.ClusterName);

			var v = version;
			this.ElasticsearchVersion = v;
			this.DesiredPort = desiredPort;

			var attr = v.Major >= 5 ? "attr." : "";
			var indexedOrStored = v > new ElasticsearchVersion("5.0.0-alpha1") ? "stored" : "indexed";
			var shieldOrSecurity = v > new ElasticsearchVersion("5.0.0-alpha1") ? "xpack.security" : "shield";
			var es = v > new ElasticsearchVersion("5.0.0-alpha2") ? "" : "es.";
			var b = this.XPackEnabled.ToString().ToLowerInvariant();
			var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
			this.ElasticsearchArguments = new List<string>
			{
				$"{es}cluster.name={this.ClusterName}",
				$"{es}node.name={this.NodeName}",
				$"{es}path.repo={this.FileSystem.RepositoryPath}",
				$"{es}path.data={this.FileSystem.DataPath}",
				$"{es}script.inline=true",
				$"{es}script.max_compilations_per_minute=10000",
				$"{es}script.{indexedOrStored}=true",
				$"{es}node.{attr}testingcluster=true",
				$"{es}node.{attr}gateway=true",
			};

			if (this.XPackEnabled)
				this.ElasticsearchArguments.AddRange(new List<string>
				{
					$"{es}{shieldOrSecurity}.enabled={b}",
					$"{es}{shieldOrSecurity}.http.ssl.enabled={sslEnabled}",
					$"{es}{shieldOrSecurity}.authc.realms.pki1.enabled={sslEnabled}",
				});
			if (v >= new ElasticsearchVersion("5.4.0"))
				this.ElasticsearchArguments.Add($"{es}search.remote.connect=true");
		}

		public string[] CreateSettings(string[] additionalSettings) => this.CreateSettings(ElasticsearchArguments, additionalSettings);
		public string[] CreateSettings(IEnumerable<string> elasticsearchSettings, IEnumerable<string> additionalSettings)
		{
			var settingMarker = this.ElasticsearchVersion.Major >= 5 ? "-E " : "-D";
			return elasticsearchSettings
				.Concat(additionalSettings ?? Enumerable.Empty<string>())
				//allow additional settings to take precedence over already DefaultNodeSettings
				//without relying on elasticsearch to dedup, 5.4.0 no longer allows passing the same setting twice
				//on the command with the latter taking precedence
				.GroupBy(setting => setting.Split(new[] {'='}, 2, StringSplitOptions.RemoveEmptyEntries)[0])
				.Select(g => g.Last())
				.Select(s => s.StartsWith(settingMarker) ? s : $"{settingMarker}{s}")
				.ToArray();
		}
	}
}
