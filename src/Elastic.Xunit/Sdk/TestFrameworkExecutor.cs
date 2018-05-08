using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Managed.Configuration;
using ProcNet;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	internal class TestFrameworkExecutor : XunitTestFrameworkExecutor
	{
		public TestFrameworkExecutor(AssemblyName a, ISourceInformationProvider sip, IMessageSink d) : base(a, sip, d) { }

		public ElasticXunitRunOptions Options { get; set; }

		public override void RunAll(IMessageSink executionMessageSink, ITestFrameworkDiscoveryOptions discoveryOptions, ITestFrameworkExecutionOptions executionOptions)
		{
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.Version), this.Options.Version);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), this.Options.RunIntegrationTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), this.Options.RunUnitTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), this.Options.TestFilter);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), this.Options.ClusterFilter);

			executionOptions.SetValue(nameof(ElasticXunitRunOptions.Version), this.Options.Version);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), this.Options.RunIntegrationTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), this.Options.RunUnitTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), this.Options.TestFilter);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), this.Options.ClusterFilter);

			base.RunAll(executionMessageSink, discoveryOptions, executionOptions);
		}


		public override void RunTests(IEnumerable<ITestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
		{
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.Version), this.Options.Version);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), this.Options.RunIntegrationTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), this.Options.RunUnitTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), this.Options.TestFilter);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), this.Options.ClusterFilter);
			base.RunTests(testCases, executionMessageSink, executionOptions);
		}

		protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink sink, ITestFrameworkExecutionOptions options)
		{
			options.SetValue(nameof(ElasticXunitRunOptions.Version), this.Options.Version);
			options.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), this.Options.RunIntegrationTests);
			options.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), this.Options.RunUnitTests);
			options.SetValue(nameof(ElasticXunitRunOptions.TestFilter), this.Options.TestFilter);
			options.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), this.Options.ClusterFilter);
			try
			{
				using (var runner = new TestAssemblyRunner(TestAssembly, testCases, DiagnosticMessageSink, sink, options))
				{
					this.Options.OnBeforeTestsRun();
					await runner.RunAsync();
					this.Options.OnTestsFinished(runner.ClusterTotals, runner.FailedCollections);
				}
			}
			catch (Exception e)
			{
				if (e is CleanExitException || e is AggregateException ae && ae.Flatten().InnerException is CleanExitException)
				{
					sink.OnMessage(new TestAssemblyCleanupFailure(Enumerable.Empty<ITestCase>(), this.TestAssembly,
						new CleanExitException("Node failed to start", e)));
				}
				else
					sink.OnMessage(new TestAssemblyCleanupFailure(Enumerable.Empty<ITestCase>(), this.TestAssembly, e));
				throw;
			}
		}
	}
}
