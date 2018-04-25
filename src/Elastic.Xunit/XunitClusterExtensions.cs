using System;
using System.Collections.Concurrent;
using Elastic.Managed.Ephemeral;

namespace Elastic.Xunit
{
	public static class XunitClusterExtensions
	{
		private static readonly ConcurrentDictionary<IEphemeralCluster, object> Clients = new ConcurrentDictionary<IEphemeralCluster, object>();

		public static T GetOrAddClient<T>(this IEphemeralCluster cluster, Func<IEphemeralCluster, T> getOrAdd)
		{
			return (T) Clients.GetOrAdd(cluster, c => getOrAdd(c));
		}
	}
}
