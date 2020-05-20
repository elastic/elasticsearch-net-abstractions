// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Managed;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.Sdk
{
	internal class TestFrameworkExecutor : XunitTestFrameworkExecutor
	{
		public TestFrameworkExecutor(AssemblyName a, ISourceInformationProvider sip, IMessageSink d) : base(a, sip, d) { }

		public ElasticXunitRunOptions Options { get; set; }

		public override void RunAll(IMessageSink executionMessageSink, ITestFrameworkDiscoveryOptions discoveryOptions, ITestFrameworkExecutionOptions executionOptions)
		{
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.Version), Options.Version);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), Options.RunIntegrationTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.IntegrationTestsMayUseAlreadyRunningNode), Options.IntegrationTestsMayUseAlreadyRunningNode);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), Options.RunUnitTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), Options.TestFilter);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), Options.ClusterFilter);

			executionOptions.SetValue(nameof(ElasticXunitRunOptions.Version), Options.Version);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), Options.RunIntegrationTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.IntegrationTestsMayUseAlreadyRunningNode), Options.IntegrationTestsMayUseAlreadyRunningNode);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), Options.RunUnitTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), Options.TestFilter);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), Options.ClusterFilter);

			base.RunAll(executionMessageSink, discoveryOptions, executionOptions);
		}


		public override void RunTests(IEnumerable<ITestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
		{
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.Version), Options.Version);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), Options.RunIntegrationTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.IntegrationTestsMayUseAlreadyRunningNode), Options.IntegrationTestsMayUseAlreadyRunningNode);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), Options.RunUnitTests);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), Options.TestFilter);
			executionOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), Options.ClusterFilter);
			base.RunTests(testCases, executionMessageSink, executionOptions);
		}

		protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink sink, ITestFrameworkExecutionOptions options)
		{
			options.SetValue(nameof(ElasticXunitRunOptions.Version), Options.Version);
			options.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), Options.RunIntegrationTests);
			options.SetValue(nameof(ElasticXunitRunOptions.IntegrationTestsMayUseAlreadyRunningNode), Options.IntegrationTestsMayUseAlreadyRunningNode);
			options.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), Options.RunUnitTests);
			options.SetValue(nameof(ElasticXunitRunOptions.TestFilter), Options.TestFilter);
			options.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), Options.ClusterFilter);
			try
			{
				using (var runner = new TestAssemblyRunner(TestAssembly, testCases, DiagnosticMessageSink, sink, options))
				{
					Options.OnBeforeTestsRun();
					await runner.RunAsync().ConfigureAwait(false);
					Options.OnTestsFinished(runner.ClusterTotals, runner.FailedCollections);
				}
			}
			catch (Exception e)
			{
				if (e is ElasticsearchCleanExitException || e is AggregateException ae && ae.Flatten().InnerException is ElasticsearchCleanExitException)
				{
					sink.OnMessage(new TestAssemblyCleanupFailure(Enumerable.Empty<ITestCase>(), TestAssembly,
						new ElasticsearchCleanExitException("Node failed to start", e)));
				}
				else
					sink.OnMessage(new TestAssemblyCleanupFailure(Enumerable.Empty<ITestCase>(), TestAssembly, e));
				throw;
			}
		}
	}
}
