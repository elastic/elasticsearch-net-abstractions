using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.AfterNodeStoppedTasks
{
	public abstract class AfterNodeStoppedTaskBase
	{
		public abstract void Run(EphemeralCluster cluster, INodeFileSystem fileSystem);
	}
}
