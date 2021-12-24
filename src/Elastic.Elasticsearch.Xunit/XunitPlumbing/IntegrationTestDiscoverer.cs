// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.Sdk;
using Elastic.Stack.ArtifactsApi;
using SemVer;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Enumerable = System.Linq.Enumerable;

namespace Elastic.Elasticsearch.Xunit.XunitPlumbing
{
	/// <summary>
	///     An Xunit test that should be skipped, and a reason why.
	/// </summary>
	public abstract class SkipTestAttributeBase : Attribute
	{
		/// <summary>
		///     Whether the test should be skipped
		/// </summary>
		public abstract bool Skip { get; }

		/// <summary>
		///     The reason why the test should be skipped
		/// </summary>
		public abstract string Reason { get; }
	}

	/// <summary>
	///     An Xunit integration test
	/// </summary>
	[XunitTestCaseDiscoverer("Elastic.Elasticsearch.Xunit.XunitPlumbing.IntegrationTestDiscoverer",
		"Elastic.Elasticsearch.Xunit")]
	public class I : FactAttribute
	{
	}

	/// <summary>
	///     A test discoverer used to discover integration tests cases attached
	///     to test methods that are attributed with <see cref="I" /> attribute
	/// </summary>
	public class IntegrationTestDiscoverer : ElasticTestCaseDiscoverer
	{
		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
		{
		}

		/// <inheritdoc />
		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod,
			out string skipReason)
		{
			skipReason = null;
			var runIntegrationTests =
				discoveryOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunIntegrationTests));
			if (!runIntegrationTests) return true;

			var cluster = TestAssemblyRunner.GetClusterForClass(testMethod.TestClass.Class);
			if (cluster == null)
			{
				skipReason +=
					$"{testMethod.TestClass.Class.Name} does not define a cluster through IClusterFixture or {nameof(IntegrationTestClusterAttribute)}";
				return true;
			}

			var elasticsearchVersion =
				discoveryOptions.GetValue<ElasticVersion>(nameof(ElasticXunitRunOptions.Version));

			// Skip if the version we are testing against is attributed to be skipped do not run the test nameof(SkipVersionAttribute.Ranges)
			var skipVersionAttribute = Enumerable.FirstOrDefault(GetAttributes<SkipVersionAttribute>(testMethod));
			if (skipVersionAttribute != null)
			{
				var skipVersionRanges =
					skipVersionAttribute.GetNamedArgument<IList<SemVer.Range>>(nameof(SkipVersionAttribute.Ranges)) ??
					new List<SemVer.Range>();
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
						// inrange takes prereleases into account
						if (!elasticsearchVersion.InRange(range)) continue;
						skipReason =
							$"{nameof(SkipVersionAttribute)} has range {range} that {elasticsearchVersion} satisfies";
						if (!string.IsNullOrWhiteSpace(reason)) skipReason += $": {reason}";
						return true;
					}
				}
			}

			var skipTests = GetAttributes<SkipTestAttributeBase>(testMethod)
				.FirstOrDefault(a => a.GetNamedArgument<bool>(nameof(SkipTestAttributeBase.Skip)));

			if (skipTests == null) return false;

			skipReason = skipTests.GetNamedArgument<string>(nameof(SkipTestAttributeBase.Reason));
			return true;
		}
	}
}
