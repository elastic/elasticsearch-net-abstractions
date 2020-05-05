// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using JustAssembly.Core;

namespace Differ.Exporters
{
	public interface IExporter
	{
		string Format { get; }

		void Export(AssemblyDiffPair assemblyDiffPair, string outputPath);
	}
}
