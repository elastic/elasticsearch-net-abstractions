using System;

namespace Elastic.Managed.Ephemeral.Plugins
{
	[AttributeUsage(AttributeTargets.Field)]
	public class MonikerAttribute : Attribute
	{
		public string Moniker { get; }

		public MonikerAttribute(string moniker)
		{
			if (moniker == null) throw new ArgumentNullException(nameof(moniker));
			if (moniker.Length == 0) throw new ArgumentException("must have a value");
			Moniker = moniker;
		}
	}
}
