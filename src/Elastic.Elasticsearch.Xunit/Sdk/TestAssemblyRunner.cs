// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Tasks.ValidationTasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nullean.Xunit.Partitions.Sdk;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.Sdk
{

	public class TestAssemblyRunnerFactory : ITestAssemblyRunnerFactory
	{
		public XunitTestAssemblyRunner Create(ITestAssembly testAssembly, IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions) =>
			new TestAssemblyRunner(testAssembly, testCases, diagnosticMessageSink, executionMessageSink,
				executionOptions);
	}
	internal class TestAssemblyRunner : PartitionTestAssemblyRunner<IEphemeralCluster<XunitClusterConfiguration>>
	{
		public TestAssemblyRunner(ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
			: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions, typeof(IClusterFixture<>))
		{
			RunIntegrationTests = executionOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunIntegrationTests));
			RunUnitTests = executionOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunUnitTests));
			IntegrationTestsMayUseAlreadyRunningNode =
				executionOptions.GetValue<bool>(nameof(ElasticXunitRunOptions
					.IntegrationTestsMayUseAlreadyRunningNode));
		}

		private bool RunIntegrationTests { get; }
		private bool IntegrationTestsMayUseAlreadyRunningNode { get; }
		private bool RunUnitTests { get; }

		protected override async Task<RunSummary> RunTestCollectionsAsync(IMessageBus bus,
			CancellationTokenSource cancellationTokenSource)
		{
			if (RunUnitTests && !RunIntegrationTests)
				return await RunAllWithoutPartitionFixture(bus, cancellationTokenSource)
					.ConfigureAwait(false);

			return await RunAllTests(bus, cancellationTokenSource)
				.ConfigureAwait(false);
		}

		protected override async Task UseStateAndRun(
			IEphemeralCluster<XunitClusterConfiguration> cluster,
			Func<int?, Task> runGroup,
			Func<Exception, string, Task> failAll
		)
		{
			using (cluster)
			{
				ElasticXunitRunner.CurrentCluster = cluster;
				var clusterConfiguration = cluster.ClusterConfiguration;
				var timeout = clusterConfiguration?.Timeout ?? TimeSpan.FromMinutes(2);

				var started = false;
				try
				{
					if (!IntegrationTestsMayUseAlreadyRunningNode || !ValidateRunningVersion(cluster))
						 cluster.Start(timeout);

					started = true;
				}
				catch (Exception e)
				{
					await failAll(e, $"Further logs might be available at: {cluster.ClusterConfiguration?.FileSystem?.LogsPath}")
						.ConfigureAwait(false);
				}
				if (started)
					await runGroup(clusterConfiguration?.MaxConcurrency).ConfigureAwait(false);
			}
		}

		private static bool ValidateRunningVersion(IEphemeralCluster<XunitClusterConfiguration> cluster)
		{
			try
			{
				var t = new ValidateRunningVersion();
				t.Run(cluster);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static Type GetClusterFixtureType(ITypeInfo testClass) =>
			GetPartitionFixtureType(testClass, typeof(IClusterFixture<>));


	}
}
