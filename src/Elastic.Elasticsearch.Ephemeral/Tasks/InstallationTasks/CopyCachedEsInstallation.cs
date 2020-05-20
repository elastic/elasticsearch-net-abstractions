// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks
{
	public class CopyCachedEsInstallation : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.CacheEsHomeInstallation) return;

			var fs = cluster.FileSystem;
			var cachedEsHomeFolder = Path.Combine(fs.LocalFolder, cluster.GetCacheFolderName());
			if (!Directory.Exists(cachedEsHomeFolder)) return;

			var source = cachedEsHomeFolder;
			var target = fs.ElasticsearchHome;
			cluster.Writer?.WriteDiagnostic($"{{{nameof(CopyCachedEsInstallation)}}} using cached ES_HOME {{{source}}} and copying it to [{target}]");
			CopyFolder(source, target);
		}
	}
}
