using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.AfterNodeStoppedTasks
{
	public abstract class AfterNodeStoppedTaskBase
	{
		public abstract void Run(EphimeralClusterBase cluster, INodeFileSystem fileSystem);
	}
}
