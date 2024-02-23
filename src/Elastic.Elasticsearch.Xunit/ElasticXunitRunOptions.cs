// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Runtime.Serialization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Stack.ArtifactsApi;
using Nullean.Xunit.Partitions;
using Xunit.Abstractions;
using static System.StringSplitOptions;

namespace Elastic.Elasticsearch.Xunit
{
	/// <summary>
	///     The Xunit test runner options
	/// </summary>
	public class ElasticXunitRunOptions : PartitionOptions
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
		[Obsolete("Use PartitionFilterRegex instead", false)]
		[IgnoreDataMember]
		public string ClusterFilter
		{
			get => PartitionFilterRegex;
			set
			{
				if (string.IsNullOrWhiteSpace(value)) PartitionFilterRegex = value;
				else
				{
					//attempt at being backwards compatible with old way of filtering
					var re = string.Join("|", value.Split(new[] { ','}, RemoveEmptyEntries).Select(s => s.Trim()));
					PartitionFilterRegex = re;
				}
			}
		}

		/// <summary>
		///     A global test filter that can be used to only run certain cluster's tests.
		///     Accepts a comma separated list of filters
		/// </summary>
		[Obsolete("Use ParitionFilterRegex instead", false)]
		[IgnoreDataMember]
		public string TestFilter
		{
			get => TestFilterRegex;
			set
			{
				if (string.IsNullOrWhiteSpace(value)) TestFilterRegex = value;
				else
				{
					//attempt at being backwards compatible with old way of filtering
					var re = string.Join("|", value.Split(new[] { ','}, RemoveEmptyEntries).Select(s => s.Trim()));
					TestFilterRegex = re;
				}
			}
		}

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
#pragma warning disable CS0618 // Type or member is obsolete
			discoveryOptions.SetValue(nameof(TestFilter), TestFilter);
			discoveryOptions.SetValue(nameof(ClusterFilter), ClusterFilter);
#pragma warning restore CS0618 // Type or member is obsolete
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
#pragma warning disable CS0618 // Type or member is obsolete
			executionOptions.SetValue(nameof(TestFilter), TestFilter);
			executionOptions.SetValue(nameof(ClusterFilter), ClusterFilter);
#pragma warning restore CS0618 // Type or member is obsolete

		}
	}
}
