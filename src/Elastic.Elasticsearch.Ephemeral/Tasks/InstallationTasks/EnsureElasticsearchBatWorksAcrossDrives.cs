// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks
{
	/// <summary> Fixes https://github.com/elastic/elasticsearch/issues/29057 </summary>
	public class EnsureElasticsearchBatWorksAcrossDrives : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (cluster.CachingAndCachedHomeExists()) return;

			var config = cluster.ClusterConfiguration;
			if (config.Version < "6.2.0" || config.Version >= "6.3.0")
				return;

			var batFile = cluster.FileSystem.Binary;
			cluster.Writer.WriteDiagnostic($"{{{nameof(EnsureElasticsearchBatWorksAcrossDrives)}}} patching {batFile} according to elastic/elasticsearch#29057");
			var contents = File.ReadAllLines(batFile);
			for (var i = 0; i < contents.Length; i++)
			{
				if (contents[i] != "cd \"%ES_HOME%\"") continue;

				contents[i] = "cd /d \"%ES_HOME%\"";
				break;
			}

			File.WriteAllLines(batFile, contents);
		}
	}
}
