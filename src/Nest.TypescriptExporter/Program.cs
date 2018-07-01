using System;
using System.IO;
using Nest.TypescriptGenerator.Touchups;
using ShellProgressBar;

namespace Nest.TypescriptGenerator
{
	public static class Program
	{
		/// <summary>
		/// generate [definitionfile] [nest_source_folder]
		/// split [definitionfile] [nest_source_folder] [out_folder]
		/// </summary>
		public static int Main(string[] args)
		{
			const string defaultTsFile = "typedefinitions.ts";
			const string defaultNestSourceFolder = @"..\..\..\net-master\src\Nest";
			var command = args.Length > 0 ? args[0] : "generate";
			var definitionFile = args.Length > 1 ? args[1] : defaultTsFile;
			var nestSourceFolder = args.Length > 2 ? args[2] : defaultNestSourceFolder;
			var restSpec = new RestSpec(nestSourceFolder);
			switch (command)
			{
				case "generate": return Generate(definitionFile, nestSourceFolder, restSpec);
				case "both":
				case "split":
					var outFolder = args.Length > 3 ? args[3] : @"..\..\..\elastic-client-generator\specification\specs";
					var r = 0;
					if (command == "both") r += Generate(definitionFile, nestSourceFolder, restSpec);
					r += Split(definitionFile, restSpec, outFolder);
					return r;
				default:
					Console.Error.WriteLine("Unknown command for generator, valid are generate|split");
					return 2;
			}
		}

		private static int Split(string definitionFile, RestSpec restSpec, string outFolder)
		{
			var splitter = new TypescriptDumpSplitter(definitionFile, restSpec, outFolder);
			return splitter.Split();
		}

		private static int Generate(string definitionFile, string nestSourceFolder, RestSpec restSpec)
		{
			var sourceDirectory = new CSharpSourceDirectory(nestSourceFolder);
			var typeInfoProvider = new CsharpTypeInfoProvider();
			var scriptGenerator = new ClientTypescriptGenerator(typeInfoProvider, sourceDirectory, restSpec);

			using (var pbar = new ProgressBar(3, "Generating typescript information from NEST sources/code", new ProgressBarOptions {ForegroundColor = ConsoleColor.Yellow}))
			{
				var generator = new ClientTypesExporter(typeInfoProvider, scriptGenerator);
				File.WriteAllText(definitionFile, generator.Generate());
				pbar.Tick($"Generated {definitionFile}");
				GenerateLineScrubber.LineBasedHacks(definitionFile);
				pbar.Tick($"Performed line based scrubber over {definitionFile}");
				GeneratePrependDefinitions.PrependDefinitions(definitionFile);
				pbar.Tick($"Prepended known types and annotations {definitionFile}");
				return 0;
			}
		}
	}
}
