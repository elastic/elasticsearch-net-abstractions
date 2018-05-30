using Elastic.Managed.Ephemeral;

namespace Elastic.Xunit
{
	/// <summary>
	/// Base class for a cluster that integrates with Xunit tests
	/// </summary>
	public abstract class XunitClusterBase : XunitClusterBase<XunitClusterConfiguration>
	{
		protected XunitClusterBase(XunitClusterConfiguration configuration) : base(configuration) { }
	}

	/// <summary>
	/// Base class for a cluster that integrates with Xunit tests
	/// </summary>
	public abstract class XunitClusterBase<TConfiguration> : EphemeralCluster<TConfiguration>
		where TConfiguration : XunitClusterConfiguration
	{
		protected XunitClusterBase(TConfiguration configuration) : base(configuration) { }
	}
}
