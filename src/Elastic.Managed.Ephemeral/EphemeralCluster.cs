using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.Configuration;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralCluster : EphemeralCluster<EphemeralClusterConfiguration>
	{
		public EphemeralCluster(ElasticsearchVersion version, int numberOfNodes = 1)
			: base(new EphemeralClusterConfiguration(version, ClusterFeatures.None, numberOfNodes: numberOfNodes)) { }

		public EphemeralCluster(EphemeralClusterConfiguration clusterConfiguration) : base(clusterConfiguration) { }
	}

	public abstract class EphemeralCluster<TConfiguration> : ClusterBase<TConfiguration>, IEphemeralCluster<TConfiguration>
		where TConfiguration : EphemeralClusterConfiguration
	{
		protected EphemeralCluster(TConfiguration clusterConfiguration) : base(clusterConfiguration)
		{
			this.Composer = new EphemeralClusterComposer<TConfiguration>(this);
		}

		protected EphemeralClusterComposer<TConfiguration> Composer { get; }

		protected override void OnBeforeStart()
		{
			this.Composer.Install();
			this.Composer.OnBeforeStart();
		}

		protected override void OnDispose() => this.Composer.OnStop();

		protected override void OnAfterStarted() => this.Composer.OnAfterStart();

		public ICollection<Uri> NodesUris(string hostName = "ipv4.fiddler")
		{
			var ssl = this.ClusterConfiguration.EnableSsl ? "s" : "";
			return this.Nodes
				.Select(n=>$"http{ssl}://{hostName}:{n.Port ?? 9200}")
				.Distinct()
				.Select(n => new Uri(n))
				.ToList();
		}

		protected override string SeeLogsMessage(string message)
		{
			var log = Path.Combine(this.FileSystem.LogsPath, $"{this.ClusterConfiguration.ClusterName}.log");
			if (!File.Exists(log)) return message;
			using (var fileStream = new FileStream(log, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var textReader = new StreamReader(fileStream))
			{
				var logContents = textReader.ReadToEnd();
				return message + $" contents of {log}:{Environment.NewLine}" + logContents;
			}
		}
	}
}
