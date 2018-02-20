using Elastic.Managed.Configuration;
using Nest;

namespace Elastic.Managed.Ephimeral.Clusters
{
	public class EphimeralCluster : EphimeralClusterBase
	{
		public EphimeralCluster(ElasticsearchVersion version, int instanceCount = 1) : base(version, instanceCount) { }

		protected override ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings) => connectionSettings;
	}
}
