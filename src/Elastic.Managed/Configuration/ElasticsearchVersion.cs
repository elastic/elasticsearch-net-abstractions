using System;
using System.Runtime.CompilerServices;
using Version = SemVer.Version;

namespace Elastic.Managed.Configuration
{
	public enum ReleaseState { Released, Snapshot, BuildCandidate }

	public class ElasticsearchVersion : Version
	{
		public static implicit operator ElasticsearchVersion(string version) => ElasticsearchVersionResolver.From(version);

		public static ElasticsearchVersion From(string version) => ElasticsearchVersionResolver.From(version);

		internal ElasticsearchVersion(string version, string zip, ReleaseState state, string localFolder) : base(version)
		{
			this.Version = version;
			this.ReleaseState = state;
			this.Zip = zip;
			this.ExtractFolderName = localFolder;
			this.DownloadLocations = new  ElasticsearchDownloadLocations(this);
		}

		public ElasticsearchDownloadLocations DownloadLocations { get; }

		/// <summary>
		/// The zip file name for this version
		/// </summary>
		public string Zip { get; }

		/// <summary>
		/// Original string representation for this version
		/// </summary>
		public string Version { get; }

		/// <summary>
		/// The release state for this version, e.g is this a Snapshot or not.
		/// </summary>
		public ReleaseState ReleaseState { get; }

		/// <summary>
		/// Returns the version in elasticsearch-{version} format, for SNAPSHOTS this includes a /// datetime suffix
		/// </summary>
		public string FullyQualifiedVersion => this.Zip?.Replace(".zip", "").Replace("elasticsearch-", "");

		/// <summary>
		/// The folder name to expect inside the zip distribution
		/// </summary>
		public string FolderInZip => $"elasticsearch-{this.Version}";

		/// <summary>
		/// The local folder name to extract to, e.g for BC builds rather than extraction to a folder named version this will take the git hash into account
		/// </summary>
		public string ExtractFolderName { get; }

		public override string ToString() => this.Version;

	}
}
