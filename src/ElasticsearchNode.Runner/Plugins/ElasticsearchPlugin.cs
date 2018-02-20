using System;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.Ephimeral.Plugins
{
	public class ElasticsearchPlugin
	{
		public string Moniker { get; }
		private readonly string _url;
		private readonly OfficialPlugin? _plugin;
		private readonly Func<ElasticsearchVersion, bool> _validForVersion;

		public ElasticsearchPlugin(OfficialPlugin plugin, Func<ElasticsearchVersion, bool> validForVersion = null)
		{
			this._plugin = plugin;
			this._validForVersion = validForVersion;
			this.Moniker = plugin.Moniker();
		}

		public ElasticsearchPlugin(string moniker, string url, Func<ElasticsearchVersion, bool> validForVersion = null)
		{
			this.Moniker = moniker;
			this._url = url;
			this._validForVersion = validForVersion;
		}

		public static implicit operator ElasticsearchPlugin(OfficialPlugin officialPlugin) =>
			new ElasticsearchPlugin(officialPlugin);

		public bool IsValid(ElasticsearchVersion elasticsearchVersion) =>
			this._validForVersion?.Invoke(elasticsearchVersion) ?? true;

		public string SnapshotDownloadUrl(ElasticsearchVersion version)  =>
			$"https://snapshots.elastic.co/downloads/elasticsearch-plugins/{Moniker}/{SnapshotZip(version)}";

		public string SnapshotZip(ElasticsearchVersion version) => $"{Moniker}-{version.Version}.zip";

	}
}