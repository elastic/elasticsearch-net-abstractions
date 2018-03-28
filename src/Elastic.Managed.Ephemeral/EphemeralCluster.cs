using System;
using System.Linq;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Managed.FileSystem;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Managed.Ephemeral
{
	public class EphemeralClusterConfiguration : ClusterConfiguration
	{
		private static string UniqueishSuffix => Guid.NewGuid().ToString("N").Substring(0, 6);
		private static string EphemeralClusterName => $"ephemeral-cluster-{UniqueishSuffix}";

		public EphemeralClusterConfiguration(ElasticsearchVersion version, int numberOfNodes = 1)
			: base(version, numberOfNodes, EphemeralClusterName, (v, s) => new EphemeralFileSystem(v, s)) { }

		public override string CreateNodeName(int? node)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"ephemeral-node-{suffix}{node}";
		}
	}

	public class EphemeralCluster : ClusterBase
	{
		public EphemeralCluster(ElasticsearchVersion version, int numberOfNodes = 1) : this(new EphemeralClusterConfiguration(version, numberOfNodes)) { }

		public EphemeralCluster(EphemeralClusterConfiguration clusterConfiguration) : base(clusterConfiguration) { }

		protected override void OnBeforeStart()
		{
			EphemeralClusterComposer.Install(this);
			EphemeralClusterComposer.OnBeforeStart(this);
		}

		protected override void OnDispose() => EphemeralClusterComposer.OnStop(this);

		protected override void OnAfterStarted() => EphemeralClusterComposer.ValidateAfterStart(this);

		private readonly object _lockGetClient = new object();
		private IElasticClient _client;
		public IElasticClient Client
		{
			get
			{
				if (!this.Started)
					throw new Exception("can not request a client from an ElasticsearchNode if that node hasn't started yet");

				if (this._client != null) return this._client;
				lock (_lockGetClient)
				{
					if (this._client != null) return this._client;

					var hosts = this.Nodes.Select(n => new Uri($"http://localhost:{n.NodeConfiguration.DesiredPort ?? 9200}"));
					var settings = new ConnectionSettings(new StaticConnectionPool(hosts));
					var client = new ElasticClient(CreateConnectionSettings(settings));
					// ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
					// static analysis fails on the first if != null check, reverse it and its fine
					this._client = client;
				}
				return this._client;
			}
		}

		public virtual ElasticsearchPluginConfiguration[] RequiredPlugins { get; } = new ElasticsearchPluginConfiguration[0];

		protected virtual ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings) => connectionSettings;

	}
}
