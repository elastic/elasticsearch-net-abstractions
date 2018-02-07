﻿using System;
using System.IO;
using System.Reflection;

namespace Elastic.ProcessManagement.Tests
{
	public abstract class TestsBase
	{
		private static string _observableprocessTestsBinary = "ObservableProcess.Tests.Binary";

		//increase this if you are using the debugger
		protected static TimeSpan WaitTimeout { get; } = TimeSpan.FromSeconds(5);

		private static string GetWorkingDir()
		{
			var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

			// If running the classic .NET solution, tests run from bin/{config} directory,
			// but when running DNX solution, tests run from the test project root
			var root = (directoryInfo.Name == "ObservableProcess.Tests"
			            && directoryInfo.Parent != null
			            && directoryInfo.Parent.Name == "src")
				? "./.."
				: @"../../../..";

			var binaryFolder = Path.Combine(Path.GetFullPath(root), _observableprocessTestsBinary);
			return binaryFolder;
		}

		protected ObservableProcessArguments TestCaseArguments(string testcase) =>
			new ObservableProcessArguments("dotnet", GetDll(), testcase)
			{
				AlterProcessStartInfo = p =>
				{
					p.WorkingDirectory = GetWorkingDir();
				}
			};

		private static string GetDll()
		{
			var dll = Path.Combine("bin", GetRunningConfiguration(), "netcoreapp1.1", _observableprocessTestsBinary + ".dll");
			var fullPath = Path.Combine(GetWorkingDir(), dll);
			if (!File.Exists(fullPath)) throw new Exception($"Can not find {fullPath}");

			return dll;
		}

		private static string GetRunningConfiguration()
		{
			var l = typeof(TestsBase).GetTypeInfo().Assembly.Location;
			return new DirectoryInfo(l).Parent?.Parent?.Name;
		}
	}
}
