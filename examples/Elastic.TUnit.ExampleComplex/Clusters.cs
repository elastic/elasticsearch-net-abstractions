// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Clients.Elasticsearch;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.TUnit;
using Elastic.Transport;

namespace Elastic.TUnit.ExampleComplex;

internal static class EphemeralClusterExtensions
{
	/// <summary>
	///     Gets or creates an <see cref="ElasticsearchClient" /> for the cluster with
	///     per-request diagnostics routed to the current TUnit test's output.
	/// </summary>
	public static ElasticsearchClient GetOrAddClient(this IEphemeralCluster cluster) =>
		ElasticsearchClusterExtensions.GetOrAddClient(cluster, (c, output) =>
		{
			var connectionPool = new StaticNodePool(c.NodesUris());
			var settings = new ElasticsearchClientSettings(connectionPool)
				.EnableDebugMode()
				.OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
			return new ElasticsearchClient(settings);
		});
}

public interface IMyCluster
{
	ElasticsearchClient Client { get; }
}

public abstract class MyClusterBase : ElasticsearchCluster, IMyCluster
{
	protected MyClusterBase() : base(new ElasticsearchConfiguration("latest-9")
	{
		ShowElasticsearchOutputAfterStarted = false,
	})
	{
	}

	public ElasticsearchClient Client => this.GetOrAddClient();
}

public class TestCluster : MyClusterBase
{
	protected override void SeedCluster()
	{
		var response = Client.Info();
	}
}

public class TestGenericCluster : ElasticsearchCluster<ElasticsearchConfiguration>, IMyCluster
{
	public TestGenericCluster() : base(new ElasticsearchConfiguration("latest-9"))
	{
	}

	public ElasticsearchClient Client => this.GetOrAddClient();

	protected override void SeedCluster()
	{
		var response = Client.Info();
	}
}
