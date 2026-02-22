// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nullean.Xunit.Partitions.v3.Sdk;

namespace Elastic.Xunitv3.Elasticsearch.Core;

/// <summary>
///     Marker interface for test classes that require an Elasticsearch cluster.
///     Inherits from <see cref="IPartitionFixture{TCluster}" /> so the partition
///     framework groups tests by cluster type, starts each cluster once, and
///     injects the instance via constructor.
///     <para>
///         Equivalent to the xUnit v2 <c>IClusterFixture&lt;T&gt;</c> and
///         TUnit's <c>[ClassDataSource&lt;T&gt;(Shared = SharedType.Keyed)]</c>.
///     </para>
/// </summary>
// ReSharper disable once UnusedTypeParameter
public interface IClusterFixture<TCluster> : IPartitionFixture<TCluster>
	where TCluster : IPartitionLifetime;
