using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	public class ElasticTestFramework : XunitTestFramework
	{
		public ElasticTestFramework(IMessageSink messageSink) : base(messageSink) { }

		protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo) =>
			new ElasticTestFrameworkDiscoverer(assemblyInfo, this.SourceInformationProvider, this.DiagnosticMessageSink);

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		{
			var assembly = Assembly.Load(assemblyName);
			var options = assembly.GetCustomAttribute<ElasticXunitConfigurationAttribute>()?.Options ?? new ElasticXunitRunOptions();

			return new TestFrameworkExecutor(assemblyName, this.SourceInformationProvider, this.DiagnosticMessageSink)
			{
				Options = options
			};
		}
	}
}
