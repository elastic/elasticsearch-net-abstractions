// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Plugins;
using Elastic.Stack.ArtifactsApi;

namespace Elastic.Elasticsearch.Xunit
{
	public class XunitClusterConfiguration : EphemeralClusterConfiguration
	{
		public XunitClusterConfiguration(
			ElasticVersion version,
			ClusterFeatures features = ClusterFeatures.None,
			ElasticsearchPlugins plugins = null,
			int numberOfNodes = 1)
			: base(version, features, plugins, numberOfNodes)
		{
			this.AdditionalAfterStartedTasks.Add(new PrintXunitAfterStartedTask());
		}

		/// <inheritdoc />
		protected override string NodePrefix => "xunit";

		/// <summary>
		/// The maximum number of tests that can run concurrently against a cluster using this configuration.
		/// </summary>
		public int MaxConcurrency { get; set; }

		/// <summary>
		/// The maximum amount of time a cluster can run using this configuration.
		/// </summary>
		public TimeSpan? Timeout { get; set; }
	}
}
