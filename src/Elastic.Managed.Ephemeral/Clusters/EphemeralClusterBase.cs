using System;
using System.Linq;
using Elastic.Managed.Clusters;
using Elastic.Managed.Configuration;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Managed.Ephemeral.Tasks;
using Elastic.Managed.FileSystem;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Managed.Ephemeral.Clusters
{
	public abstract class EphemeralClusterBase : ClusterBase
	{
		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		public string ClusterName => $"ephemeral-cluster-{_uniqueSuffix}";

		protected EphemeralClusterBase(ElasticsearchVersion version, int instanceCount = 1, NodeFeatures nodeFeatures = NodeFeatures.None)
			: base(new EphemeralFileSystem(version), instanceCount, nodeFeatures) => this.Composer = new EphemeralClusterComposer(this);

		protected override string CreateNodeName(int p)
		{
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"ephemeral-node-{suffix}{p}";
		}

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

					var hosts = this.Nodes.Select(n => new Uri($"http://localhost:{n.DesiredPort}"));
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

		protected abstract ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings);

	}
}
