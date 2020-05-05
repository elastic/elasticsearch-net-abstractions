// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using ProcNet.Std;

namespace Elastic.Managed.ConsoleWriters
{
	public class ConsoleLineWriter : IConsoleLineHandler
	{
		public void Handle(LineOut lineOut)
		{
			var w = lineOut.Error ? Console.Error : Console.Out;
			w.WriteLine(lineOut);
		}

		public void Handle(Exception e) => Console.Error.WriteLine(e);
	}
}
