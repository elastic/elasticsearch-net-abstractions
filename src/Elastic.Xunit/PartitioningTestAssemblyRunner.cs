// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit
{
	public interface ITestAssemblyRunnerFactory
	{
		public XunitTestAssemblyRunner Create(
			ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions
		);
	}

	public class PartitioningTestRunnerFactory : ITestAssemblyRunnerFactory
	{
		public XunitTestAssemblyRunner Create(ITestAssembly testAssembly, IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions) =>
			new PartitioningTestAssemblyRunner(testAssembly, testCases, diagnosticMessageSink, executionMessageSink,
				executionOptions);
	}

	public interface ITestPartition : IAsyncLifetime
	{
		public int? MaxConcurrency { get; }
	}

	// ReSharper disable once UnusedTypeParameter
	public interface IPartitionFixture<out TPartition> where TPartition : ITestPartition { }

	public class PartitioningTestAssemblyRunner : PartitioningTestAssemblyRunner<ITestPartition>
	{
		public PartitioningTestAssemblyRunner(
			ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
			: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions,
				typeof(IPartitionFixture<>))
		{
		}

		protected override async Task UseStateAndRun(ITestPartition partition, Func<int?, Task> runGroup)
		{
			await using (partition)
			{
				await partition.InitializeAsync().ConfigureAwait(false);
				await runGroup(partition.MaxConcurrency).ConfigureAwait(false);
			}
		}
	}

	public record TestType
	{
		public Type? Type { get; set; }
	}

	public class TestPartitionGrouping
	{
		public Type? Type { get; set; }
		public ITestCollection Collection { get; set; } = null!;
		public List<IXunitTestCase> TestCases { get; set; } = null!;
	}

	public abstract class PartitioningTestAssemblyRunner<TState> : XunitTestAssemblyRunner
		where TState : class
	{
		private readonly Type _fixtureType;
		private readonly Dictionary<Type, TState> _partitionFixtureInstances = new();

		protected Dictionary<TestType, IEnumerable<TestPartitionGrouping>> Partionings { get; }

		protected PartitioningTestAssemblyRunner(ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions, Type fixtureType)
			: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
		{
			_fixtureType = fixtureType;
			GroupFilter = executionOptions.GetValue<string>(nameof(PartitioningRunOptions.GroupFilter));
			TestFilter = executionOptions.GetValue<string>(nameof(PartitioningRunOptions.TestFilter));

			var testCollections = OrderTestCollections();

			var cases =
				from testCollection in testCollections
				from classes in testCollection.Item2
					.Select(collection => collection.TestMethod.TestClass.Class)
					.Distinct()
				let partition = GetPartitionFixtureType(classes)
				let testcase = new TestPartitionGrouping
				{
					Collection = testCollection.Item1, TestCases = testCollection.Item2, Type = partition
				}
				select testcase;

			Partionings = cases
				.GroupBy(c => c.Type)
				.OrderBy(g => g.Count())
				.ToDictionary(k => new TestType { Type = k.Key }, v => v.Select(g => g));

			// types need to be instantiated ahead of time in order for xunit constructor injection checks.
			foreach (var partitioning in Partionings)
			{
				var partitionType = partitioning.Key.Type;
				if (partitionType == null)
					continue;

				var state = CreatePartitionStateInstance(partitionType);
				if (state != null)
					_partitionFixtureInstances[partitionType] = state;
			}
		}

		//threading guess
		private static int DefaultConcurrency => Environment.ProcessorCount * 4;

		private ConcurrentBag<RunSummary> Summaries { get; } = new();
		public ConcurrentBag<Tuple<string, string>> FailedCollections { get; } = new();
		public Dictionary<string, Stopwatch> ClusterTotals { get; } = new();

		private string GroupFilter { get; }
		private string TestFilter { get; }

		protected abstract Task UseStateAndRun(TState state, Func<int?, Task> runGroup);

		protected override Task<RunSummary> RunTestCollectionAsync(
			IMessageBus b,
			ITestCollection c,
			IEnumerable<IXunitTestCase> t, CancellationTokenSource s)
		{
			var aggregator = new ExceptionAggregator(Aggregator);
			var fixtureObjects = new Dictionary<Type, object>();
			foreach (var kv in _partitionFixtureInstances)
				fixtureObjects.Add(kv.Key, kv.Value);
			var runner = new TestCollectionRunner(fixtureObjects, c, t, DiagnosticMessageSink, b, TestCaseOrderer, aggregator, s);
			return runner.RunAsync();
		}

		protected async Task<RunSummary> RunAllWithoutPartitionFixture(IMessageBus bus, CancellationTokenSource ctx) =>
			await RunWithoutPartitionFixture(Partionings.SelectMany(g => g.Value), bus, ctx).ConfigureAwait(false);

		protected async Task<RunSummary> RunWithoutPartitionFixture(
			IEnumerable<TestPartitionGrouping> partitionTests,
			IMessageBus messageBus, CancellationTokenSource ctx)
		{
			var testFilters = CreateTestFilters(TestFilter);

			await RunPartitionGroupConcurrently(
					partitionTests, DefaultConcurrency, messageBus,
					testFilters, ctx)
				.ConfigureAwait(false);

			return new RunSummary
			{
				Total = Summaries.Sum(s => s.Total),
				Failed = Summaries.Sum(s => s.Failed),
				Skipped = Summaries.Sum(s => s.Skipped)
			};
		}

		protected override async Task<RunSummary> RunTestCollectionsAsync(IMessageBus bus, CancellationTokenSource ctx) =>
			await RunAllTests(bus, ctx).ConfigureAwait(false);

		protected async Task<RunSummary> RunAllTests(IMessageBus messageBus, CancellationTokenSource ctx)
		{
			var testFilters = CreateTestFilters(TestFilter);
			foreach (var partitioning in Partionings)
			{
				var partitionType = partitioning.Key.Type;
				if (partitionType == null)
				{
					var summary =await RunWithoutPartitionFixture(partitioning.Value, messageBus, ctx).ConfigureAwait(false);
					Summaries.Add(summary);
					continue;
				}

				var state = CreatePartitionStateInstance(partitionType);
				if (state == null)
				{
					var testClass = partitioning.Value.Select(g => g.Collection.DisplayName).FirstOrDefault();
					throw new Exception($"{typeof(TState)} did not yield partition state for e.g: {testClass}");
				}

				var type = state.GetType();
				var partitionName = type.Name;
				if (!MatchesGroupFilter(partitionName)) continue;

				var skipReasons = partitioning.Value.SelectMany(g => g.TestCases.Select(t => t.SkipReason)).ToList();
				var allSkipped = skipReasons.All(r => !string.IsNullOrWhiteSpace(r));
				if (allSkipped)
				{
					Summaries.Add(new RunSummary { Total = skipReasons.Count, Skipped = skipReasons.Count });
					continue;
				}

				ClusterTotals.Add(partitionName, Stopwatch.StartNew());

				await UseStateAndRun(state, async (concurrency) =>
				{
					await RunPartitionGroupConcurrently(partitioning.Value, concurrency, messageBus, testFilters, ctx)
						.ConfigureAwait(false);
				}).ConfigureAwait(false);

				ClusterTotals[partitionName].Stop();
			}

			return new RunSummary
			{
				Total = Summaries.Sum(s => s.Total),
				Failed = Summaries.Sum(s => s.Failed),
				Skipped = Summaries.Sum(s => s.Skipped)
			};
		}

		private async Task RunPartitionGroupConcurrently(
			IEnumerable<TestPartitionGrouping> source,
			int? concurrency,
			IMessageBus bus, string[] testFilters, CancellationTokenSource ctx) =>
			await source
				.ForEachAsync(Math.Max(concurrency ?? 0, DefaultConcurrency),
					async g => await ExecutePartitionGrouping(bus, ctx, g, testFilters).ConfigureAwait(false)
				)
				.ConfigureAwait(false);

		private async Task ExecutePartitionGrouping(
			IMessageBus messageBus,
			CancellationTokenSource ctx,
			TestPartitionGrouping g,
			string[] testFilters
		)
		{
			var test = g.Collection.DisplayName.Replace("Test collection for", string.Empty).Trim();
			if (!MatchesATestFilter(test, testFilters)) return;
			try
			{
				var summary = await RunTestCollectionAsync(messageBus, g.Collection, g.TestCases, ctx)
					.ConfigureAwait(false);
				var type = g.Type;
				var partitionName = type?.Name ?? "UNKNOWN";
				if (summary.Failed > 0)
					FailedCollections.Add(Tuple.Create(partitionName, test));
				Summaries.Add(summary);
			}
			catch (TaskCanceledException)
			{
				// TODO: What should happen here?
			}
		}

		private static string[] CreateTestFilters(string? testFilters) =>
			testFilters?.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray()
			?? new string[] { };

		private static bool MatchesATestFilter(string test, IReadOnlyCollection<string> testFilters)
		{
			if (testFilters.Count == 0 || string.IsNullOrWhiteSpace(test)) return true;
			return testFilters
				.Any(filter => test.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		private bool MatchesGroupFilter(string cluster)
		{
			if (string.IsNullOrWhiteSpace(cluster) || string.IsNullOrWhiteSpace(GroupFilter)) return true;
			return GroupFilter
				.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(c => c.Trim())
				.Any(c => cluster.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		private TState? CreatePartitionStateInstance(Type partitionType)
		{
			if (_partitionFixtureInstances.TryGetValue(partitionType, out var partition)) return partition;
			Aggregator.Run(() =>
			{
				var o = Activator.CreateInstance(partitionType);
				partition = o as TState;
			});
			_partitionFixtureInstances.Add(partitionType, partition);
			return partition;
		}

		private Type? GetPartitionFixtureType(ITypeInfo testClass) =>
			GetPartitionFixtureType(testClass, _fixtureType);

		protected static Type? GetPartitionFixtureType(ITypeInfo testClass, Type openGenericState) =>
			testClass.ToRuntimeType().GetInterfaces()
				.Where(i => i.IsGenericType)
				.Where(t => t.GetGenericTypeDefinition() == openGenericState)
				.SelectMany(t => t.GetGenericArguments(), (_, a) => a)
				.FirstOrDefault();
	}
}
