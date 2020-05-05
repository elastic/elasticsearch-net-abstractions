// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using JustAssembly.Core;

namespace Differ
{
	public class AssemblyDiffPair
	{
		public AssemblyDiffPair(FileInfo first, FileInfo second)
		{
			First = first ?? throw new ArgumentNullException(nameof(first));
			Second = second ?? throw new ArgumentNullException(nameof(second));
		}

		public FileInfo First { get; }
		public FileInfo Second { get; }
		public IDiffItem Diff { get; set; }
	}
}
