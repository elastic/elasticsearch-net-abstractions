// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.RegularExpressions;

namespace Elastic.Managed.ConsoleWriters
{
	public static class ExceptionLineParser
	{
		private static readonly Regex CauseRegex = new Regex(@"^(?<cause>.*?Exception:)(?<message>.*?)$");

		public static bool TryParseCause(string line, out string cause, out string message)
		{
			cause = message = null;
			if (string.IsNullOrEmpty(line)) return false;
			var match = CauseRegex.Match(line);
			if (!match.Success) return false;
			cause = match.Groups["cause"].Value.Trim();
			message = match.Groups["message"].Value.Trim();
			return true;
		}

		private static readonly Regex LocRegex = new Regex(@"^(?<at>\s*?at )(?<method>.*?)\((?<file>.*?)\)(?<jar>.*?)$");
		public static bool TryParseStackTrace(string line, out string at, out string method, out string file, out string jar)
		{
			at = method = file = jar = null;
			if (string.IsNullOrEmpty(line)) return false;
			var match = LocRegex.Match(line);
			if (!match.Success) return false;
			at = match.Groups["at"].Value;
			method = match.Groups["method"].Value.Trim();
			file = match.Groups["file"].Value.Trim();
			jar = match.Groups["jar"].Value.Trim();
			return true;
		}

	}
}