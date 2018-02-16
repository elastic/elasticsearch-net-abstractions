using Elastic.ManagedNode.Configuration;

namespace Elastic.Net.Abstractions.Tasks.AfterNodeStoppedTasks
{
	public abstract class AfterNodeStoppedTaskBase
	{
		public abstract void Run(NodeConfiguration config, NodeFileSystem fileSystem);
	}
}
