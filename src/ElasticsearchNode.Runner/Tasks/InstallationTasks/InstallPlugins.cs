using System;
using System.IO;
using System.Linq;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;

namespace Elastic.Net.Abstractions.Tasks.InstallationTasks
{
	public class InstallPlugins : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem, ElasticsearchPlugin[] requiredPlugins)
		{
			var v = config.ElasticsearchVersion;
			//on 2.x we so not support tests requiring plugins for 2.x since we can not reliably install them
			if (v.IsSnapshot && v.Major == 2) return;
			var plugins =
				from plugin in requiredPlugins
				let validForCurrentVersion = plugin.IsValid(v)
				let alreadyInstalled = AlreadyInstalled(plugin, fileSystem)
				where !alreadyInstalled && validForCurrentVersion
				select plugin;

			foreach (var plugin in plugins)
			{
				var installParameter = !v.IsSnapshot ? plugin.Moniker : this.DownloadSnapshotIfNeeded(fileSystem, plugin, v);
				ExecuteBinary(fileSystem.PluginBinary, $"install elasticsearch plugin: {plugin.Moniker}", "install --batch", installParameter);
			}
		}

		private static bool AlreadyInstalled(ElasticsearchPlugin plugin, NodeFileSystem fileSystem)
		{
			var folder = plugin.Moniker;
			var pluginFolder = Path.Combine(fileSystem.ElasticsearchHome, "plugins", folder);

			// assume plugin already installed
			return Directory.Exists(pluginFolder);
		}

		private string DownloadSnapshotIfNeeded(NodeFileSystem fileSystem, ElasticsearchPlugin plugin, ElasticsearchVersion v)
		{
			var downloadLocation = Path.Combine(fileSystem.LocalFolder, plugin.SnapshotZip(v));
			DownloadPluginSnapshot(downloadLocation, plugin, v);
			//transform downloadLocation to file uri and use that to install from
			return new Uri(downloadLocation).AbsoluteUri;
		}

		private static void DownloadPluginSnapshot(string downloadLocation, ElasticsearchPlugin plugin, ElasticsearchVersion v)
		{
			if (File.Exists(downloadLocation)) return;
			var downloadUrl = plugin.SnapshotDownloadUrl(v);
			Console.WriteLine($"Download plugin snapshot {plugin.Moniker}: {downloadUrl}");
			try
			{
				DownloadFile(downloadUrl, downloadLocation);
				Console.WriteLine($"Downloaded plugin snapshot {plugin.Moniker}");
			}
			catch (Exception e)
			{
				Console.WriteLine($"Failed downloading plugin snapshot {plugin.Moniker}, {e.Message}");
			}
		}
	}
}
