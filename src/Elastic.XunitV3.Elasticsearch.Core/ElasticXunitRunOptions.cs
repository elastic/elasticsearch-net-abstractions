// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Elastic.Stack.ArtifactsApi;
using Nullean.Xunit.Partitions.v3;

namespace Elastic.XunitV3.Elasticsearch.Core;

/// <summary>
///     Options for the Elasticsearch xUnit v3 test pipeline.
///     Inherits partition/test filtering from <see cref="PartitionOptions" />.
///     <para>
///         Register with:
///         <code>[assembly: PartitionOptions(typeof(MyRunOptions))]</code>
///         or use the convenience alias:
///         <code>[assembly: ElasticXunitConfiguration(typeof(MyRunOptions))]</code>
///     </para>
/// </summary>
public class ElasticXunitRunOptions : PartitionOptions
{
	/// <summary>
	///     The Elasticsearch version under test. Informational — version-skip evaluation
	///     uses the version resolved from each cluster's <see cref="ElasticsearchCluster{TConfiguration}.InitializeAsync" />.
	/// </summary>
	public ElasticVersion Version { get; set; }
}
