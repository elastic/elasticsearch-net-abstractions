using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.ValidationTasks
{
	public abstract class NodeValidationTaskBase
	{
		public abstract void Validate(EphimeralClusterBase cluster, INodeFileSystem fs);
	}
}
