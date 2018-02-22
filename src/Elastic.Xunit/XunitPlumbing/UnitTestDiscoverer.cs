using Elastic.Xunit.Configuration;
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

	public class UnitTestDiscoverer : ElasticTestDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			return !TestConfiguration.Configuration.RunUnitTests;

		}
	}
}
