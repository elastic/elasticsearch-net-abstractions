// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit
{
	internal class PartitioningTestFrameworkExecutor<TOptions, TRunnerFactory> : XunitTestFrameworkExecutor
		where TOptions : PartitioningRunOptions, new()
		where TRunnerFactory : ITestAssemblyRunnerFactory, new()
	{
		public PartitioningTestFrameworkExecutor(
			TOptions options,
			AssemblyName a,
			ISourceInformationProvider sip,
			IMessageSink d
		)
			: base(a, sip, d)
		{
			Options = options;
			RunnerFactory = new TRunnerFactory();
		}

		private ITestAssemblyRunnerFactory RunnerFactory { get; }

		private TOptions Options { get; }

		public override void RunAll(
			IMessageSink executionMessageSink,
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestFrameworkExecutionOptions executionOptions
		)
		{
			Options.SetOptions(discoveryOptions);
			Options.SetOptions(executionOptions);

			base.RunAll(executionMessageSink, discoveryOptions, executionOptions);
		}


		public override void RunTests(
			IEnumerable<ITestCase> testCases,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
		{
			Options.SetOptions(executionOptions);
			base.RunTests(testCases, executionMessageSink, executionOptions);
		}

		protected override async void RunTestCases(
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink sink,
			ITestFrameworkExecutionOptions options)
		{
			Options.SetOptions(options);
			try
			{
				using var runner = RunnerFactory.Create(TestAssembly, testCases, DiagnosticMessageSink, sink, options);
				Options.OnBeforeTestsRun();
				await runner.RunAsync().ConfigureAwait(false);
				var r = runner as PartitioningTestAssemblyRunner;
				Options.OnTestsFinished(
					r?.ClusterTotals,
					r?.FailedCollections
				);
			}
			catch (Exception e)
			{
				sink.OnMessage(new TestAssemblyCleanupFailure(Enumerable.Empty<ITestCase>(), TestAssembly, e));
				throw;
			}
		}
	}
}
