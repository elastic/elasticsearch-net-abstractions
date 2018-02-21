using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks
{
	public abstract class BeforeStartNodeTaskBase
	{
		public abstract void Run(EphemeralClusterBase cluster, INodeFileSystem fs);
	}
}
