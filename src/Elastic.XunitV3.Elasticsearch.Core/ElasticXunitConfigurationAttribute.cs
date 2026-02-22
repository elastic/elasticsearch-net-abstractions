// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nullean.Xunit.Partitions.v3;

namespace Elastic.XunitV3.Elasticsearch.Core;

/// <summary>
///     An assembly attribute that specifies the <see cref="ElasticXunitRunOptions" />
///     for xUnit v3 tests within the assembly. Convenience alias for
///     <see cref="PartitionOptionsAttribute" />.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class ElasticXunitConfigurationAttribute : PartitionOptionsAttribute
{
	/// <summary>Creates a new instance of <see cref="ElasticXunitConfigurationAttribute" />.</summary>
	/// <param name="type">
	///     A type deriving from <see cref="ElasticXunitRunOptions" /> that specifies the run options
	/// </param>
	public ElasticXunitConfigurationAttribute(Type type) : base(type) { }
}
