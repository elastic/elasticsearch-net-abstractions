using SemVer;

namespace Elastic.Managed.Configuration
{
	public class ElasticsearchDownloadLocations
	{
		private const string ArtifactsHost = "https://artifacts.elastic.co";
		private const string SnapshotsHost = "https://snapshots.elastic.co";
		private const string StagingUrlFormat = "https://staging.elastic.co/{0}-{1}";
		internal const string SonaTypeUrl = "https://oss.sonatype.org/content/repositories/snapshots/org/elasticsearch/distribution/zip/elasticsearch";

		private ElasticsearchVersion Version { get; }
		public string ElasticsearchDownloadUrl { get; }

		public ElasticsearchDownloadLocations(ElasticsearchVersion version)
		{
			this.Version = version;

			switch (this.Version.ReleaseState)
			{
				case ReleaseState.Released:
					var downloadPath = Range.IsSatisfied("<5.0.0", version.Version)
						? "https://download.elasticsearch.org/elasticsearch/release/org/elasticsearch/distribution/zip/elasticsearch"
						: $"{ArtifactsHost}/downloads/elasticsearch";
					this.ElasticsearchDownloadUrl = $"{downloadPath}/{this.Version}/{this.Version.Zip}";
					break;
				case ReleaseState.Snapshot:
					this.ElasticsearchDownloadUrl = $"{SonaTypeUrl}/{version}/{this.Version.Zip}";
					break;
				case ReleaseState.BuildCandidate when ElasticsearchVersionResolver.TryParseBuildCandidate(version.Version, out var v, out var gitHash):
					var stagingRoot = string.Format(StagingUrlFormat, v, gitHash);
					this.ElasticsearchDownloadUrl = $"{stagingRoot}/downloads/elasticsearch/{this.Version.Zip}";
					break;
			}
		}

		public string PluginDownloadUrl(string moniker)
		{
			var zip = $"{moniker}-{this.Version}.zip";
			switch (this.Version.ReleaseState)
			{
				case ReleaseState.Snapshot: return $"{SnapshotsHost}/downloads/elasticsearch-plugins/{moniker}/{zip}";
				case ReleaseState.BuildCandidate:
					return $"{ArtifactsHost}/downloads/elasticsearch-plugins/{moniker}/{zip}";
			}
			return moniker;
		}

	}
}
