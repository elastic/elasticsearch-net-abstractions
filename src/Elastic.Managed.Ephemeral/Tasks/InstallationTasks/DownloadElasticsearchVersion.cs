using System;
using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class DownloadElasticsearchVersion : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var fs = cluster.FileSystem;
			var v = cluster.ClusterConfiguration.Version;
			var from = v.DownloadLocations.ElasticsearchDownloadUrl;
			var to = Path.Combine(fs.LocalFolder, v.Zip);
			if (File.Exists(to)) return;

			cluster.Writer?.WriteDiagnostic($"{{{nameof(DownloadElasticsearchVersion)}}} downloading Elasticsearch [{v}] from {{{from}}} {{{to}}}");
			DownloadFile(from, to);
			cluster.Writer?.WriteDiagnostic($"{{{nameof(DownloadElasticsearchVersion)}}} downloaded Elasticsearch [{v}] from {{{from}}} {{{to}}}");
		}
	}
}
