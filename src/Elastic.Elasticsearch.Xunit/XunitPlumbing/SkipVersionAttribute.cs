// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using SemVer;

namespace Elastic.Elasticsearch.Xunit.XunitPlumbing
{
	/// <summary>
	/// An Xunit test that should be skipped for given Elasticsearch versions, and a reason why.
	/// </summary>
	public class SkipVersionAttribute : Attribute
	{
		/// <summary>
		/// The reason why the test should be skipped
		/// </summary>
		public string Reason { get; }

		/// <summary>
		/// The version ranges for which the test should be skipped
		/// </summary>
		public IList<Range> Ranges { get; }

		// ReSharper disable once UnusedParameter.Local
		// reason is used to allow the test its used on to self document why its been put in place
		public SkipVersionAttribute(string skipVersionRangesSeparatedByComma, string reason)
		{
			Reason = reason;
			Ranges = string.IsNullOrEmpty(skipVersionRangesSeparatedByComma)
				? new List<Range>()
				: skipVersionRangesSeparatedByComma.Split(',')
					.Select(r => r.Trim())
					.Where(r => !string.IsNullOrWhiteSpace(r))
					.Select(r => new Range(r))
					.ToList();
		}
	}
}
