// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using Elastic.Stack.ArtifactsApi.Platform;
using Elastic.Stack.ArtifactsApi.Products;
using Elastic.Stack.ArtifactsApi.Resolvers;
using SemVer;
using Version = SemVer.Version;

namespace Elastic.Stack.ArtifactsApi
{
	public class ElasticVersion : Version, IComparable<string>
	{
		private readonly ConcurrentDictionary<string, Artifact> _resolved = new();

		protected ElasticVersion(string version, ArtifactBuildState state, string buildHash = null) : base(version)
		{
			ArtifactBuildState = state;
			BuildHash = buildHash;
		}

		public ArtifactBuildState ArtifactBuildState { get; }
		private string BuildHash { get; }

		public int CompareTo(string other)
		{
			var v = (ElasticVersion)other;
			return CompareTo(v);
		}

		public Artifact Artifact(Product product)
		{
			var cacheKey = product.ToString();
			if (_resolved.TryGetValue(cacheKey, out var artifact))
				return artifact;
			switch (ArtifactBuildState)
			{
				case ArtifactBuildState.Released:
					ReleasedVersionResolver.TryResolve(product, this, OsMonikers.CurrentPlatform(), out artifact);
					break;
				case ArtifactBuildState.Snapshot:
					SnapshotApiResolver.TryResolve(product, this, OsMonikers.CurrentPlatform(), null, out artifact);
					break;
				case ArtifactBuildState.BuildCandidate:
					StagingVersionResolver.TryResolve(product, this, BuildHash, out artifact);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(ArtifactBuildState),
						$"{ArtifactBuildState} not expected here");
			}

			_resolved.TryAdd(cacheKey, artifact);

			return artifact;
		}


		/// <summary>
		///     Resolves an Elasticsearch version using either (version | version-snapshot | 'latest' | 'latest-MAJORVERSION')
		/// </summary>
		public static ElasticVersion From(string managedVersionString)
		{
			ArtifactBuildState GetReleaseState(string s)
			{
				return s.EndsWith("-SNAPSHOT")
					? ArtifactBuildState.Snapshot
					: ApiResolver.IsReleasedVersion(s)
						? ArtifactBuildState.Released
						: ArtifactBuildState.BuildCandidate;
			}

			if (string.IsNullOrWhiteSpace(managedVersionString))
				return null;

			var version = managedVersionString;
			var state = GetReleaseState(version);
			var buildHash = string.Empty;

			switch (managedVersionString)
			{
				case { } when managedVersionString.StartsWith("latest-", StringComparison.OrdinalIgnoreCase):
					var major = int.Parse(managedVersionString.Replace("latest-", ""));
					version = SnapshotApiResolver.LatestReleaseOrSnapshotForMajor(major).ToString();
					state = GetReleaseState(version);
					if (state == ArtifactBuildState.BuildCandidate)
						buildHash = ApiResolver.LatestBuildHash(version);
					break;
				// When the version is not yet released but contains the alpha label, we treat it in the same way as snapshots so it is resolved correctly
				case { } _ when managedVersionString.EndsWith("-snapshot", StringComparison.OrdinalIgnoreCase)
				                || state != ArtifactBuildState.Released &&
				                managedVersionString.IndexOf("-alpha", StringComparison.OrdinalIgnoreCase) >= 0:
					state = ArtifactBuildState.Snapshot;
					break;
				case { } _ when TryParseBuildCandidate(managedVersionString, out var v, out buildHash):
					state = ArtifactBuildState.BuildCandidate;
					version = v;
					break;
				case "latest":
					version = SnapshotApiResolver.LatestReleaseOrSnapshot.ToString();
					state = GetReleaseState(version);
					break;
			}

			return new ElasticVersion(version, state, buildHash);
		}

		internal static bool TryParseBuildCandidate(string passedVersion, out string version, out string gitHash)
		{
			version = null;
			gitHash = null;
			var tokens = passedVersion.Split(':');
			if (tokens.Length < 2)
				return false;
			version = tokens[1].Trim();
			gitHash = tokens[0].Trim();
			return true;
		}

		public bool InRange(string range)
		{
			var versionRange = new Range(range);
			return InRange(versionRange);
		}

		public bool InRange(Range versionRange)
		{
			var satisfied = versionRange.IsSatisfied(this);
			if (satisfied)
				return true;

			//Semver can only match snapshot version with ranges on the same major and minor
			//anything else fails but we want to know e.g 2.4.5-SNAPSHOT satisfied by <5.0.0;
			var wholeVersion = $"{Major}.{Minor}.{Patch}";
			return versionRange.IsSatisfied(wholeVersion);
		}


		public static implicit operator ElasticVersion(string version) => From(version);

		public static bool operator <(ElasticVersion first, string second) => first < (ElasticVersion)second;
		public static bool operator >(ElasticVersion first, string second) => first > (ElasticVersion)second;

		public static bool operator <(string first, ElasticVersion second) => (ElasticVersion)first < second;
		public static bool operator >(string first, ElasticVersion second) => (ElasticVersion)first > second;

		public static bool operator <=(ElasticVersion first, string second) => first <= (ElasticVersion)second;
		public static bool operator >=(ElasticVersion first, string second) => first >= (ElasticVersion)second;

		public static bool operator <=(string first, ElasticVersion second) => (ElasticVersion)first <= second;
		public static bool operator >=(string first, ElasticVersion second) => (ElasticVersion)first >= second;

		public static bool operator ==(ElasticVersion first, string second) => first == (ElasticVersion)second;
		public static bool operator !=(ElasticVersion first, string second) => first != (ElasticVersion)second;


		public static bool operator ==(string first, ElasticVersion second) => (ElasticVersion)first == second;
		public static bool operator !=(string first, ElasticVersion second) => (ElasticVersion)first != second;

		// ReSharper disable once UnusedMember.Local
		private bool Equals(ElasticVersion other) => base.Equals(other);
		public override bool Equals(object obj) => base.Equals(obj);

		public override int GetHashCode() => base.GetHashCode();
	}
}
