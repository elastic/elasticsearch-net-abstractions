// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using Elastic.Elasticsearch.Ephemeral;

namespace Elastic.Elasticsearch.Xunit
{
	/// <summary>
	///     Extension methods for <see cref="IEphemeralCluster" />
	/// </summary>
	public static class XunitClusterExtensions
	{
		private static readonly ConcurrentDictionary<IEphemeralCluster, object> Clients = new();

		/// <summary>
		///     Gets a client for the cluster if one exists, or creates a new client if one doesn't.
		/// </summary>
		/// <param name="cluster">the cluster to create a client for</param>
		/// <param name="getOrAdd">A delegate to create a client, given the cluster to create it for</param>
		/// <typeparam name="T">the type of the client</typeparam>
		/// <returns>An instance of a client</returns>
		public static T GetOrAddClient<T>(this IEphemeralCluster cluster, Func<IEphemeralCluster, T> getOrAdd) =>
			(T) Clients.GetOrAdd(cluster, c => getOrAdd(c));
	}
}
