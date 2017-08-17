namespace Elastic.Net.Abstractions.Tasks.BeforeStartNodeTasks
{
	public abstract class BeforeStartNodeTaskBase
	{
		public abstract void Run(NodeConfiguration config, NodeFileSystem fileSystem, string[] serverSettings);
	}
}
