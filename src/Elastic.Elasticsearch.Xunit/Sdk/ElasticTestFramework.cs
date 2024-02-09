// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Xunit.Abstractions;
using Xunit.Sdk;
using Nullean.Xunit.Partitions;
using Nullean.Xunit.Partitions.Sdk;

namespace Elastic.Elasticsearch.Xunit.Sdk;

// ReSharper disable once UnusedType.Global
public class ElasticTestFramework : PartitionTestFramework<ElasticXunitRunOptions, TestAssemblyRunnerFactory, TestFrameworkDiscovererFactory>
{
	public ElasticTestFramework(IMessageSink messageSink) : base(messageSink) { }
}

public class TestFrameworkDiscovererFactory : ITestFrameworkDiscovererFactory
{
	public XunitTestFrameworkDiscoverer Create<TOptions>(
		IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink
	)
		where TOptions : PartitionOptions, new() =>
		new PartitionTestFrameworkDiscoverer<TOptions>(assemblyInfo, sourceProvider, diagnosticMessageSink, typeof(IClusterFixture<>));
}
