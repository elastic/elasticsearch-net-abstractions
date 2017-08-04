using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Elastic.ProcessManagement.Tests
{
	public class DisposeTestCases : TestsBase
	{
		[Fact]
		public void DelayedWriterRunsToCompletion()
		{
			var seen = new List<string>();
			var process = new ObservableProcess(TestCaseArguments(nameof(DelayedWriter)));
			var subscription = process.SubscribeLines(c=>seen.Add(c.Line));
			process.WaitForCompletion(WaitTimeout);

			process.ExitCode.Should().Be(20);
			seen.Should().NotBeEmpty().And.HaveCount(1, string.Join(Environment.NewLine, seen));
			seen[0].Should().Be(nameof(DelayedWriter));
		}
		[Fact]
		public void DelayedWriterStopNoWaitDispose()
		{
			var seen = new List<string>();
			var args = TestCaseArguments(nameof(DelayedWriter));
			args.WaitForExit = null; //never wait for exit
			var process = new ObservableProcess(args);
			process.SubscribeLines(c=>seen.Add(c.Line));
			process.Dispose();
			process.WaitForCompletion(WaitTimeout);

			//disposing the process itself will stop the underlying Process
			process.ExitCode.Should().NotHaveValue();
			seen.Should().BeEmpty(string.Join(Environment.NewLine, seen));
		}
		[Fact]
		public void DelayedWriterStopWaitToShort()
		{
			var seen = new List<string>();
			var args = TestCaseArguments(nameof(DelayedWriter));
			args.WaitForExit = TimeSpan.FromMilliseconds(200);
			var process = new ObservableProcess(args);
			process.SubscribeLines(c=>seen.Add(c.Line));
			process.Dispose();
			process.WaitForCompletion(WaitTimeout);

			process.ExitCode.Should().Be(-1);
			seen.Should().BeEmpty(string.Join(Environment.NewLine, seen));
		}
		[Fact]
		public void DelayedWriter()
		{
			var seen = new List<string>();
			var process = new ObservableProcess(TestCaseArguments(nameof(DelayedWriter)));
			var subscription = process.SubscribeLines(c=>seen.Add(c.Line));
			subscription.Dispose();
			process.WaitForCompletion(WaitTimeout);

			//disposing the subscription did not kill the process
			process.ExitCode.Should().Be(20);
			seen.Should().BeEmpty(string.Join(Environment.NewLine, seen));
		}

	}
}
