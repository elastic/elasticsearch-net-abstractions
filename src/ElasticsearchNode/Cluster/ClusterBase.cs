using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Elastic.ManagedNode.Configuration;

namespace Elastic.ManagedNode.Cluster
{
	public abstract class ClusterBase : IDisposable
	{
		public IClusterComposer Composer { get; protected set; }
		public INodeFileSystem FileSystem { get; }
		public ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		//public virtual ElasticsearchPlugin[] RequiredPlugins { get; }
		public string ClusterMoniker => this.GetType().Name.Replace("Cluster", "");

		protected ClusterBase(ElasticsearchVersion version, int instanceCount = 1, string clusterName = null)
			: this(new NodeFileSystem(version, clusterName), instanceCount) { }

		protected ClusterBase(INodeFileSystem fileSystem, int instanceCount = 1)
		{
			this.FileSystem = fileSystem;
			this._clusterSettings = DefaultClusterSettings(instanceCount);

			var nodes = Enumerable.Range(9200, instanceCount)
				.Select(p=> new NodeConfiguration(fileSystem, NodeFeatures.None, p, CreateNodeName(p)))
				.Select(CreateNodeClusterSettings)
				.Select(n => new ElasticsearchNode(n)
				{
					AssumeStartedOnNotEnoughMasterPing = instanceCount > 1
				})
				.ToList();
			this.Nodes = new ReadOnlyCollection<ElasticsearchNode>(nodes);
			//this.TaskRunner = new ClusterTaskRunner(config);
		}

		protected virtual string CreateNodeName(int i) => null;

		protected virtual Dictionary<string, string> AdditonalNodeSettings(int port) => null;

		private static Dictionary<string, string> DefaultClusterSettings(int instanceCount) => new Dictionary<string, string>
		{
			{"node.max_local_storage_nodes", $"{instanceCount}"},
			{"discovery.zen.minimum_master_nodes", Quorum(instanceCount).ToString()}
		};

		private static int Quorum(int instanceCount) => Math.Max(1, (int) Math.Floor((double) instanceCount / 2) + 1);

		private readonly Dictionary<string, string> _clusterSettings;
		private NodeConfiguration CreateNodeClusterSettings(NodeConfiguration config)
		{
			foreach(var kv in this._clusterSettings) config.Add(kv.Key, kv.Value);

			var nodeSettings = AdditonalNodeSettings(config.DesiredPort);
			if (nodeSettings == null || nodeSettings.Count == 0) return config;
			foreach (var kv in nodeSettings) config.Add(kv.Key, kv.Value);
			return config;
		}

		protected bool Started { get; set; }

//		protected abstract ConnectionSettings CreateConnectionSettings(ConnectionSettings connectionSettings);

		protected virtual void SeedNode() { }

		public void Start() => this.Start(TimeSpan.FromMinutes(2));
		public void Start(TimeSpan waitForStarted) =>
			this.Start(new HighlightWriter(this.Nodes.Select(n => n.NodeConfiguration.NodeName).ToArray()), waitForStarted);

		public void Start(IConsoleOutWriter writer, TimeSpan waitForStarted)
		{
			this.Composer?.Install();
			this.Composer?.OnBeforeStart();
//			this.TaskRunner.Install(this.AdditionalInstallationTasks, this.RequiredPlugins);
//			this.TaskRunner.OnBeforeStart();

			foreach (var node in this.Nodes) node.Subscribe(writer);

			var waitHandles = this.Nodes.Select(w => w.StartedHandle).ToArray();
			if (!WaitHandle.WaitAll(waitHandles, waitForStarted))
				throw new Exception($"Not all nodes started on time");

			this.Started = true;
			this.Composer?.ValidateAfterStart();
			//this.TaskRunner.ValidateAfterStart(this.Client, this.RequiredPlugins ?? new ElasticsearchPlugin[]{});
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
			this.Composer.OnStop();
			//this.TaskRunner?.Dispose();
		}
	}
}
