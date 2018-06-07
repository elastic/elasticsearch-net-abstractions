using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Nest.TypescriptGenerator
{
	public class References : CSharpSyntaxWalker
	{
		public string Namespace { get; }
		public List<string> Declarations { get; } = new List<string>();

		public string[] Skip { get; } = new[] { "IElasticClient", "ElasticClient" };

		public References(FileInfo fileInfo)
		{
			var code = File.ReadAllText(fileInfo.FullName);
			var namespaces = new List<string>();
			var dirInfo = fileInfo.Directory;
			do
			{
				namespaces.Add(dirInfo.Name);
				dirInfo = dirInfo.Parent;
			} while (dirInfo.Name != "src");
			namespaces.Reverse();
			namespaces.Remove("Nest");
			this.Namespace = string.Join(".", namespaces);


			var ast = CSharpSyntaxTree.ParseText(code);
			this.Visit(ast.GetRoot());
		}

		public override void VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			var c = node.Identifier.Text;
			if (!Skip.Contains(c))
				this.Declarations.Add(c);
			base.VisitClassDeclaration(node);
		}

		public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			var c = node.Identifier.Text;
			if (!Skip.Contains(c))
				this.Declarations.Add(c);
			base.VisitInterfaceDeclaration(node);
		}

		public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			var c = node.Identifier.Text;
			if (!Skip.Contains(c))
				this.Declarations.Add(c);
			base.VisitEnumDeclaration(node);
		}
	}
}