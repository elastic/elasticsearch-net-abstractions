using System;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks.InstallationTasks
{
	public class EnsureJavaHomeEnvironmentVariableIsSet : InstallationTaskBase
	{
		public override void Run(EphimeralClusterBase cluster, INodeFileSystem fs)
		{
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
			if (string.IsNullOrWhiteSpace(javaHome))
				throw new Exception("The elasticsearch bat files are resillient to JAVA_HOME not being set, however the shield tooling is not");
		}
	}
}
