using System;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class EnsureJavaHomeEnvironmentVariableIsSet : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
			if (string.IsNullOrWhiteSpace(javaHome))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureJavaHomeEnvironmentVariableIsSet)}}} JAVA_HOME is not SET exiting..");
				throw new Exception("The elasticsearch bat files are resillient to JAVA_HOME not being set, however the shield tooling is not");
			}
			cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureJavaHomeEnvironmentVariableIsSet)}}} JAVA_HOME is set proceeding");

		}
	}
}
