// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Elastic.Elasticsearch.Managed.FileSystem;
using Elastic.Stack.ArtifactsApi;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks.XPack
{
	public class PathXPackInBatFile : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (!cluster.ClusterConfiguration.XPackInstalled) return;

			var config = cluster.ClusterConfiguration;
			var fileSystem = cluster.FileSystem;
			var v = config.Version;

			if (v.Major != 5) return;
			if (!IsWindows) return;

			cluster.Writer?.WriteDiagnostic(
				$"{{{nameof(PathXPackInBatFile)}}} patching x-pack .in.bat to accept CONF_DIR");
			PatchPlugin(v, fileSystem);
		}

		private static void PatchPlugin(ElasticVersion v, INodeFileSystem fileSystem)
		{
			var h = fileSystem.ElasticsearchHome;
			var file = Path.Combine(h, "bin", "x-pack", ".in.bat");
			var contents = File.ReadAllText(file);
			contents = contents.Replace("set ES_PARAMS=-Des.path.home=\"%ES_HOME%\"",
				"set ES_PARAMS=-Des.path.home=\"%ES_HOME%\" -Des.path.conf=\"%CONF_DIR%\"");
			File.WriteAllText(file, contents);
		}
	}
}
