using Elastic.Managed.Configuration;
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
		public static ElasticsearchVersion TestVersion { get; } = "6.2.3";
		public MyRunOptions()
		{
			this.ClusterFilter = "test";
			this.RunUnitTests = false;
			this.RunIntegrationTests = true;
			this.Version = TestVersion;
		}
	}
}
