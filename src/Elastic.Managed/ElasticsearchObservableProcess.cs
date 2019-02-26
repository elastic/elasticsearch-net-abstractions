using System;
using System.Threading;
using Elastic.Managed.ConsoleWriters;
using ProcNet;
using ProcNet.Std;

namespace Elastic.Managed
{
	public class ElasticsearchObservableProcess : ObservableProcess
	{
		internal ElasticsearchObservableProcess(StartArguments args) : base(args) { }

		public ElasticsearchObservableProcess(bool interactive, string binary, params string[] arguments)
			: base(StartArgs(binary, arguments))
		{
			this.ShowElasticsearchOutputAfterStarted = interactive;
		}

		public string Version { get; private set; }
		public int? Port { get; private set; }
		public bool NodeStarted { get; private set; }

		private IConsoleLineWriter Writer { get; set; }

		private int? JavaProcessId { get; set; }
		public override int? ProcessId => this.JavaProcessId ?? base.ProcessId;
		public int? HostProcessId => base.ProcessId;

		public bool ShowElasticsearchOutputAfterStarted { get; protected set; }

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

		private static StartArguments StartArgs(string binary, params string[] arguments)
		{
			var waitForExit = TimeSpan.FromSeconds(10);
			var startArguments = new StartArguments(binary, arguments)
			{
				SendControlCFirst = true,
				WaitForExit = waitForExit,
				WaitForStreamReadersTimeout = waitForExit
			};
			return startArguments;
		}

		public IDisposable Start() => this.Start(TimeSpan.FromMinutes(2));

		public IDisposable Start(TimeSpan waitForStarted) => this.Start(new LineHighlightWriter(), waitForStarted);

		public IDisposable Start(IConsoleLineWriter writer, TimeSpan waitForStarted)
		{
			var subscription = this.SubscribeLines(writer);
			if (this.WaitForStarted(waitForStarted)) return subscription;
			subscription.Dispose();
			throw new ObservableProcessException(this.StartTimeoutExceptionMessage(waitForStarted));
		}

		protected virtual string StartTimeoutExceptionMessage(TimeSpan waitForStarted) => $"Failed to start node before the configured timeout of: {waitForStarted}";

		public IDisposable SubscribeLines() => this.SubscribeLines(new LineHighlightWriter());
		public IDisposable SubscribeLines(IConsoleLineWriter writer) =>
			this.SubscribeLines(writer, delegate { }, delegate { }, delegate { });

		public IDisposable SubscribeLines(IConsoleLineWriter writer, Action<LineOut> onNext) =>
			this.SubscribeLines(writer, onNext, delegate { }, delegate { });

		public IDisposable SubscribeLines(IConsoleLineWriter writer, Action<LineOut> onNext, Action<Exception> onError) =>
			this.SubscribeLines(writer, onNext, onError, delegate { });

		protected virtual void WriteStartedMessage(IConsoleLineWriter writer) {}

		public IDisposable SubscribeLines(IConsoleLineWriter writer, Action<LineOut> onNext, Action<Exception> onError, Action onCompleted)
		{
			this.Writer = writer;
			this.WriteStartedMessage(writer);
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

		protected virtual void ValidatePort(int port) { }

		protected virtual void OnNoPortCaptured() { }

		protected override bool KeepBufferingLines(LineOut c)
		{
			//if the node is already started only keep buffering lines while we have a writer and the nodeconfiguration wants output after started
			if (this.NodeStarted)
			{
				var keepBuffering = this.Writer != null && this.ShowElasticsearchOutputAfterStarted;
				if (!keepBuffering) this.CancelAsyncReads();
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
				this.ValidatePort(port);
			}

			if (!started) started = AssumedStartedStateChecker(section, message);
			if (started)
			{
				if (!this.Port.HasValue) this.OnNoPortCaptured();
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
