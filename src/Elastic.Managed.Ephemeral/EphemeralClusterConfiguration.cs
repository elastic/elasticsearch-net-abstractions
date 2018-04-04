using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Tasks;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralClusterConfiguration : ClusterConfiguration
	{
		private static string UniqueishSuffix => Guid.NewGuid().ToString("N").Substring(0, 6);
		private static string EphemeralClusterName => $"ephemeral-cluster-{UniqueishSuffix}";

		public EphemeralClusterConfiguration(ElasticsearchVersion version, ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1)
			: base(version, numberOfNodes, EphemeralClusterName, (v, s) => new EphemeralFileSystem(v, s))
		{
			this.Features = features;
			this.AddXpackSettings();
		}

		public ClusterFeatures Features { get; }

		public bool XpackEnabled => this.Features.HasFlag(ClusterFeatures.XPack);
		private bool EnableSsl => this.Features.HasFlag(ClusterFeatures.SSL);
		private bool EnableSecurity => this.Features.HasFlag(ClusterFeatures.Security);

		public IList<IClusterComposeTask<EphemeralClusterConfiguration>> AdditionalInstallationTasks { get; set; }
		public bool SkipValidation { get; set; }

		public override string CreateNodeName(int? node)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"{this.NodePrefix}-node-{suffix}{node}";
		}

		protected virtual string NodePrefix => "ephemeral";

		private static readonly ElasticsearchVersion LastVersionThatAcceptedShieldSettings = "5.0.0-alpha1";
		public void AddXPackSetting(string key, string value)
		{
			var shieldOrSecurity = this.Version > LastVersionThatAcceptedShieldSettings ? "xpack.security" : "shield";
			key = Regex.Replace(key, @"^(?:xpack\.security|shield)\.", "");
			this.Add($"{shieldOrSecurity}.{key}", value);
		}

		private void AddXpackSettings()
		{
			if (!EnableSecurity) return;
			var b = this.EnableSecurity.ToString().ToLowerInvariant();
			var sslEnabled = this.EnableSsl.ToString().ToLowerInvariant();
			this.AddXPackSetting("enabled", b);
			this.AddXPackSetting("http.ssl.enabled", sslEnabled);
			this.AddXPackSetting("authc.realms.pki1.enabled", sslEnabled);
		}

	}
}
