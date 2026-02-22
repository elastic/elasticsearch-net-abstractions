// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nullean.Xunit.Partitions.v3;

namespace Elastic.XunitV3.Elasticsearch.Core;

/// <summary>
///     The xUnit v3 test framework for Elasticsearch integration tests.
///     Uses <see cref="Nullean.Xunit.Partitions.v3.PartitionTestFramework{TOptions}" /> to manage
///     cluster lifecycles — each cluster type is a partition that starts before its tests
///     and disposes after.
///     <para>
///         Register in your test assembly with:
///         <code>[assembly: TestFramework(typeof(ElasticTestFramework))]</code>
///     </para>
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class ElasticTestFramework : PartitionTestFramework<ElasticXunitRunOptions>;
