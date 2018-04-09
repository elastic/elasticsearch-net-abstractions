using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	public class ElasticTestFramework : XunitTestFramework
	{
		public ElasticTestFramework(IMessageSink messageSink) : base(messageSink) {  }

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		{
			var a = Assembly.Load(assemblyName);
			var options = a.GetCustomAttributes<ElasticXunitConfigurationAttribute>().FirstOrDefault()?.Options ?? new ElasticXunitRunOptions();


			return new TestFrameworkExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink)
			{
				RunIntegrationTests = options.RunIntegrationTests,
				RunUnitTests = options.RunUnitTests,
				TestFilter = options.TestFilter,
				ClusterFilter = options.ClusterFilter
			};
		}
	}
}
