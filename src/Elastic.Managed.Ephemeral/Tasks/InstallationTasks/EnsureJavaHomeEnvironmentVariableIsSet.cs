using System;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class EnsureJavaHomeEnvironmentVariableIsSet : InstallationTaskBase
	{
		public override void Run(EphemeralCluster cluster, INodeFileSystem fs)
		{
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
			if (string.IsNullOrWhiteSpace(javaHome))
				throw new Exception("The elasticsearch bat files are resillient to JAVA_HOME not being set, however the shield tooling is not");
		}
	}
}
