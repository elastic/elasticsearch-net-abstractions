// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class EnsureJavaHomeEnvironmentVariableIsSet : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var fs = cluster.FileSystem;
			var v = cluster.ClusterConfiguration.Version;
			var a = cluster.ClusterConfiguration.Artifact;
			
			var java8Home = Environment.GetEnvironmentVariable("JAVA8_HOME");
			if (cluster.ClusterConfiguration.Version < "6.0.0" && !string.IsNullOrWhiteSpace(java8Home))
			{
				//EnvironmentVariableTarget.Process only using this overload
				Environment.SetEnvironmentVariable("JAVA_HOME", java8Home);
				cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureJavaHomeEnvironmentVariableIsSet)}}} Forcing [JAVA8_HOME] as [JAVA_HOME] since we are on Elasticsearch <6.0.0");
			}
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
			//7.0.0 ships with its own JDK
			if (cluster.ClusterConfiguration.Version < "7.0.0" && string.IsNullOrWhiteSpace(javaHome))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureJavaHomeEnvironmentVariableIsSet)}}} [JAVA_HOME] is not SET exiting..");
				throw new Exception("The elasticsearch bat files are resillient to JAVA_HOME not being set, however the shield tooling is not");
			}
			var cachedEsHomeFolder = Path.Combine(fs.LocalFolder, cluster.GetCacheFolderName());
			var jdkFolder = Path.Combine(cachedEsHomeFolder, "jdk");
			if (Directory.Exists(jdkFolder))
			{
				//prefer bundled jdk
				cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureJavaHomeEnvironmentVariableIsSet)}}} [JAVA_HOME] is set to bundled jdk: {{{jdkFolder}}} ");
				Environment.SetEnvironmentVariable("JAVA_HOME", jdkFolder);
				
			}
			else if (cluster.ClusterConfiguration.Version >= "7.0.0" && !string.IsNullOrWhiteSpace(javaHome))
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureJavaHomeEnvironmentVariableIsSet)}}} [JAVA_HOME] is set, unsetting for process to prefer bundled jdk..");
				Environment.SetEnvironmentVariable("JAVA_HOME", null);
			}

			else cluster.Writer?.WriteDiagnostic($"{{{nameof(EnsureJavaHomeEnvironmentVariableIsSet)}}} JAVA_HOME is set proceeding or using default JDK");

		}
	}
}
