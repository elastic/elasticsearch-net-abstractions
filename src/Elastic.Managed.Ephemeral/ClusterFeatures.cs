// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elastic.Managed.Ephemeral
{
	/// <summary>
	/// Hints to <see cref="EphemeralClusterConfiguration"/> what features the cluster to be started should have.
	/// It's up to the <see cref="EphemeralClusterComposer{TConfiguration}"/> to actually bootstrap these features.
	/// </summary>
	[Flags]
	public enum ClusterFeatures
	{
		/// <summary>
		/// No features, note that as of Elasticsearch 6.3.0 x-pack ships OOTB
		/// </summary>
		None = 1 << 0,
		/// <summary>
		/// X-Pack features
		/// </summary>
		XPack = 1 << 1,
		/// <summary>
		/// X-Pack security
		/// </summary>
		Security = 1 << 2,
		/// <summary>
		/// SSL/TLS for HTTP and Transport layers
		/// </summary>
		SSL = 1 << 3,
	}

	/// <summary>
	/// As of 6.3.0 x-pack is included by default, this enum allows you to optionally make ephemeral cluster accept the basic license or start a trial
	/// </summary>
	public enum XPackTrialMode
	{
		None,
		Basic,
		Trial
	}
}
