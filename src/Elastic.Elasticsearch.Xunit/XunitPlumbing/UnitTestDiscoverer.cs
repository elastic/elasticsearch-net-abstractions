// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.XunitPlumbing
{
    /// <summary>
    /// An Xunit unit test
    /// </summary>
    [XunitTestCaseDiscoverer("Elastic.Elasticsearch.Xunit.XunitPlumbing.UnitTestDiscoverer", "Elastic.Elasticsearch.Xunit")]
	public class U : FactAttribute { }

	/// <summary>
	/// A test discoverer used to discover unit tests cases attached
	/// to test methods that are attributed with <see cref="U" /> attribute
	/// </summary>
	public class UnitTestDiscoverer : ElasticTestCaseDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink) { }

		/// <inheritdoc />
		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, out string skipReason)
		{
			skipReason = null;
			var runUnitTests = discoveryOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunUnitTests));
			if (!runUnitTests) return true;

			var skipTests = GetAttributes<SkipTestAttributeBase>(testMethod)
				.FirstOrDefault(a=>a.GetNamedArgument<bool>(nameof(SkipTestAttributeBase.Skip)));

			if (skipTests == null) return false;

			skipReason = skipTests.GetNamedArgument<string>(nameof(SkipTestAttributeBase.Reason));
			return true;
		}
	}
}
