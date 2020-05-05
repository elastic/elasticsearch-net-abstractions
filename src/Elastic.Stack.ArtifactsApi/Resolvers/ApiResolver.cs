// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Elastic.Stack.ArtifactsApi.Resolvers
{
	public static class ApiResolver
	{
		private const string ArtifactsApiUrl = "https://artifacts-api.elastic.co/v1/";

		private static HttpClient HttpClient { get; } = new HttpClient(new HttpClientHandler
		{
			SslProtocols = SslProtocols.Tls12
		}) {BaseAddress = new Uri(ArtifactsApiUrl)};

		public static string FetchJson(string path)
		{
			using (var stream = HttpClient.GetStreamAsync(path).GetAwaiter().GetResult())
			using (var fileStream = new StreamReader(stream))
				return fileStream.ReadToEnd();
		}

		private static ConcurrentDictionary<string, bool> Releases = new ConcurrentDictionary<string, bool>();
		public static bool IsReleasedVersion(string version)
		{
			if (Releases.TryGetValue(version, out var released)) return released;
			var versionPath = "https://github.com/elastic/elasticsearch/releases/tag/v" + version;
			var message = new HttpRequestMessage
			{
				Method = HttpMethod.Head,
				RequestUri = new Uri(versionPath)
			};

			using (var response = HttpClient.SendAsync(message).GetAwaiter().GetResult())
			{
				released = response.IsSuccessStatusCode;
				Releases.TryAdd(version, released);
				return released;
			}
		}
		public static string LatestBuildHash(string version)
		{
			var json = FetchJson($"search/{version}/msi");
			try
			{
				// if packages is empty it turns into an array[] otherwise its a dictionary :/
				var packages = JsonSerializer.Deserialize<ArtifactsSearchResponse>(json).Packages;
				if (packages.Count == 0)
					throw new Exception("Can not get build hash for: " + version);
				return GetBuildHash(packages.First().Value.DownloadUrl);
			}
			catch
			{
				throw new Exception("Can not get build hash for: " + version);
			}

		}
		private static Regex BuildHashRegex { get; } = new Regex(@"https://(?:snapshots|staging).elastic.co/(\d+\.\d+\.\d+-([^/]+)?)");
		public static string GetBuildHash(string url)
		{
			var tokens = BuildHashRegex.Split(url).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
			if (tokens.Length < 2)
				throw new Exception("Can not parse build hash from: " + url);

			return tokens[1];
		}
	}

	internal class ArtifactsVersionsResponse
	{
		[JsonPropertyName("versions")] public List<string> Versions { get; set; }
	}

	internal class ArtifactsSearchResponse
	{
		[JsonPropertyName("packages")] public Dictionary<string, SearchPackage> Packages { get; set; }
	}

	internal class SearchPackage
	{
		[JsonPropertyName("url")] public string DownloadUrl { get; set; }
		[JsonPropertyName("sha_url")] public string ShaUrl { get; set; }
		[JsonPropertyName("asc_url")] public string AscUrl { get; set; }
		[JsonPropertyName("type")] public string Type { get; set; }
		[JsonPropertyName("architecture")] public string Architecture { get; set; }
		[JsonPropertyName("os")] public string[] OperatingSystem { get; set; }
		[JsonPropertyName("classifier")] public string Classifier { get; set; }
	}
}
