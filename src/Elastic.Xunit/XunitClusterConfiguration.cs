using System;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;

namespace Elastic.Xunit
{
	public class XunitClusterConfiguration : EphemeralClusterConfiguration
	{
		public XunitClusterConfiguration(ElasticsearchVersion version, ClusterFeatures features = ClusterFeatures.None, ElasticsearchPlugins plugins = null, int numberOfNodes = 1)
			: base(version, features, plugins, numberOfNodes)
		{
		}

		protected override string NodePrefix => "xunit";

		public int MaxConcurrency { get; set; }
		public TimeSpan? Timeout { get; set; }
	}
}
