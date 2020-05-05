// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Managed;
using Elastic.Managed.Ephemeral;

namespace Elastic.Xunit.XunitPlumbing
{
	// ReSharper disable once UnusedTypeParameter
	// used by the runner to new() the proper cluster
	public interface IClusterFixture<out TCluster>
		where TCluster : ICluster<EphemeralClusterConfiguration>, new()
	{
	}
}
