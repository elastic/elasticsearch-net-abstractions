using System;
using System.IO;
using System.Runtime.InteropServices;
using Version = SemVer.Version;

namespace Elastic.Managed.Configuration
{
	public enum ReleaseState { Released, Snapshot, BuildCandidate }

	public class ElasticsearchVersion : Version, IComparable<string>
	{
		public static implicit operator ElasticsearchVersion(string version) => ElasticsearchVersionResolver.From(version);

		public static ElasticsearchVersion From(string version) => ElasticsearchVersionResolver.From(version);

		public static bool TryFrom(string version, out ElasticsearchVersion v)
		{
			v = null;
			try
			{
				v = ElasticsearchVersionResolver.From(version);
			}
			catch
			{
				// ignored
			}

			return v != null;
		}

		internal ElasticsearchVersion(string version, string zip, ReleaseState state, string localFolder) : base(version)
		{
			this.Version = version;
			this.ReleaseState = state;
			this.Zip = zip;
			this.ExtractFolderName = localFolder;
			//after 7 OS suffix is added e.g: elasticsearch-7.0.0-beta1-windows-x86_64.zip
			if (this.Major >= 7)
			{
				this.Zip = this.Zip.Replace(".zip", $"-{OsSuffix()}.{OsFileType()}");
				
			}
			this.DownloadLocations = new  ElasticsearchDownloadLocations(this);
		}
		
		private static string OsFileType()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "zip";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "tar.gz";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return "tar.gz";
			
			throw new Exception($"{RuntimeInformation.OSDescription} is currently not supported please open an issue @elastic/elasticsearch-net-abstractions");
		}
		
		private static string OsSuffix()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return ElasticsearchVersionResolver.WindowsSuffix;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return ElasticsearchVersionResolver.OsxSuffix;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return ElasticsearchVersionResolver.LinuxSuffix;
			
			throw new Exception($"{RuntimeInformation.OSDescription} is currently not supported please open an issue @elastic/elasticsearch-net-abstractions");
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
		public string FullyQualifiedVersion => this.Zip
			?.Replace(".zip", "")
			?.Replace(".tar.gz", "")
			.Replace("elasticsearch-", "");

		/// <summary>
		/// The folder name to expect inside the zip distribution
		/// </summary>
		public string FolderInZip => $"elasticsearch-{this.Version}";

		/// <summary>
		/// The local folder name to extract to, e.g for BC builds rather than extraction to a folder named version this will take the git hash into account
		/// </summary>
		public string ExtractFolderName { get; }

		public override string ToString() => this.Version;

		public bool InRange(string range)
		{
			var versionRange = new SemVer.Range(range);
			var satisfied = versionRange.IsSatisfied(this.Version);
			if (this.ReleaseState != ReleaseState.Released || satisfied) return satisfied;

			//Semver can only match snapshot version with ranges on the same major and minor
			//anything else fails but we want to know e.g 2.4.5-SNAPSHOT satisfied by <5.0.0;
			var wholeVersion = $"{this.Major}.{this.Minor}.{this.Patch}";
			return versionRange.IsSatisfied(wholeVersion);
		}

		public int CompareTo(string other)
		{
			var v = (ElasticsearchVersion) other;
			return this.CompareTo(v);
		}

		public static bool operator <(ElasticsearchVersion first, string second) => first < (ElasticsearchVersion) second;
		public static bool operator >(ElasticsearchVersion first, string second) => first > (ElasticsearchVersion) second;

		public static bool operator <(string first, ElasticsearchVersion second) => (ElasticsearchVersion)first < second;
		public static bool operator >(string first, ElasticsearchVersion second) => (ElasticsearchVersion)first > second;

		public static bool operator <=(ElasticsearchVersion first, string second) => first <= (ElasticsearchVersion) second;
		public static bool operator >=(ElasticsearchVersion first, string second) => first >= (ElasticsearchVersion) second;

		public static bool operator <=(string first, ElasticsearchVersion second) => (ElasticsearchVersion)first <= second;
		public static bool operator >=(string first, ElasticsearchVersion second) => (ElasticsearchVersion)first >= second;

		public static bool operator ==(ElasticsearchVersion first, string second) => first == (ElasticsearchVersion) second;
		public static bool operator !=(ElasticsearchVersion first, string second) => first != (ElasticsearchVersion) second;


		public static bool operator ==(string first, ElasticsearchVersion second) => (ElasticsearchVersion)first == second;
		public static bool operator !=(string first, ElasticsearchVersion second) => (ElasticsearchVersion)first != second;

		// ReSharper disable once UnusedMember.Local
		private bool Equals(ElasticsearchVersion other) => base.Equals(other);

		public override bool Equals(object obj) => base.Equals(obj);

		public override int GetHashCode() => base.GetHashCode();

	}
}
