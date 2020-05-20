// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks
{
	public class SetElasticsearchBundledJdkJavaHome : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			
			var fs = cluster.FileSystem;
			var jdkFolder = Path.Combine(fs.ElasticsearchHome, "jdk");
			if (Directory.Exists(jdkFolder))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(SetElasticsearchBundledJdkJavaHome)}}} [JAVA_HOME] is set to bundled jdk: {{{jdkFolder}}} ");
				Environment.SetEnvironmentVariable("JAVA_HOME", jdkFolder);
			}
			else 
				cluster.Writer?.WriteDiagnostic($"{{{nameof(SetElasticsearchBundledJdkJavaHome)}}} [No bundled jdk found] looked in: {{{jdkFolder}}} ");
		}
	}
}