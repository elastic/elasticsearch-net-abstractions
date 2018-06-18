using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Nest.TypescriptGenerator
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var sources = args.Length > 0 ? args[0] : @"..\..\..\net-master\src\Nest";
			var sourceDirectory = new CSharpSourceDirectory(sources);
			var typeInfoProvider = new CsharpTypeInfoProvider();
			var scriptGenerator = new ClientTypescriptGenerator(typeInfoProvider, sourceDirectory);
			var generator = new ClientTypesExporter(typeInfoProvider, scriptGenerator);

			var file = args.Length > 1 ? args[1] : "typedefinitions.ts";
			File.WriteAllText(file, generator.Generate());
			LineBasedHacks(file);
			PrependDefinitions(file);
		}

		private static void LineBasedHacks(string file)
		{
			var lines = File.ReadAllLines(file);
			var newLines = new List<string>();
			var skipTillNextBracket = false;
			var singleOrArray = false;
			foreach (var l in lines)
			{
				if (l.Contains("ReadSingleOrEnumerable"))
				{
					singleOrArray = true;
					continue;
				}

				if (singleOrArray)
				{
					var ll = Regex.Replace(l, @"^(.+?): (.+?)\[\];", "$1: $2 | $2[];");
					newLines.Add(ll);
					singleOrArray = false;
					continue;
				}

				if (l == "}" && skipTillNextBracket)
				{
					skipTillNextBracket = false;
					continue;
				}

				if (l.StartsWith("class ErrorCause ")
					|| l.StartsWith("class Error extends"))
				{
					newLines.RemoveAt(newLines.Count -1);
					skipTillNextBracket = true;
				}
				if (skipTillNextBracket) continue;

				newLines.Add(l);
			}
			File.WriteAllLines(file, newLines);
		}

		private static void PrependDefinitions(string file)
		{
			var errorCauseDef = @"@namespace(""common"")
class ErrorCause {
	reason: string;
	type: string;
	caused_by: ErrorCause;
	stack_trace: string;
	metadata: ErrorCauseMetadata;
}
";
			var errorDef = @"@namespace(""common"")
class Error extends ErrorCause {
	root_cause: ErrorCause[];
	headers: Map<string, string>;
}
";
			var hack = @"
function class_serializer(ns: string) {return function (ns: any){}}
function prop_serializer(ns: string) {return function (ns: any, x:any){}}
function request_parameter() {return function (ns: any, x:any){}}
function namespace(ns: string) {return function (ns: any){}}

interface Uri {}
interface Date {}
interface TimeSpan {}
interface SourceDocument {}
";
			hack += errorCauseDef;
			hack += errorDef;
			var contents = File.ReadAllText(file);
			contents = contents
				.Replace(errorCauseDef, "")
				.Replace(errorDef, "")
				.Replace("\thits: Hit<T>[];", "\t//hits: Hit<T>[];");
			File.WriteAllText(file, hack);
			File.AppendAllText(file, contents);
		}

	}
}
