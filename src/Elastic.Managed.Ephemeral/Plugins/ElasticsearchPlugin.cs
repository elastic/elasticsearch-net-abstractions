using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Elastic.Managed.Ephemeral.Plugins
{
	public class ElasticsearchPlugins : ReadOnlyCollection<ElasticsearchPlugin>
	{
		public ElasticsearchPlugins(IList<ElasticsearchPlugin> list) : base(list) { }

		public ElasticsearchPlugins(params ElasticsearchPlugin[] list) : base(list) { }

		public static ElasticsearchPlugins Supported { get; } =
			new ElasticsearchPlugins(new List<ElasticsearchPlugin>
			{
				ElasticsearchPlugin.AnalysisIcu,
				ElasticsearchPlugin.AnalysisKuromoji,
				ElasticsearchPlugin.AnalysisPhonetic,
				ElasticsearchPlugin.AnalysisSmartCn,
				ElasticsearchPlugin.AnalysisStempel,
				ElasticsearchPlugin.AnalysisUkrainian,

				ElasticsearchPlugin.DiscoveryAzureClassic,
				ElasticsearchPlugin.DiscoveryEC2,
				ElasticsearchPlugin.DiscoveryFile,
				ElasticsearchPlugin.DiscoveryGCE,

				ElasticsearchPlugin.IngestAttachment,
				ElasticsearchPlugin.IngestGeoIp,
				ElasticsearchPlugin.IngestUserAgent,

				ElasticsearchPlugin.MapperAttachment,
				ElasticsearchPlugin.MapperMurmur3,
				ElasticsearchPlugin.MapperSize,

				ElasticsearchPlugin.RepositoryAzure,
				ElasticsearchPlugin.RepositoryGCS,
				ElasticsearchPlugin.RepositoryHDFS,
				ElasticsearchPlugin.RepositoryS3,

				ElasticsearchPlugin.StoreSMB,

				ElasticsearchPlugin.DeleteByQuery,
				ElasticsearchPlugin.XPack,
			});

		public override string ToString() => string.Join(", ", this.Items.Select(s => s.Moniker));
	}
}
