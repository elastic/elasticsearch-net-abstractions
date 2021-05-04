// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.Sdk
{
	public class ElasticTestFrameworkDiscoverer : XunitTestFrameworkDiscoverer
	{
		public ElasticTestFrameworkDiscoverer(IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceProvider,
			IMessageSink diagnosticMessageSink, IXunitTestCollectionFactory collectionFactory = null) : base(
			assemblyInfo, sourceProvider, diagnosticMessageSink, collectionFactory)
		{
			var a = Assembly.Load(new AssemblyName(assemblyInfo.Name));
			var options = a.GetCustomAttribute<ElasticXunitConfigurationAttribute>()?.Options ??
			              new ElasticXunitRunOptions();
			Options = options;
		}

		/// <summary>
		///     The options for
		/// </summary>
		public ElasticXunitRunOptions Options { get; }

		protected override bool FindTestsForType(ITestClass testClass, bool includeSourceInformation,
			IMessageBus messageBus, ITestFrameworkDiscoveryOptions discoveryOptions)
		{
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.Version), Options.Version);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), Options.RunIntegrationTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.IntegrationTestsMayUseAlreadyRunningNode),
				Options.IntegrationTestsMayUseAlreadyRunningNode);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), Options.RunUnitTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), Options.TestFilter);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), Options.ClusterFilter);
			return base.FindTestsForType(testClass, includeSourceInformation, messageBus, discoveryOptions);
		}
	}
}
