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

			var supported = cluster.ClusterConfiguration.Plugins.Select(p => p.ListedPluginName(v)).ToList();
			if (!supported.Any()) return;

			var checkPlugins = cluster.Client().CatPlugins();

			if (!checkPlugins.IsValid)
				throw new Exception($"Failed to check plugins: {checkPlugins.DebugInformation}.");

			var installedPlugins = (checkPlugins.Records ?? Enumerable.Empty<CatPluginsRecord>()).Select(r => r.Component).ToList();
			var missingPlugins = supported.Except(installedPlugins).ToList();
			if (!missingPlugins.Any()) return;

			var missingString = string.Join(", ", missingPlugins);
			var pluginsString = string.Join(", ", installedPlugins);
			throw new Exception($"Already running elasticsearch missed the following plugin(s): {missingString} currently installed: {pluginsString}.");
		}
	}
}
