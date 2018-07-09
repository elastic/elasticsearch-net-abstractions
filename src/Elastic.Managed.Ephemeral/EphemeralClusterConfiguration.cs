using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Managed.Ephemeral.Tasks;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralClusterConfiguration : ClusterConfiguration<EphemeralFileSystem>
	{
		private static string UniqueishSuffix => Guid.NewGuid().ToString("N").Substring(0, 6);
		private static string EphemeralClusterName => $"ephemeral-cluster-{UniqueishSuffix}";

		public EphemeralClusterConfiguration(ElasticsearchVersion version, ElasticsearchPlugins plugins = null, int numberOfNodes = 1)
			: this(version, ClusterFeatures.None, plugins, numberOfNodes) { }

		public EphemeralClusterConfiguration(ElasticsearchVersion version, ClusterFeatures features = ClusterFeatures.None, ElasticsearchPlugins plugins = null, int numberOfNodes = 1)
			: base(version, (v, s) => new EphemeralFileSystem(v, s), numberOfNodes, EphemeralClusterName)
		{
			this.TrialMode = XPackTrialMode.None;
			this.Features = features;


			var pluginsList = plugins?.ToList() ?? new List<ElasticsearchPlugin>();
			if (this.Features.HasFlag(ClusterFeatures.XPack) && !pluginsList.Any(p=>p.Moniker == "x-pack"))
				pluginsList.Add(ElasticsearchPlugin.XPack);

			this.Plugins = new ElasticsearchPlugins(pluginsList);

			AddDefaultXPackSettings();
		}

		public ClusterFeatures Features { get; }
		public ElasticsearchPlugins Plugins { get; }

		public bool XPackInstalled => this.Features.HasFlag(ClusterFeatures.XPack)
		                              || this.Version >= "6.3.0"
		                              || this.Plugins.Any(p => p.Moniker == "x-pack")
		                              || this.EnableSsl
		                              || this.EnableSecurity;

		public bool EnableSecurity => this.Features.HasFlag(ClusterFeatures.Security) || this.EnableSsl;
		public bool EnableSsl => this.Features.HasFlag(ClusterFeatures.SSL);

		public IList<IClusterComposeTask> AdditionalBeforeNodeStartedTasks { get; } = new List<IClusterComposeTask>();

		public IList<IClusterComposeTask> AdditionalAfterStartedTasks { get; } = new List<IClusterComposeTask>();
		/// <summary>
		/// Expert level setting, skips all builtin validation tasks for cases where you need to guarantee your call is the first call into the cluster
		/// </summary>
		public bool SkipBuiltInAfterStartTasks { get; set; }
		/// <summary> If not null or empty will be posted as the x-pack license to use. </summary>
		public string XPackLicenseJson { get; set; }
		public XPackTrialMode TrialMode { get; set; }

		public override string CreateNodeName(int? node)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"{this.NodePrefix}-node-{suffix}{node}";
		}

		protected virtual string NodePrefix => "ephemeral";

		private static readonly ElasticsearchVersion LastVersionThatAcceptedShieldSettings = "5.0.0-alpha1";

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
            this.AddSecuritySetting("authc.token.enabled", "true", ">=5.5.0");
			if (this.EnableSecurity)
			{
	           	this.AddSecuritySetting("authc.realms.pki1.enabled", sslEnabled);
			}

			if (this.EnableSsl)
			{
                this.Add("xpack.ssl.key", this.FileSystem.NodePrivateKey);
                this.Add("xpack.ssl.certificate", this.FileSystem.NodeCertificate);
                this.Add("xpack.ssl.certificate_authorities", this.FileSystem.CaCertificate);
			}
		}


	}
}
