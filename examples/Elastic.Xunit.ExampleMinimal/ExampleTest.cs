// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Clients.Elasticsearch;
using Elastic.Elasticsearch.Xunit;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Xunit;

// we need to put this assembly attribute in place for xunit to use our custom test execution pipeline
[assembly: TestFramework("Elastic.Elasticsearch.Xunit.Sdk.ElasticTestFramework", "Elastic.Elasticsearch.Xunit")]

namespace Elastic.Xunit.ExampleMinimal
{
	/// <summary> Declare our cluster that we want to inject into our test classes </summary>
	public class MyTestCluster : XunitClusterBase
	{
		/// <summary>
		///     We pass our configuration instance to the base class.
		///     We only configure it to run version 6.2.3 here but lots of additional options are available.
		/// </summary>
		public MyTestCluster() : base(new XunitClusterConfiguration("latest-9"))
		{
		}
	}

	public class ExampleTest : IClusterFixture<MyTestCluster>
	{
		public ExampleTest(MyTestCluster cluster) =>
			// This registers a single client for the cluster's lifetime to be reused and shared.
			// A client is not directly exposed on a cluster for two reasons
			//
			// 1) We do not want to prescribe how to create an instance of the client
			//
			// 2) We do not want Elastic.Elasticsearch.Xunit to depend on NEST. Elastic.Elasticsearch.Xunit can start 2.x, 5.x and 6.x clusters
			//    and NEST Major.x is only tested and supported against Elasticsearch Major.x.
			//
			Client = cluster.GetOrAddClient(c =>
			{
				var nodes = cluster.NodesUris();
				var connectionPool = new StaticNodePool(nodes);
				var settings = new ElasticsearchClientSettings(connectionPool)
					.EnableDebugMode();
				return new ElasticsearchClient(settings);
			});

		private ElasticsearchClient Client { get; }

		/// <summary> [I] marks an integration test (like [Fact] would for plain Xunit) </summary>
		[I]
		public void SomeTest()
		{
			var rootNodeInfo = Client.Info();

			rootNodeInfo.Name.Should().NotBeNullOrEmpty();
		}
	}
}
