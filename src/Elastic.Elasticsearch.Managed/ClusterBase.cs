// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using Elastic.Elasticsearch.Managed.Configuration;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Elastic.Elasticsearch.Managed.FileSystem;
using ProcNet.Std;

namespace Elastic.Elasticsearch.Managed
{
	public interface ICluster<out TConfiguration> : IDisposable
		where TConfiguration : IClusterConfiguration<NodeFileSystem>
	{
		string ClusterMoniker { get; }
		TConfiguration ClusterConfiguration { get; }
		INodeFileSystem FileSystem { get; }
		bool Started { get; }
		ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		IConsoleLineHandler Writer { get; }

		IDisposable Start();

		IDisposable Start(TimeSpan waitForStarted);

		IDisposable Start(IConsoleLineHandler writer, TimeSpan waitForStarted);
	}


	public abstract class ClusterBase : ClusterBase<ClusterConfiguration>
	{
		protected ClusterBase(ClusterConfiguration clusterConfiguration) : base(clusterConfiguration) { }
	}

	public abstract class ClusterBase<TConfiguration> : ICluster<TConfiguration>
		where TConfiguration : IClusterConfiguration<NodeFileSystem>
	{
		protected ClusterBase(TConfiguration clusterConfiguration)
		{
			this.ClusterConfiguration = clusterConfiguration;
			this.ClusterMoniker = this.GetType().Name.Replace("Cluster", "");

			NodeConfiguration Modify(NodeConfiguration n, int p)
			{
				this.ModifyNodeConfiguration(n, p);
				return n;
			}

			var nodes =
				(from port in Enumerable.Range(this.ClusterConfiguration.StartingPortNumber, this.ClusterConfiguration.NumberOfNodes)
				let config = new NodeConfiguration(clusterConfiguration, port, this.ClusterMoniker)
				{
					ShowElasticsearchOutputAfterStarted = clusterConfiguration.ShowElasticsearchOutputAfterStarted,
				}
				let node = new ElasticsearchNode(Modify(config, port))
				{
					AssumeStartedOnNotEnoughMasterPing = this.ClusterConfiguration.NumberOfNodes > 1,
				}
				select node).ToList();

			var initialMasterNodes = string.Join(",", nodes.Select(n=>n.NodeConfiguration.DesiredNodeName));
			foreach (var node in nodes)
				node.NodeConfiguration.InitialMasterNodes(initialMasterNodes);

			this.Nodes = new ReadOnlyCollection<ElasticsearchNode>(nodes);
		}

		/// <summary> A short name to identify the cluster defaults to the <see cref="ClusterBase"/> subclass name with Cluster removed </summary>
		public virtual string ClusterMoniker { get; }

		public TConfiguration ClusterConfiguration { get; }
		public INodeFileSystem FileSystem => this.ClusterConfiguration.FileSystem;

		public ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		public bool Started { get; private set; }
		public IConsoleLineHandler Writer { get; private set; } = NoopConsoleLineWriter.Instance;

		private Action<NodeConfiguration, int> _defaultConfigSelector = (n, i) => { };
		protected virtual void ModifyNodeConfiguration(NodeConfiguration nodeConfiguration, int port) { }

		protected virtual void SeedCluster() { }

		public IDisposable Start() => this.Start(TimeSpan.FromMinutes(2));

		public IDisposable Start(TimeSpan waitForStarted) =>
			this.Start(new LineHighlightWriter(this.Nodes.Select(n => n.NodeConfiguration.DesiredNodeName).ToArray()), waitForStarted);

		public IDisposable Start(IConsoleLineHandler writer, TimeSpan waitForStarted)
		{
			this.Writer = writer ?? NoopConsoleLineWriter.Instance;

			this.OnBeforeStart();

			var subscriptions = new Subscriptions();
			foreach (var node in this.Nodes) subscriptions.Add(node.SubscribeLines(writer));

			var waitHandles = this.Nodes.Select(w => w.StartedHandle).ToArray();
			if (!WaitHandle.WaitAll(waitHandles, waitForStarted))
			{
				var nodeExceptions = this.Nodes.Select(n => n.LastSeenException).Where(e => e != null).ToList();
				writer?.WriteError($"{{{this.GetType().Name}.{nameof(Start)}}} cluster did not start after {waitForStarted}");
				throw new AggregateException($"Not all nodes started after waiting {waitForStarted}", nodeExceptions);
			}

			this.Started = this.Nodes.All(n => n.NodeStarted);
			if (!this.Started)
			{
				var nodeExceptions = this.Nodes.Select(n => n.LastSeenException).Where(e => e != null).ToList();
				var message = $"{{{this.GetType().Name}.{nameof(Start)}}} cluster did not start succesfully";
				var seeLogsMessage = this.SeeLogsMessage(message);
				writer?.WriteError(seeLogsMessage);
				throw new AggregateException(seeLogsMessage, nodeExceptions);
			}

			try
			{
                this.OnAfterStarted();
                this.SeedCluster();
			}
			catch (Exception e)
			{
				writer?.WriteError(e.ToString());
				throw;
			}

			return subscriptions;
		}

		private class Subscriptions : IDisposable
		{
			private List<IDisposable> Disposables { get; } = new List<IDisposable>();

			internal void Add(IDisposable disposable) => this.Disposables.Add(disposable);

			public void Dispose()
			{
				foreach(var d in Disposables) d.Dispose();
			}
		}


		protected virtual string SeeLogsMessage(string message)
		{
			var log = Path.Combine(this.FileSystem.LogsPath, $"{this.ClusterConfiguration.ClusterName}.log");
			return $"{message} see {log} to diagnose the issue";
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
			foreach (var node in this.Nodes)
				node?.Dispose();

			this.OnDispose();
		}
	}
}
