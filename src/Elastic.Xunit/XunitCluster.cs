using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.Configuration;

namespace Elastic.Xunit
{
	public abstract class XunitClusterBase : EphemeralCluster
	{
		public virtual int MaxConcurrency { get; } = 1;

		protected XunitClusterBase() : base(
			new EphemeralClusterConfiguration(
				TestConfiguration.Configuration.ElasticsearchVersion,
				ClusterFeatures.None,
				numberOfNodes: 1
			)
		) { }

	}
}
