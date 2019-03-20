using System;
using System.Linq;
using Elastic.Managed.ConsoleWriters;
using static Elastic.Managed.Ephemeral.ClusterFeatures;

namespace Elastic.Managed.Ephemeral.Tasks.InstallationTasks
{
	public class PrintConfiguration: ClusterComposeTask
	{
		public override void Run(IEphemeralCluster<EphemeralClusterConfiguration> cluster)
		{
			var c = cluster.ClusterConfiguration;
			var version = c.Version;

			string F(ClusterFeatures feature) => c.Features.HasFlag(feature) ? Enum.GetName(typeof(ClusterFeatures), feature) : string.Empty;

			var features = string.Join("|", new[] {F(Security), F(ClusterFeatures.XPack), F(SSL)}.Where(v=>!string.IsNullOrWhiteSpace(v)));
			features = string.IsNullOrWhiteSpace(features) ? "None" : features;
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} starting {{{version}}} with features [{features}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.NumberOfNodes)}}} [{c.NumberOfNodes}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.ClusterName)}}} [{c.ClusterName}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.EnableSsl)}}} [{c.EnableSsl}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.EnableSecurity)}}} [{c.EnableSecurity}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.XPackInstalled)}}} [{c.XPackInstalled}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.Plugins)}}} [{c.Plugins}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.TrialMode)}}} [{c.TrialMode}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.CacheEsHomeInstallation)}}} [{c.CacheEsHomeInstallation}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.ShowElasticsearchOutputAfterStarted)}}} [{c.ShowElasticsearchOutputAfterStarted}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.ValidatePluginsToInstall)}}} [{c.ValidatePluginsToInstall}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.PrintYamlFilesInConfigFolder)}}} [{c.PrintYamlFilesInConfigFolder}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.SkipBuiltInAfterStartTasks)}}} [{c.SkipBuiltInAfterStartTasks}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.HttpFiddlerAware)}}} [{c.HttpFiddlerAware}]");
			cluster.Writer?.WriteDiagnostic($"{{{nameof(PrintConfiguration)}}} {{{nameof(c.NoCleanupAfterNodeStopped)}}} [{c.NoCleanupAfterNodeStopped}]");
		}


	}
}
