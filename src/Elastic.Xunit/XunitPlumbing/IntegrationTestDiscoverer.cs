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
	/// <summary>
	/// An Xunit test that should be skipped, and a reason why.
	/// </summary>
	public abstract class SkipTestAttributeBase : Attribute
	{
		/// <summary>
		/// Whether the test should be skipped
		/// </summary>
		public abstract bool Skip { get; }

		/// <summary>
		/// The reason why the test should be skipped
		/// </summary>
		public abstract string Reason { get; }
	}

	/// <summary>
	/// An Xunit integration test
	/// </summary>
	[XunitTestCaseDiscoverer("Elastic.Xunit.XunitPlumbing.IntegrationTestDiscoverer", "Elastic.Xunit")]
	public class I : FactAttribute { }

	/// <summary>
	/// A test discoverer used to discover integration tests cases attached
	/// to test methods that are attributed with <see cref="I" /> attribute
	/// </summary>
	public class IntegrationTestDiscoverer : ElasticTestCaseDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink) { }

		/// <inheritdoc />
		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, out string skipReason)
		{
			skipReason = null;
			var runIntegrationTests = discoveryOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunIntegrationTests));
			if (!runIntegrationTests) return true;

			var elasticsearchVersion = discoveryOptions.GetValue<ElasticsearchVersion>(nameof(ElasticXunitRunOptions.Version));

			// Skip if the version we are testing against is attributed to be skipped do not run the test nameof(SkipVersionAttribute.Ranges)
			var skipVersionAttribute = GetAttributes<SkipVersionAttribute>(testMethod).FirstOrDefault();
			if (skipVersionAttribute != null)
			{
				var skipVersionRanges = skipVersionAttribute.GetNamedArgument<IList<Range>>(nameof(SkipVersionAttribute.Reason)) ?? new List<Range>();
				if (elasticsearchVersion == null && skipVersionRanges.Count > 0)
                {
                    skipReason = $"{nameof(SkipVersionAttribute)} has ranges defined for this test but " +
                                 $"no {nameof(ElasticXunitRunOptions.Version)} has been provided to {nameof(ElasticXunitRunOptions)}";
                    return true;
                }

				if (elasticsearchVersion != null)
				{
					var reason = skipVersionAttribute.GetNamedArgument<string>(nameof(SkipVersionAttribute.Reason));
					for (var index = 0; index < skipVersionRanges.Count; index++)
					{
						var range = skipVersionRanges[index];
						if (!range.IsSatisfied(elasticsearchVersion)) continue;
						skipReason = $"{nameof(SkipVersionAttribute)} has range {range} that {elasticsearchVersion} satisfies";
						if (!string.IsNullOrWhiteSpace(reason)) skipReason += $": {reason}";
						return true;
					}
				}
			}

			var skipTests = GetAttributes<SkipTestAttributeBase>(testMethod)
				.FirstOrDefault(a=>a.GetNamedArgument<bool>(nameof(SkipTestAttributeBase.Skip)));

			if (skipTests == null) return false;

			skipReason = skipTests.GetNamedArgument<string>(nameof(SkipTestAttributeBase.Reason));
			return true;
		}
	}
}
