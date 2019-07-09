using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading;
using System.Xml.Linq;
using Elastic.Managed.Configuration.Artifacts;
using SemVer;
using Version = SemVer.Version;

namespace Elastic.Managed.Configuration
{
	public static class ElasticsearchVersionResolver
	{
		public static readonly string WindowsSuffix = "windows-x86_64";
		public static readonly string LinuxSuffix = "linux-x86_64";
		public static readonly string OsxSuffix = "darwin-x86_64";

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
					version = ArtifactsResolver.LatestReleaseOrSnapshot.ToString();
					zip = $"elasticsearch-{version}.zip";
					state = version.Contains("SNAPSHOT") ? ReleaseState.Snapshot : ReleaseState.Released;
					if (state == ReleaseState.Snapshot)
						localFolder = zip?.Replace(".zip", "").Replace("elasticsearch-", "");
					break;
				case string _ when version.StartsWith("latest-", StringComparison.OrdinalIgnoreCase):
					var major = int.Parse(version.Replace("latest-", ""));
					version = ArtifactsResolver.LatestReleaseOrSnapshotForMajor(major).ToString();
					zip = $"elasticsearch-{version}.zip";
					state = version.Contains("SNAPSHOT") ? ReleaseState.Snapshot : ReleaseState.Released;
					if (state == ReleaseState.Snapshot)
						localFolder = zip?.Replace(".zip", "").Replace("elasticsearch-", "");
					break;
				case string _ when version.EndsWith("-snapshot", StringComparison.OrdinalIgnoreCase):
					state = ReleaseState.Snapshot;
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
	}
}
