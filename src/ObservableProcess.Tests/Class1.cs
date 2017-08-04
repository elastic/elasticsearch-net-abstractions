using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elastic.ProcessManagement.Std;
using Xunit;
using FluentAssertions;

namespace Elastic.ProcessManagement.Tests
{
	public class Tests : TestsBase
	{
		[Fact]
		public void SingleLineNoEnter()
		{
			var seen = new List<string>();
			var process = new ObservableProcess(TestCaseArguments(nameof(SingleLineNoEnter)));
			process.SubscribeLines(c=>seen.Add(c.Line));
			process.WaitForCompletion(TimeSpan.FromSeconds(50));

			seen.Should().NotBeEmpty().And.HaveCount(1);
			seen[0].Should().Be(nameof(SingleLineNoEnter));
		}

		[Fact]
		public void SingleLine()
		{
			var seen = new List<string>();
			var process = new ObservableProcess(TestCaseArguments(nameof(SingleLine)));
			process.SubscribeLines(c=>seen.Add(c.Line), e=>throw e);
			process.WaitForCompletion(TimeSpan.FromSeconds(10));

			seen.Should().NotBeEmpty().And.HaveCount(1);
			seen[0].Should().Be(nameof(SingleLine));
		}

		[Fact]
		public void SingleLineNoEnterCharacters()
		{
			var seen = new List<char[]>();
			var process = new ObservableProcess(TestCaseArguments(nameof(SingleLineNoEnter)));
			process.Subscribe(c=>seen.Add(c.Characters));
			process.WaitForCompletion(TimeSpan.FromSeconds(5));

			seen.Should().NotBeEmpty().And.HaveCount(1);
			var chars = nameof(SingleLineNoEnter).ToCharArray();
			seen[0].Should()
				.HaveCount(chars.Length)
				.And.ContainInOrder(chars);

		}
		[Fact]
		public void SingleLineCharacters()
		{
			var seen = new List<char[]>();
			var process = new ObservableProcess(TestCaseArguments(nameof(SingleLine)));
			process.Subscribe(c=>seen.Add(c.Characters));
			process.WaitForCompletion(TimeSpan.FromSeconds(5));

			seen.Should().NotBeEmpty().And.HaveCount(1);
			var chars = nameof(SingleLine).ToCharArray()
				.Concat(Environment.NewLine.ToCharArray())
				.ToArray();
			seen[0].Should()
				.HaveCount(chars.Length)
				.And.ContainInOrder(chars);

		}
	}
}
