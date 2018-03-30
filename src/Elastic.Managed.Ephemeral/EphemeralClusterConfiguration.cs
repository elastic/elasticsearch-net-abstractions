using System;
using Elastic.Managed.Configuration;

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

		public override string CreateNodeName(int? node)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"ephemeral-node-{suffix}{node}";
		}

		public ClusterFeatures Features { get; }

		public bool XpackEnabled => this.Features.HasFlag(ClusterFeatures.XPack);
		private bool EnableSsl => this.Features.HasFlag(ClusterFeatures.SSL);
		private bool EnableSecurity => this.Features.HasFlag(ClusterFeatures.Security);

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

	}
}
