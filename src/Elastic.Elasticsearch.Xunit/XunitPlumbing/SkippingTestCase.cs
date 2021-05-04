// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.XunitPlumbing
{
	public class SkippingTestCase : TestMethodTestCase, IXunitTestCase
	{
		/// <summary>Used for de-serialization.</summary>
		public SkippingTestCase()
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="SkippingTestCase" /> class.
		/// </summary>
		/// <param name="testMethod">The test method this test case belongs to.</param>
		/// <param name="testMethodArguments">The arguments for the test method.</param>
		public SkippingTestCase(string skipReason, ITestMethod testMethod, object[] testMethodArguments = null)
			: base(TestMethodDisplay.ClassAndMethod, testMethod, testMethodArguments) =>
			SkipReason = skipReason ?? "skipped";

		/// <inheritdoc />
		public Task<RunSummary> RunAsync(
			IMessageSink diagnosticMessageSink,
			IMessageBus messageBus,
			object[] constructorArguments,
			ExceptionAggregator aggregator,
			CancellationTokenSource cancellationTokenSource) =>
			new XunitTestCaseRunner(
				this,
				DisplayName,
				SkipReason,
				constructorArguments,
				TestMethodArguments,
				messageBus,
				aggregator,
				cancellationTokenSource).RunAsync();
	}
}
