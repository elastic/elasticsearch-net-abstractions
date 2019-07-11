using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Managed.Ephemeral.Tasks;
using Elastic.Stack.Artifacts;
using Elastic.Stack.Artifacts.Products;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralClusterConfiguration : ClusterConfiguration<EphemeralFileSystem>
	{
		private static string UniqueishSuffix => Guid.NewGuid().ToString("N").Substring(0, 6);
		private static string EphemeralClusterName => $"ephemeral-cluster-{UniqueishSuffix}";

		public EphemeralClusterConfiguration(ElasticVersion version, ElasticsearchPlugins plugins = null, int numberOfNodes = 1)
			: this(version, ClusterFeatures.None, plugins, numberOfNodes) { }

		public EphemeralClusterConfiguration(ElasticVersion version, ClusterFeatures features, ElasticsearchPlugins plugins = null, int numberOfNodes = 1)
			: base(version, (v, s) => new EphemeralFileSystem(v, s), numberOfNodes, EphemeralClusterName)
		{
			this.TrialMode = XPackTrialMode.None;
			this.Features = features;

			var pluginsList = plugins?.ToList() ?? new List<ElasticsearchPlugin>();
			if (this.Features.HasFlag(ClusterFeatures.XPack) && pluginsList.All(p => p.SubProductName != "x-pack"))
				pluginsList.Add(ElasticsearchPlugin.XPack);

			this.Plugins = new ElasticsearchPlugins(pluginsList);

			AddDefaultXPackSettings();
		}

		/// <summary>
		/// The features supported by the cluster
		/// </summary>
		public ClusterFeatures Features { get; }

		/// <summary>
		/// The collection of plugins to install
		/// </summary>
		public ElasticsearchPlugins Plugins { get; }

		/// <summary>
		/// Validates that the plugins to install can be installed on the target Elasticsearch version.
		/// This can be useful to fail early when subsequent operations are relying on installation
		/// succeeding.
		/// </summary>
		public bool ValidatePluginsToInstall { get; } = true;

		public bool XPackInstalled => this.Features.HasFlag(ClusterFeatures.XPack)
		                              || this.Version >= "6.3.0"
		                              || this.Plugins.Any(p => p.SubProductName == "x-pack")
		                              || this.EnableSsl
		                              || this.EnableSecurity;

		public bool EnableSecurity => this.Features.HasFlag(ClusterFeatures.Security) || this.EnableSsl;
		public bool EnableSsl => this.Features.HasFlag(ClusterFeatures.SSL);

		public IList<IClusterComposeTask> AdditionalBeforeNodeStartedTasks { get; } = new List<IClusterComposeTask>();

		public IList<IClusterComposeTask> AdditionalAfterStartedTasks { get; } = new List<IClusterComposeTask>();

		/// <summary>
		/// Expert level setting, skips all built-in validation tasks for cases where you need to guarantee your call is the first call into the cluster
		/// </summary>
		public bool SkipBuiltInAfterStartTasks { get; set; }

		/// <summary> If not null or empty will be posted as the x-pack license to use. </summary>
		public string XPackLicenseJson { get; set; }

		/// <summary>
		/// From 6.3.0 and up this property allows you to control what type of license is applied (trial/basic) in the absense of
		/// <see cref="XPackLicenseJson"/>
		/// </summary>
		public XPackTrialMode TrialMode { get; set; }

		/// <summary> Bootstrapping HTTP calls should attempt to auto route traffic through fiddler if its running </summary>
		public bool HttpFiddlerAware { get; set; }

		public override string CreateNodeName(int? node)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"{this.NodePrefix}-node-{suffix}{node}";
		}

		protected virtual string NodePrefix => "ephemeral";

		private static readonly ElasticVersion LastVersionThatAcceptedShieldSettings = "5.0.0-alpha1";

		public void AddSecuritySetting(string key, string value) => AddSecuritySetting(key, value, null);

		public void AddSecuritySetting(string key, string value, string range)
		{
			if (!this.XPackInstalled) return;
			var shieldOrSecurity = this.Version > LastVersionThatAcceptedShieldSettings ? "xpack.security" : "shield";
			key = Regex.Replace(key, @"^(?:xpack\.security|shield)\.", "");
			this.Add($"{shieldOrSecurity}.{key}", value, range);
		}

		private void AddDefaultXPackSettings()
		{
			if (!this.XPackInstalled) return;

			var securityEnabled = this.EnableSecurity.ToString().ToLowerInvariant();
            var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
			this.AddSecuritySetting("enabled", securityEnabled);

            this.AddSecuritySetting("http.ssl.enabled", sslEnabled);
            this.AddSecuritySetting("transport.ssl.enabled", sslEnabled);
			if (this.EnableSecurity && this.EnableSsl)
			{
	           	this.AddSecuritySetting("authc.realms.pki1.enabled", sslEnabled, "<7.0.0-beta1");
	           	this.AddSecuritySetting("authc.realms.pki.pki1.enabled", sslEnabled, ">=7.0.0-beta1");
			}

			if (this.EnableSsl)
			{
				this.AddSecuritySetting("authc.token.enabled", "true", ">=5.5.0");
				if (this.Version < "7.0.0-beta1")
				{
					this.Add("xpack.ssl.key", this.FileSystem.NodePrivateKey);
					this.Add("xpack.ssl.certificate", this.FileSystem.NodeCertificate);
					this.Add("xpack.ssl.certificate_authorities", this.FileSystem.CaCertificate);

				}
				else
				{
					this.Add("xpack.security.transport.ssl.key", this.FileSystem.NodePrivateKey);
					this.Add("xpack.security.transport.ssl.certificate", this.FileSystem.NodeCertificate);
					this.Add("xpack.security.transport.ssl.certificate_authorities", this.FileSystem.CaCertificate);

					this.Add("xpack.security.http.ssl.key", this.FileSystem.NodePrivateKey);
					this.Add("xpack.security.http.ssl.certificate", this.FileSystem.NodeCertificate);
					this.Add("xpack.security.http.ssl.certificate_authorities", this.FileSystem.CaCertificate);

				}

			}
		}
	}
}
