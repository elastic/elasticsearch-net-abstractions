using System;
using System.IO;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
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