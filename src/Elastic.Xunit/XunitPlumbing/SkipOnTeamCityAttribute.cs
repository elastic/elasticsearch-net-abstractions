using System;

namespace Elastic.Xunit.XunitPlumbing
{
	public class SkipOnTeamCityAttribute : Attribute
	{
		public SkipOnTeamCityAttribute(string reason)
		{
		}
	}
}
