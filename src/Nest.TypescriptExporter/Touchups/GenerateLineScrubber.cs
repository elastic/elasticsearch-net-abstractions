using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Nest.TypescriptGenerator.Touchups
{
	public static class GenerateLineScrubber
	{
		private static readonly Regex ReadSingleOrEnumerableToUnion = new Regex(@"^(.+?): (.+?)\[\];");
		public static void LineBasedHacks(string file)
		{
			var lines = File.ReadAllLines(file);
			var newLines = new List<string>();
			List<string> errorCause = new List<string>(), error = new List<string>();
			bool readingError = false, readingErrorCause = false;
			var singleOrArray = false;
			foreach (var l in lines)
			{
				if (TryHandleReadSingleOrEnumerable(l, newLines, ref singleOrArray)) continue;

				// Touching up the fact we can not sort modules in the generated output.
				// this removes ErrorCause and Error definitions these are manually added to to top afterwards.
				if (TryMove(l, "class ErrorCause ", errorCause, newLines, ref readingErrorCause)) continue;
				if (TryMove(l, "class Error extends", error, newLines, ref readingError)) continue;

				newLines.Add(l);
			}
			File.WriteAllLines(file, errorCause);
			File.AppendAllLines(file, error);
			File.AppendAllLines(file, newLines);
		}

		private static bool TryMove(string l, string classDef, ICollection<string> movedLines, IList<string> newLines, ref bool skipTillNextBracket)
		{
			if (l == "}" && skipTillNextBracket)
			{
				movedLines.Add(l);
				skipTillNextBracket = false;
				return true;
			}

			if (l.StartsWith(classDef))
			{
				var previousLine = newLines[newLines.Count - 1];
				newLines.RemoveAt(newLines.Count - 1);
				movedLines.Add(previousLine);
				skipTillNextBracket = true;
			}

			if (!skipTillNextBracket) return false;
			movedLines.Add(l);
			return true;

		}

		//in NEST we always choose the array and use a json converter to accept the value
		//define it as a union of value | value[] instead
		private static bool TryHandleReadSingleOrEnumerable(string l, ICollection<string> newLines, ref bool singleOrArray)
		{
			if (l.Contains("ReadSingleOrEnumerable"))
			{
				singleOrArray = true;
				return true;
			}

			if (!singleOrArray) return false;

			var ll = ReadSingleOrEnumerableToUnion.Replace(l, "$1: $2 | $2[];");
			newLines.Add(ll);
			singleOrArray = false;
			return true;

		}
	}
}
