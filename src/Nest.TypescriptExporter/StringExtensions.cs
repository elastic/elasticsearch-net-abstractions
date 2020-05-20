// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.RegularExpressions;

namespace Nest.TypescriptGenerator
{
	public static class StringExtensions
	{
		public static string QuoteMaybe(this string s)
		{
			if (s == null) return null;
			//this is dumb on purpose
			if (s.Contains('-')) return $"\'{s}\'";
			if (s.Contains('+')) return $"\'{s}\'";
			if (s.Contains('.')) return $"\'{s}\'";
			if (Regex.IsMatch(s, @"^\d")) return $"\'{s}\'";
			return s;
		}

		private static readonly Regex SnakeCaseRe = new Regex("(?<=.)([A-Z])");
		public static string SnakeCase(this string token) => token == null ? null : SnakeCaseRe.Replace(token, "_$0").ToLowerInvariant();
	}
}
