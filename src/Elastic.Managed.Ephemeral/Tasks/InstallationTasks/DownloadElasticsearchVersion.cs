using System;
using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class DownloadElasticsearchVersion : InstallationTaskBase
	{
		public override void Run(EphemeralClusterBase cluster, INodeFileSystem fs)
		{
			var v = fs.Version;
			var from = v.DownloadLocations.ElasticsearchDownloadUrl;
			var to = Path.Combine(fs.LocalFolder, fs.Version.Zip);
			if (File.Exists(to)) return;

			cluster.Writer?.WriteDiagnostic($"{{{nameof(DownloadElasticsearchVersion)}}} downloading Elasticsearch [{v}] from {{{from}}} {{{to}}}");
			Console.WriteLine($"Download elasticsearch: {v} from {from} to {to}");
			DownloadFile(from, to);
			cluster.Writer?.WriteDiagnostic($"{{{nameof(DownloadElasticsearchVersion)}}} downloaded Elasticsearch [{v}] from {{{from}}} {{{to}}}");
		}
	}
}
