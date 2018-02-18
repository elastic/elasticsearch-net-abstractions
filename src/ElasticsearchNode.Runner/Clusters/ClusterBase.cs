using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Elastic.ManagedNode;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Plugins;
using Elastic.Net.Abstractions.Tasks;
using Elastic.Net.Abstractions.Tasks.InstallationTasks;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Net.Abstractions.Clusters
{
	public abstract class ClusterBase : IDisposable
	{
		private readonly string _uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
		private readonly string _clusterName;

		public string ClusterMoniker => "default";
		public string ClusterName => this._clusterName ?? $"{this.ClusterMoniker}-cluster-{_uniqueSuffix}";

		protected ClusterBase(ElasticsearchVersion version, int instanceCount = 1, string clusterName = null, Dictionary<string, string> clusterSettings = null)
		{
			this._clusterName = clusterName;
			clusterSettings = DefaultClusterSettings(instanceCount, clusterSettings);

			var nodes = Enumerable.Range(9200, instanceCount)
				.Select(p=> new NodeConfiguration(version, ClusterName, null, CreateNodeSpecificClusterSettings(p, clusterSettings), p))
				.Select(n => new ElasticsearchNode(n)
				{
					AssumeStartedOnNotEnoughMasterPing = instanceCount > 1
				})
				.ToList();
			this.Nodes = new ReadOnlyCollection<ElasticsearchNode>(nodes);
			var config = this.Nodes.First().NodeConfiguration;
			this.TaskRunner = new NodeTaskRunner(config);
		}

		public ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		public virtual ElasticsearchPlugin[] RequiredPlugins { get; }

		private static Dictionary<string, string> DefaultClusterSettings(int instanceCount, Dictionary<string, string> clusterSettings) =>
			new Dictionary<string, string>(clusterSettings ?? new Dictionary<string, string>())
            {
                {"node.max_local_storage_nodes", $"{instanceCount}" },
                {"discovery.zen.minimum_master_nodes", Quorum(instanceCount).ToString() }
            };

		protected static int Quorum(int instanceCount) => Math.Max(1, (int) Math.Floor((double) instanceCount / 2) + 1);

		private Dictionary<string, string> CreateNodeSpecificClusterSettings(int port, Dictionary<string, string> clusterSettings)
		{
			var nodeSettings = AdditonalNodeSettings(port);
			if (nodeSettings == null || nodeSettings.Count == 0) return clusterSettings;
			var n = new Dictionary<string, string>(clusterSettings);
			foreach (var kv in nodeSettings) n[kv.Key] = kv.Value;
			return n;
		}

		protected virtual Dictionary<string, string> AdditonalNodeSettings(int port) => null;

		private NodeTaskRunner TaskRunner { get; }
		private bool Started { get; set; }

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

		protected abstract ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings);

		private string[] NodeSettings { get; } = { };
		protected virtual InstallationTaskBase[] AdditionalInstallationTasks { get; } = { };

		protected virtual void SeedNode()
		{
		}

		public void Start(IElasticsearchConsoleOutWriter writer)
		{
			writer = writer ?? new ElasticsearchConsoleOutWriter();
			this.TaskRunner.Install(this.AdditionalInstallationTasks, this.RequiredPlugins);
			this.TaskRunner.OnBeforeStart();

			//foreach (var node in this.Nodes)
			foreach (var node in this.Nodes)
			{
				node.Start(writer);
			}

			var waitHandles = this.Nodes.Select(w => w.StartedHandle).ToArray();
			if (!WaitHandle.WaitAll(waitHandles, TimeSpan.FromSeconds(120)))
				throw new Exception($"Not all nodes started on time");

			this.Started = true;
			this.TaskRunner.ValidateAfterStart(this.Client, this.RequiredPlugins ?? new ElasticsearchPlugin[]{});
//			if (this.Node.Port != this.Node.)
//				throw new Exception($"The cluster that was started of type {this.GetType().Name} runs on {this.Node.Port} but this cluster wants {this.DesiredPort}");
			this.SeedNode();

		}


		public void WaitForExit(TimeSpan waitForCompletion)
		{
			foreach (var node in this.Nodes)
				node.WaitForCompletion(waitForCompletion);
		}

		public void Dispose()
		{
			this.Started = false;
			foreach (var node in this.Nodes) node.SendControlC();
			foreach (var node in this.Nodes) node?.Dispose();
			this.TaskRunner?.Dispose();
		}
	}
}
