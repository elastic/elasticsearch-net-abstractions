// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using Elastic.Elasticsearch.Managed.FileSystem;
using Elastic.Stack.ArtifactsApi;
using ProcNet;

namespace Elastic.Elasticsearch.Managed.Configuration
{
	public class NodeConfiguration
	{
		private Action<StartArguments> _defaultStartArgs = s => { };

		public NodeConfiguration(ElasticVersion version, int? port = null) : this(new ClusterConfiguration(version),
			port)
		{
		}

		public NodeConfiguration(IClusterConfiguration<NodeFileSystem> clusterConfiguration, int? port = null,
			string nodePrefix = null)
		{
			ClusterConfiguration = clusterConfiguration;
			DesiredPort = port;
			DesiredNodeName = CreateNodeName(port, nodePrefix) ?? clusterConfiguration.CreateNodeName(port);
			Settings = new NodeSettings(clusterConfiguration.DefaultNodeSettings);

			if (!string.IsNullOrWhiteSpace(DesiredNodeName)) Settings.Add("node.name", DesiredNodeName);
			if (DesiredPort.HasValue)
				Settings.Add("http.port", DesiredPort.Value.ToString(CultureInfo.InvariantCulture));
		}

		private IClusterConfiguration<NodeFileSystem> ClusterConfiguration { get; }

		public int? DesiredPort { get; }
		public string DesiredNodeName { get; }

		public Action<StartArguments> ModifyStartArguments
		{
			get => _defaultStartArgs;
			set => _defaultStartArgs = value ?? (s => { });
		}

		/// <summary>
		///     Wheter <see cref="ElasticsearchNode" /> should continue to write output to console after it has started.
		///     <para>Defaults to true but useful to turn of if it proofs to be too noisy </para>
		/// </summary>
		public bool ShowElasticsearchOutputAfterStarted { get; set; } = true;

		/// <summary>
		///     The expected duration of the shut down sequence for Elasticsearch. After this wait duration a hard kill will occur.
		/// </summary>
		public TimeSpan WaitForShutdown { get; set; } = TimeSpan.FromSeconds(10);

		/// <summary>
		///     Copy of <see cref="IClusterConfiguration{TFileSystem}.DefaultNodeSettings" />. Add individual node settings here.
		/// </summary>
		public NodeSettings Settings { get; }

		public INodeFileSystem FileSystem => ClusterConfiguration.FileSystem;
		public ElasticVersion Version => ClusterConfiguration.Version;
		public string[] CommandLineArguments => Settings.ToCommandLineArguments(Version);

		public void InitialMasterNodes(string initialMasterNodes) =>
			Settings.Add("cluster.initial_master_nodes", initialMasterNodes, ">=7.0.0.beta1");

		public string AttributeKey(string attribute)
		{
			var attr = Version.Major >= 5 ? "attr." : "";
			return $"node.{attr}{attribute}";
		}

		public void Add(string key, string value) => Settings.Add(key, value);

		private string CreateNodeName(int? node, string prefix = null)
		{
			if (prefix == null) return null;
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			return $"{prefix.Replace("Cluster", "").ToLowerInvariant()}-node-{suffix}{node}";
		}
	}
}
