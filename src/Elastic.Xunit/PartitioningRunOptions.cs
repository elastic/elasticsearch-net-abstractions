// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Elastic.Xunit;

/// <summary>
///     The Xunit test runner options
/// </summary>
public class PartitioningRunOptions
{
	/// <summary>
	///     A global test filter that can be used to only run certain tests.
	///     Accepts a comma separated list of filters
	/// </summary>
	public string? TestFilter { get; set; }

	public string? GroupFilter { get; set; }

	/// <summary>
	///     Called when the tests have finished running successfully
	/// </summary>
	/// <param name="partitionTimings">Per cluster timings of the total test time, including starting Elasticsearch</param>
	/// <param name="failedPartitionTests">All collection of failed cluster, failed tests tuples</param>
	public virtual void OnTestsFinished(
		Dictionary<string, Stopwatch> partitionTimings,
		ConcurrentBag<Tuple<string, string>> failedPartitionTests)
	{
	}

	/// <summary>
	///     Called before tests run. An ideal place to perform actions such as writing information to
	///     <see cref="Console" />.
	/// </summary>
	public virtual void OnBeforeTestsRun()
	{
	}

	public virtual void SetOptions(ITestFrameworkDiscoveryOptions discoveryOptions)
	{
		discoveryOptions.SetValue(nameof(GroupFilter), GroupFilter);
		discoveryOptions.SetValue(nameof(TestFilter), TestFilter);
	}

	public virtual void SetOptions(ITestFrameworkExecutionOptions executionOptions)
	{
		executionOptions.SetValue(nameof(GroupFilter), GroupFilter);
		executionOptions.SetValue(nameof(GroupFilter), TestFilter);

	}
}
