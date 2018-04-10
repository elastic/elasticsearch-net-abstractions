using Elastic.Xunit.Example;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: Elastic.Xunit.ElasticXunitConfiguration(typeof(MyRunOptions))]

namespace Elastic.Xunit.Example
{
	/// <summary>
	/// Allows us to control the custom xunit test pipeline
	/// </summary>
	public class MyRunOptions : ElasticXunitRunOptions
	{
		public MyRunOptions()
		{
			this.ClusterFilter = "test";
		}
	}
}
