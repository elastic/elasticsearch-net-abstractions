using System;
using System.Collections.ObjectModel;

namespace Elastic.Managed.Configuration
{
	/// <summary>
	/// Hints to <see cref="ClusterConfiguration"/> and <see cref="NodeConfiguration"/> what features the cluster they are booting has.
	/// Its up to the <see cref="ClusterBase"/>'s <see cref="ClusterComposer"/> to actually bootstrap these features.
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
