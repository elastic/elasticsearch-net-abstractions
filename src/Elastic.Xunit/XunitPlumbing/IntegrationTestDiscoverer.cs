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

	public class IntegrationTestDiscoverer : ElasticTestCaseDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var runIntegrationTests = discoveryOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunIntegrationTests));
			if (!runIntegrationTests) return true;

			var v = discoveryOptions.GetValue<ElasticsearchVersion>(nameof(TestFrameworkExecutor.Version));

			//Skip if the version we are testing against is attributed to be skipped do not run the test
			var skipVersionRanges = GetAttribute<SkipVersionAttribute, IList<Range>>(testMethod, nameof(SkipVersionAttribute.Ranges));
			if (skipVersionRanges != null && skipVersionRanges.Any(range => range.IsSatisfied(v))) return true;

			var skipTests = GetAttributes<SkipTestAttributeBase, bool>(testMethod, nameof(SkipTestAttributeBase.Skip));
			return skipTests.Any(skip => skip);
		}
	}
}
