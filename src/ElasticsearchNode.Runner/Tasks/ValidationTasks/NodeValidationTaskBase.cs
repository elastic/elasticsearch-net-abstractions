using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;
using Nest;

namespace Elastic.Net.Abstractions.Tasks.ValidationTasks
{
	public abstract class NodeValidationTaskBase
	{
		public abstract void Validate(IElasticClient client, NodeConfiguration configuration, ElasticsearchPlugin[] requiredPlugins);
	}
}
