// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
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

	protected ElasticsearchCluster(TConfiguration configuration) : base(configuration)
	{
	}

	/// <summary>
	///     Starts the Elasticsearch cluster. Cluster startups are serialized via a
	///     semaphore since Elasticsearch is resource-intensive.
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
	///     Disposes the Elasticsearch cluster.
	/// </summary>
	public ValueTask DisposeAsync()
	{
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
