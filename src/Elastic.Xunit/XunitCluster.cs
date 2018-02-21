using Elastic.Managed.Ephemeral;
using Elastic.Xunit.Configuration;

namespace Elastic.Xunit
{
	public abstract class XunitClusterBase : EphemeralCluster
	{
		public virtual int MaxConcurrency { get; }

		//todo pipe `instanceCount` to base
		protected XunitClusterBase() : base(TestConfiguration.Configuration.ElasticsearchVersion, instanceCount: 0) { }

	}
}
