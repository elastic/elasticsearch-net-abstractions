using System;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.Ephemeral.Plugins
{
	public class ElasticsearchPlugin
	{
		public static ElasticsearchPlugin DeleteByQuery { get; } = new ElasticsearchPlugin("delete-by-query", version => version < ElasticsearchVersion.From("5.0.0-alpha3"));
		public static ElasticsearchPlugin CloudAzure { get; } = new ElasticsearchPlugin("cloud-azure");
		public static ElasticsearchPlugin MapperAttachment { get; } = new ElasticsearchPlugin("mapper-attachments");
		public static ElasticsearchPlugin MapperMurmur3 { get; } = new ElasticsearchPlugin("mapper-murmur3");
		public static ElasticsearchPlugin XPack { get; } = new ElasticsearchPlugin("x-pack");
		public static ElasticsearchPlugin IngestGeoIp { get; } = new ElasticsearchPlugin("ingest-geoip", version => version >= ElasticsearchVersion.From("5.0.0-alpha3"));
		public static ElasticsearchPlugin IngestAttachment { get; } = new ElasticsearchPlugin("ingest-attachment", version => version >= ElasticsearchVersion.From("5.0.0-alpha3"));
		public static ElasticsearchPlugin AnalysisKuromoji { get; } = new ElasticsearchPlugin("analysis-kuromoju");
		public static ElasticsearchPlugin AnalysisIcu { get; } = new ElasticsearchPlugin("analysis-icu");

		public ElasticsearchPlugin(string moniker, Func<ElasticsearchVersion, bool> isValid = null)
		{
			Moniker = moniker;
			FolderName = moniker;
			_isValid = isValid ?? (v => true);
		}

		private readonly Func<ElasticsearchVersion, bool> _isValid;

		/// <summary>
		/// The moniker the plugin is known by in Elasticsearch e.g what /_cat/plugins will return for it
		/// </summary>
		public string Moniker { get;  }

		/// <summary>
		/// The folder name under /plugins, defaults to moniker
		/// </summary>
		public virtual string FolderName { get; }

		public bool IsValid(ElasticsearchVersion version) => _isValid(version);

		public virtual string DownloadUrl(ElasticsearchVersion version)  => version.DownloadLocations.PluginDownloadUrl(this.Moniker);
	}
}
