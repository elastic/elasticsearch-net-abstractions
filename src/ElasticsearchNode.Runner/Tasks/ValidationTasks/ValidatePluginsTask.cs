using System;
using System.Linq;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;
using Nest;

namespace Elastic.Net.Abstractions.Tasks.ValidationTasks
{
	public class ValidatePluginsTask : NodeValidationTaskBase
	{
		public override void Validate(IElasticClient client, NodeConfiguration configuration, ElasticsearchPlugin[] requiredPlugins)
		{
			var v = configuration.ElasticsearchVersion;
			//if the version we are running against is a s snapshot version we do not validate plugins
			//because we can not reliably install plugins against snapshots
			if (v.IsSnapshot) return;

			var supported = requiredPlugins.Select(p => p.Moniker).ToList();
			if (!supported.Any()) return;

			var checkPlugins = client.CatPlugins();

			if (!checkPlugins.IsValid)
				throw new Exception($"Failed to check plugins: {checkPlugins.DebugInformation}.");

			var missingPlugins = supported
				.Except((checkPlugins.Records ?? Enumerable.Empty<CatPluginsRecord>()).Select(r => r.Component))
				.ToList();
			if (!missingPlugins.Any()) return;

			var pluginsString = string.Join(", ", missingPlugins);
			throw new Exception($"Already running elasticsearch missed the following plugin(s): {pluginsString}.");
		}
	}
}
