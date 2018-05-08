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
		public abstract string Reason { get; }
	}

	/// <summary> An integration test </summary>
	[XunitTestCaseDiscoverer("Elastic.Xunit.XunitPlumbing.IntegrationTestDiscoverer", "Elastic.Xunit")]
	public class I : FactAttribute { }

	public class IntegrationTestDiscoverer : ElasticTestCaseDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, out string skipReason)
		{
			skipReason = null;
			var runIntegrationTests = discoveryOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunIntegrationTests));
			if (!runIntegrationTests) return true;

			var v = discoveryOptions.GetValue<ElasticsearchVersion>(nameof(ElasticXunitRunOptions.Version));
			//Skip if the version we are testing against is attributed to be skipped do not run the test
			var skipVersionRanges = GetAttribute<SkipVersionAttribute, IList<Range>>(testMethod, nameof(SkipVersionAttribute.Ranges)) ?? new List<Range>();
			if (v == null && skipVersionRanges.Count > 0)
			{
				skipReason = $"{nameof(SkipVersionAttribute)} has ranges defined for this test but no Version has been provided to {nameof(ElasticXunitRunOptions)}";
				return true;
			}
			foreach (var range in skipVersionRanges)
			{
				if (v == null || !range.IsSatisfied(v)) continue;

				skipReason = $"{nameof(SkipVersionAttribute)} has range {range} that {v} satisfies";
				return true;
			}

			var skipTests = GetAttributes<SkipTestAttributeBase>(testMethod).FirstOrDefault(a=>a.GetNamedArgument<bool>(nameof(SkipTestAttributeBase.Skip)));
			if (skipTests == null) return false;

			skipReason = skipTests.GetNamedArgument<string>(nameof(SkipTestAttributeBase.Reason));
			return true;
		}
	}
}
