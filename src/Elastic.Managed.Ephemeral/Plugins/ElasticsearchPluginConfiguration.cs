using System;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.Ephemeral.Plugins
{
	/// <summary>
	/// An Elasticsearch plugin
	/// </summary>
	public class ElasticsearchPlugin
	{

		// ReSharper disable InconsistentNaming
		public static ElasticsearchPlugin AnalysisIcu { get; } = new ElasticsearchPlugin("analysis-icu");
		public static ElasticsearchPlugin AnalysisKuromoji { get; } = new ElasticsearchPlugin("analysis-kuromoji");
		public static ElasticsearchPlugin AnalysisPhonetic { get; } = new ElasticsearchPlugin("analysis-phonetic");
		public static ElasticsearchPlugin AnalysisSmartCn { get; } = new ElasticsearchPlugin("analysis-smartcn");
		public static ElasticsearchPlugin AnalysisStempel { get; } = new ElasticsearchPlugin("analysis-stempel");
		public static ElasticsearchPlugin AnalysisUkrainian { get; } = new ElasticsearchPlugin("analysis-ukrainian");

		public static ElasticsearchPlugin DiscoveryAzureClassic { get; } = new ElasticsearchPlugin("discovery-azure-classic");
		public static ElasticsearchPlugin DiscoveryEC2 { get; } = new ElasticsearchPlugin("discovery-ec2");
		public static ElasticsearchPlugin DiscoveryFile { get; } = new ElasticsearchPlugin("discovery-file");
		public static ElasticsearchPlugin DiscoveryGCE { get; } = new ElasticsearchPlugin("discovery-gce");

		public static ElasticsearchPlugin IngestAttachment { get; } = new ElasticsearchPlugin("ingest-attachment", version => version >= "5.0.0-alpha3");
		public static ElasticsearchPlugin IngestGeoIp { get; } = new ElasticsearchPlugin("ingest-geoip", version => version >= "5.0.0-alpha3")
		{
			ShippedByDefaultAsOf = "7.0.0-beta1"
		};
		public static ElasticsearchPlugin IngestUserAgent { get; } = new ElasticsearchPlugin("ingest-geoip", version => version >= "5.0.0-alpha3");

		public static ElasticsearchPlugin MapperAttachment { get; } = new ElasticsearchPlugin("mapper-attachments");
		public static ElasticsearchPlugin MapperMurmur3 { get; } = new ElasticsearchPlugin("mapper-murmur3");
		public static ElasticsearchPlugin MapperSize { get; } = new ElasticsearchPlugin("mapper-size");

		public static ElasticsearchPlugin RepositoryAzure { get; } = new ElasticsearchPlugin("repository-azure");
		public static ElasticsearchPlugin RepositoryGCS { get; } = new ElasticsearchPlugin("repository-gcs");
		public static ElasticsearchPlugin RepositoryHDFS { get; } = new ElasticsearchPlugin("repository-hdfs");
		public static ElasticsearchPlugin RepositoryS3 { get; } = new ElasticsearchPlugin("repository-s3");

		public static ElasticsearchPlugin StoreSMB { get; } = new ElasticsearchPlugin("store-smb");

		public static ElasticsearchPlugin DeleteByQuery { get; } = new ElasticsearchPlugin("delete-by-query", version => version < "5.0.0-alpha3");
		public static ElasticsearchPlugin XPack { get; } =
			new ElasticsearchPlugin("x-pack", listName: v => v >= "6.2.0" && v < "6.3.0" ? "x-pack-core" : "x-pack", isValid: v => v < "6.3.0")
			{
				ShippedByDefaultAsOf = "6.3.0"
			};

		// ReSharper enable InconsistentNaming

		public ElasticsearchPlugin(string moniker, Func<ElasticsearchVersion, bool> isValid = null, Func<ElasticsearchVersion, string> listName = null)
		{
			Moniker = moniker;
			FolderName = moniker;
			_isValid = isValid ?? (v => true);
			_getListedPluginName = listName ?? (v => moniker);
		}

		internal ElasticsearchVersion ShippedByDefaultAsOf { get; set; }
		private readonly Func<ElasticsearchVersion, bool> _isValid;
		private readonly Func<ElasticsearchVersion, string> _getListedPluginName;

		/// <summary> The moniker the plugin is known by in Elasticsearch </summary>
		public string Moniker { get; }

		/// <summary> what name to check under /_cat/plugins for the plugins existence</summary>
		public string ListedPluginName(ElasticsearchVersion version) => _getListedPluginName(version);

		/// <summary> The folder name under /plugins, defaults to the value of <see cref="Moniker"/></summary>
		public virtual string FolderName { get; }

		public bool IsIncludedOutOfTheBox(ElasticsearchVersion version) => ShippedByDefaultAsOf != null && version >= ShippedByDefaultAsOf;

		public bool IsValid(ElasticsearchVersion version) => IsIncludedOutOfTheBox(version) || _isValid(version);

		public virtual string DownloadUrl(ElasticsearchVersion version)  => version.DownloadLocations.PluginDownloadUrl(this.Moniker);
	}
}
