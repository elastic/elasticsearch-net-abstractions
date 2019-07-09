using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using SemVer;
using Version = SemVer.Version;

namespace Elastic.Managed.Configuration.Artifacts
{
	public static class ArtifactsResolver
	{
		private const string ArtifactsApiUrl = "https://artifacts-api.elastic.co/v1/";
		
		public static readonly Lazy<IReadOnlyCollection<Version>> AvailableVersions = new Lazy<IReadOnlyCollection<Version>>(LoadVersions, LazyThreadSafetyMode.ExecutionAndPublication);

		private static HttpClient HttpClient { get; } = new HttpClient() { BaseAddress = new Uri(ArtifactsApiUrl) };
		
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
		
	}
}
