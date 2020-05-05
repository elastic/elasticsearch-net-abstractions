// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;

namespace Differ.Providers.NuGet
{
	public class NuGetDiffCommand
	{
		public NuGetDiffCommand(string[] command)
		{
			if (command == null)
				throw new ArgumentNullException(nameof(command));

			if (command.Length < 2)
				throw new Exception("command must have a minimum length of 2");

			Package = command[0];
			Version = command[1];

			if (command.Length > 2)
				FrameworkVersion = command[2];
		}

		public string Package { get; }

		public string Version { get; }

		public string FrameworkVersion { get; }

		public string TempDir { get; } = Environment.GetEnvironmentVariable("TEMP");
	}
}
