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
		public override string CreateNodeName(int? node)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"xunit-node-{suffix}{node}";
		}
		public virtual int MaxConcurrency { get; } = 1;
	}
}