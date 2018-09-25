using Elastic.Managed.Ephemeral;

namespace Elastic.Xunit
{
	public static class ElasticXunitRunner
	{
		public static IEphemeralCluster<XunitClusterConfiguration> CurrentCluster { get; internal set; }
	}
}
