using System;
using System.Collections.Generic;
using Elastic.Managed.Ephemeral.Tasks;
using Elastic.Managed.Ephemeral.Tasks.AfterNodeStoppedTasks;
using Elastic.Managed.Ephemeral.Tasks.InstallationTasks;
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
		};

		protected static IEnumerable<IClusterComposeTask> BeforeStart { get; } = new List<IClusterComposeTask>
		{
			new CreateEphemeralDirectory()
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

		public void Install()
		{
			var tasks = new List<IClusterComposeTask>(InstallationTasks);
			if (this.Cluster.ClusterConfiguration.AdditionalInstallationTasks != null)
				tasks.AddRange(this.Cluster.ClusterConfiguration.AdditionalInstallationTasks);

			Itterate(tasks, (t, c, fs) => t.Run(c));
		}

		public void OnBeforeStart() => Itterate(BeforeStart, (t, c, fs) => t.Run(c));

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
