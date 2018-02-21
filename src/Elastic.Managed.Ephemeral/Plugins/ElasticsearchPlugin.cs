using System;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.Ephemeral.Plugins
{
	public class ElasticsearchPlugin
	{
		public string Moniker { get; }
		private readonly Func<ElasticsearchVersion, bool> _validForVersion;

		public ElasticsearchPlugin(OfficialPlugin plugin, Func<ElasticsearchVersion, bool> validForVersion = null)
		{
			this._validForVersion = validForVersion;
			this.Moniker = plugin.Moniker();
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
