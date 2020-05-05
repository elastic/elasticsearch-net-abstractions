// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.Sdk
{
	public class ElasticTestFramework : XunitTestFramework
	{
		public ElasticTestFramework(IMessageSink messageSink) : base(messageSink) { }

		protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo) =>
			new ElasticTestFrameworkDiscoverer(assemblyInfo, this.SourceInformationProvider, this.DiagnosticMessageSink);

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		{
			var assembly = Assembly.Load(assemblyName);
			var options = assembly.GetCustomAttribute<ElasticXunitConfigurationAttribute>()?.Options ?? new ElasticXunitRunOptions();

			return new TestFrameworkExecutor(assemblyName, this.SourceInformationProvider, this.DiagnosticMessageSink)
			{
				Options = options
			};
		}
	}
}
