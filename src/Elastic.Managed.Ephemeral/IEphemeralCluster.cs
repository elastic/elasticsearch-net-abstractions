using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Elastic.Managed.Ephemeral
{
	public interface IEphemeralCluster
	{
		ICollection<Uri> NodesUris(string hostName = "localhost");
	}

	public interface IEphemeralCluster<out TConfiguration> : IEphemeralCluster, ICluster<TConfiguration>
		where TConfiguration : EphemeralClusterConfiguration { }

}
