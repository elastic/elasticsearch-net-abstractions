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
		private static readonly object _lock = new { };
		private static readonly ConcurrentDictionary<string, string> SnapshotVersions = new ConcurrentDictionary<string, string>();
		private static readonly string SonaTypeUrl = "https://oss.sonatype.org/content/repositories/snapshots/org/elasticsearch/distribution/zip/elasticsearch";

		/// <summary>
		/// Resolves an elasticsearch version using either (version | version-snapshot | 'latest')
		/// </summary>
		public static ElasticsearchVersion From(string managedVersionString)
		{
			var version = managedVersionString;
			var zip = $"elasticsearch-{version}.zip";
			if (managedVersionString.Equals("latest", StringComparison.OrdinalIgnoreCase))
			{
				version = LatestVersion.Value;
				zip = LatestSnapshot.Value;
			}
			else if (version?.ToLowerInvariant().Contains("snapshot") ?? false)
			{
				lock (_lock)
				{
					if (SnapshotVersions.TryGetValue(version, out var zipLocation))
						zip = zipLocation;
					else
					{
						zipLocation = ResolveLatestSnapshotFor(version);
						SnapshotVersions.TryAdd(version, zipLocation);
						zip = zipLocation;
					}
				}
			}
			return new ElasticsearchVersion(version, zip);
		}

		internal static string RootUrl(ElasticsearchVersion version) => version.IsSnapshot
			? SonaTypeUrl
			: Range.IsSatisfied("<5.0.0", version.Version)
				? "https://download.elasticsearch.org/elasticsearch/release/org/elasticsearch/distribution/zip/elasticsearch"
				: "https://artifacts.elastic.co/downloads/elasticsearch";

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
			var url = $"{SonaTypeUrl}/{version}/maven-metadata.xml";
			try
			{
				var mavenMetadata = XElement.Parse(LoadUrl(url));
				var snapshot = mavenMetadata.Descendants("versioning").Descendants("snapshot").FirstOrDefault();
				var snapshotTimestamp = snapshot.Descendants("timestamp").FirstOrDefault().Value;
				var snapshotBuildNumber = snapshot.Descendants("buildNumber").FirstOrDefault().Value;
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
			var url = $"{SonaTypeUrl}/maven-metadata.xml";
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
