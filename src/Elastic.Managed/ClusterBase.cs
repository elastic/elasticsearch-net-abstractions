using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using Elastic.Managed.Configuration;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;

namespace Elastic.Managed
{
	public interface ICluster<out TConfiguration> : IDisposable
		where TConfiguration : IClusterConfiguration<NodeFileSystem>
	{
		string ClusterMoniker { get; }
		TConfiguration ClusterConfiguration { get; }
		INodeFileSystem FileSystem { get; }
		bool Started { get; }
		ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		IConsoleLineWriter Writer { get; }

		IDisposable Start();

		IDisposable Start(TimeSpan waitForStarted);

		IDisposable Start(IConsoleLineWriter writer, TimeSpan waitForStarted);
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

			var nodes = Enumerable.Range(this.ClusterConfiguration.StartingPortNumber, this.ClusterConfiguration.NumberOfNodes)
				.Select(p =>
				{
					var config = new NodeConfiguration(clusterConfiguration, p, this.ClusterMoniker)
					{
						ShowElasticsearchOutputAfterStarted = clusterConfiguration.ShowElasticsearchOutputAfterStarted,
					};
					this.ModifyNodeConfiguration(config, p);
					return config;
				})
				.Select(n => new ElasticsearchNode(n)
				{
					AssumeStartedOnNotEnoughMasterPing = this.ClusterConfiguration.NumberOfNodes > 1,
				})
				.ToList();
			this.Nodes = new ReadOnlyCollection<ElasticsearchNode>(nodes);
		}

		/// <summary> A short name to identify the cluster defaults to the <see cref="ClusterBase"/> subclass name with Cluster removed </summary>
		public virtual string ClusterMoniker { get; }

		public TConfiguration ClusterConfiguration { get; }
		public INodeFileSystem FileSystem => this.ClusterConfiguration.FileSystem;

		public ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		public bool Started { get; private set; }
		public IConsoleLineWriter Writer { get; private set; } = NoopConsoleLineWriter.Instance;

		private Action<NodeConfiguration, int> _defaultConfigSelector = (n, i) => { };
		protected virtual void ModifyNodeConfiguration(NodeConfiguration nodeConfiguration, int port) { }

		protected virtual void SeedCluster() { }

		public IDisposable Start() => this.Start(TimeSpan.FromMinutes(2));

		public IDisposable Start(TimeSpan waitForStarted) =>
			this.Start(new LineHighlightWriter(this.Nodes.Select(n => n.NodeConfiguration.DesiredNodeName).ToArray()), waitForStarted);

		public IDisposable Start(IConsoleLineWriter writer, TimeSpan waitForStarted)
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
