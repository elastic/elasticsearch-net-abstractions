using System.Reflection;
using Elastic.Managed.Configuration;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	public class ElasticTestFramework : XunitTestFramework
	{
		public ElasticTestFramework(IMessageSink messageSink) : base(messageSink) {  }

		public ElasticsearchVersion Version { get; set; }
		public bool RunIntegrationTests { get; set; } = true;
		public bool RunUnitTests { get; set; } = false;
		public string TestFilter { get; set; }
		public string ClusterFilter { get; set; }

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		{
			return new TestFrameworkExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink)
			{
				Version = this.Version,
				RunIntegrationTests = this.RunIntegrationTests,
				RunUnitTests = this.RunUnitTests,
				TestFilter = this.TestFilter,
				ClusterFilter = this.ClusterFilter
			};
		}
	}
}
