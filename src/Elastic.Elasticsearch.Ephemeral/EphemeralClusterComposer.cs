// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Ephemeral.Tasks.AfterNodeStoppedTasks;
using Elastic.Elasticsearch.Ephemeral.Tasks.BeforeStartNodeTasks;
using Elastic.Elasticsearch.Ephemeral.Tasks.BeforeStartNodeTasks.XPack;
using Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks;
using Elastic.Elasticsearch.Ephemeral.Tasks.InstallationTasks.XPack;
using Elastic.Elasticsearch.Ephemeral.Tasks.ValidationTasks;
using Elastic.Elasticsearch.Managed.FileSystem;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Elasticsearch.Ephemeral
{
	public class EphemeralClusterComposerBase
	{
		protected EphemeralClusterComposerBase()
		{
		}

		internal static IEnumerable<IClusterComposeTask> InstallationTasks { get; } = new List<IClusterComposeTask>
		{
			new PrintConfiguration(),
			new CreateLocalApplicationDirectory(),
			new CopyCachedEsInstallation(),
			new EnsureJavaHomeEnvironmentVariableIsSet(),
			new DownloadElasticsearchVersion(),
			new UnzipElasticsearch(),
			new SetElasticsearchBundledJdkJavaHome(),
			new InstallPlugins(),
			new EnsureElasticsearchBatWorksAcrossDrives(),
			new EnsureXPackEnvBinaryExists(),
			new PathXPackInBatFile()
		};

		protected static IEnumerable<IClusterComposeTask> BeforeStart { get; } = new List<IClusterComposeTask>
		{
			new CreateEphemeralDirectory(),
			new EnsureSecurityRealms(),
			new EnsureSecurityRolesFileExists(),
			new EnsureSecurityUsersInDefaultRealmAreAdded(),
			new GenerateCertificatesTask(),
			new AddClientCertificateRoleMappingTask(),
			new CacheElasticsearchInstallation()
		};

		protected static IEnumerable<IClusterTeardownTask> NodeStoppedTasks { get; } = new List<IClusterTeardownTask>
		{
			new CleanUpDirectoriesAfterNodeStopped()
		};

		protected static IEnumerable<IClusterComposeTask> AfterStartedTasks { get; } = new List<IClusterComposeTask>
		{
			new ValidateRunningVersion(),
			new ValidateClusterStateTask(),
			new PostLicenseTask(),
			new ValidateLicenseTask(),
			new ValidatePluginsTask(),
		};
	}


	public class EphemeralClusterComposer<TConfiguration> : EphemeralClusterComposerBase
		where TConfiguration : EphemeralClusterConfiguration
	{
		private readonly object _lock = new object();
		public EphemeralClusterComposer(IEphemeralCluster<TConfiguration> cluster) => Cluster = cluster;

		private IEphemeralCluster<TConfiguration> Cluster { get; }

		private bool NodeStarted { get; set; }

		public void OnStop() => Iterate(NodeStoppedTasks, (t, c, fs) => t.Run(c, NodeStarted), false);

		public void Install() => Iterate(InstallationTasks, (t, c, fs) => t.Run(c));

		public void OnBeforeStart()
		{
			var tasks = new List<IClusterComposeTask>(BeforeStart);
			if (Cluster.ClusterConfiguration.AdditionalBeforeNodeStartedTasks != null)
				tasks.AddRange(Cluster.ClusterConfiguration.AdditionalBeforeNodeStartedTasks);

			if (Cluster.ClusterConfiguration.PrintYamlFilesInConfigFolder)
				tasks.Add(new PrintYamlContents());

			Iterate(tasks, (t, c, fs) => t.Run(c));

			NodeStarted = true;
		}

		public void OnAfterStart()
		{
			if (Cluster.ClusterConfiguration.SkipBuiltInAfterStartTasks) return;
			var tasks = new List<IClusterComposeTask>(AfterStartedTasks);
			if (Cluster.ClusterConfiguration.AdditionalAfterStartedTasks != null)
				tasks.AddRange(Cluster.ClusterConfiguration.AdditionalAfterStartedTasks);
			Iterate(tasks, (t, c, fs) => t.Run(c), false);
		}

		private void Iterate<T>(IEnumerable<T> collection,
			Action<T, IEphemeralCluster<TConfiguration>, INodeFileSystem> act, bool callOnStop = true)
		{
			lock (_lock)
			{
				var cluster = Cluster;
				foreach (var task in collection)
					try
					{
						act(task, cluster, cluster.FileSystem);
					}
					catch (Exception ex)
					{
						if (callOnStop) OnStop();
						cluster.Writer.WriteError($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
						throw;
					}
			}
		}
	}
}
