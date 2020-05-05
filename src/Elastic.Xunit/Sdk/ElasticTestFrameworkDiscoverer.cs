// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	public class ElasticTestFrameworkDiscoverer : XunitTestFrameworkDiscoverer
	{
		public ElasticTestFrameworkDiscoverer(IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink, IXunitTestCollectionFactory collectionFactory = null) : base(assemblyInfo, sourceProvider, diagnosticMessageSink, collectionFactory)
		{
			var a = Assembly.Load(new AssemblyName(assemblyInfo.Name));
			var options = a.GetCustomAttribute<ElasticXunitConfigurationAttribute>()?.Options ?? new ElasticXunitRunOptions();
			this.Options = options;
		}

		/// <summary>
		/// The options for
		/// </summary>
		public ElasticXunitRunOptions Options { get; }

		protected override bool FindTestsForType(ITestClass testClass, bool includeSourceInformation, IMessageBus messageBus, ITestFrameworkDiscoveryOptions discoveryOptions)
		{
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.Version), this.Options.Version);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunIntegrationTests), this.Options.RunIntegrationTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.IntegrationTestsMayUseAlreadyRunningNode), this.Options.IntegrationTestsMayUseAlreadyRunningNode);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.RunUnitTests), this.Options.RunUnitTests);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.TestFilter), this.Options.TestFilter);
			discoveryOptions.SetValue(nameof(ElasticXunitRunOptions.ClusterFilter), this.Options.ClusterFilter);
			return base.FindTestsForType(testClass, includeSourceInformation, messageBus, discoveryOptions);
		}
	}
}
