using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Managed.Configuration;
using Elastic.Xunit.Sdk;
using SemVer;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.XunitPlumbing
{
	public abstract class SkipTestAttributeBase : Attribute
	{
		public abstract bool Skip { get; }
	}

	/// <summary> An integration test </summary>
	[XunitTestCaseDiscoverer("Elastic.Xunit.XunitPlumbing.IntegrationTestDiscoverer", "Elastic.Xunit")]
	public class I : FactAttribute { }

	public class IntegrationTestDiscoverer : ElasticTestDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var runIntegrationTests = discoveryOptions.GetValue<bool>(nameof(TestFrameworkExecutor.RunIntegrationTests));
			if (runIntegrationTests) return true;
			var v = discoveryOptions.GetValue<ElasticsearchVersion>(nameof(TestFrameworkExecutor.Version));

			//Skip if the version we are testing against is attributed to be skipped do not run the test
			var skipVersionRange = GetAttribute<SkipVersionAttribute, IList<Range>>(testMethod, nameof(SkipVersionAttribute.Ranges));
			if (skipVersionRange != null) return VersionSatisfiedBy(skipVersionRange.ToString(), v);

			var skipTests = GetAttributes<SkipTestAttributeBase, bool>(testMethod, nameof(SkipTestAttributeBase.Skip));
			return skipTests.Any(skip => skip);
		}

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
	}
}
