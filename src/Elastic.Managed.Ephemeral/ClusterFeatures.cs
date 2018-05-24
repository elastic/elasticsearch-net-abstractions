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
		/// No features
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
}
