// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;

namespace Elastic.Xunit.ExampleComplex
{
	[SkipVersion("<6.3.0", "")]
	public class TestWithoutClusterFixture
	{
		[I]
		public void Test()
		{
			(1 + 1).Should().Be(2);
			var info = ElasticXunitRunner.CurrentCluster.GetOrAddClient().Info();
			info.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
