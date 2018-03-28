using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.Ephemeral.Tasks.AfterNodeStoppedTasks;
using Elastic.Managed.Ephemeral.Tasks.BeforeStartNodeTasks;
using Elastic.Managed.Ephemeral.Tasks.InstallationTasks;
using Elastic.Managed.Ephemeral.Tasks.ValidationTasks;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed.Ephemeral
{
	public static class EphemeralClusterComposer
	{
		private static readonly object Lock = new object();

		private static IEnumerable<InstallationTaskBase> InstallationTasks { get; } = new List<InstallationTaskBase>
		{
			new CreateLocalApplicationDirectory(),
			new EnsureJavaHomeEnvironmentVariableIsSet(),
			new DownloadElasticsearchVersion(),
			new UnzipElasticsearch(),
			new CreateEasyRunBatFile(),
			new InstallPlugins(),
		};

		private static IEnumerable<BeforeStartNodeTaskBase> BeforeStart { get; } = new List<BeforeStartNodeTaskBase>
		{
			new CreateEasyRunClusterBatFile(),
			new CreateEphemeralDirectory()
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

		public static void OnStop(EphemeralCluster cluster) => Itterate(cluster, NodeStoppedTasks, (t, c, fs) => t.Run(c, fs), log: false);

		public static void Install(EphemeralCluster cluster)=> Itterate(cluster, InstallationTasks, (t, c, fs) => t.Run(c, fs));

		public static void OnBeforeStart(EphemeralCluster cluster) => Itterate(cluster, BeforeStart, (t, c, fs) => t.Run(c, fs), log: false);

		public static void ValidateAfterStart(EphemeralCluster cluster) => Itterate(cluster, ValidationTasks, (t, c, fs) => t.Validate(c, fs), log: false);

		private static IList<string> GetCurrentRunnerLog(ClusterBase cluster)
		{
			var file = TaskRunnerLogFile(cluster);
			return !File.Exists(file) ? new List<string>() : File.ReadAllLines(file).ToList();
		}

		private static string TaskRunnerLogFile(ClusterBase cluster) => Path.Combine(cluster.FileSystem.LocalFolder, "taskrunner.log");

		private static void LogTasks(ClusterBase cluster, IEnumerable<string> logs)
		{
			var file = Path.Combine(cluster.FileSystem.LocalFolder, "taskrunner.log");
			File.WriteAllText(file, string.Join(Environment.NewLine, logs));
		}

		private static void Itterate<T>(EphemeralCluster cluster, IEnumerable<T> collection, Action<T, EphemeralCluster, INodeFileSystem> act, bool log = true)
		{
			lock (EphemeralClusterComposer.Lock)
			{
				var taskLog = GetCurrentRunnerLog(cluster);
				foreach (var task in collection)
				{
					var name = task.GetType().Name;
					if (log && taskLog.Contains(name)) continue;
					act(task, cluster, cluster.FileSystem);
					if (log) taskLog.Add(name);
				}
				if (log) LogTasks(cluster, taskLog);
			}
		}



	}
}
