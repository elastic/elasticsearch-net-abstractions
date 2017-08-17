using System;
using System.Reflection;

namespace Elastic.Net.Abstractions.Plugins
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

	public enum OfficialPlugin
	{
		[Moniker("delete-by-query")] DeleteByQuery,
		[Moniker("cloud-azure")] CloudAzure,
		[Moniker("mapper-attachments")] MapperAttachments,
		[Moniker("mapper-murmur3")] MapperMurmer3,
		[Moniker("x-pack")] XPack,
		[Moniker("ingest-geoip")] IngestGeoIp,
		[Moniker("ingest-attachment")] IngestAttachment,
		[Moniker("analysis-kuromoji")] AnalysisKuromoji,
		[Moniker("analysis-icu")] AnalysisIcu
	}
	public static class ElasticsearchPluginExtensions
	{
		public static string Moniker(this OfficialPlugin plugin)
		{
			var info = typeof(OfficialPlugin).GetField(plugin.ToString());
			var da = info.GetCustomAttribute<MonikerAttribute>();

			if (da == null) throw new InvalidOperationException($"{plugin} plugin must have a {nameof(MonikerAttribute)}");
			return da.Moniker;
		}
	}
}
