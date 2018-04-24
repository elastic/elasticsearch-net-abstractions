using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Managed.Ephemeral.Tasks;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralClusterConfiguration : ClusterConfiguration
	{
		private static string UniqueishSuffix => Guid.NewGuid().ToString("N").Substring(0, 6);
		private static string EphemeralClusterName => $"ephemeral-cluster-{UniqueishSuffix}";


		public EphemeralClusterConfiguration(ElasticsearchVersion version, ElasticsearchPlugins plugins = null, int numberOfNodes = 1)
			: this(version, ClusterFeatures.None, plugins, numberOfNodes) { }

		public EphemeralClusterConfiguration(ElasticsearchVersion version, ClusterFeatures features = ClusterFeatures.None, ElasticsearchPlugins plugins = null, int numberOfNodes = 1)
			: base(version, numberOfNodes, EphemeralClusterName, (v, s) => new EphemeralFileSystem(v, s))
		{
			this.Features = features;

			var pluginsList = plugins?.ToList() ?? new List<ElasticsearchPlugin>();
			if (this.Features.HasFlag(ClusterFeatures.XPack) && !pluginsList.Any(p=>p.Moniker == "x-pack"))
				pluginsList.Add(ElasticsearchPlugin.XPack);

			this.Plugins = new ElasticsearchPlugins(pluginsList);

			AddDefaultXPackSettings();
		}

		public ClusterFeatures Features { get; }
		public ElasticsearchPlugins Plugins { get; }

		public bool XPackInstalled => this.Features.HasFlag(ClusterFeatures.XPack) || this.Plugins.Any(p => p.Moniker == "x-pack");
		public bool EnableSsl => this.Features.HasFlag(ClusterFeatures.SSL) && XPackInstalled;
		public bool EnableSecurity => this.Features.HasFlag(ClusterFeatures.Security) && XPackInstalled;

		public IList<IClusterComposeTask<EphemeralClusterConfiguration>> AdditionalInstallationTasks { get; } = new List<IClusterComposeTask<EphemeralClusterConfiguration>>();
		public bool SkipValidation { get; set; }

		public override string CreateNodeName(int? node)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"{this.NodePrefix}-node-{suffix}{node}";
		}

		protected virtual string NodePrefix => "ephemeral";

		private static readonly ElasticsearchVersion LastVersionThatAcceptedShieldSettings = "5.0.0-alpha1";

		public void AddXPackSetting(string key, string value) => AddXPackSetting(key, value, null);

		public void AddXPackSetting(string key, string value, string range)
		{
			if (!this.XPackInstalled) return;
			var shieldOrSecurity = this.Version > LastVersionThatAcceptedShieldSettings ? "xpack.security" : "shield";
			key = Regex.Replace(key, @"^(?:xpack\.security|shield)\.", "");
			this.Add($"{shieldOrSecurity}.{key}", value, range);
		}

		private void AddDefaultXPackSettings()
		{
			if (!this.XPackInstalled) return;
			this.AddXPackSetting("enabled", this.XPackInstalled.ToString().ToLower());

			var securityEnabled = this.EnableSecurity.ToString().ToLowerInvariant();
			this.AddXPackSetting("http.security.enabled", securityEnabled);
			if (this.EnableSecurity)
			{
                var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
                this.AddXPackSetting("http.ssl.enabled", sslEnabled);
                this.AddXPackSetting("authc.realms.pki1.enabled", sslEnabled);

			}
		}

	}
}
