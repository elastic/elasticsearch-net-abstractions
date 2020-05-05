// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.XunitPlumbing
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
