using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.XunitPlumbing
{
    /// <summary>
    /// A unit test
    /// </summary>
    [XunitTestCaseDiscoverer("Elastic.Xunit.XunitPlumbing.UnitTestDiscoverer", "Elastic.Xunit")]
	public class U : FactAttribute { }

	public class UnitTestDiscoverer : ElasticTestCaseDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, out string skipReason)
		{
			skipReason = null;
			var runUnitTests = discoveryOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunUnitTests));
			if (!runUnitTests) return true;

			var skipTests = GetAttributes<SkipTestAttributeBase>(testMethod).FirstOrDefault(a=>a.GetNamedArgument<bool>(nameof(SkipTestAttributeBase.Skip)));
			if (skipTests == null) return false;

			skipReason = skipTests.GetNamedArgument<string>(nameof(SkipTestAttributeBase.Reason));
			return true;

		}
	}
}
