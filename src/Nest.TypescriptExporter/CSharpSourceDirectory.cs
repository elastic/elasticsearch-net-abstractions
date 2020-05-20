// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nest.TypescriptGenerator
{
	public class CSharpSourceDirectory
	{
		private static readonly string[] SkipFolders = { "_Generated", "Debug", "Release" };

		private static IEnumerable<CSharpSourceFile> InputFiles(string directory) =>
			from f in Directory.GetFiles(directory, $"*.cs", SearchOption.AllDirectories)
			let dir = new DirectoryInfo(f)
			where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
			let fi = new FileInfo(f)
			where !Regex.IsMatch(fi.Name, $@"(Requests|Descriptors)\..*?\.cs")
			select new CSharpSourceFile(fi);

		public CSharpSourceDirectory(string directory)
		{
			var csharpSourceFiles = InputFiles(directory).SelectMany(r => r.Declarations, (r, s) => new { r, s });
			foreach(var f in csharpSourceFiles)
			{
				if (TypeNameToNamespaceMapping.ContainsKey(f.s) && (f.s.EndsWith("Request") || f.s.EndsWith("Descriptor")) )
					continue;
				if (TypeNameToNamespaceMapping.ContainsKey(f.s)) continue;
				TypeNameToNamespaceMapping.Add(f.s, f.r.Namespace);
			}
		}

		public Dictionary<string, string> TypeNameToNamespaceMapping { get; } = new Dictionary<string, string>();
	}
}
