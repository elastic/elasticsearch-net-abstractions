using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;

namespace Elastic.Net.Abstractions.Tasks.BeforeStartNodeTasks
{
	public abstract class BeforeStartNodeTaskBase
	{
		public abstract void Run(EphimeralClusterBase cluster, INodeFileSystem fs);
	}
}
