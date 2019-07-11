using System.IO;
using Elastic.Managed.ConsoleWriters;
using ProcNet.Std;

namespace Elastic.Managed.Ephemeral.Tasks.AfterNodeStoppedTasks
{
	public class CleanUpDirectoriesAfterNodeStopped : IClusterTeardownTask
	{
		public void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster, bool nodeStarted)
		{
			var fs = cluster.FileSystem;
			var w = cluster.Writer;
			var v = cluster.ClusterConfiguration.Version;
			var a = cluster.ClusterConfiguration.Artifact;
			if (cluster.ClusterConfiguration.NoCleanupAfterNodeStopped)
			{
				w.WriteDiagnostic($"{{{nameof(CleanUpDirectoriesAfterNodeStopped)}}} skipping cleanup as requested on cluster configuration");
				return;
			}


			DeleteDirectory(w, "cluster data", fs.DataPath);
			DeleteDirectory(w, "cluster config", fs.ConfigPath);
			DeleteDirectory(w, "cluster logs", fs.LogsPath);
			DeleteDirectory(w, "repositories", fs.RepositoryPath);
			var efs = fs as EphemeralFileSystem;
			if (!string.IsNullOrWhiteSpace(efs?.TempFolder))
				DeleteDirectory(w, "cluster temp folder", efs.TempFolder);

			if (efs != null)
			{
				var extractedFolder = Path.Combine(fs.LocalFolder, a.FolderInZip);
				if (extractedFolder != fs.ElasticsearchHome)
					DeleteDirectory(w, "ephemeral ES_HOME", fs.ElasticsearchHome);
				//if the node was not started delete the cached extractedFolder
				if (!nodeStarted)
					DeleteDirectory(w, "cached extracted folder - node failed to start", extractedFolder);
			}

			//if the node did not start make sure we delete the cached folder as we can not assume its in a good state
			if (cluster.ClusterConfiguration.CacheEsHomeInstallation && !nodeStarted)
			{
				var cachedEsHomeFolder = Path.Combine(fs.LocalFolder, cluster.GetCacheFolderName());
				DeleteDirectory(w, "cached installation - node failed to start", cachedEsHomeFolder);
			}
		}

		private static void DeleteDirectory(IConsoleLineHandler w, string description, string path)
		{
			if (!Directory.Exists(path)) return;
			w.WriteDiagnostic($"{{{nameof(CleanUpDirectoriesAfterNodeStopped)}}} attempting to delete [{description}]: {{{path}}}");
			Directory.Delete(path, true);
		}

	}
}
