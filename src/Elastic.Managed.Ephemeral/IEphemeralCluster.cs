using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Elastic.Managed.Ephemeral
{
	public interface IEphemeralCluster
	{
		ICollection<Uri> NodesUris(string hostName = null);
		string GetCacheFolderName();
		bool CachingAndCachedHomeExists();
	}

	public interface IEphemeralCluster<out TConfiguration> : IEphemeralCluster, ICluster<TConfiguration>
		where TConfiguration : EphemeralClusterConfiguration { }

}
