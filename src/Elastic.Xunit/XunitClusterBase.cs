using Elastic.Managed.Ephemeral;

namespace Elastic.Xunit
{
	public abstract class XunitClusterBase : XunitClusterBase<XunitClusterConfiguration>
	{
		protected XunitClusterBase(XunitClusterConfiguration configuration) : base(configuration) { }
	}

	public abstract class XunitClusterBase<TConfiguration> : EphemeralCluster<TConfiguration>
		where TConfiguration : XunitClusterConfiguration
	{
		protected override string ClientHostName => "ipv4.fiddler";

		protected XunitClusterBase(TConfiguration configuration) : base(configuration) { }
	}
}
