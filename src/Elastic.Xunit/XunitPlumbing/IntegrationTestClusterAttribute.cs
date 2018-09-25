using System;
using Elastic.Xunit.Sdk;

namespace Elastic.Xunit.XunitPlumbing
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class IntegrationTestClusterAttribute : Attribute
	{
		public IntegrationTestClusterAttribute(Type clusterType)
		{
			if (!TestAssemblyRunner.IsAnIntegrationTestClusterType(clusterType))
				throw new ArgumentException($"Cluster must be subclass of {nameof(XunitClusterBase)} or {nameof(XunitClusterBase)}<>");
			this.ClusterType = clusterType;
		}

		public Type ClusterType { get; }
	}
}
