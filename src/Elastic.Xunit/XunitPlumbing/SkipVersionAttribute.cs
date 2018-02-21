using System;
using System.Collections.Generic;
using System.Linq;
using SemVer;

namespace Elastic.Xunit.XunitPlumbing
{
	public class SkipVersionAttribute : Attribute
	{
		public IList<Range> Ranges { get; }

		public SkipVersionAttribute(string skipVersionRangesSeparatedByComma, string reason)
		{
			this.Ranges = skipVersionRangesSeparatedByComma.Split(',')
				.Select(r => new Range(r))
				.ToList();
		}
	}
}
