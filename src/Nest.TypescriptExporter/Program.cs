using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Nest.TypescriptGenerator.Touchups;

namespace Nest.TypescriptGenerator
{
	public static class Program
	{
		/// <summary>
		/// generate [definitionfile] [in_folder]
		/// split [definitionfile] [out_folder]
		/// </summary>
		public static int Main(string[] args)
		{
			const string defaultTsFile = "typedefinitions.ts";
			var command = args.Length > 0 ? args[0] : "generate";
			var definitionFile = args.Length > 1 ? args[1] : defaultTsFile;
			switch (command)
			{
				case "split":
					var outFolder = args.Length > 2 ? args[2] : @"..\..\..\net-master\src\Nest";
					return Split(definitionFile, outFolder);
				case "generate":
					var inFolder = args.Length > 2 ? args[2] : @"..\..\..\net-master\src\Nest";
					return Generate(definitionFile, inFolder);
				default:
					Console.Error.WriteLine("Unknown command for generator, valid are generate|split");
					return 2;
			}
		}

		private static int Split(string definitionFile, string outFolder)
		{
			return 0;
		}

		private static int Generate(string definitionFile, string inFolder)
		{
			var sourceDirectory = new CSharpSourceDirectory(inFolder);
			var typeInfoProvider = new CsharpTypeInfoProvider();
			var scriptGenerator = new ClientTypescriptGenerator(typeInfoProvider, sourceDirectory);
			var generator = new ClientTypesExporter(typeInfoProvider, scriptGenerator);
			File.WriteAllText(definitionFile, generator.Generate());
			GenerateLineScrubber.LineBasedHacks(definitionFile);
			GeneratePrependDefinitions.PrependDefinitions(definitionFile);
			return 0;
		}

	}
}
