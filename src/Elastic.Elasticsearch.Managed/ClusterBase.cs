// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Elastic.Elasticsearch.Managed.Configuration;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Elastic.Elasticsearch.Managed.FileSystem;
using ProcNet.Std;
using static Elastic.Elasticsearch.Managed.DetectedProxySoftware;

namespace Elastic.Elasticsearch.Managed
{
	public interface ICluster
	{
		/// <summary>
		/// Whether known proxies were detected as running during startup
		/// </summary>
		DetectedProxySoftware DetectedProxy { get; }

		/// <summary> A friendly name for this cluster, derived from the implementation name</summary>
		string ClusterMoniker { get; }

		/// <inheritdoc cref="INodeFileSystem"/>
		INodeFileSystem FileSystem { get; }

		/// <summary> Indicating if this cluster was started correctly </summary>
		bool Started { get; }

		/// <summary>
		/// The collection of <see cref="ElasticsearchNode"/>'s that make up the cluster
		/// </summary>
		ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
	}

	public interface ICluster<out TConfiguration> : ICluster,IDisposable
		where TConfiguration : IClusterConfiguration<NodeFileSystem>
	{
		TConfiguration ClusterConfiguration { get; }
		IConsoleLineHandler Writer { get; }

		IDisposable Start();

		IDisposable Start(TimeSpan waitForStarted);

		IDisposable Start(IConsoleLineHandler writer, TimeSpan waitForStarted);
	}


	public abstract class ClusterBase : ClusterBase<ClusterConfiguration>
	{
		protected ClusterBase(ClusterConfiguration clusterConfiguration) : base(clusterConfiguration)
		{
		}
	}

	public abstract class ClusterBase<TConfiguration> : ICluster<TConfiguration>
		where TConfiguration : IClusterConfiguration<NodeFileSystem>
	{
		private Action<NodeConfiguration, int> _defaultConfigSelector = (n, i) => { };

		protected ClusterBase(TConfiguration clusterConfiguration)
		{
			ClusterConfiguration = clusterConfiguration;
			ClusterMoniker = GetType().Name.Replace("Cluster", "");

			NodeConfiguration Modify(NodeConfiguration n, int p)
			{
				ModifyNodeConfiguration(n, p);
				return n;
			}

			var nodes =
				(from port in Enumerable.Range(ClusterConfiguration.StartingPortNumber,
						ClusterConfiguration.NumberOfNodes)
					let config = new NodeConfiguration(clusterConfiguration, port, ClusterMoniker)
					{
						ShowElasticsearchOutputAfterStarted =
							clusterConfiguration.ShowElasticsearchOutputAfterStarted,
					}
					let node = new ElasticsearchNode(Modify(config, port))
					{
						AssumeStartedOnNotEnoughMasterPing = ClusterConfiguration.NumberOfNodes > 1,
					}
					select node).ToList();

			var initialMasterNodes = string.Join(",", nodes.Select(n => n.NodeConfiguration.DesiredNodeName));
			foreach (var node in nodes)
				node.NodeConfiguration.InitialMasterNodes(initialMasterNodes);

			Nodes = new ReadOnlyCollection<ElasticsearchNode>(nodes);

			if (Process.GetProcessesByName("fiddler").Any()) DetectedProxy = Fiddler;
			else if (Process.GetProcessesByName("mitmproxy").Any()) DetectedProxy = MitmProxy;
			else DetectedProxy = None;
		}

		/// <summary>
		/// Whether known proxies were detected as running during startup
		/// </summary>
		public DetectedProxySoftware DetectedProxy { get; }


		/// <summary>
		///     A short name to identify the cluster defaults to the <see cref="ClusterBase" /> subclass name with Cluster
		///     removed
		/// </summary>
		public virtual string ClusterMoniker { get; }

		public TConfiguration ClusterConfiguration { get; }
		public INodeFileSystem FileSystem => ClusterConfiguration.FileSystem;

		public ReadOnlyCollection<ElasticsearchNode> Nodes { get; }
		public bool Started { get; private set; }
		public IConsoleLineHandler Writer { get; private set; } = NoopConsoleLineWriter.Instance;

		public IDisposable Start() => Start(TimeSpan.FromMinutes(2));

		public IDisposable Start(TimeSpan waitForStarted) =>
			Start(new LineHighlightWriter(Nodes.Select(n => n.NodeConfiguration.DesiredNodeName).ToArray()),
				waitForStarted);

		public IDisposable Start(IConsoleLineHandler writer, TimeSpan waitForStarted)
		{
			Writer = writer ?? NoopConsoleLineWriter.Instance;

			OnBeforeStart();

			var subscriptions = new Subscriptions();
			foreach (var node in Nodes) subscriptions.Add(node.SubscribeLines(writer));

			var waitHandles = Nodes.Select(w => w.StartedHandle).ToArray();
			if (!WaitHandle.WaitAll(waitHandles, waitForStarted))
			{
				var nodeExceptions = Nodes.Select(n => n.LastSeenException).Where(e => e != null).ToList();
				writer?.WriteError(
					$"{{{GetType().Name}.{nameof(Start)}}} cluster did not start after {waitForStarted}");
				throw new AggregateException($"Not all nodes started after waiting {waitForStarted}", nodeExceptions);
			}

			Started = Nodes.All(n => n.NodeStarted);
			if (!Started)
			{
				var nodeExceptions = Nodes.Select(n => n.LastSeenException).Where(e => e != null).ToList();
				var message = $"{{{GetType().Name}.{nameof(Start)}}} cluster did not start successfully";
				var seeLogsMessage = SeeLogsMessage(message);
				writer?.WriteError(seeLogsMessage);
				throw new AggregateException(seeLogsMessage, nodeExceptions);
			}

			try
			{
				OnAfterStarted();
				SeedCluster();
			}
			catch (Exception e)
			{
				writer?.WriteError(e.ToString());
				throw;
			}

			return subscriptions;
		}

		public void Dispose()
		{
			try
			{
				Started = false;
				foreach (var node in Nodes)
					node?.Dispose();

				OnDispose();
			}
			catch(Exception ex)
			{
				Writer.WriteError($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
			}
		}

		protected virtual void ModifyNodeConfiguration(NodeConfiguration nodeConfiguration, int port)
		{
		}

		protected virtual void SeedCluster()
		{
		}


		protected virtual string SeeLogsMessage(string message)
		{
			var log = Path.Combine(FileSystem.LogsPath, $"{ClusterConfiguration.ClusterName}.log");
			return $"{message} see {log} to diagnose the issue";
		}

		public void WaitForExit(TimeSpan waitForCompletion)
		{
			foreach (var node in Nodes)
				node.WaitForCompletion(waitForCompletion);
		}

		protected virtual void OnAfterStarted()
		{
		}

		protected virtual void OnBeforeStart()
		{
		}

		protected virtual void OnDispose()
		{
		}

		private class Subscriptions : IDisposable
		{
			private List<IDisposable> Disposables { get; } = new List<IDisposable>();

			public void Dispose()
			{
				foreach (var d in Disposables) d.Dispose();
			}

			internal void Add(IDisposable disposable) => Disposables.Add(disposable);
		}
	}
}
