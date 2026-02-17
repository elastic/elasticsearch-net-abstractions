// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Ephemeral.Plugins;
using Elastic.Stack.ArtifactsApi;

namespace Elastic.Elasticsearch.TUnit;

internal enum BootstrapDiagnosticsMode
{
	/// <summary> No bootstrap output. </summary>
	None,

	/// <summary> Periodic progress heartbeat showing elapsed time and last log line. </summary>
	Progress,

	/// <summary> Full verbose ANSI-colored output of every log line. </summary>
	Full
}

public class ElasticsearchConfiguration : EphemeralClusterConfiguration
{
	public ElasticsearchConfiguration(
		ElasticVersion version,
		ClusterFeatures features = ClusterFeatures.None,
		ElasticsearchPlugins plugins = null,
		int numberOfNodes = 1)
		: base(version, features, plugins, numberOfNodes) =>
		AdditionalAfterStartedTasks.Add(new PrintAfterStartedTask());

	/// <inheritdoc />
	protected override string NodePrefix => "tunit";

	/// <summary>
	///     The maximum amount of time to wait for the cluster to start.
	/// </summary>
	public TimeSpan StartTimeout { get; set; } = TimeSpan.FromMinutes(2);

	/// <summary>
	///     Controls cluster bootstrap diagnostics output to the terminal.
	///     <list type="bullet">
	///         <item><c>true</c> — always show full verbose output (every log line, ANSI-colored)</item>
	///         <item><c>false</c> — suppress all bootstrap output</item>
	///         <item>
	///             <c>null</c> (default) — auto-detect:
	///             CI and non-interactive environments get full verbose output;
	///             interactive terminals get a periodic progress heartbeat
	///         </item>
	///     </list>
	/// </summary>
	public bool? ShowBootstrapDiagnostics { get; set; }

	/// <summary>
	///     The interval between progress heartbeat messages when running in
	///     interactive mode (default auto-detect). Defaults to 5 seconds.
	/// </summary>
	public TimeSpan ProgressInterval { get; set; } = TimeSpan.FromSeconds(5);

	internal BootstrapDiagnosticsMode ResolveDiagnosticsMode()
	{
		if (ShowBootstrapDiagnostics == true)
			return BootstrapDiagnosticsMode.Full;
		if (ShowBootstrapDiagnostics == false)
			return BootstrapDiagnosticsMode.None;

		// CI environments: always full output
		if (IsCi())
			return BootstrapDiagnosticsMode.Full;

		// Interactive terminal: periodic progress heartbeat
		if (Environment.UserInteractive)
			return BootstrapDiagnosticsMode.Progress;

		// Non-interactive, non-CI (rare): full output as a safe default
		return BootstrapDiagnosticsMode.Full;
	}

	private static bool IsCi() =>
		!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"))
		|| !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TF_BUILD"))
		|| !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS"))
		|| !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JENKINS_URL"))
		|| !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"))
		|| !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILDKITE"));
}
