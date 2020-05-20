// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Elastic.Elasticsearch.Ephemeral.Plugins;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Managed.Configuration;
using Elastic.Stack.ArtifactsApi;
using Elastic.Stack.ArtifactsApi.Products;

namespace Elastic.Elasticsearch.Ephemeral
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
			TrialMode = XPackTrialMode.None;
			Features = features;

			var pluginsList = plugins?.ToList() ?? new List<ElasticsearchPlugin>();
			if (Features.HasFlag(ClusterFeatures.XPack) && pluginsList.All(p => p.SubProductName != "x-pack"))
				pluginsList.Add(ElasticsearchPlugin.XPack);

			Plugins = new ElasticsearchPlugins(pluginsList);

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

		public bool XPackInstalled => Features.HasFlag(ClusterFeatures.XPack)
		                              || Version >= "6.3.0"
		                              || Plugins.Any(p => p.SubProductName == "x-pack")
		                              || EnableSsl
		                              || EnableSecurity;

		public bool EnableSecurity => Features.HasFlag(ClusterFeatures.Security) || EnableSsl;
		public bool EnableSsl => Features.HasFlag(ClusterFeatures.SSL);

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
			return $"{NodePrefix}-node-{suffix}{node}";
		}

		protected virtual string NodePrefix => "ephemeral";

		private static readonly ElasticVersion LastVersionThatAcceptedShieldSettings = "5.0.0-alpha1";

		public void AddSecuritySetting(string key, string value) => AddSecuritySetting(key, value, null);

		public void AddSecuritySetting(string key, string value, string range)
		{
			if (!XPackInstalled) return;
			var shieldOrSecurity = Version > LastVersionThatAcceptedShieldSettings ? "xpack.security" : "shield";
			key = Regex.Replace(key, @"^(?:xpack\.security|shield)\.", "");
			Add($"{shieldOrSecurity}.{key}", value, range);
		}

		private void AddDefaultXPackSettings()
		{
			if (!XPackInstalled) return;

			var securityEnabled = EnableSecurity.ToString().ToLowerInvariant();
            var sslEnabled = EnableSsl.ToString().ToLowerInvariant();
			AddSecuritySetting("enabled", securityEnabled);

            AddSecuritySetting("http.ssl.enabled", sslEnabled);
            AddSecuritySetting("transport.ssl.enabled", sslEnabled);
			if (EnableSecurity && EnableSsl)
			{
	           	AddSecuritySetting("authc.realms.pki1.enabled", sslEnabled, "<7.0.0-beta1");
	           	AddSecuritySetting("authc.realms.pki.pki1.enabled", sslEnabled, ">=7.0.0-beta1");
			}

			if (EnableSsl)
			{
				AddSecuritySetting("authc.token.enabled", "true", ">=5.5.0");
				if (Version < "7.0.0-beta1")
				{
					Add("xpack.ssl.key", FileSystem.NodePrivateKey);
					Add("xpack.ssl.certificate", FileSystem.NodeCertificate);
					Add("xpack.ssl.certificate_authorities", FileSystem.CaCertificate);

				}
				else
				{
					Add("xpack.security.transport.ssl.key", FileSystem.NodePrivateKey);
					Add("xpack.security.transport.ssl.certificate", FileSystem.NodeCertificate);
					Add("xpack.security.transport.ssl.certificate_authorities", FileSystem.CaCertificate);

					Add("xpack.security.http.ssl.key", FileSystem.NodePrivateKey);
					Add("xpack.security.http.ssl.certificate", FileSystem.NodeCertificate);
					Add("xpack.security.http.ssl.certificate_authorities", FileSystem.CaCertificate);

				}

			}
		}
	}
}
