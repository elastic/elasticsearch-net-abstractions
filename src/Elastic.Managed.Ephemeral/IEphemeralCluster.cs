// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
