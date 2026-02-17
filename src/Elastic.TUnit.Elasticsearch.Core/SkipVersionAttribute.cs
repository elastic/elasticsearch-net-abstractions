// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     An attribute that marks a test or test class to be skipped for given Elasticsearch versions.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class SkipVersionAttribute : Attribute
{
	public SkipVersionAttribute(string skipVersionRangesSeparatedByComma, string reason)
	{
		Reason = reason;
		Ranges = string.IsNullOrEmpty(skipVersionRangesSeparatedByComma)
			? new List<SemVer.Range>()
			: skipVersionRangesSeparatedByComma.Split(',')
				.Select(r => r.Trim())
				.Where(r => !string.IsNullOrWhiteSpace(r))
				.Select(r => new SemVer.Range(r))
				.ToList();
	}

	/// <summary>
	///     The reason why the test should be skipped.
	/// </summary>
	public string Reason { get; }

	/// <summary>
	///     The version ranges for which the test should be skipped.
	/// </summary>
	public IList<SemVer.Range> Ranges { get; }
}
