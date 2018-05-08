using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Xml.Linq;
using SemVer;
using Version = SemVer.Version;

namespace Elastic.Managed.Configuration
{
	public static class ElasticsearchVersionResolver
	{
		private static readonly Lazy<string> LatestVersion = new Lazy<string>(ResolveLatestVersion, LazyThreadSafetyMode.ExecutionAndPublication);
		private static readonly Lazy<string> LatestSnapshot = new Lazy<string>(ResolveLatestSnapshot, LazyThreadSafetyMode.ExecutionAndPublication);
		private static readonly object Lock = new { };
		private static readonly ConcurrentDictionary<string, string> SnapshotVersions = new ConcurrentDictionary<string, string>();

		/// <summary>
		/// Resolves an elasticsearch version using either (version | version-snapshot | 'latest')
		/// </summary>
		public static ElasticsearchVersion From(string managedVersionString)
		{
			if (string.IsNullOrWhiteSpace(managedVersionString)) return null;
			var version = managedVersionString;
			var zip = $"elasticsearch-{version}.zip";
			ReleaseState state;
			var localFolder = version;

			switch (version)
			{
				case "latest":
					state = ReleaseState.Released;
					version = LatestVersion.Value;
					zip = LatestSnapshot.Value;
					break;
				case string _ when version.EndsWith("-snapshot", StringComparison.OrdinalIgnoreCase):
					state = ReleaseState.Snapshot;
					zip = SnapshotZipFilename(version);
					localFolder = zip?.Replace(".zip", "").Replace("elasticsearch-", "");
					break;
				case string _ when TryParseBuildCandidate(version, out var v, out var gitHash):
					state = ReleaseState.BuildCandidate;
					version = v;
					zip = $"elasticsearch-{version}.zip";
					localFolder = $"{v}-bc+{gitHash}";
					break;
				default:
					state = ReleaseState.Released;
					break;
			}
			return new ElasticsearchVersion(version, zip, state, localFolder);
		}

		public static string SnapshotZipFilename(string version)
		{
			lock (Lock)
			{
				if (SnapshotVersions.TryGetValue(version, out var zipLocation))
					return zipLocation;

				zipLocation = ResolveLatestSnapshotFor(version);
				SnapshotVersions.TryAdd(version, zipLocation);
				return zipLocation;
			}
		}

		public static bool TryParseBuildCandidate(string passedVersion, out string version, out string gitHash)
		{
			version = null;
			gitHash = null;
			var tokens = passedVersion.Split(':');
			if (tokens.Length < 2) return false;
			version = tokens[1].Trim();
			gitHash = tokens[0].Trim();
			return true;
		}

		private static string ResolveLatestSnapshot()
		{
			var version = LatestVersion.Value;
			return ResolveLatestSnapshotFor(version);
		}

		private static string LoadUrl(string url)
		{
			var http = new HttpClient();
			using (var stream = http.GetStreamAsync(new Uri(url)).GetAwaiter().GetResult())
			using (var fileStream = new StreamReader(stream))
				return fileStream.ReadToEnd();
		}

		private static string ResolveLatestSnapshotFor(string version)
		{
			var url = $"{ElasticsearchDownloadLocations.SonaTypeUrl}/{version}/maven-metadata.xml";
			try
			{
				var mavenMetadata = XElement.Parse(LoadUrl(url));
				var snapshot = mavenMetadata.Descendants("versioning").Descendants("snapshot").FirstOrDefault();
				// ReSharper disable PossibleNullReferenceException
				// dont care let it blow up whatevs
				var snapshotTimestamp = snapshot.Descendants("timestamp").FirstOrDefault().Value;
				var snapshotBuildNumber = snapshot.Descendants("buildNumber").FirstOrDefault().Value;
				// ReSharper restore PossibleNullReferenceException
				var identifier = $"{snapshotTimestamp}-{snapshotBuildNumber}";
				var zip = $"elasticsearch-{version.Replace("SNAPSHOT", "")}{identifier}.zip";
				return zip;

			}
			catch (Exception e)
			{
				throw new Exception($"Can not download maven data from {url}", e);
			}
		}

		private static string ResolveLatestVersion()
		{
			var url = $"{ElasticsearchDownloadLocations.SonaTypeUrl}/maven-metadata.xml";
			try
			{
				var versions = XElement.Parse(LoadUrl(url))
					.Descendants("version")
					.Select(n => new Version(n.Value))
					.OrderByDescending(n => n);
				return versions.First().ToString();
			}
			catch (Exception e)
			{
				throw new Exception($"Can not download maven data from {url}", e);
			}
		}

	}
}
