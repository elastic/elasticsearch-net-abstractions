using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Elastic.ProcessManagement.Abstractions;
using Elastic.ProcessManagement.Std;
using static Elastic.ProcessManagement.ElasticsearchConsoleOutParser;

namespace Elastic.ProcessManagement
{
	public class ElasticsearchNode : ConfirmedStartedStateProcessBase<ObservableProcess>
	{
		protected override string PrintableName { get; } = "elasticsearch-node";

		public string Version { get; private set; }
		public int? JavaProcessId { get; private set; }
		public int DesiredPort { get; set; } = 9200;
		public int? Port { get; private set; }
		public bool Started { get; private set; }

		private bool Highlight { get; }

		private bool _writeToConsoleAfterStarted;
		public bool WriteToConsoleAfterStarted
		{
			get => _writeToConsoleAfterStarted;
			set
			{
				if (this.Highlight)
					this.SubscribeToMessagesAfterStartedConfirmation = value;
				else
					this.ConsoleWriterSubscribesAfterStarted = value;
				_writeToConsoleAfterStarted = value;
			}
		}

		public ElasticsearchNode(string binary, params string[] arguments)
			: this(new ObservableProcess(binary, arguments), new HighlightElasticsearchLineConsoleOutWriter()) { }

		public ElasticsearchNode(ObservableProcess process, IConsoleOutWriter writer)
			: base(ShouldWaitForExit(process), writer is HighlightElasticsearchLineConsoleOutWriter ? null : writer)
		{
			this.Highlight = writer is HighlightElasticsearchLineConsoleOutWriter;
			this.WriteToConsoleAfterStarted = true;
		}

		public bool AssumeStartedOnNotEnoughMasterPing { get; set; }
		protected bool AssumedStartedStateChecker(string section, string message)
		{
			if (AssumeStartedOnNotEnoughMasterPing
			    && section.Contains("ZenDiscovery")
			    && message.Contains("not enough master nodes discovered during pinging"))
				return true;
			return false;
		}

		protected override bool HandleMessage(LineOut c)
		{
			//already confirmed started so we can early exit unless we want to write the line highlighted to console
			if (this.Started && !this.Highlight) return false;

			var parsed =
				TryParse(c.Line, out string date, out string level, out string section, out string node,
					out string message, out bool started, out bool matched)
				&& started;

			if (!started) started = AssumedStartedStateChecker(section, message);

			if (this.Highlight && matched)
				LineOutElasticsearchHighlighter.Write(c.Error, date, level, section, node, message);
			else if (this.Highlight)
				if (c.Error) Console.Error.WriteLine(c.Line);
				else Console.WriteLine(c.Line);

			//already confirmed started so we can early exit.
			if (this.Started) return false;

			if (started) this.Started = true;

			string version;
			int? pid;
			int port;

			if (this.JavaProcessId == null && TryParseNodeInfo(section, message, out version, out pid))
			{
				this.JavaProcessId = pid;
				this.Version = version;
			}
			else if (TryGetPortNumber(section, message, out port))
			{
				this.Port = port;
				if (this.Port != this.DesiredPort)
					throw new CleanExitException("Node started on port {}");

			}
			return started;
		}

		private static ObservableProcess ShouldWaitForExit(ObservableProcess p)
		{
			p.WaitForExit = p.Binary.EndsWith(".exe") ? (TimeSpan?) TimeSpan.FromSeconds(2) : null;
			return p;
		}

		protected override void OnBeforeStop()
		{
			var esProcess = this.JavaProcessId == null ? null : Process.GetProcesses().FirstOrDefault(p => p.Id == this.JavaProcessId.Value);
			if (esProcess != null)
			{
				Console.WriteLine($"Killing elasticsearch PID {this.JavaProcessId}");
				esProcess.Kill();
				esProcess.Dispose();
			}
			base.OnBeforeStop();
		}
	}
}
