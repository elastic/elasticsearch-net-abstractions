// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Ephemeral;

namespace Elastic.Elasticsearch.Xunit
{
	/// <summary>
	///     Base class for a cluster that integrates with Xunit tests
	/// </summary>
	public abstract class XunitClusterBase : XunitClusterBase<XunitClusterConfiguration>
	{
		protected XunitClusterBase(XunitClusterConfiguration configuration) : base(configuration)
		{
		}
	}

	/// <summary>
	///     Base class for a cluster that integrates with Xunit tests
	/// </summary>
	public abstract class XunitClusterBase<TConfiguration> : EphemeralCluster<TConfiguration>
		where TConfiguration : XunitClusterConfiguration
	{
		protected XunitClusterBase(TConfiguration configuration) : base(configuration)
		{
		}
	}
}
