// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Stack.Artifacts;
using Elastic.Xunit.ExampleComplex;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: Elastic.Xunit.ElasticXunitConfiguration(typeof(MyRunOptions))]

namespace Elastic.Xunit.ExampleComplex
{
	/// <summary>
	/// Allows us to control the custom xunit test pipeline
	/// </summary>
	public class MyRunOptions : ElasticXunitRunOptions
	{
		public static ElasticVersion TestVersion { get; } = "6.3.0";
		public MyRunOptions()
		{
			this.ClusterFilter = "";
			this.RunUnitTests = false;
			this.RunIntegrationTests = true;
			this.IntegrationTestsMayUseAlreadyRunningNode = true;
			this.Version = TestVersion;
		}
	}
}
