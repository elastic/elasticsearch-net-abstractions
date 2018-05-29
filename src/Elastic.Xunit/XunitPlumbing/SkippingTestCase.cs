using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.XunitPlumbing
{
	public class SkippingTestCase : TestMethodTestCase, IXunitTestCase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SkippingTestCase"/> class.
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
			CancellationTokenSource cancellationTokenSource)
		{
			return new XunitTestCaseRunner(
				this,
				this.DisplayName,
				this.SkipReason,
				constructorArguments,
				this.TestMethodArguments,
				messageBus,
				aggregator,
				cancellationTokenSource).RunAsync();
		}
	}
}
