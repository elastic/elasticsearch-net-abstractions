using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public abstract class NodeValidationTaskBase
	{
		public abstract void Validate(EphemeralCluster cluster, INodeFileSystem fs);
	}
}
