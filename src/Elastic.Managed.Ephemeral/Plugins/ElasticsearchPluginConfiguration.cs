using System;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.Ephemeral.Plugins
{
	public class ElasticsearchPlugin
	{
		public static ElasticsearchPlugin DeleteByQuery { get; } = new ElasticsearchPlugin("delete-by-query", version => version < "5.0.0-alpha3");
		public static ElasticsearchPlugin RepositoryAzure { get; } = new ElasticsearchPlugin("repository-azure", version => version >= "6.0.0");
		public static ElasticsearchPlugin MapperAttachment { get; } = new ElasticsearchPlugin("mapper-attachments");
		public static ElasticsearchPlugin MapperMurmur3 { get; } = new ElasticsearchPlugin("mapper-murmur3");
		public static ElasticsearchPlugin XPack { get; } = new ElasticsearchPlugin("x-pack", listName: v => v >= "6.2.0" && v < "6.3.0" ? "x-pack-core" : "x-pack");
		public static ElasticsearchPlugin IngestGeoIp { get; } = new ElasticsearchPlugin("ingest-geoip", version => version >= "5.0.0-alpha3");
		public static ElasticsearchPlugin IngestAttachment { get; } = new ElasticsearchPlugin("ingest-attachment", version => version >= "5.0.0-alpha3");
		public static ElasticsearchPlugin AnalysisKuromoji { get; } = new ElasticsearchPlugin("analysis-kuromoji");
		public static ElasticsearchPlugin AnalysisIcu { get; } = new ElasticsearchPlugin("analysis-icu");

		public ElasticsearchPlugin(string moniker, Func<ElasticsearchVersion, bool> isValid = null, Func<ElasticsearchVersion, string> listName = null)
		{
			Moniker = moniker;
			FolderName = moniker;
			_isValid = isValid ?? (v => true);
			_getListedPluginName = listName ?? (v => moniker);
		}

		private readonly Func<ElasticsearchVersion, bool> _isValid;
		private readonly Func<ElasticsearchVersion, string> _getListedPluginName;

		/// <summary>
		/// The moniker the plugin is known by in Elasticsearch e.g what /_cat/plugins will return for it
		/// </summary>
		public string Moniker { get; }

		public string ListedPluginName(ElasticsearchVersion version) => _getListedPluginName(version);

		/// <summary>
		/// The folder name under /plugins, defaults to moniker
		/// </summary>
		public virtual string FolderName { get; }

		public bool IsValid(ElasticsearchVersion version) => _isValid(version);

		public virtual string DownloadUrl(ElasticsearchVersion version)  => version.DownloadLocations.PluginDownloadUrl(this.Moniker);
	}
}
