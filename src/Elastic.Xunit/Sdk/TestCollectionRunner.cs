// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	internal class TestCollectionRunner : XunitTestCollectionRunner
	{
		private readonly Dictionary<Type, object> _assemblyFixtureMappings;
		private readonly IMessageSink _diagnosticMessageSink;

		public TestCollectionRunner(Dictionary<Type, object> assemblyFixtureMappings,
			ITestCollection testCollection,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageBus messageBus,
			ITestCaseOrderer testCaseOrderer,
			ExceptionAggregator aggregator,
			CancellationTokenSource cancellationTokenSource)
			: base(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
		{
			this._assemblyFixtureMappings = assemblyFixtureMappings;
			this._diagnosticMessageSink = diagnosticMessageSink;
		}

		protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases)
		{
			// whats this doing exactly??
			var combinedFixtures = new Dictionary<Type, object>(_assemblyFixtureMappings);
			foreach (var kvp in CollectionFixtureMappings)
				combinedFixtures[kvp.Key] = kvp.Value;

			// We've done everything we need, so hand back off to default Xunit implementation for class runner
			return new XunitTestClassRunner(testClass, @class, testCases, _diagnosticMessageSink, MessageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource, combinedFixtures).RunAsync();
		}
	}
}
