using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.Clusters;
using Elastic.Managed.Ephimeral.Clusters;
using Elastic.Managed.Ephimeral.Tasks.AfterNodeStoppedTasks;
using Elastic.Managed.Ephimeral.Tasks.BeforeStartNodeTasks;
using Elastic.Managed.Ephimeral.Tasks.InstallationTasks;
using Elastic.Managed.Ephimeral.Tasks.ValidationTasks;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephimeral.Tasks
{
	public class EphimeralClusterComposer : IClusterComposer
	{
		private EphimeralClusterBase Cluster { get; }
		private static readonly object Lock = new object();

		public EphimeralClusterComposer(EphimeralClusterBase cluster) => this.Cluster = cluster;

		private static IEnumerable<InstallationTaskBase> InstallationTasks { get; } = new List<InstallationTaskBase>
		{
			new CreateLocalApplicationDirectory(),
			new EnsureJavaHomeEnvironmentVariableIsSet(),
			new DownloadCurrentElasticsearchDistribution(),
			new UnzipCurrentElasticsearchDistribution(),
			new CreateEasyRunBatFile(),
			new InstallPlugins(),
		};
		private static IEnumerable<BeforeStartNodeTaskBase> BeforeStart { get; } = new List<BeforeStartNodeTaskBase>
		{
			new CreateEasyRunClusterBatFile()
		};
		private static IEnumerable<AfterNodeStoppedTaskBase> NodeStoppedTasks { get; } = new List<AfterNodeStoppedTaskBase>
		{
			new CleanUpDirectoriesAfterNodeStopped()
		};

		private static IEnumerable<NodeValidationTaskBase> ValidationTasks { get; } = new List<NodeValidationTaskBase>
		{
			new ValidateRunningVersion(),
			new ValidateLicenseTask(),
			new ValidatePluginsTask(),
			new ValidateClusterStateTask()
		};

		public void OnStop() => Itterate(NodeStoppedTasks, (t, c, fs) => t.Run(c, fs));

		public void Install()=> Itterate(InstallationTasks, (t, c, fs) => t.Run(c, fs));

		public void OnBeforeStart() => Itterate(BeforeStart, (t, c, fs) => t.Run(c, fs), log: false);

		public void ValidateAfterStart() => Itterate(ValidationTasks, (t, c, fs) => t.Validate(c, fs), log: false);

		private IList<string> GetCurrentRunnerLog()
		{
			var file = TaskRunnerLogFile;
			return !File.Exists(file) ? new List<string>() : File.ReadAllLines(file).ToList();
		}

		private string TaskRunnerLogFile => Path.Combine(this.Cluster.FileSystem.LocalFolder, "taskrunner.log");

		private void LogTasks(IEnumerable<string> logs)
		{
			var file = Path.Combine(this.Cluster.FileSystem.LocalFolder, "taskrunner.log");
			File.WriteAllText(file, string.Join(Environment.NewLine, logs));
		}

		private void Itterate<T>(IEnumerable<T> collection, Action<T, EphimeralClusterBase, INodeFileSystem> act, bool log = true)
		{
			lock (EphimeralClusterComposer.Lock)
			{
				var taskLog = this.GetCurrentRunnerLog();
				foreach (var task in collection)
				{
					var name = task.GetType().Name;
					if (log && taskLog.Contains(name)) continue;
					act(task, this.Cluster, this.Cluster.FileSystem);
					if (log) taskLog.Add(name);
				}
				if (log) this.LogTasks(taskLog);
			}
		}



	}
}
