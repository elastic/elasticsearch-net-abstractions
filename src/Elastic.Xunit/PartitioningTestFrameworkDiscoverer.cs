// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit;

internal class PartitioningTestFrameworkDiscoverer<TOptions> : XunitTestFrameworkDiscoverer
	where TOptions : PartitioningRunOptions, new()
{
	public PartitioningTestFrameworkDiscoverer(
		IAssemblyInfo assemblyInfo,
		ISourceInformationProvider sourceProvider,
		IMessageSink diagnosticMessageSink,
		IXunitTestCollectionFactory? collectionFactory = null) : base(
		assemblyInfo, sourceProvider, diagnosticMessageSink, collectionFactory)
	{
		var a = Assembly.Load(new AssemblyName(assemblyInfo.Name));
		Options = PartitioningConfigurationAttribute.GetOptions<TOptions>(a);
	}

	private TOptions Options { get; }

	protected override bool FindTestsForType(ITestClass testClass, bool includeSourceInformation,
		IMessageBus messageBus, ITestFrameworkDiscoveryOptions discoveryOptions)
	{
		Options.SetOptions(discoveryOptions);
		return base.FindTestsForType(testClass, includeSourceInformation, messageBus, discoveryOptions);
	}
}
