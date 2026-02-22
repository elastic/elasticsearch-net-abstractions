// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Elastic.Stack.ArtifactsApi;
using Nullean.Xunit.Partitions.v3.Sdk;
using ProcNet.Std;

namespace Elastic.Xunitv3.Elasticsearch.Core;

/// <summary>
///     Base class for an Elasticsearch cluster that integrates with xUnit v3's partition lifecycle.
///     Implements <see cref="IPartitionLifetime" /> so the partition framework manages startup and teardown.
///     <para>
///         Supports skipping ephemeral cluster startup when an external cluster is
///         available. The resolution order in <see cref="InitializeAsync" /> is:
///         <list type="number">
///             <item>
///                 <see cref="TryUseExternalCluster" /> — override for programmatic control
///             </item>
///             <item>
///                 <c>TEST_ELASTICSEARCH_URL</c> environment variable (with optional
///                 <c>TEST_ELASTICSEARCH_API_KEY</c>)
///             </item>
///             <item>Start an ephemeral cluster as usual</item>
///         </list>
///     </para>
/// </summary>
public abstract class ElasticsearchCluster<TConfiguration> : EphemeralCluster<TConfiguration>,
	IPartitionLifetime
	where TConfiguration : ElasticsearchConfiguration
{
	/// <summary>
	///     A registry of cluster type to its resolved Elasticsearch version,
	///     populated during <see cref="InitializeAsync" /> and consumed by
	///     <see cref="SkipVersionAttribute" /> for version-skip evaluation.
	/// </summary>
	internal static ConcurrentDictionary<Type, ElasticVersion> ClusterVersions { get; } = new();

	private ExternalClusterConfiguration _externalCluster;

	protected ElasticsearchCluster(TConfiguration configuration) : base(configuration)
	{
	}

	/// <summary>
	///     Whether this cluster is backed by an external (remote) Elasticsearch instance
	///     rather than a locally managed ephemeral process.
	/// </summary>
	public bool IsExternal => _externalCluster != null;

	/// <summary>
	///     The API key for the external cluster, or <c>null</c> if not applicable.
	/// </summary>
	public string ExternalApiKey => _externalCluster?.ApiKey;

	/// <summary>
	///     Override to programmatically provide an external Elasticsearch cluster,
	///     skipping ephemeral cluster startup entirely. Return <c>null</c> to fall
	///     through to environment variable detection and then ephemeral startup.
	/// </summary>
	protected virtual ExternalClusterConfiguration TryUseExternalCluster() => null;

	/// <summary>
	///     Returns the node URIs for this cluster. When connected to an external cluster,
	///     returns the external URI; otherwise delegates to the ephemeral cluster's nodes.
	/// </summary>
	public override ICollection<Uri> NodesUris(string hostName = null) =>
		_externalCluster != null ? [_externalCluster.Uri] : base.NodesUris(hostName);

	/// <inheritdoc />
	int? IPartitionLifetime.MaxConcurrency => ClusterConfiguration.MaxConcurrency;

	/// <inheritdoc />
	string IPartitionLifetime.FailureTestOutput() => BuildFailureDiagnostics();

	/// <summary>
	///     Starts the Elasticsearch cluster. Called by the partition framework before
	///     running any tests that belong to this partition.
	///     <para>
	///         Before starting an ephemeral cluster, checks for an external cluster
	///         via <see cref="TryUseExternalCluster" /> and then the
	///         <c>TEST_ELASTICSEARCH_URL</c> environment variable.
	///     </para>
	/// </summary>
	public async ValueTask InitializeAsync()
	{
		var external = ResolveExternalCluster();
		if (external != null)
		{
			await external.ValidateAsync().ConfigureAwait(false);
			_externalCluster = external;
			ClusterVersions[GetType()] = ClusterConfiguration.Version;
			WriteExternalClusterInfo(external);
			return;
		}

		var writer = CreateBootstrapWriter();
		try
		{
			await Task.Run(() => Start(writer, ClusterConfiguration.StartTimeout)).ConfigureAwait(false);
			ClusterVersions[GetType()] = ClusterConfiguration.Version;
		}
		catch (Exception ex)
		{
			WriteBootstrapFailure(ex);
			throw;
		}
		finally
		{
			(writer as BootstrapProgressWriter)?.Stop();

			if (writer != null)
				Console.OpenStandardOutput().Write("\n\n"u8);
		}
	}

	/// <summary>
	///     Disposes the Elasticsearch cluster. No-op when using an external cluster.
	///     Called by the partition framework after all tests in this partition complete.
	/// </summary>
	public ValueTask DisposeAsync()
	{
		if (_externalCluster == null)
			Dispose();
		return default;
	}

	private IConsoleLineHandler CreateBootstrapWriter()
	{
		var mode = ClusterConfiguration.ResolveDiagnosticsMode();
		return mode switch
		{
			BootstrapDiagnosticsMode.Full =>
				new AnsiConsoleLineWriter(Nodes.Select(n => n.NodeConfiguration.DesiredNodeName).ToList()),
			BootstrapDiagnosticsMode.Progress =>
				new BootstrapProgressWriter(
					GetType().Name,
					FileSystem.ElasticsearchHome,
					ClusterConfiguration.ProgressInterval),
			_ => null
		};
	}

	private ExternalClusterConfiguration ResolveExternalCluster()
	{
		var programmatic = TryUseExternalCluster();
		if (programmatic != null)
			return programmatic;

		var url = Environment.GetEnvironmentVariable("TEST_ELASTICSEARCH_URL");
		if (string.IsNullOrWhiteSpace(url))
			return null;

		if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
			throw new InvalidOperationException(
				$"TEST_ELASTICSEARCH_URL is set but is not a valid URI: {url}");

		var apiKey = Environment.GetEnvironmentVariable("TEST_ELASTICSEARCH_API_KEY");
		return new ExternalClusterConfiguration(uri, string.IsNullOrWhiteSpace(apiKey) ? null : apiKey);
	}

	private void WriteExternalClusterInfo(ExternalClusterConfiguration external)
	{
		var name = GetType().Name;
		var auth = string.IsNullOrEmpty(external.ApiKey) ? "no auth" : "API key";
		var message = $"[{name}] using external cluster at {external.Uri} ({auth}) — skipping ephemeral startup";

		using var stdout = new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
		stdout.WriteLine(message);
	}

	private void WriteBootstrapFailure(Exception ex)
	{
		var diagnostics = BuildFailureDiagnostics(ex);
		Writer?.WriteError(diagnostics);
	}

	private string BuildFailureDiagnostics(Exception ex = null)
	{
		var sb = new StringBuilder();
		sb.AppendLine($"Cluster [{GetType().Name}] failed to start within {ClusterConfiguration.StartTimeout}");
		sb.AppendLine();

		foreach (var node in Nodes)
		{
			var nodeName = node.NodeConfiguration.DesiredNodeName;
			sb.AppendLine($"  Node [{nodeName}]:");
			sb.AppendLine($"    Started: {node.NodeStarted}");
			sb.AppendLine($"    Port: {node.Port?.ToString() ?? "not assigned"}");
			sb.AppendLine($"    Version: {node.Version ?? "unknown"}");
			if (node.LastSeenException != null)
				sb.AppendLine($"    Last Exception: {node.LastSeenException.Message}");
		}

		if (ex != null)
		{
			sb.AppendLine();
			sb.AppendLine(ex.Message);
		}

		sb.AppendLine();
		sb.AppendLine($"Further logs might be available at: {ClusterConfiguration?.FileSystem?.LogsPath}");

		return sb.ToString();
	}
}
