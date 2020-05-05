// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;

namespace Differ.Exporters
{
	public class XmlExporter : IExporter
	{
		public string Format { get; } = "xml";

		public void Export(AssemblyDiffPair assemblyDiffPair, string outputPath)
		{
			var xml = assemblyDiffPair.Diff.ToXml();
			using (var writer = new StreamWriter(Path.Combine(outputPath, Path.ChangeExtension(assemblyDiffPair.First.Name, "xml"))))
				writer.Write(xml);
		}
	}
}
