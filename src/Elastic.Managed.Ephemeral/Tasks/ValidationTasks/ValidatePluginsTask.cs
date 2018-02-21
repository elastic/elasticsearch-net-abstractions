using System;
using System.Linq;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;
using Nest;

namespace Elastic.Managed.Ephemeral.Tasks.ValidationTasks
{
	public class ValidatePluginsTask : NodeValidationTaskBase
	{
		public override void Validate(EphemeralClusterBase cluster, INodeFileSystem fs)
		{
			var v = fs.Version;
			//if the version we are running against is a s snapshot version we do not validate plugins
			//because we can not reliably install plugins against snapshots
			if (v.IsSnapshot) return;

			var supported = cluster.RequiredPlugins.Select(p => p.Moniker).ToList();
			if (!supported.Any()) return;

			var checkPlugins = cluster.Client.CatPlugins();

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
