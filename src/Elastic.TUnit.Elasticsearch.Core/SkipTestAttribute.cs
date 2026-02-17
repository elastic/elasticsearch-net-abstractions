// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     Abstract base for custom skip conditions. Subclass this to implement
///     arbitrary skip logic evaluated before each test.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public abstract class SkipTestAttribute : Attribute
{
	/// <summary>
	///     Whether the test should be skipped.
	/// </summary>
	public abstract bool Skip { get; }

	/// <summary>
	///     The reason why the test should be skipped.
	/// </summary>
	public abstract string Reason { get; }
}
