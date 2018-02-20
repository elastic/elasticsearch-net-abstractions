using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.BeforeStartNodeTasks
{
	public abstract class BeforeStartNodeTaskBase
	{
		public abstract void Run(EphimeralClusterBase cluster, INodeFileSystem fs);
	}
}
