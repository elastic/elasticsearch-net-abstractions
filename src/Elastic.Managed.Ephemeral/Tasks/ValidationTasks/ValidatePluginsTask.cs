using System;
using System.Linq;
using Elastic.Managed.ConsoleWriters;

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

			var requestPlugins = cluster.ClusterConfiguration.Plugins
				.Where(p => p.IsValid(v))
				.Where(p => !p.IsIncludedOutOfTheBox(v))
				.Select(p => p.GetExistsMoniker(v))
				.ToList();
			if (!requestPlugins.Any()) return;

			cluster.Writer.WriteDiagnostic($"{{{nameof(ValidatePluginsTask)}}} validating the cluster is running the requested plugins");
			var catPlugins = this.Get(cluster, "_cat/plugins", "h=component");
			if (catPlugins == null || !catPlugins.IsSuccessStatusCode) throw new Exception($"Calling _cat/plugins did not result in an OK response {GetResponseException(catPlugins)}");

			var installedPlugins = GetResponseString(catPlugins).Split(new [] {'\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();

			var missingPlugins = requestPlugins.Except(installedPlugins).ToList();
			if (!missingPlugins.Any()) return;

			var missingString = string.Join(", ", missingPlugins);
			var pluginsString = string.Join(", ", installedPlugins);
			throw new Exception($"Already running elasticsearch missed the following plugin(s): {missingString} currently installed: {pluginsString}.");
		}
	}
}
