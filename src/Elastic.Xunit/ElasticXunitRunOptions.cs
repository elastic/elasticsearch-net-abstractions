using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Elastic.Managed.Configuration;
using Elastic.Xunit.XunitPlumbing;

namespace Elastic.Xunit
{
	public class ElasticXunitRunOptions
	{
		/// <summary> Informs the runner wheter we expect to run integration tests, defaults to true </summary>
		public bool RunIntegrationTests { get; set; } = true;
		/// <summary> Informs the runner wheter we expect to run unit tests, defaults to false.
		/// if true and <see cref="RunIntegrationTests"/> is false the runner will run all the tests with maximum parallelism</summary>
		public bool RunUnitTests { get; set; } = false;
		/// <summary> A global test filter that can be used to only run certain tests, accepts a comma separated list of filters </summary>
		public string TestFilter { get; set; }
		/// <summary> A global cluster filter that can be used to only run certain cluster's tests, accepts a comma separated list of filters</summary>
		public string ClusterFilter { get; set; }
		/// <summary> Hints the execution engine what version is under test, required for <see cref="SkipVersionAttribute"/> to kick in</summary>
		public ElasticsearchVersion Version { get; set; }

		/// <summary>
		/// Gets called when the tests have fininshed running succesfully
		/// </summary>
		/// <param name="runnerClusterTotals">Per cluster timings of the total test time (including starting elasticsearch)</param>
		/// <param name="runnerFailedCollections">All collection of failed cluster, failed tests tuples</param>
		public virtual void OnTestsFinished(Dictionary<string, Stopwatch> runnerClusterTotals, ConcurrentBag<Tuple<string, string>> runnerFailedCollections)
		{
		}

		/// <summary>
		/// Gets called before testing begins, ideal place to dump information to e.g Console.
		/// </summary>
		public virtual void OnBeforeTestsRun()
		{
		}
	}
}
