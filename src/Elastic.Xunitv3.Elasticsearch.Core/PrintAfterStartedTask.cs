// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Tasks;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Xunitv3.Elasticsearch.Core;

/// <summary>
///     A task that writes a diagnostic message after the cluster has started.
///     Outputs through the cluster's <see cref="IConsoleLineHandler" /> (raw stdout).
/// </summary>
public class PrintAfterStartedTask : ClusterComposeTask
{
	/// <inheritdoc />
	public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
	{
		var name = cluster.GetType().Name;
		var uris = string.Join(", ", cluster.NodesUris().Select(u => u.ToString()));
		var version = cluster.ClusterConfiguration.Version;
		var nodeCount = cluster.Nodes.Count;

		var summary = nodeCount == 1
			? $"[{name}] cluster ready — version {version}, node: {uris}"
			: $"[{name}] cluster ready — version {version}, {nodeCount} nodes: {uris}";

		cluster.Writer.WriteDiagnostic(summary);
	}
}
