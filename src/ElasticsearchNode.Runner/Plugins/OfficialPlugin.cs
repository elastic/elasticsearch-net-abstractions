using System;
using System.Reflection;

namespace Elastic.Managed.Ephimeral.Plugins
{
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
