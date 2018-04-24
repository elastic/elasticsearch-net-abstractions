using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.Ephemeral.Tasks;
using Elastic.Managed.Ephemeral.Tasks.AfterNodeStoppedTasks;
using Elastic.Managed.Ephemeral.Tasks.InstallationTasks;
using Elastic.Managed.Ephemeral.Tasks.ValidationTasks;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralClusterComposer<TConfiguration> where TConfiguration : EphemeralClusterConfiguration
	{
		public EphemeralClusterComposer(IEphemeralCluster<TConfiguration> cluster)
		{
			this.Cluster = cluster;
		}

		private IEphemeralCluster<TConfiguration> Cluster { get; }

		private static IEnumerable<IClusterComposeTask<TConfiguration>> InstallationTasks { get; } = new List<IClusterComposeTask<TConfiguration>>
		{
			new CreateLocalApplicationDirectory(),
			new EnsureJavaHomeEnvironmentVariableIsSet(),
			new DownloadElasticsearchVersion(),
			new UnzipElasticsearch(),
			new InstallPlugins(),
		};

		private static IEnumerable<IClusterComposeTask<TConfiguration>> BeforeStart { get; } = new List<IClusterComposeTask<TConfiguration>>
		{
			new CreateEphemeralDirectory()
		};

		private static IEnumerable<IClusterComposeTask<TConfiguration>> NodeStoppedTasks { get; } = new List<IClusterComposeTask<TConfiguration>>
		{
			new CleanUpDirectoriesAfterNodeStopped()
		};

		private static IEnumerable<IClusterComposeTask<TConfiguration>> ValidationTasks  { get; } = new List<IClusterComposeTask<TConfiguration>>
		{
			new ValidateRunningVersion(),
			new ValidateLicenseTask(),
			new ValidatePluginsTask(),
			new ValidateClusterStateTask()
		};

		public void OnStop() => Itterate(NodeStoppedTasks, (t, c, fs) => t.Run(c));

		public void Install()
		{
			var tasks = new List<IClusterComposeTask<TConfiguration>>(InstallationTasks);
			if (this.Cluster.ClusterConfiguration.AdditionalInstallationTasks != null)
				tasks.AddRange(this.Cluster.ClusterConfiguration.AdditionalInstallationTasks);

			Itterate(tasks, (t, c, fs) => t.Run(c));

		}

		public void OnBeforeStart() => Itterate(BeforeStart, (t, c, fs) => t.Run(c));

		public void ValidateAfterStart()
		{
			if (this.Cluster.ClusterConfiguration.SkipValidation) return;
			Itterate(ValidationTasks, (t, c, fs) => t.Run(c));
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
