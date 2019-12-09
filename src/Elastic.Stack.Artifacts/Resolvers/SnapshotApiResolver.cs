using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using Elastic.Stack.Artifacts.Platform;
using Elastic.Stack.Artifacts.Products;
using SemVer;
using Version = SemVer.Version;

namespace Elastic.Stack.Artifacts.Resolvers
{
	public static class SnapshotApiResolver
	{
		public static readonly Lazy<IReadOnlyCollection<Version>> AvailableVersions = new Lazy<IReadOnlyCollection<Version>>(LoadVersions, LazyThreadSafetyMode.ExecutionAndPublication);

		private static Regex PackageProductRegex { get; } = new Regex(@"(.*?)-(\d+\.\d+\.\d+(?:-(?:SNAPSHOT|alpha\d+|beta\d+|rc\d+))?)");

		private static Version IncludeOsMoniker { get; } = new Version("7.0.0");

		public static bool TryResolve(Product product, Version version, OSPlatform os, string filters, out Artifact artifact)
		{
			artifact = null;
			var p = product.SubProduct?.SubProductName ?? product.ProductName;
			var query = p;
			if (product.PlatformDependent && version > product.PlatformSuffixAfter)
				query += $",{OsMonikers.From(os)}";
			else if (product.PlatformDependent)
				query += $",{OsMonikers.CurrentPlatformSearchFilter()}";
			if (!string.IsNullOrWhiteSpace(filters))
				query += $",{filters}";

			var packages = new Dictionary<string, SearchPackage>();
			try
			{
				var json = ApiResolver.FetchJson($"search/{version}/{query}");
				// if packages is empty it turns into an array[] otherwise its a dictionary :/
				packages = JsonSerializer.Deserialize<ArtifactsSearchResponse>(json).Packages;
			}
			catch { }

			if (packages == null || packages.Count == 0) return false;
			var list = packages
				.OrderByDescending(k => k.Value.Classifier == null ? 1 : 0)
				.ToArray();

			var ext = OsMonikers.CurrentPlatformArchiveExtension();
			var shouldEndWith = $"{version}.{ext}";
			if (product.PlatformDependent && version > product.PlatformSuffixAfter)
				shouldEndWith = $"{version}-{OsMonikers.CurrentPlatformPackageSuffix()}.{ext}";
			foreach (var kv in list)
			{
				if (product.PlatformDependent && !kv.Key.EndsWith(shouldEndWith)) continue;


				var tokens = PackageProductRegex.Split(kv.Key).Where(s=>!string.IsNullOrWhiteSpace(s)).ToArray();
				if (tokens.Length < 2) continue;

				if (!tokens[0].Equals(p, StringComparison.CurrentCultureIgnoreCase)) continue;
				if (!tokens[1].Equals(version.ToString(), StringComparison.CurrentCultureIgnoreCase)) continue;
				// https://snapshots.elastic.co/7.4.0-677857dd/downloads/elasticsearch-plugins/analysis-icu/analysis-icu-7.4.0-SNAPSHOT.zip
				var buildHash = ApiResolver.GetBuildHash(kv.Value.DownloadUrl);
				artifact = new Artifact(product, version, kv.Value, buildHash);
			}
			return false;
		}


		private static IReadOnlyCollection<Version> LoadVersions()
		{
			var json = ApiResolver.FetchJson("versions");
			var versions = JsonSerializer.Deserialize<ArtifactsVersionsResponse>(json).Versions;

			return new List<Version>(versions.Select(v => new Version(v)));
		}

		public static Version LatestReleaseOrSnapshot => AvailableVersions.Value.OrderByDescending(v => v).First();

		public static Version LatestSnapshotForMajor(int major)
		{
			var range = new Range($"~{major}");
			return AvailableVersions.Value
				.Reverse()
				.FirstOrDefault(v => v.PreRelease == "SNAPSHOT" && range.IsSatisfied(v.ToString().Replace("-SNAPSHOT", "")));
		}

		public static Version LatestReleaseOrSnapshotForMajor(int major)
		{
			var range = new Range($"~{major}");
			return AvailableVersions.Value
				.Reverse()
				.FirstOrDefault(v => range.IsSatisfied(v.ToString().Replace("-SNAPSHOT", "")));
		}

	}
}
