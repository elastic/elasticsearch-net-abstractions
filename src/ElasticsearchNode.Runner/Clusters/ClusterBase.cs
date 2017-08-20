using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Elastic.Net.Abstractions.Tasks;
using Elastic.Net.Abstractions.Tasks.InstallationTasks;
using Elastic.ProcessManagement;
using Elasticsearch.Net;
using Nest;

namespace Elastic.Net.Abstractions.Clusters
{
	public abstract class ClusterBase : IDisposable
	{
		protected ClusterBase(ElasticsearchVersion version, int instanceCount = 1, string clusterName = null, string[] additionalSettings = null)
		{
			var n = new NodeConfiguration(version, clusterName, null);
			this.NodeConfiguration = n;

			var nodeSettings = n.CreateSettings(additionalSettings);
			this.NodeSettings = nodeSettings;
			var fs = this.NodeConfiguration.FileSystem;
			var nodes = Enumerable.Range(9200, instanceCount)
				.Select(p => new ElasticsearchNode(fs.Binary, n.CreateSettings(nodeSettings, NodeSpecificSettings(p)))
				{
					DesiredPort = p,
					AssumeStartedOnNotEnoughMasterPing = instanceCount > 1
				})
				.ToList();
			this.Nodes = new ReadOnlyCollection<ElasticsearchNode>(nodes);
			this.TaskRunner = new NodeTaskRunner(this.NodeConfiguration);
		}

		protected virtual IEnumerable<string> NodeSpecificSettings(int port)
		{
			yield return $"http.port={port}";
		}

		public ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		private NodeConfiguration NodeConfiguration { get; }
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

		public void Start(TimeSpan waitTimeout = default(TimeSpan))
		{
			this.TaskRunner.Install(this.AdditionalInstallationTasks);
			var nodeSettings = this.NodeConfiguration.CreateSettings(this.NodeSettings);
			this.TaskRunner.OnBeforeStart(nodeSettings);

			foreach (var node in this.Nodes)
			{
				var started = node.Start(waitTimeout);
				if (!started)
					throw new CleanExitException($"failed to start cluster node {node.DesiredPort}");
			}

			this.Started = true;
			this.TaskRunner.ValidateAfterStart(this.Client);
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
			foreach (var node in this.Nodes)
				node?.Dispose();
			this.TaskRunner?.Dispose();
		}
	}
}
