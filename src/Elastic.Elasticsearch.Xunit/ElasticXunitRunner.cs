// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Ephemeral;

namespace Elastic.Elasticsearch.Xunit
{
	public static class ElasticXunitRunner
	{
		public static IEphemeralCluster<XunitClusterConfiguration> CurrentCluster { get; internal set; }
	}
}
