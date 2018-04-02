using System;
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

	public interface IEphemeralCluster<out TConfiguration> : ICluster<TConfiguration>
		where TConfiguration : EphemeralClusterConfiguration
	{
		IElasticClient Client { get; }
		ElasticsearchPluginConfiguration[] RequiredPlugins { get; }
	}

	public abstract class EphemeralCluster<TConfiguration>
		: ClusterBase<TConfiguration>, IEphemeralCluster<TConfiguration>
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

		protected virtual string ClientHostName => "localhost";
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

					var hosts = this.Nodes.Select(n => new Uri($"http://{this.ClientHostName}:{n.NodeConfiguration.DesiredPort ?? 9200}"));
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
