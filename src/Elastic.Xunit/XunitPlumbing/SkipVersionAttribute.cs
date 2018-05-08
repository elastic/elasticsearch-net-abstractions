using System;
using System.Collections.Generic;
using System.Linq;
using SemVer;

namespace Elastic.Xunit.XunitPlumbing
{
	public class SkipVersionAttribute : Attribute
	{
		public IList<Range> Ranges { get; }

		// ReSharper disable once UnusedParameter.Local
		// reason is used to allow the test its used on to self document why its been put in place
		public SkipVersionAttribute(string skipVersionRangesSeparatedByComma, string reason)
		{
			this.Ranges = skipVersionRangesSeparatedByComma.Split(',')
				.Select(r=>r.Trim())
				.Where(r=>!string.IsNullOrWhiteSpace(r))
				.Select(r => new Range(r))
				.ToList();
		}
	}
}
