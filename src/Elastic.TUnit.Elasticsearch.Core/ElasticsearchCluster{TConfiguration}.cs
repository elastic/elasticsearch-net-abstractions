// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Elastic.Stack.ArtifactsApi;
using ProcNet.Std;
using TUnit.Core;
using TUnit.Core.Interfaces;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     Base class for an Elasticsearch cluster that integrates with TUnit's lifecycle.
///     Implements <see cref="IAsyncInitializer" /> to start the cluster and
///     <see cref="IAsyncDisposable" /> to tear it down.
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
	IAsyncInitializer, IAsyncDisposable
	where TConfiguration : ElasticsearchConfiguration
{
	private static readonly SemaphoreSlim StartupSemaphore = new(1, 1);

	/// <summary>
	///     A registry of cluster type to its resolved Elasticsearch version,
	///     populated during <see cref="InitializeAsync" /> and consumed by
	///     <see cref="ElasticsearchTestHooks" /> for version-skip evaluation.
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

	/// <summary>
	///     Starts the Elasticsearch cluster. Cluster startups are serialized via a
	///     semaphore since Elasticsearch is resource-intensive.
	///     <para>
	///         Before starting an ephemeral cluster, checks for an external cluster
	///         via <see cref="TryUseExternalCluster" /> and then the
	///         <c>TEST_ELASTICSEARCH_URL</c> environment variable.
	///     </para>
	///     <para>
	///         Bootstrap output mode is determined by
	///         <see cref="ElasticsearchConfiguration.ShowBootstrapDiagnostics" />:
	///         <list type="bullet">
	///             <item><c>true</c> — full ANSI-colored verbose output</item>
	///             <item><c>false</c> — silent</item>
	///             <item>
	///                 <c>null</c> (default) — CI gets full output;
	///                 interactive terminals get a periodic progress heartbeat
	///             </item>
	///         </list>
	///     </para>
	/// </summary>
	public async Task InitializeAsync()
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

		await StartupSemaphore.WaitAsync().ConfigureAwait(false);
		try
		{
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

				// Two trailing newlines — TUnit's progress line overwrites the first
				if (writer != null)
					Console.OpenStandardOutput().Write("\n\n"u8);
			}
		}
		finally
		{
			StartupSemaphore.Release();
		}
	}

	/// <summary>
	///     Disposes the Elasticsearch cluster. No-op when using an external cluster.
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
		TestContext.Current?.Output.WriteLine(message);
	}

	private void WriteBootstrapFailure(Exception ex)
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

		sb.AppendLine();
		sb.AppendLine(ex.Message);

		var diagnostics = sb.ToString();

		// Write failure through the cluster's console writer (raw stdout)
		Writer?.WriteError(diagnostics);

		// Write through TUnit's output system when available
		TestContext.Current?.Output.WriteError(diagnostics);
	}
}
