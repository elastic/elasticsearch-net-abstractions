using System;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
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
