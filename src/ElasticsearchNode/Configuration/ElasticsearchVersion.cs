using Version = SemVer.Version;

namespace Elastic.ManagedNode.Configuration
{
	public class ElasticsearchVersion : Version
	{
		public static implicit operator ElasticsearchVersion(string version) => ElasticsearchVersionResolver.From(version);

		public static ElasticsearchVersion From(string version) => ElasticsearchVersionResolver.From(version);
		internal ElasticsearchVersion(string version, string zip) : base(version)
		{
			this.Version = version;
			this.Zip = zip;
		}

		private string _downloadUrl;
		public string DownloadUrl
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this._downloadUrl)) return this._downloadUrl;
				var rootUrl = ElasticsearchVersionResolver.RootUrl(this);
                this._downloadUrl = this.IsSnapshot
	                ? $"{rootUrl}/{this.Version}/{this.Zip}"
	                : $"{rootUrl}/{this.Zip}";
				return this._downloadUrl;

			}
		}
		public string Zip { get; }
		public string Version { get; }

		/// <summary>
		/// Returns the version in elasticsearch-{version} format, for SNAPSHOTS this includes a
		/// datetime suffix
		/// </summary>
		public string FullyQualifiedVersion => this.Zip?.Replace(".zip", "").Replace("elasticsearch-", "");

		/// <summary>
		/// The folder name to expect inside the zip distribution
		/// </summary>
		public string FolderInZip => $"elasticsearch-{this.Version}";

		/// <summary>
		/// Whether this version is a snapshot or officicially released distribution
		/// </summary>
		public bool IsSnapshot => this.Version?.ToLower().Contains("snapshot") ?? false;

		public override string ToString() => this.Version;

	}
}
