// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Stack.ArtifactsApi;
using Elastic.Xunit;
using Xunit.Abstractions;

namespace Elastic.Elasticsearch.Xunit
{
	/// <summary>
	///     The Xunit test runner options
	/// </summary>
	public class ElasticXunitRunOptions : PartitioningRunOptions
	{
		/// <summary>
		///     Informs the runner whether we expect to run integration tests. Defaults to <c>true</c>
		/// </summary>
		public bool RunIntegrationTests { get; set; } = true;

		/// <summary>
		///     Setting this to true will assume the cluster that is currently running was started for the purpose of these tests
		///     Defaults to <c>false</c>
		/// </summary>
		public bool IntegrationTestsMayUseAlreadyRunningNode { get; set; } = false;

		/// <summary>
		///     Informs the runner whether unit tests will be run. Defaults to <c>false</c>.
		///     If set to <c>true</c> and <see cref="RunIntegrationTests" /> is <c>false</c>, the runner will run all the
		///     tests in parallel with the maximum degree of parallelism
		/// </summary>
		public bool RunUnitTests { get; set; }

		/// <summary>
		///     A global cluster filter that can be used to only run certain cluster's tests.
		///     Accepts a comma separated list of filters
		/// </summary>
		public string ClusterFilter { get => GroupFilter; set => GroupFilter = value;  }

		/// <summary>
		///     Informs the runner what version of Elasticsearch is under test. Required for
		///     <see cref="SkipVersionAttribute" /> to kick in
		/// </summary>
		public ElasticVersion Version { get; set; }

		public override void SetOptions(ITestFrameworkDiscoveryOptions discoveryOptions)
		{
			base.SetOptions(discoveryOptions);
			discoveryOptions.SetValue(nameof(Version), Version);
			discoveryOptions.SetValue(nameof(RunIntegrationTests), RunIntegrationTests);
			discoveryOptions.SetValue(
				nameof(IntegrationTestsMayUseAlreadyRunningNode),
				IntegrationTestsMayUseAlreadyRunningNode
			);
			discoveryOptions.SetValue(nameof(RunUnitTests), RunUnitTests);
			discoveryOptions.SetValue(nameof(TestFilter), TestFilter);
			discoveryOptions.SetValue(nameof(ClusterFilter), ClusterFilter);
		}

		public override void SetOptions(ITestFrameworkExecutionOptions executionOptions)
		{

			base.SetOptions(executionOptions);
			executionOptions.SetValue(nameof(Version), Version);
			executionOptions.SetValue(nameof(RunIntegrationTests), RunIntegrationTests);
			executionOptions.SetValue(
				nameof(IntegrationTestsMayUseAlreadyRunningNode),
				IntegrationTestsMayUseAlreadyRunningNode
			);
			executionOptions.SetValue(nameof(RunUnitTests), RunUnitTests);
			executionOptions.SetValue(nameof(TestFilter), TestFilter);
			executionOptions.SetValue(nameof(ClusterFilter), ClusterFilter);

		}
	}
}
