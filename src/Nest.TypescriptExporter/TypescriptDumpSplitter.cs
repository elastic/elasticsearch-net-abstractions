// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ShellProgressBar;

namespace Nest.TypescriptGenerator
{
	public class TypescriptDumpSplitter
	{
		private readonly string _definitionFile;
		private readonly RestSpec _restSpec;
		private readonly string _outFolder;

		public TypescriptDumpSplitter(string definitionFile, RestSpec restSpec, string outFolder)
		{
			this._definitionFile = definitionFile;
			this._restSpec = restSpec;
			this._outFolder = Path.GetFullPath(outFolder);
		}

		public int Split()
		{
			using (var pbar = new ProgressBar(6, "splitting local typedefinitions.ts to elastic-client-generator", new ProgressBarOptions {ForegroundColor = ConsoleColor.Blue}))
			{
				if (Directory.Exists(this._outFolder))
				{
					Directory.Delete(this._outFolder, true);
					pbar.Tick($"Deleted {this._outFolder}");
				}
				else pbar.Tick($"{this._outFolder} does not exist yet");

				Directory.CreateDirectory(this._outFolder);
				pbar.Tick($"{this._outFolder} created");
				var additionalLines = SplitDeclarationDumpFile();
				pbar.Tick($"split typedefinitions.ts");
				DumpRemainder(additionalLines);
				pbar.Tick($"written common.ts");
				CopyTsConfig();
				pbar.Tick($"copied tsconfig.json");
				WriteTsLintFile();
				pbar.Tick($"wrote empty tslint.json in {this._outFolder}");
				return 0;
			}
		}

		private List<string> SplitDeclarationDumpFile()
		{
			var lines = File.ReadAllLines(this._definitionFile);
			var additionalLines = new List<string>();
			var symbolContents = new Queue<string>();
			var readingType = false;
			string currentNamespace = null;
			string currentSpecName = null;
			string currentSymbol = null;
			foreach (var l in lines)
			{
				if (TryGetNamespace(l, out var ns))
				{
					currentNamespace = ns;
					readingType = true;
					continue;
				}

				if (TryGetRequestSpecName(l, out var n))
				{
					currentSpecName = n;
				}

				if (TryGetSymbol(l, out var s))
					currentSymbol = s;

				if (readingType && !string.IsNullOrEmpty(currentSymbol))
				{
					if (currentSymbol.StartsWith("SearchResponse"))
					{
						if (l.StartsWith("\thits: Hit<")) continue;
					}
					symbolContents.Enqueue(l);
				}
				else additionalLines.Add(l);

				if ((l == "}" || l.EndsWith("extends String {}")) && readingType)
				{
					readingType = false;
					DumpFile(currentNamespace, currentSymbol, symbolContents, currentSpecName);
					symbolContents.Clear();
					currentSpecName = null;
				}
			}

			return additionalLines;
		}

		private void WriteTsLintFile()
		{
			var target = Path.Combine(this._outFolder, "tslint.json");
			File.WriteAllText(target, "{}");
		}
		private void CopyTsConfig()
		{
			var tsconfigJson = "tsconfig.json";
			var config = Path.Combine(new FileInfo(this._definitionFile).Directory.FullName, tsconfigJson);
			var target = Path.Combine(this._outFolder, tsconfigJson);
			File.Copy(config, target);
		}

		private void DumpRemainder(List<string> additionalLines)
		{
			var path = Path.Combine(_outFolder, "common.ts");
			File.WriteAllLines(path, additionalLines);
		}

		private void DumpFile(string currentNamespace, string currentSymbol, Queue<string> symbolContents, string currentSpecName)
		{
			var folder = _outFolder + Path.DirectorySeparatorChar + NamespaceToFolder(currentNamespace);
			var path = Path.Combine(folder, ToFilename(currentSymbol));
			Directory.CreateDirectory(folder);
			File.WriteAllLines(path, symbolContents);

			if (!string.IsNullOrWhiteSpace(currentSpecName))
			{
				var name = currentSpecName + ".json";
				var source = this._restSpec.SpecificationFiles[currentSpecName].FullName;
				var target = Path.Combine(folder, name);
				File.Copy(source, target);
			}

		}

		private static readonly Regex NamespaceRe = new Regex(@"^(?:@namespace\(""(?<namespace>.+?)""\)|\/\*\* namespace:(?<namespace>\S+))");
		private static bool TryGetNamespace(string l, out string ns)
		{
			ns = null;
			var namespaceMatch = NamespaceRe.Match(l);
			if (!namespaceMatch.Success) return false;

			ns = namespaceMatch.Groups["namespace"].Value;
			return true;
		}
		private static readonly Regex RequestSpecName = new Regex(@"^@rest_spec_name\(""(?<name>.+?)""\)");
		private static bool TryGetRequestSpecName(string l, out string ns)
		{
			ns = null;
			var namespaceMatch = RequestSpecName.Match(l);
			if (!namespaceMatch.Success) return false;

			ns = namespaceMatch.Groups["name"].Value;
			return true;
		}

		private static readonly Regex SymbolRe = new Regex(@"^(?:class|enum|interface) (?<symbol>.+?)(?: (extends|implements).+$| \{.*?$|$)");
		private static bool TryGetSymbol(string l, out string symbol)
		{
			symbol = null;
			var namespaceMatch = SymbolRe.Match(l);
			if (!namespaceMatch.Success) return false;

			symbol = namespaceMatch.Groups["symbol"].Value.Trim();
			return true;
		}

		private static string NamespaceToFolder(string ns) => ns.Replace(".", Path.DirectorySeparatorChar.ToString());

		private static readonly Regex FilenameRe = new Regex(@"<.+?>");
		private static string ToFilename(string symbol) => $"{FilenameRe.Replace(symbol, "")}.ts";

	}
}
