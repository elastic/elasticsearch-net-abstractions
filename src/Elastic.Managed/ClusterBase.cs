using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Elastic.Managed.Configuration;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed
{
	public abstract class ClusterBase : IDisposable
	{
		protected ClusterBase(ClusterConfiguration clusterConfiguration)
		{
			this.ClusterConfiguration = clusterConfiguration;

			var nodes = Enumerable.Range(9200, this.ClusterConfiguration.NumberOfNodes)
				.Select(p => new NodeConfiguration(clusterConfiguration))
				.Select(n => new ElasticsearchNode(n)
				{
					AssumeStartedOnNotEnoughMasterPing = this.ClusterConfiguration.NumberOfNodes > 1
				})
				.ToList();
			this.Nodes = new ReadOnlyCollection<ElasticsearchNode>(nodes);
			this.ClusterMoniker = this.GetType().Name.Replace("Cluster", "");
		}

		/// <summary> A short name to identify the cluster defaults to the <see cref="ClusterBase"/> subclass name with Cluster removed </summary>
		public virtual string ClusterMoniker { get; }

		public ClusterConfiguration ClusterConfiguration { get; }
		public INodeFileSystem FileSystem => this.ClusterConfiguration.FileSystem;

		public ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		public bool Started { get; private set; }
		public IConsoleLineWriter Writer { get; private set; }

		protected virtual void SeedCluster()
		{
		}

		public void Start() => this.Start(TimeSpan.FromMinutes(2));

		public void Start(TimeSpan waitForStarted) =>
			this.Start(new HighlightWriter(this.Nodes.Select(n => n.NodeConfiguration.DesiredNodeName).ToArray()), waitForStarted);

		public void Start(IConsoleLineWriter writer, TimeSpan waitForStarted)
		{
			this.Writer = writer;

			this.OnBeforeStart();

			foreach (var node in this.Nodes) node.Subscribe(writer);

			var waitHandles = this.Nodes.Select(w => w.StartedHandle).ToArray();
			if (!WaitHandle.WaitAll(waitHandles, waitForStarted))
			{
				writer?.WriteError($"{{{this.GetType().Name}.{nameof(Start)}}} cluster did not start after {waitForStarted}");
				throw new Exception($"Not all nodes started after waiting {waitForStarted}");
			}

			this.Started = this.Nodes.All(n => n.NodeStarted);
			if (this.Started)
			{
				this.OnAfterStarted();
				this.SeedCluster();
			}
			else writer?.WriteError($"{{{this.GetType().Name}.{nameof(Start)}}} cluster did not start succesfully");
		}

		public void WaitForExit(TimeSpan waitForCompletion)
		{
			foreach (var node in this.Nodes)
				node.WaitForCompletion(waitForCompletion);
		}

		protected virtual void OnAfterStarted() { }

		protected virtual void OnBeforeStart() { }

		protected virtual void OnDispose() { }

		public void Dispose()
		{
			this.Started = false;
			foreach (var node in this.Nodes) node.SendControlC();
			foreach (var node in this.Nodes) node?.Dispose();
			this.OnDispose();
		}
	}
}
