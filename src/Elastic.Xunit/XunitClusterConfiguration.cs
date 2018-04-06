using System;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;

namespace Elastic.Xunit
{
	public class XunitClusterConfiguration : EphemeralClusterConfiguration
	{
		public XunitClusterConfiguration(ElasticsearchVersion version, ClusterFeatures features = ClusterFeatures.None, int numberOfNodes = 1)
			: base(version, features, numberOfNodes)
		{
		}

		protected override string NodePrefix => "xunit";

		public int MaxConcurrency { get; set; }
		public TimeSpan? Timeout { get; set; }
	}
}
