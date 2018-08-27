using System;
using System.Collections.Generic;
using System.Threading;
using Elastic.Managed.Configuration;
using Elastic.Managed.ConsoleWriters;
using Elastic.Managed.FileSystem;
using ProcNet;
using ProcNet.Std;

namespace Elastic.Managed
{
	public class ElasticsearchNode : ObservableProcess
	{
		public string Version { get; private set; }
		public int? Port { get; private set; }
		public bool NodeStarted { get; private set; }
		public NodeConfiguration NodeConfiguration { get; }

		private int? JavaProcessId { get; set; }
		public override int? ProcessId => this.JavaProcessId ?? base.ProcessId;
		public int? HostProcessId => base.ProcessId;

		public ElasticsearchNode(ElasticsearchVersion version, string elasticsearchHome = null)
			: this(new NodeConfiguration(new ClusterConfiguration(version, fileSystem: (v, s) => new NodeFileSystem(v, elasticsearchHome)))) { }

		public ElasticsearchNode(NodeConfiguration config) : base(StartArgs(config)) => this.NodeConfiguration = config;

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

		private static Dictionary<string, string> EnvVars(NodeConfiguration config)
		{
			if (string.IsNullOrWhiteSpace(config.FileSystem.ConfigPath)) return null;
			return new Dictionary<string, string>
			{
				{ config.FileSystem.ConfigEnvironmentVariableName, config.FileSystem.ConfigPath },
				{"ES_HOME", config.FileSystem.ElasticsearchHome}
			};
		}

		/// <summary>
		/// Set this true if you want the node to go into assumed started state as soon as its waiting for more nodes to start doing the election.
		/// <para>Useful to speed up starting multi node clusters</para>
		/// </summary>
		public bool AssumeStartedOnNotEnoughMasterPing { get; set; }

		private bool AssumedStartedStateChecker(string section, string message)
		{
			if (AssumeStartedOnNotEnoughMasterPing
			    && section.Contains("ZenDiscovery")
			    && message.Contains("not enough master nodes discovered during pinging"))
				return true;
			return false;
		}

		public IDisposable Start() => this.Start(TimeSpan.FromMinutes(2));

		public IDisposable Start(TimeSpan waitForStarted) => this.Start(new LineHighlightWriter(), waitForStarted);

		public IDisposable Start(IConsoleLineWriter writer, TimeSpan waitForStarted)
		{
			var node = this.NodeConfiguration.DesiredNodeName;
			var subscription = this.SubscribeLines(writer);
			if (this.WaitForStarted(waitForStarted)) return subscription;
			subscription.Dispose();
			throw new CleanExitException($"Failed to start node: {node} before the configured timeout of: {waitForStarted}");
		}

		internal IConsoleLineWriter Writer { get; private set; }

		public IDisposable SubscribeLines() => this.SubscribeLines(new LineHighlightWriter());
		public IDisposable SubscribeLines(IConsoleLineWriter writer) =>
			this.SubscribeLines(writer, delegate { }, delegate { }, delegate { });

		public IDisposable SubscribeLines(IConsoleLineWriter writer, Action<LineOut> onNext) =>
			this.SubscribeLines(writer, onNext, delegate { }, delegate { });

		public IDisposable SubscribeLines(IConsoleLineWriter writer, Action<LineOut> onNext, Action<Exception> onError) =>
			this.SubscribeLines(writer, onNext, onError, delegate { });

		public IDisposable SubscribeLines(IConsoleLineWriter writer, Action<LineOut> onNext, Action<Exception> onError, Action onCompleted)
		{
			this.Writer = writer;
			var node = this.NodeConfiguration.DesiredNodeName;
			writer?.WriteDiagnostic($"Elasticsearch location: [{this.Binary}]", node);
			writer?.WriteDiagnostic($"Settings: {{{string.Join(" ", this.NodeConfiguration.CommandLineArguments)}}}", node);
			return this.SubscribeLines(
				l => {
					writer?.Write(l);
					onNext?.Invoke(l);
				},
				e =>
				{
					this.LastSeenException = e;
					writer?.Write(e);
					onError?.Invoke(e);
					this._startedHandle.Set();
				},
				() =>
				{
					onCompleted?.Invoke();
					this._startedHandle.Set();
				});
		}

		public Exception LastSeenException { get; set; }

		private readonly ManualResetEvent _startedHandle = new ManualResetEvent(false);
		public WaitHandle StartedHandle => _startedHandle;
		public bool WaitForStarted(TimeSpan timeout) => this._startedHandle.WaitOne(timeout);

		protected override void OnBeforeSetCompletedHandle()
		{
			this._startedHandle.Set();
			base.OnBeforeSetCompletedHandle();
		}

		protected override void OnBeforeWaitForEndOfStreamsError(TimeSpan waited)
		{
			// The wait for streams finished before streams were fully read.
			// this usually indicates the process is still running.
			// Proc will successfully kill the host but will leave the JavaProcess the bat file starts running
			// The elasticsearch jar is closing down so won't leak but might prevent EphemeralClusterComposer to do its clean up.
			// We do a hard kill on both here to make sure both processes are gone.
			HardKill(this.HostProcessId);
			HardKill(this.JavaProcessId);
		}

		private static void HardKill(int? processId)
		{
			if (!processId.HasValue) return;
			try
			{
				var p = System.Diagnostics.Process.GetProcessById(processId.Value);
				p.Kill();
			}
			catch (Exception) { }
		}

		protected override bool ContinueReadingFromProcessReaders()
		{
			if (!this.NodeStarted) return true;
			return true;

			// some how if we return false here it leads to Task starvation in Proc and tests in e.g will Elastic.Xunit will start
			// to timeout. This makes little sense to me now, so leaving this performance optimization out for now. Hopefully another fresh look will yield
			// to (not so) obvious.
			//return this.NodeConfiguration.ShowElasticsearchOutputAfterStarted;
		}

		protected override bool KeepBufferingLines(LineOut c)
		{
			//if the node is already started only keep buffering lines while we have a writer and the nodeconfiguration wants output after started
			if (this.NodeStarted)
			{
				var keepBuffering = this.Writer != null && this.NodeConfiguration.ShowElasticsearchOutputAfterStarted;
				if (!keepBuffering) this.CancelAsyncReads();
				// if we want to StartAsyncReadsLater we need to keep this subscription alive otherwise we can bail out early
				if (this.NodeConfiguration.ShowElasticsearchOutputAfterDispose) return true;
				// bail out early
				return keepBuffering;
			}

			var parsed = LineOutParser.TryParse(c?.Line, out _, out _, out var section, out _, out var message, out var started);

			if (!parsed) return this.Writer != null;

			if (this.JavaProcessId == null && LineOutParser.TryParseNodeInfo(section, message, out var version, out var pid))
			{
				this.JavaProcessId = pid;
				this.Version = version;
			}
			else if (LineOutParser.TryGetPortNumber(section, message, out var port))
			{
				this.Port = port;
				var dp = this.NodeConfiguration.DesiredPort;
				if (dp.HasValue && this.Port != dp.Value)
					throw new CleanExitException($"Node started on port {port} but {dp.Value} was requested");
			}

			if (!started) started = AssumedStartedStateChecker(section, message);
			if (started)
			{
				if (!this.Port.HasValue) throw new CleanExitException($"Node started but ElasticsearchNode did not grab its port number");
				this.NodeStarted = true;
				this._startedHandle.Set();
			}

			// if we have dont a writer always return true
			if (this.Writer != null) return true;
			//otherwise only keep buffering if we are not started
			return !started;
		}
	}
}
