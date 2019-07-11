using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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
		private const string ArtifactsApiUrl = "https://artifacts-api.elastic.co/v1/";
		
		public static readonly Lazy<IReadOnlyCollection<Version>> AvailableVersions = new Lazy<IReadOnlyCollection<Version>>(LoadVersions, LazyThreadSafetyMode.ExecutionAndPublication);

		private static Regex PackageProductRegex { get; } = new Regex(@"(.*?)-(\d+\.\d+\.\d+(?:-(?:SNAPSHOT|alpha\d+|beta\d+|rc\d+))?)");
		
		public static bool TryResolve(Product product, Version version, OSPlatform os, string filters, out Artifact artifact)
		{
			artifact = null;
			var p = product.SubProduct?.SubProductName ?? product.ProductName; 
			var query = p;
			if (product.PlatformDependant)
				query += $",{OsMonikers.From(os)}";
			if (!string.IsNullOrWhiteSpace(filters)) 
				query += $",{filters}";
			
			var json = FetchJson($"search/{version}/{query}");
			var packages = JsonSerializer.Parse<ArtifactsSearchResponse>(json).Packages;
			if (packages == null || packages.Count == 0) return false;
			foreach (var kv in packages)
			{
				var tokens = PackageProductRegex.Split(kv.Key).Where(s=>!string.IsNullOrWhiteSpace(s)).ToArray();
				if (tokens.Length < 2) continue;
				
				if (!tokens[0].Equals(p, StringComparison.CurrentCultureIgnoreCase)) continue;
				if (!tokens[1].Equals(version.ToString(), StringComparison.CurrentCultureIgnoreCase)) continue;
				
				artifact = new Artifact(product, version, kv.Value);
			}
			return false;
		}
		
		private static IReadOnlyCollection<Version> LoadVersions()
		{
			var json = FetchJson("versions");
			var versions = JsonSerializer.Parse<ArtifactsVersionsResponse>(json).Versions;
			
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

		private static HttpClient HttpClient { get; } = new HttpClient() { BaseAddress = new Uri(ArtifactsApiUrl) };
		private static string FetchJson(string path)
		{
			using (var stream = HttpClient.GetStreamAsync(path).GetAwaiter().GetResult())
			using (var fileStream = new StreamReader(stream))
				return fileStream.ReadToEnd();
		}

		private class ArtifactsVersionsResponse
		{
			[JsonPropertyName("versions")]
			public List<string> Versions { get; set; }
		}
		private class ArtifactsSearchResponse
		{
			[JsonPropertyName("packages")]
			public Dictionary<string, SearchPackage> Packages { get; set; }
		}

		internal class SearchPackage
		{
			[JsonPropertyName("url")] public string DownloadUrl { get; set; }
			[JsonPropertyName("sha_url")] public string ShaUrl { get; set; }
			[JsonPropertyName("asc_url")] public string AscUrl { get; set; }
			[JsonPropertyName("type")] public string Type { get; set; }
			[JsonPropertyName("architecture")] public string Architecture { get; set; }
			[JsonPropertyName("os")] public string OperatingSystem { get; set; }
			[JsonPropertyName("classifier")] public string Classifier { get; set; }
		}
		
	}
}
