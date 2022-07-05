// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elastic.Stack.ArtifactsApi.Products
{
	/// <summary> An Elasticsearch plugin </summary>
	public class ElasticsearchPlugin : SubProduct
	{
		public ElasticsearchPlugin(string plugin, Func<ElasticVersion, bool> isValid = null,
			Func<ElasticVersion, string> listName = null)
			: base(plugin, isValid, listName)
		{
			PlatformDependent = false;
			PatchDownloadUrl = s =>
			{
				//Temporary correct plugin download urls as reported by the snapshot API as it currently has a bug
				var correct = $"downloads/elasticsearch-plugins/{plugin}";
				return !s.Contains(correct) ? s.Replace("downloads/elasticsearch", correct) : s;
			};
		} // ReSharper disable InconsistentNaming
		public static ElasticsearchPlugin AnalysisIcu { get; } = new ElasticsearchPlugin("analysis-icu");
		public static ElasticsearchPlugin AnalysisKuromoji { get; } = new ElasticsearchPlugin("analysis-kuromoji");
		public static ElasticsearchPlugin AnalysisPhonetic { get; } = new ElasticsearchPlugin("analysis-phonetic");
		public static ElasticsearchPlugin AnalysisSmartCn { get; } = new ElasticsearchPlugin("analysis-smartcn");
		public static ElasticsearchPlugin AnalysisStempel { get; } = new ElasticsearchPlugin("analysis-stempel");
		public static ElasticsearchPlugin AnalysisUkrainian { get; } = new ElasticsearchPlugin("analysis-ukrainian");

		public static ElasticsearchPlugin DiscoveryAzureClassic { get; } =
			new ElasticsearchPlugin("discovery-azure-classic");

		public static ElasticsearchPlugin DiscoveryEC2 { get; } = new ElasticsearchPlugin("discovery-ec2");
		public static ElasticsearchPlugin DiscoveryFile { get; } = new ElasticsearchPlugin("discovery-file");
		public static ElasticsearchPlugin DiscoveryGCE { get; } = new ElasticsearchPlugin("discovery-gce");

		public static ElasticsearchPlugin IngestAttachment { get; } =
			new ElasticsearchPlugin("ingest-attachment", version => version >= "5.0.0-alpha3")
			{
				ShippedByDefaultAsOf = "8.4.0"
			};

		public static ElasticsearchPlugin IngestGeoIp { get; } =
			new ElasticsearchPlugin("ingest-geoip", version => version >= "5.0.0-alpha3")
			{
				ShippedByDefaultAsOf = "6.6.1"
			};

		public static ElasticsearchPlugin IngestUserAgent { get; } =
			new ElasticsearchPlugin("ingest-user-agent", version => version >= "5.0.0-alpha3")
			{
				ShippedByDefaultAsOf = "6.6.1"
			};

		public static ElasticsearchPlugin MapperAttachment { get; } = new ElasticsearchPlugin("mapper-attachments");
		public static ElasticsearchPlugin MapperMurmur3 { get; } = new ElasticsearchPlugin("mapper-murmur3");
		public static ElasticsearchPlugin MapperSize { get; } = new ElasticsearchPlugin("mapper-size");

		public static ElasticsearchPlugin RepositoryAzure { get; } = new ElasticsearchPlugin("repository-azure");
		public static ElasticsearchPlugin RepositoryGCS { get; } = new ElasticsearchPlugin("repository-gcs");
		public static ElasticsearchPlugin RepositoryHDFS { get; } = new ElasticsearchPlugin("repository-hdfs");
		public static ElasticsearchPlugin RepositoryS3 { get; } = new ElasticsearchPlugin("repository-s3");

		public static ElasticsearchPlugin StoreSMB { get; } = new ElasticsearchPlugin("store-smb");

		public static ElasticsearchPlugin DeleteByQuery { get; } =
			new ElasticsearchPlugin("delete-by-query", version => version < "5.0.0-alpha3");

		public static ElasticsearchPlugin XPack { get; } =
			new ElasticsearchPlugin("x-pack", listName: v => v >= "6.2.0" && v < "6.3.0" ? "x-pack-core" : "x-pack",
				isValid: v => v < "6.3.0") {ShippedByDefaultAsOf = "6.3.0"};
	}
}
