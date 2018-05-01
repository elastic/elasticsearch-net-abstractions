using Elastic.Managed.Configuration;

namespace Elastic.Managed
{
	public class ElasticsearchCluster : ClusterBase
	{
		public ElasticsearchCluster(ClusterConfiguration clusterConfiguration) : base(clusterConfiguration)
		{
		}
	}
}