using Elastic.Managed.Configuration;
using Nest;

namespace Elastic.Managed.Ephemeral.Clusters
{
	public class EphemeralCluster : EphemeralClusterBase
	{
		public EphemeralCluster(ElasticsearchVersion version, int instanceCount = 1) : base(version, instanceCount) { }

		protected override ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings) => connectionSettings;
	}
}
