// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks
{
	public class UnzipElasticsearch : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (cluster.CachingAndCachedHomeExists()) return;

			var fs = cluster.FileSystem;
			var v = cluster.ClusterConfiguration.Version;
			var a = cluster.ClusterConfiguration.Artifact;
			if (Directory.Exists(fs.ElasticsearchHome))
			{
                cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} skipping [{fs.ElasticsearchHome}] already exists");
				return;
			}

			var from = Path.Combine(fs.LocalFolder, a.Archive);
			var extractedFolder = Path.Combine(fs.LocalFolder, a.FolderInZip);
			if (!Directory.Exists(extractedFolder))
			{
                cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} unzipping version [{v}] {{{from}}}");
                Extract(from, fs.LocalFolder);
                
                cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} extracted version [{v}] to {{{fs.LocalFolder}}}");
			}
			
			if (extractedFolder == fs.ElasticsearchHome) return;

			cluster.Writer?.WriteDiagnostic($"{{{nameof(UnzipElasticsearch)}}} Copying extracted folder {{{extractedFolder}}} => {fs.ElasticsearchHome}");
			CopyFolder(extractedFolder, fs.ElasticsearchHome);
		}
	}
}
