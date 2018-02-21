using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.AfterNodeStoppedTasks
{
	public abstract class AfterNodeStoppedTaskBase
	{
		public abstract void Run(EphemeralClusterBase cluster, INodeFileSystem fileSystem);
	}
}
