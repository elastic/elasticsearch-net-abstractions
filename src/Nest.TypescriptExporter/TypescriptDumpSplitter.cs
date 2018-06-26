using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nest.TypescriptGenerator
{
	public class RestSpec
	{
		private readonly IList<FileInfo> _jsonFiles;

		public RestSpec(string nestSourceFolder)
		{
			var specFolder = Path.GetFullPath(Path.Combine(nestSourceFolder, "..", "CodeGeneration", "ApiGenerator", "RestSpecification"));

			this._jsonFiles = Directory.GetFiles(specFolder, $"*.json", SearchOption.AllDirectories)
				.Select(f => new FileInfo(f))
				.ToList();
		}
		public Dictionary<string, FileInfo> GetSpecFiles() => this._jsonFiles.ToDictionary(f=>Path.GetFileNameWithoutExtension(f.Name), f=>f);
	}



	public class TypescriptDumpSplitter
	{
		private readonly string _definitionFile;
		private readonly Dictionary<string, FileInfo> _restSpec;
		private readonly string _outFolder;

		public TypescriptDumpSplitter(string definitionFile, string nestSourceFolder, string outFolder)
		{
			this._definitionFile = definitionFile;
			this._outFolder = Path.GetFullPath(outFolder);
			this._restSpec = new RestSpec(nestSourceFolder).GetSpecFiles();
		}

		public int Split()
		{
			var lines = File.ReadAllLines(this._definitionFile);
			var additionalLines = new List<string>();
			var symbolContents = new Queue<string>();
			var readingType = false;
			string currentNamespace = null;
			string currentSymbol = null;
			foreach (var l in lines)
			{
				if (TryGetNamespace(l, out var ns))
				{
					currentNamespace = ns;
					readingType = true;
					continue;
				}
				if (TryGetSymbol(l, out var s))
					currentSymbol = s;

				if (readingType) symbolContents.Enqueue(l);
				else additionalLines.Add(l);

				if (l == "}" && readingType)
				{
					readingType = false;
					DumpFile(currentNamespace, currentSymbol, symbolContents);
					symbolContents.Clear();
				}
			}

			DumpRemainder(additionalLines);

			return 0;
		}

		private void DumpRemainder(List<string> additionalLines)
		{
			var path = Path.Combine(_outFolder, "common.ts");
			File.WriteAllLines(path, additionalLines);
		}

		private void DumpFile(string currentNamespace, string currentSymbol, Queue<string> symbolContents)
		{
			var folder = _outFolder + Path.DirectorySeparatorChar + NamespaceToFolder(currentNamespace);
			var path = Path.Combine(folder, ToFilename(currentSymbol));
			Directory.CreateDirectory(folder);
			File.WriteAllLines(path, symbolContents);
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

		private static readonly Regex SymbolRe = new Regex(@"^(?:class|enum|interface) (?<symbol>.+?)(?: extends.+$| \{.*?$|$)");
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
