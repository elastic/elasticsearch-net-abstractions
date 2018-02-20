using System;
using System.Linq;
using Elastic.Managed.Clusters;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephimeral.Plugins;
using Elastic.Managed.Ephimeral.Tasks;
using Elastic.Managed.FileSystem;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Managed.Ephimeral.Clusters
{
	public abstract class EphimeralClusterBase : ClusterBase
	{
		public EphimeralClusterBase(ElasticsearchVersion version, int instanceCount = 1) : base(new LocalAppDataFileSystem(version), instanceCount)
		{
			var config = this.Nodes.First().NodeConfiguration;
			this.Composer = new EphimeralClusterComposer(this);
		}

		private readonly object _lockGetClient = new object { };
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

					var hosts = this.Nodes.Select(n => new Uri($"http://localhost:{n.DesiredPort}"));
					var settings = new ConnectionSettings(new StaticConnectionPool(hosts));
					var client = new ElasticClient(CreateConnectionSettings(settings));
					this._client = client;
				}
				return this._client;
			}
		}

		public virtual ElasticsearchPlugin[] RequiredPlugins { get; } = new ElasticsearchPlugin[0];

		protected abstract ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings);

	}
}
