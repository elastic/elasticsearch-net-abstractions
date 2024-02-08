// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;
using Elastic.Xunit;

namespace Elastic.Elasticsearch.Xunit.Sdk
{
	public class ElasticTestFramework : PartitioningTestFramework<ElasticXunitRunOptions, TestAssemblyRunnerFactory>
	{
		public ElasticTestFramework(IMessageSink messageSink) : base(messageSink) { }
	}
}
