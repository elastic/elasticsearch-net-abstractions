using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elastic.ProcessManagement.Std;
using Xunit;
using FluentAssertions;

namespace Elastic.ProcessManagement.Tests
{
	public class ReadLineTestCases : TestsBase
	{
		[Fact]
		public void ReadKeyFirst()
		{
			var seen = new List<string>();
			var process = new ObservableProcess(TestCaseArguments(nameof(ReadKeyFirst)));
			process.SubscribeLines(c=>seen.Add(c.Line));
			process.StandardInput.Write("y");
			process.WaitForCompletion(WaitTimeout);

			seen.Should().NotBeEmpty().And.HaveCount(1, string.Join(Environment.NewLine, seen));
			seen[0].Should().Be($"y{nameof(ReadKeyFirst)}");
		}
		[Fact]
		public void ReadKeyAfter()
		{
			var seen = new List<string>();
			var process = new ObservableProcess(TestCaseArguments(nameof(ReadKeyAfter)));
			process.Subscribe(c=>
			{
				var chars = new string(c.Characters);
				seen.Add(chars);
				if (chars == "input:")
					process.StandardInput.Write("y");
			});
			process.WaitForCompletion(WaitTimeout);

			seen.Should().NotBeEmpty().And.HaveCount(2, string.Join(Environment.NewLine, seen));
			seen[0].Should().Be($"input:");
			seen[1].Should().Be(nameof(ReadKeyAfter));
		}

	}
}
