using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Plugins;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralCluster : EphemeralCluster<EphemeralClusterConfiguration>
	{
		public EphemeralCluster(ElasticsearchVersion version, int numberOfNodes = 1)
			: base(new EphemeralClusterConfiguration(version, numberOfNodes: numberOfNodes)) { }

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

		protected override void OnAfterStarted() => this.Composer.ValidateAfterStart();

		public ICollection<Uri> NodesUris(string hostName = "localhost")
		{
			var ssl = this.ClusterConfiguration.EnableSsl ? "s" : "";
			return this.Nodes
				.Select(n=>$"http{ssl}://{hostName}:{n.Port ?? 9200}")
				.Distinct()
				.Select(n => new Uri(n))
				.ToList();
		}

		protected virtual string ClientHostName => "localhost";
		private readonly object _lockGetClient = new object();
		private IElasticClient _client;
		internal IElasticClient Client
		{
			get
			{
				if (!this.Started) throw new Exception("can not request a client from an ElasticsearchNode if that node hasn't started yet");

				if (this._client != null) return this._client;
				lock (_lockGetClient)
				{
					if (this._client != null) return this._client;

					var hosts = this.Nodes.Select(n => new Uri($"http://{this.ClientHostName}:{n.Port ?? 9200}"));
					var settings = new ConnectionSettings(new StaticConnectionPool(hosts));
					var client = new ElasticClient(settings);
					// ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
					// static analysis fails on the first if != null check, reverse it and its fine
					this._client = client;
				}
				return this._client;
			}
		}


		protected override string SeeLogsMessage(string message)
		{

			var log = Path.Combine(this.FileSystem.LogsPath, $"{this.ClusterConfiguration.ClusterName}.log");
			if (!File.Exists(log)) return message;
			using (var fileStream = new FileStream(log, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var textReader = new StreamReader(fileStream))
			{
				var logContents = textReader.ReadToEnd();
				return message + logContents;
			}
		}
	}
}
