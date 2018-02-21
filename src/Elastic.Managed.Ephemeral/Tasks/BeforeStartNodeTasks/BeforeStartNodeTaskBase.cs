using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks
{
	public abstract class BeforeStartNodeTaskBase
	{
		public abstract void Run(EphemeralCluster cluster, INodeFileSystem fs);
	}
}
