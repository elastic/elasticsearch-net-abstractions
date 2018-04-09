using System;
using System.Collections.Generic;
using System.Reflection;
using Elastic.Managed.Configuration;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	internal class TestFrameworkExecutor : XunitTestFrameworkExecutor
	{
		public TestFrameworkExecutor(AssemblyName a, ISourceInformationProvider sip, IMessageSink d) : base(a, sip, d) { }

		public ElasticsearchVersion Version { get; set; }

		public bool RunIntegrationTests { get; set; } = true;
		public bool RunUnitTests { get; set; }
		public string TestFilter { get; set; }
		public string ClusterFilter { get; set; }

		protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink sink, ITestFrameworkExecutionOptions options)
		{
			options.SetValue(nameof(RunIntegrationTests), RunIntegrationTests);
			options.SetValue(nameof(RunUnitTests), RunUnitTests);
			options.SetValue(nameof(TestFilter), TestFilter);
			options.SetValue(nameof(ClusterFilter), ClusterFilter);
			try
			{
				using (var runner = new TestAssemblyRunner(TestAssembly, testCases, DiagnosticMessageSink, sink, options))
				{
					await runner.RunAsync();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
