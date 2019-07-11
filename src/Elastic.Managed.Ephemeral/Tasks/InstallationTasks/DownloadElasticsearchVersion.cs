using System;
using System.IO;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;
using Elastic.Stack.Artifacts.Products;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class DownloadElasticsearchVersion : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (cluster.CachingAndCachedHomeExists()) return;

			var fs = cluster.FileSystem;
			var v = cluster.ClusterConfiguration.Version;
			var a = cluster.ClusterConfiguration.Artifact;
			var from = v.Artifact(Product.Elasticsearch).DownloadUrl;
			var to = Path.Combine(fs.LocalFolder, a.Archive);
			if (File.Exists(to))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(DownloadElasticsearchVersion)}}} {v} was already downloaded");
				return;
			}

			cluster.Writer?.WriteDiagnostic($"{{{nameof(DownloadElasticsearchVersion)}}} downloading Elasticsearch [{v}] from {{{from}}} {{{to}}}");
			DownloadFile(from, to);
			cluster.Writer?.WriteDiagnostic($"{{{nameof(DownloadElasticsearchVersion)}}} downloaded Elasticsearch [{v}] from {{{from}}} {{{to}}}");
		}
	}
}
