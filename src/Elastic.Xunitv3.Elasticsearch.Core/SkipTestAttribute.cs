// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Reflection;
using Xunit;
using Xunit.v3;

namespace Elastic.Xunitv3.Elasticsearch.Core;

/// <summary>
///     Abstract base for custom skip conditions. Subclass this to implement
///     arbitrary skip logic evaluated before each test via xUnit v3's
///     <see cref="BeforeAfterTestAttribute" /> pipeline.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public abstract class SkipTestAttribute : BeforeAfterTestAttribute
{
	/// <summary>
	///     Whether the test should be skipped.
	/// </summary>
	public abstract bool Skip { get; }

	/// <summary>
	///     The reason why the test should be skipped.
	/// </summary>
	public abstract string Reason { get; }

	/// <inheritdoc />
	public override void Before(MethodInfo methodUnderTest, IXunitTest test) =>
		Assert.SkipUnless(!Skip, Reason);
}
