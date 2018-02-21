using System.Collections.Generic;
using Elastic.Managed.Configuration;
using Xunit.Abstractions;

namespace Elastic.Xunit.Configuration
{
	public interface ITestConfiguration
	{
		TestMode Mode { get; }
		ElasticsearchVersion ElasticsearchVersion { get; }
		string ClusterFilter { get; }
		string TestFilter { get; }
		bool ForceReseed { get; }
		bool TestAgainstAlreadyRunningElasticsearch { get; }

		int Seed { get; }

		bool RunIntegrationTests { get; }
		bool RunUnitTests { get; }

		RandomConfiguration Random { get; }
	}

	public class RandomConfiguration
	{
		public bool SourceSerializer { get; set; }
		public bool TypedKeys { get; set; }
		public bool OldConnection { get; set; }
	}

	public static class TestConfiguration
	{
		public static IList<string> SeenDeprecations { get; } = new List<string>();
		public static ITestConfiguration Configuration { get; } = new EnvironmentConfiguration();

		private static bool VersionSatisfiedBy(string range, ElasticsearchVersion version)
		{
			var versionRange = new SemVer.Range(range);
			var satisfied = versionRange.IsSatisfied(version.Version);
			if (version.ReleaseState != ReleaseState.Released || satisfied)
				return satisfied;

			//Semver can only match snapshot version with ranges on the same major and minor
			//anything else fails but we want to know e.g 2.4.5-SNAPSHOT satisfied by <5.0.0;
			var wholeVersion = $"{version.Major}.{version.Minor}.{version.Patch}";
			return versionRange.IsSatisfied(wholeVersion);
		}

		public static bool VersionUnderTestSatisfiedBy(string range) =>
			VersionSatisfiedBy(range, Configuration.ElasticsearchVersion);
	}

}
