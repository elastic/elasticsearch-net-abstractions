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
	}
}