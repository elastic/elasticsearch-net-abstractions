using System;
using System.Linq;
using System.Threading;
using Elastic.ManagedNode.Configuration;
using ProcNet;
using ProcNet.Std;

namespace Elastic.ManagedNode
{
	public class ElasticsearchNode : ObservableProcess
	{
		public string Version { get; private set; }
		public int DesiredPort { get; set; } = 9200;
		public int? Port { get; private set; }
		public bool NodeStarted { get; private set; }
		public NodeConfiguration NodeConfiguration { get; }


		private int? JavaProcessId { get; set; }
		public override int? ProcessId => this.JavaProcessId ?? base.ProcessId;
		public int? HostProcessId => base.ProcessId;

		public ElasticsearchNode(NodeConfiguration config) : base(StartArgs(config))
		{
			this.NodeConfiguration = config;
		}

		private static StartArguments StartArgs(NodeConfiguration config) =>
			new StartArguments(config.FileSystem.Binary, config.CommandLineArguments)
			{
				SendControlCFirst = true
			};

		public bool AssumeStartedOnNotEnoughMasterPing { get; set; }

		private bool AssumedStartedStateChecker(string section, string message)
		{
			if (AssumeStartedOnNotEnoughMasterPing
			    && section.Contains("ZenDiscovery")
			    && message.Contains("not enough master nodes discovered during pinging"))
				return true;
			return false;
		}


		private IElasticsearchConsoleOutWriter _writer;
		public void Start() => this.Start(null);
		public void Start(IElasticsearchConsoleOutWriter writer)
		{
			this._writer = writer;
			writer.Write(ConsoleOut.Out($"Elasticsearch: {this.Binary}"));
			writer.Write(ConsoleOut.Out($"Settings: {string.Join(" ", this.NodeConfiguration.CommandLineArguments)}"));
			this.SubscribeLines(l => writer?.Write(l), e => writer?.Write(e), delegate {});
		}

		private readonly ManualResetEvent _startedHandle = new ManualResetEvent(false);
		public WaitHandle StartedHandle => _startedHandle;
		public bool WaitForStarted(TimeSpan timeout) => this._startedHandle.WaitOne(timeout);

		protected override void OnBeforeSetCompletedHandle()
		{
			this._startedHandle.Set();
			base.OnBeforeSetCompletedHandle();
		}

		protected override bool KeepBufferingLines(LineOut c)
		{
			//if the node is already started only keep buffering lines while we have a writer;
			if (this.NodeStarted) return this._writer != null;

			var parsed = ElasticsearchConsoleOutParser.TryParse(c.Line, out _, out _, out var section, out _, out var message, out var started);

			if (!started) started = AssumedStartedStateChecker(section, message);
			if (started)
			{
				this.NodeStarted = true;
				this._startedHandle.Set();
			}

			if (!parsed) return this._writer != null;

			if (this.JavaProcessId == null && ElasticsearchConsoleOutParser.TryParseNodeInfo(section, message, out var version, out var pid))
			{
				this.JavaProcessId = pid;
				this.Version = version;
			}
			else if (ElasticsearchConsoleOutParser.TryGetPortNumber(section, message, out var port))
			{
				this.Port = port;
				if (this.Port != this.DesiredPort) throw new CleanExitException($"Node started on port {port} but {this.DesiredPort} was requested");
			}

			// if we have a writer always return true
			if (this._writer != null) return true;
			//otherwise only keep buffering if we are not started
			return !started;
		}
	}
}
