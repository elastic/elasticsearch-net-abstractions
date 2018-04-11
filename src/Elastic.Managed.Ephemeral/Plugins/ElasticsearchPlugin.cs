using System.Collections.Generic;

namespace Elastic.Managed.Ephemeral.Plugins
{
	public class ElasticsearchPlugins : List<ElasticsearchPlugin>
	{
		public static ElasticsearchPlugins Supported { get; } =
			new ElasticsearchPlugins
			{
				ElasticsearchPlugin.AnalysisIcu,
				ElasticsearchPlugin.AnalysisKuromoji,
				ElasticsearchPlugin.CloudAzure,
				ElasticsearchPlugin.DeleteByQuery,
				ElasticsearchPlugin.IngestAttachment,
				ElasticsearchPlugin.IngestGeoIp,
				ElasticsearchPlugin.MapperAttachment,
				ElasticsearchPlugin.MapperMurmur3,
				ElasticsearchPlugin.XPack,
			};
	}
}
