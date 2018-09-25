using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Tasks.ValidationTasks;
using Elastic.Xunit.XunitPlumbing;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	internal class TestAssemblyRunner : XunitTestAssemblyRunner
	{
		private readonly Dictionary<Type, IEphemeralCluster<XunitClusterConfiguration>> _assemblyFixtureMappings =
			new Dictionary<Type, IEphemeralCluster<XunitClusterConfiguration>>();

		private readonly List<IGrouping<IEphemeralCluster<XunitClusterConfiguration>, GroupedByCluster>> _grouped;

		public ConcurrentBag<RunSummary> Summaries { get; } = new ConcurrentBag<RunSummary>();
		public ConcurrentBag<Tuple<string, string>> FailedCollections { get; } = new ConcurrentBag<Tuple<string, string>>();
		public Dictionary<string, Stopwatch> ClusterTotals { get; } = new Dictionary<string, Stopwatch>();

		private class GroupedByCluster
		{
			public IEphemeralCluster<XunitClusterConfiguration> Cluster { get; set; }
			public ITestCollection Collection { get; set; }
			public List<IXunitTestCase> TestCases { get; set; }
		}

		public TestAssemblyRunner(ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
			: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
		{
			var tests = OrderTestCollections();
			this.RunIntegrationTests = executionOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunIntegrationTests));
			this.IntegrationTestsMayUseAlreadyRunningNode = executionOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.IntegrationTestsMayUseAlreadyRunningNode));
			this.RunUnitTests = executionOptions.GetValue<bool>(nameof(ElasticXunitRunOptions.RunUnitTests));
			this.TestFilter = executionOptions.GetValue<string>(nameof(ElasticXunitRunOptions.TestFilter));
			this.ClusterFilter = executionOptions.GetValue<string>(nameof(ElasticXunitRunOptions.ClusterFilter));

			//bit side effecty, sets up _assemblyFixtureMappings before possibly letting xunit do its regular concurrency thing
			this._grouped = (from c in tests
							 let cluster = ClusterFixture(c.Item2.First().TestMethod.TestClass)
							 let testcase = new GroupedByCluster { Collection = c.Item1, TestCases = c.Item2, Cluster = cluster }
							 group testcase by testcase.Cluster
							 into g
							 orderby g.Count() descending
							 select g).ToList();
		}

		private bool RunIntegrationTests { get; }
		private bool IntegrationTestsMayUseAlreadyRunningNode { get; }
		private bool RunUnitTests { get; }
		private string TestFilter { get; }
		private string ClusterFilter { get; }

		protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus b, ITestCollection c, IEnumerable<IXunitTestCase> t, CancellationTokenSource s)
		{
			var aggregator = new ExceptionAggregator(Aggregator);
			var fixtureObjects = new Dictionary<Type, object>();
			foreach (var kv in _assemblyFixtureMappings) fixtureObjects.Add(kv.Key, kv.Value);
			return new TestCollectionRunner(fixtureObjects,c, t, DiagnosticMessageSink, b, TestCaseOrderer, aggregator, s)
				.RunAsync();
		}

		protected override async Task<RunSummary> RunTestCollectionsAsync(IMessageBus messageBus, CancellationTokenSource cancellationTokenSource)
		{
			//threading guess
			var defaultMaxConcurrency = Environment.ProcessorCount * 4;

			if (this.RunUnitTests && !this.RunIntegrationTests)
				return await UnitTestPipeline(defaultMaxConcurrency, messageBus, cancellationTokenSource);

			return await IntegrationPipeline(defaultMaxConcurrency, messageBus, cancellationTokenSource);
		}


		private async Task<RunSummary> UnitTestPipeline(int defaultMaxConcurrency, IMessageBus messageBus, CancellationTokenSource ctx)
		{
			//make sure all clusters go in started state (won't actually start clusters in unit test mode)
			//foreach (var g in this._grouped) g.Key?.Start();

			var testFilters = CreateTestFilters(TestFilter);
			await this._grouped.SelectMany(g => g)
				.ForEachAsync(defaultMaxConcurrency, async g => { await RunTestCollections(messageBus, ctx, g, testFilters); });
			//foreach (var g in this._grouped) g.Key?.Dispose();

			return new RunSummary
			{
				Total = this.Summaries.Sum(s => s.Total),
				Failed = this.Summaries.Sum(s => s.Failed),
				Skipped = this.Summaries.Sum(s => s.Skipped)
			};
		}

		private async Task<RunSummary> IntegrationPipeline(int defaultMaxConcurrency, IMessageBus messageBus, CancellationTokenSource ctx)
		{
			var testFilters = CreateTestFilters(TestFilter);
			foreach (var group in this._grouped)
			{
				ElasticXunitRunner.CurrentCluster = @group.Key;
				if (@group.Key == null)
				{
					var testCount = @group.SelectMany(q => q.TestCases).Count();
					Console.WriteLine($" -> Several tests skipped because they have no cluster associated");
					this.Summaries.Add(new RunSummary { Total = testCount, Skipped = testCount});
					continue;
				}

				var type = @group.Key.GetType();
				var clusterName = type.Name.Replace("Cluster", string.Empty) ?? "UNKNOWN";
				if (!this.MatchesClusterFilter(clusterName)) continue;

				var dop= @group.Key.ClusterConfiguration?.MaxConcurrency ?? defaultMaxConcurrency;
				dop = dop <= 0 ? defaultMaxConcurrency : dop;

				var timeout = @group.Key.ClusterConfiguration?.Timeout ?? TimeSpan.FromMinutes(2);

				var skipReasons = @group.SelectMany(g => g.TestCases.Select(t => t.SkipReason)).ToList();
				var allSkipped = skipReasons.All(r=>!string.IsNullOrWhiteSpace(r));
				if (allSkipped)
				{
					Console.WriteLine($" -> All tests from {clusterName} are skipped under the current configuration");
					this.Summaries.Add(new RunSummary { Total = skipReasons.Count, Skipped = skipReasons.Count});
					continue;
				}

				this.ClusterTotals.Add(clusterName, Stopwatch.StartNew());
				bool ValidateRunningVersion()
				{
					try
					{
						var t = new ValidateRunningVersion();
						t.Run(@group.Key);
						return true;
					}
					catch (Exception e)
					{
						return false;
					}
				}

				using (@group.Key)
				{
					if (!this.IntegrationTestsMayUseAlreadyRunningNode || !ValidateRunningVersion())
						@group.Key?.Start(timeout);

					await @group.ForEachAsync(dop, async g => { await RunTestCollections(messageBus, ctx, g, testFilters); });
				}
				this.ClusterTotals[clusterName].Stop();
			}

			return new RunSummary
			{
				Total = this.Summaries.Sum(s => s.Total),
				Failed = this.Summaries.Sum(s => s.Failed),
				Skipped = this.Summaries.Sum(s => s.Skipped)
			};
		}

		private async Task RunTestCollections(IMessageBus messageBus, CancellationTokenSource ctx, GroupedByCluster g, string[] testFilters)
		{
			var test = g.Collection.DisplayName.Replace("Test collection for", string.Empty).Trim();
			if (!MatchesATestFilter(test, testFilters)) return;
			if (testFilters.Length > 0) Console.WriteLine(" -> " + test);

			try
			{
				var summary = await RunTestCollectionAsync(messageBus, g.Collection, g.TestCases, ctx);
				var type = g.Cluster?.GetType();
				var clusterName = type?.Name.Replace("Cluster", "") ?? "UNKNOWN";
				if (summary.Failed > 0)
					this.FailedCollections.Add(Tuple.Create(clusterName, test));
				this.Summaries.Add(summary);
			}
			catch (TaskCanceledException)
			{
				// TODO: What should happen here?
			}
		}

		private static string[] CreateTestFilters(string testFilters) =>
			testFilters?.Split(',').Select(s => s.Trim()).Where(s=>!string.IsNullOrWhiteSpace(s)).ToArray()
			?? new string[] { };

		private static bool MatchesATestFilter(string test, IReadOnlyCollection<string> testFilters)
		{
			if (testFilters.Count == 0 || string.IsNullOrWhiteSpace(test)) return true;
			return testFilters
				.Any(filter => test.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		private bool MatchesClusterFilter(string cluster)
		{
			if (string.IsNullOrWhiteSpace(cluster) || string.IsNullOrWhiteSpace(this.ClusterFilter)) return true;
			return this.ClusterFilter
				.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
				.Select(c => c.Trim())
				.Any(c => cluster.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		private IEphemeralCluster<XunitClusterConfiguration> ClusterFixture(ITestClass testMethodTestClass)
		{
			var clusterType = GetClusterForClass(testMethodTestClass.Class);
			if (clusterType == null) return null;

			if (_assemblyFixtureMappings.TryGetValue(clusterType, out var cluster)) return cluster;
			Aggregator.Run(() =>
			{
				var o = Activator.CreateInstance(clusterType);
				cluster = o as IEphemeralCluster<XunitClusterConfiguration>;
			});
			_assemblyFixtureMappings.Add(clusterType, cluster);
			return cluster;
		}

		public static bool IsAnIntegrationTestClusterType(Type type) =>
			typeof(XunitClusterBase).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo())
			|| IsSubclassOfRawGeneric(typeof(XunitClusterBase<>), type);

		public static Type GetClusterForClass(ITypeInfo testClass) =>
			GetClusterFromClassClusterFixture(testClass) ?? GetClusterFromIntegrationAttribute(testClass);

		private static Type GetClusterFromClassClusterFixture(ITypeInfo testClass) => (
			from i in testClass.Interfaces
			where i.IsGenericType
			from a in i.GetGenericArguments()
			select a.ToRuntimeType()
		).FirstOrDefault(IsAnIntegrationTestClusterType);

		private static Type GetClusterFromIntegrationAttribute(ITypeInfo testClass) =>
			testClass.GetCustomAttributes(typeof(IntegrationTestClusterAttribute))
				.FirstOrDefault()?.GetNamedArgument<Type>(nameof(IntegrationTestClusterAttribute.ClusterType));

		private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
		{
			while (toCheck != null && toCheck != typeof(object))
			{
				var cur = toCheck.GetTypeInfo().IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if (generic == cur)
				{
					return true;
				}

				toCheck = toCheck.GetTypeInfo().BaseType;
			}

			return false;
		}
	}
}
