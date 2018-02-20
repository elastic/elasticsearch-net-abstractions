using System;

namespace Elastic.Managed.Configuration
{
	[Flags]
	public enum NodeFeatures
	{
		None = 1 << 0,
		XPack = 1 << 1,
		Security = 1 << 2,
		// ReSharper disable once InconsistentNaming
		SSL = 1 << 3,
	}
}