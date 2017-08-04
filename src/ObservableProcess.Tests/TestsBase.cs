using System;
using System.IO;

namespace Elastic.ProcessManagement.Tests
{
	public abstract class TestsBase
	{
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

			var binaryFolder = Path.Combine(Path.GetFullPath(root), "ObservableProcess.Tests.Binary");
			return binaryFolder;
		}

		protected ObservableProcessArguments TestCaseArguments(string testcase) =>
			new ObservableProcessArguments("dotnet", "run", testcase)
			{
				AlterProcessStartInfo = p =>
				{
					p.WorkingDirectory = GetWorkingDir();
				}
			};
	}
}
