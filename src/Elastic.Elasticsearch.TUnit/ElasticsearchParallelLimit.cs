// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using TUnit.Core.Interfaces;

namespace Elastic.Elasticsearch.TUnit;

/// <summary>
///     Default parallel limit for Elasticsearch integration tests.
///     Limits concurrency to <see cref="Environment.ProcessorCount" />.
///     Apply to test classes with <c>[ParallelLimiter&lt;ElasticsearchParallelLimit&gt;]</c>.
/// </summary>
public record ElasticsearchParallelLimit : IParallelLimit
{
	public int Limit => Environment.ProcessorCount;
}
