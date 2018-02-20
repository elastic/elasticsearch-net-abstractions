using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;

namespace Elastic.Net.Abstractions.Tasks.AfterNodeStoppedTasks
{
	public abstract class AfterNodeStoppedTaskBase
	{
		public abstract void Run(EphimeralClusterBase cluster, INodeFileSystem fileSystem);
	}
}
