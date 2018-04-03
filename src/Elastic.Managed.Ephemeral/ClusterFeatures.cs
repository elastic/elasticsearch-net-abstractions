using System;

namespace Elastic.Managed.Ephemeral
{
	/// <summary>
	/// Hints to <see cref="EphemeralClusterConfiguration"/> what features the cluster it will try and boot should have.
	/// Its up to the <see cref="EphemeralClusterComposer<>"/> to actually bootstrap these features.
	/// </summary>
	[Flags]
	public enum ClusterFeatures
	{
		None = 1 << 0,
		XPack = 1 << 1,
		Security = 1 << 2,
		// ReSharper disable once InconsistentNaming
		SSL = 1 << 3,
	}
}
