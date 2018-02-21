using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public abstract class NodeValidationTaskBase
	{
		public abstract void Validate(EphemeralClusterBase cluster, INodeFileSystem fs);
	}
}
