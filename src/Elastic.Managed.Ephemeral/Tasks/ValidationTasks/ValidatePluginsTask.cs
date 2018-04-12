using System;
using System.Linq;
using Elastic.Managed.ConsoleWriters;
using Nest;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidatePluginsTask : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var v = cluster.ClusterConfiguration.Version;
			if (v.Major == 2)
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(ValidatePluginsTask)}}} skipping validate plugins on {{2.x}} version: [{v}]");
				return;
			}

			var supported = cluster.ClusterConfiguration.Plugins.Select(p => p.Moniker).ToList();
			if (!supported.Any()) return;

			var checkPlugins = cluster.Client().CatPlugins();

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
