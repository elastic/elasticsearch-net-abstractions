// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Stack.ArtifactsApi;
using Elastic.XunitV3.Elasticsearch.Core;
using Elastic.XunitV3.ExampleComplex;
using Xunit;

[assembly: TestFramework(typeof(ElasticTestFramework))]
[assembly: ElasticXunitConfiguration(typeof(MyRunOptions))]

namespace Elastic.XunitV3.ExampleComplex;

/// <summary>
///     Allows us to control the xUnit v3 test pipeline
/// </summary>
public class MyRunOptions : ElasticXunitRunOptions
{
	public MyRunOptions()
	{
		Version = TestVersion;
	}

	public static ElasticVersion TestVersion { get; } = "latest-9";
}
