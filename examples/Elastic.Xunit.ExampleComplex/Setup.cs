// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit;
using Elastic.Stack.ArtifactsApi;
using Elastic.Xunit.ExampleComplex;
using Xunit;

[assembly: TestFramework("Elastic.Elasticsearch.Xunit.Sdk.ElasticTestFramework", "Elastic.Elasticsearch.Xunit")]
[assembly: ElasticXunitConfiguration(typeof(MyRunOptions))]

namespace Elastic.Xunit.ExampleComplex
{
	/// <summary>
	///     Allows us to control the custom xunit test pipeline
	/// </summary>
	public class MyRunOptions : ElasticXunitRunOptions
	{
		public MyRunOptions()
		{
			ClusterFilter = "";
			RunUnitTests = true;
			RunIntegrationTests = true;
			IntegrationTestsMayUseAlreadyRunningNode = true;
			Version = TestVersion;
		}

		public static ElasticVersion TestVersion { get; } = "latest-8";
	}
}
