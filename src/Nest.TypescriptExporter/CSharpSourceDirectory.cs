using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nest.TypescriptGenerator
{
	public class CSharpSourceDirectory
	{
		private static readonly string[] SkipFolders = { "_Generated", "Debug", "Release" };

		private static IEnumerable<CSharpSourceFile> InputFiles(string directory) =>
			from f in Directory.GetFiles(directory, $"*.cs", SearchOption.AllDirectories)
			let dir = new DirectoryInfo(f)
			where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
			select new CSharpSourceFile(new FileInfo(f));

		public CSharpSourceDirectory(string directory)
		{
			var csharpSourceFiles = InputFiles(directory).SelectMany(r => r.Declarations, (r, s) => new { r, s });
			foreach(var f in csharpSourceFiles)
			{
				if (this.TypeNameToNamespaceMapping.ContainsKey(f.s) && (f.s.EndsWith("Request") || f.s.EndsWith("Descriptor")) )
					continue;
				if (this.TypeNameToNamespaceMapping.ContainsKey(f.s)) continue;
				this.TypeNameToNamespaceMapping.Add(f.s, f.r.Namespace);
			}
		}

		public Dictionary<string, string> TypeNameToNamespaceMapping { get; } = new Dictionary<string, string>();
	}
}