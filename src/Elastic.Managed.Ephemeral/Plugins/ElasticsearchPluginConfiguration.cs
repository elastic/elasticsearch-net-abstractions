using System;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.Ephemeral.Plugins
{
	public class ElasticsearchPluginConfiguration
	{
		private readonly Func<ElasticsearchVersion, bool> _isValid;

		public ElasticsearchPlugin Plugin { get; }

		/// <summary>
		/// The moniker the plugin is known by in Elasticsearch e.g what /_cat/plugins will return for it
		/// </summary>
		public string Moniker { get;  }

		/// <summary>
		/// The folder name under /plugins, defaults to moniker
		/// </summary>
		public string FolderName { get; }

		public ElasticsearchPluginConfiguration(ElasticsearchPlugin plugin, Func<ElasticsearchVersion, bool> isValid = null)
		{
			Plugin = plugin;
			Moniker = plugin.Moniker();
			FolderName = plugin.Moniker();
			_isValid = isValid ?? (v => true);
		}

		public bool IsValid(ElasticsearchVersion version) => _isValid(version);

		public string DownloadUrl(ElasticsearchVersion version)  => version.DownloadLocations.PluginDownloadUrl(this.Moniker);
	}
}
