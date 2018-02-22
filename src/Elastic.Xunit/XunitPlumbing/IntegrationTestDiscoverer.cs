using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.Configuration;
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
			if (!TestConfiguration.Configuration.RunIntegrationTests) return true;

			//Skip if the version we are testing against is attributed to be skipped do not run the test
			var skipVersionRange = GetAttribute<SkipVersionAttribute, IList<Range>>(testMethod, nameof(SkipVersionAttribute.Ranges));
			if (skipVersionRange != null) return TestConfiguration.VersionUnderTestSatisfiedBy(skipVersionRange.ToString());

			var skipTests = GetAttributes<SkipTestAttributeBase, bool>(testMethod, nameof(SkipVersionAttribute.Ranges));
			return skipTests.Any(skip => skip);

		}
	}
}
