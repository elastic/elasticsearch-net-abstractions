// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.IO;
using Elastic.Elasticsearch.Ephemeral;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     Extension methods for <see cref="IEphemeralCluster" />.
/// </summary>
public static class ElasticsearchClusterExtensions
{
	private static readonly ConcurrentDictionary<IEphemeralCluster, object> Clients = new();

	/// <summary>
	///     Gets a client for the cluster if one exists, or creates a new client if one doesn't.
	/// </summary>
	/// <param name="cluster">The cluster to create a client for.</param>
	/// <param name="getOrAdd">A delegate to create a client, given the cluster to create it for.</param>
	/// <typeparam name="T">The type of the client.</typeparam>
	/// <returns>An instance of a client.</returns>
	public static T GetOrAddClient<T>(this IEphemeralCluster cluster, Func<IEphemeralCluster, T> getOrAdd) =>
		(T)Clients.GetOrAdd(cluster, c => getOrAdd(c));

	/// <summary>
	///     Gets a client for the cluster if one exists, or creates a new client if one doesn't.
	///     The <paramref name="getOrAdd" /> delegate receives a <see cref="TextWriter" /> that
	///     dynamically routes output to the current TUnit test's output. This allows
	///     a cached client to route per-request diagnostics (e.g. request/response logging)
	///     to whichever test is currently executing.
	///     <para>
	///         Example usage:
	///         <code>
	///     var client = cluster.GetOrAddClient((c, output) =&gt;
	///     {
	///         var settings = new ElasticsearchClientSettings(new StaticNodePool(c.NodesUris()))
	///             .EnableDebugMode()
	///             .OnRequestCompleted(call =&gt; output.WriteLine(call.DebugInformation));
	///         return new ElasticsearchClient(settings);
	///     });
	///     </code>
	///     </para>
	/// </summary>
	/// <param name="cluster">The cluster to create a client for.</param>
	/// <param name="getOrAdd">
	///     A delegate receiving the cluster and a <see cref="TextWriter" /> bound to
	///     <see cref="TUnit.Core.TestContext.Current" />.
	/// </param>
	/// <typeparam name="T">The type of the client.</typeparam>
	/// <returns>An instance of a client.</returns>
	public static T GetOrAddClient<T>(this IEphemeralCluster cluster, Func<IEphemeralCluster, TextWriter, T> getOrAdd) =>
		(T)Clients.GetOrAdd(cluster, c => getOrAdd(c, TUnitTestOutputWriter.Instance));
}
