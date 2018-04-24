using System;
using Elastic.Xunit.XunitPlumbing;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]

namespace Elastic.Xunit.ExampleMinimal
{
	public class ExampleTest
	{
		[I] public void SomeTest()
		{
		}
	}
}
