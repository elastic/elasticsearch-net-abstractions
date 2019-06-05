using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Elastic.Managed.Configuration;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Managed.FileSystem;
using ProcNet;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class InstallPlugins : ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			if (cluster.CachingAndCachedHomeExists()) return;

			var v = cluster.ClusterConfiguration.Version;

			//on 2.x we do not support tests requiring plugins for 2.x since we can not reliably install them
			if (v.Major == 2)
			{
				cluster.Writer?.WriteDiagnostic($"{{{nameof(InstallPlugins)}}} skipping install plugins on {{2.x}} version: [{v}]");
				return;
			}

			var fs = cluster.FileSystem;
			var requiredPlugins = cluster.ClusterConfiguration.Plugins;

			if (cluster.ClusterConfiguration.ValidatePluginsToInstall)
			{
				var invalidPlugins = requiredPlugins
					.Where(p => !p.IsValid(v))
					.Select(p => p.Moniker).ToList();
				if (invalidPlugins.Any())
					throw new CleanExitException(
						$"Can not install the following plugins for version {v}: {string.Join(", ", invalidPlugins)} ");
			}

			foreach (var plugin in requiredPlugins)
			{
				var includedByDefault = plugin.IsIncludedOutOfTheBox(v);
				if (includedByDefault)
				{
					cluster.Writer?.WriteDiagnostic($"{{{nameof(Run)}}} SKIP plugin [{plugin.Moniker}] shipped OOTB as of: {{{plugin.ShippedByDefaultAsOf}}}");
					continue;
				}
				var validForCurrentVersion = plugin.IsValid(v);
				if (!validForCurrentVersion)
				{
					cluster.Writer?.WriteDiagnostic($"{{{nameof(Run)}}} SKIP plugin [{plugin.Moniker}] not valid for version: {{{v}}}");
					continue;
				}
				var alreadyInstalled = AlreadyInstalled(fs, plugin.FolderName);
				if (alreadyInstalled)
				{
					cluster.Writer?.WriteDiagnostic($"{{{nameof(Run)}}} SKIP plugin [{plugin.Moniker}] already installed");
					continue;
				}

				cluster.Writer?.WriteDiagnostic($"{{{nameof(Run)}}} attempting install [{plugin.Moniker}] as it's not OOTB: {{{plugin.ShippedByDefaultAsOf}}} and valid for {v}: {{{plugin.IsValid(v)}}}");
				//var installParameter = v.ReleaseState == ReleaseState.Released ? plugin.Moniker : UseHttpPluginLocation(cluster.Writer, fs, plugin, v);
				var installParameter = UseHttpPluginLocation(cluster.Writer, fs, plugin, v);
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
					ExecuteBinary(cluster.ClusterConfiguration, cluster.Writer, "cmd", $"install elasticsearch plugin: {plugin.Moniker}", $"/c CALL {fs.PluginBinary} install --batch", installParameter);
				else
				{
					if (!Directory.Exists(fs.ConfigPath)) Directory.CreateDirectory(fs.ConfigPath);
					ExecuteBinary(cluster.ClusterConfiguration, cluster.Writer, fs.PluginBinary, $"install elasticsearch plugin: {plugin.Moniker}", "install --batch", installParameter);
				}
				
				CopyConfigDirectoryToHomeCacheConfigDirectory(cluster, plugin);
			}


		}

		private static void CopyConfigDirectoryToHomeCacheConfigDirectory(IEphemeralCluster<EphemeralClusterConfiguration> cluster, ElasticsearchPlugin plugin)
		{
			if (plugin.Moniker == "x-pack") return;
			if (!cluster.ClusterConfiguration.CacheEsHomeInstallation) return;
			var fs = cluster.FileSystem;
			var cachedEsHomeFolder = Path.Combine(fs.LocalFolder, cluster.GetCacheFolderName());
			var configTarget = Path.Combine(cachedEsHomeFolder, "config");

			var configPluginPath = Path.Combine(fs.ConfigPath, plugin.Moniker);
			var configPluginPathCached = Path.Combine(configTarget, plugin.Moniker);
			if (!Directory.Exists(configPluginPath) || Directory.Exists(configPluginPathCached)) return;

			Directory.CreateDirectory(configPluginPathCached);
			CopyFolder(configPluginPath, configPluginPathCached);
		}

		private static bool AlreadyInstalled(INodeFileSystem fileSystem, string folderName)
		{
			var pluginFolder = Path.Combine(fileSystem.ElasticsearchHome, "plugins", folderName);
			return Directory.Exists(pluginFolder);
		}

		private static string UseHttpPluginLocation(IConsoleLineWriter writer, INodeFileSystem fileSystem, ElasticsearchPlugin plugin, ElasticsearchVersion v)
		{
			var downloadLocation = Path.Combine(fileSystem.LocalFolder, $"{plugin.FolderName}-{v}.zip");
			DownloadPluginSnapshot(writer, downloadLocation, plugin, v);
			//transform downloadLocation to file uri and use that to install from
			return new Uri(new Uri("file://"), downloadLocation).AbsoluteUri;
		}

		private static void DownloadPluginSnapshot(IConsoleLineWriter writer, string downloadLocation, ElasticsearchPlugin plugin, ElasticsearchVersion v)
		{
			if (File.Exists(downloadLocation)) return;
			var downloadUrl = plugin.DownloadUrl(v);
			writer?.WriteDiagnostic($"{{{nameof(DownloadPluginSnapshot)}}} downloading [{plugin.FolderName}] from {{{downloadUrl}}}");
			try
			{
				DownloadFile(downloadUrl, downloadLocation);
				writer?.WriteDiagnostic($"{{{nameof(DownloadPluginSnapshot)}}} downloaded [{plugin.FolderName}] to {{{downloadLocation}}}");
			}
			catch (Exception)
			{
				writer?.WriteDiagnostic($"{{{nameof(DownloadPluginSnapshot)}}} download failed! [{plugin.FolderName}] from {{{downloadUrl}}}");
				throw;
			}
		}
	}
}
