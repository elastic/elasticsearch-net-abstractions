// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;

namespace Elastic.Xunit.ExampleComplex
{
	public class MyTestClass : ClusterTestClassBase<TestCluster>
	{
		public MyTestClass(TestCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = this.Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();

			this.Client.CreateIndex("INASda");


			this.Client.LowLevel.Search<StringResponse>(PostData.Serializable(new
			{
				query = new { query_string = 1 }
			}));

		}
	}
//
//	public class MyGenericTestClass : ClusterTestClassBase<TestGenericCluster>
//	{
//		public MyGenericTestClass(TestGenericCluster cluster) : base(cluster) { }
//
//		[I] public void SomeTest()
//		{
//			var info = this.Client.RootNodeInfo();
//
//			info.IsValid.Should().BeTrue();
//		}
//		[U] public void UnitTest()
//		{
//			(1 + 1).Should().Be(2);
//		}
//	}

	[SkipVersion("<6.2.0", "")]
	public class SkipTestClass : ClusterTestClassBase<TestGenericCluster>
	{
		public SkipTestClass(TestGenericCluster cluster) : base(cluster) { }

		[I] public void SomeTest()
		{
			var info = this.Client.RootNodeInfo();

			info.IsValid.Should().BeTrue();
		}
		[U] public void UnitTest()
		{
			(1 + 1).Should().Be(2);
		}
	}
}
