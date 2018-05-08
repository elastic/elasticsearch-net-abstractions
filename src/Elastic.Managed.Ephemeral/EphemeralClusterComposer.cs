using System;
using System.Collections.Generic;
using Elastic.Managed.Ephemeral.Tasks;
using Elastic.Managed.Ephemeral.Tasks.AfterNodeStoppedTasks;
using Elastic.Managed.Ephemeral.Tasks.InstallationTasks;
using Elastic.Managed.Ephemeral.Tasks.InstallationTasks.XPack;
using Elastic.Managed.Ephemeral.Tasks.ValidationTasks;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralClusterComposerBase
	{
		protected EphemeralClusterComposerBase() { }

		protected static IEnumerable<IClusterComposeTask> InstallationTasks { get; } = new List<IClusterComposeTask>
		{
			new CreateLocalApplicationDirectory(),
			new EnsureJavaHomeEnvironmentVariableIsSet(),
			new DownloadElasticsearchVersion(),
			new UnzipElasticsearch(),
			new InstallPlugins(),

			new EnsureElasticsearchBatWorksAcrossDrives(),
			new EnsureXPackEnvBinaryExists(),
		};

		protected static IEnumerable<IClusterComposeTask> BeforeStart { get; } = new List<IClusterComposeTask>
		{
			new CreateEphemeralDirectory(),
			new EnsureSecurityRealms(),
			new EnsureSecurityRolesFileExists(),

			new EnsureSecurityUsersInDefaultRealmAreAdded(),
			new GenerateCertificatesTask()
		};

		protected static IEnumerable<IClusterComposeTask> NodeStoppedTasks { get; } = new List<IClusterComposeTask>
		{
			new CleanUpDirectoriesAfterNodeStopped()
		};

		protected static IEnumerable<IClusterComposeTask> AfterStartedTasks  { get; } = new List<IClusterComposeTask>
		{
			new ValidateRunningVersion(),
			new PostLicenseTask(),
			new ValidateLicenseTask(),
			new ValidatePluginsTask(),
			new ValidateClusterStateTask()
		};
	}


	public class EphemeralClusterComposer<TConfiguration> : EphemeralClusterComposerBase
		where TConfiguration : EphemeralClusterConfiguration
	{
		public EphemeralClusterComposer(IEphemeralCluster<TConfiguration> cluster) => this.Cluster = cluster;

		private IEphemeralCluster<TConfiguration> Cluster { get; }

		public void OnStop() => Itterate(NodeStoppedTasks, (t, c, fs) => t.Run(c));

		public void Install() => Itterate(InstallationTasks, (t, c, fs) => t.Run(c));

		public void OnBeforeStart()
		{
			var tasks = new List<IClusterComposeTask>(BeforeStart);
			if (this.Cluster.ClusterConfiguration.AdditionalBeforeNodeStartedTasks != null)
				tasks.AddRange(this.Cluster.ClusterConfiguration.AdditionalBeforeNodeStartedTasks);

			Itterate(tasks, (t, c, fs) => t.Run(c));
		}


		public void OnAfterStart()
		{
			if (this.Cluster.ClusterConfiguration.SkipBuiltInAfterStartTasks) return;
			var tasks = new List<IClusterComposeTask>(AfterStartedTasks);
			if (this.Cluster.ClusterConfiguration.AdditionalAfterStartedTasks != null)
				tasks.AddRange(this.Cluster.ClusterConfiguration.AdditionalAfterStartedTasks);
			Itterate(tasks, (t, c, fs) => t.Run(c));
		}

		private readonly object _lock = new object();
		private void Itterate<T>(IEnumerable<T> collection, Action<T, IEphemeralCluster<TConfiguration>, INodeFileSystem> act)
			where T : IClusterComposeTask
		{
			lock (_lock)
			{
				var cluster = this.Cluster;
				foreach (var task in collection)
				{
					act(task, cluster, cluster.FileSystem);
				}
			}
		}
	}
}
