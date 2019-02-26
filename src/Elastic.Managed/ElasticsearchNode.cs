using System;
using System.Collections.Generic;
using Elastic.Managed.Configuration;
using Elastic.Managed.FileSystem;
using ProcNet;
using ProcNet.Std;
using Elastic.Managed.ConsoleWriters;

namespace Elastic.Managed
{
	public class ElasticsearchNode : ElasticsearchObservableProcess
	{
		public NodeConfiguration NodeConfiguration { get; }

		public ElasticsearchNode(NodeConfiguration config) : base(StartArgs(config))
		{
			this.NodeConfiguration = config;
			this.ShowElasticsearchOutputAfterStarted = config.ShowElasticsearchOutputAfterStarted;
		}

		private static StartArguments StartArgs(NodeConfiguration config)
		{
			var startArguments = new StartArguments(config.FileSystem.Binary, config.CommandLineArguments)
			{
				SendControlCFirst = true,
				Environment = EnvVars(config),
				WaitForExit = config.WaitForShutdown,
				WaitForStreamReadersTimeout = config.WaitForShutdown
			};
			config.ModifyStartArguments(startArguments);
			return startArguments;
		}

		protected override string StartTimeoutExceptionMessage(TimeSpan waitForStarted) =>
			$"Failed to start node: {this.NodeConfiguration.DesiredNodeName} before the configured timeout of: {waitForStarted}";

		protected override void WriteStartedMessage(IConsoleLineWriter writer)
		{
			var node = this.NodeConfiguration.DesiredNodeName;
			writer?.WriteDiagnostic($"Elasticsearch location: [{this.Binary}]", node);
			writer?.WriteDiagnostic($"Settings: {{{string.Join(" ", this.NodeConfiguration.CommandLineArguments)}}}", node);
		}

		protected override void ValidatePort(int port)
		{
			var dp = this.NodeConfiguration.DesiredPort;
			if (dp.HasValue && this.Port != dp.Value)
				throw new ObservableProcessException($"Node started on port {port} but {dp.Value} was requested");
		}

		protected override void OnNoPortCaptured() =>
			throw new ObservableProcessException($"Node started but ElasticsearchNode did not grab its port number");

		private static Dictionary<string, string> EnvVars(NodeConfiguration config)
		{
			if (string.IsNullOrWhiteSpace(config.FileSystem.ConfigPath)) return null;
			return new Dictionary<string, string>
			{
				{ config.FileSystem.ConfigEnvironmentVariableName, config.FileSystem.ConfigPath },
				{"ES_HOME", config.FileSystem.ElasticsearchHome}
			};
		}
	}
}
