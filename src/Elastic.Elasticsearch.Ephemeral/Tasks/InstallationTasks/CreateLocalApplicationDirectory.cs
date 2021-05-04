// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks
{
	public class CreateLocalApplicationDirectory : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var fs = cluster.FileSystem;
			if (Directory.Exists(fs.LocalFolder))
			{
				cluster.Writer?.WriteDiagnostic(
					$"{{{nameof(CreateLocalApplicationDirectory)}}} already exists: {{{fs.LocalFolder}}}");
				return;
			}

			cluster.Writer?.WriteDiagnostic(
				$"{{{nameof(CreateLocalApplicationDirectory)}}} creating {{{fs.LocalFolder}}}");

			Directory.CreateDirectory(fs.LocalFolder);
		}
	}
}
