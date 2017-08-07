using System;
using System.Diagnostics;
using System.Linq;
using Elastic.ProcessManagement.Abstractions;
using Elastic.ProcessManagement.Std;
using static Elastic.ProcessManagement.ElasticsearchConsoleOutParser;

namespace Elastic.ProcessManagement
{
	public class ElasticsearchNode : ConfirmedStartedStateProcessBase<ObservableProcess>
	{
		protected override string PrintableName { get; } = "elasticsearch-node.exe";

		public string Version { get; private set; }
		public int? ProcessId { get; private set; }
		public int? Port { get; private set; }
		public bool Started { get; private set; }

		public ElasticsearchNode(string binary, params string[] arguments)
			: base(ShouldWaitForExit(new ObservableProcess(binary, arguments)), new ConsoleOutColorWriter()) { }

		public ElasticsearchNode(ObservableProcess process, IConsoleOutWriter writer) : base(ShouldWaitForExit(process), writer) { }

		protected override bool HandleMessage(LineOut c)
		{
			//already confirmed started so we can early exit.
			if (this.Started) return false;

			var confirmedStart =
				TryParse(c.Line, out string date, out string level, out string section, out string node, out string message, out bool started)
				&& started;
			if (confirmedStart) this.Started = true;

			//if (consoleOut.Error && !this.Started && !string.IsNullOrWhiteSpace(consoleOut.Data)) throw new Exception(consoleOut.Data);

			string version;
			int? pid;
			int port;

			if (this.ProcessId == null && TryParseNodeInfo(section, message, out version, out pid))
			{
				this.ProcessId = pid;
				this.Version = version;
			}
			else if (TryGetPortNumber(section, message, out port))
				this.Port = port;
			return confirmedStart;
		}

		private static ObservableProcess ShouldWaitForExit(ObservableProcess p)
		{
			p.WaitForExit = p.Binary.EndsWith(".exe") ? (TimeSpan?) TimeSpan.FromSeconds(2) : null;
			return p;
		}

		protected override void OnBeforeStop()
		{
			var esProcess = this.ProcessId == null ? null : Process.GetProcesses().FirstOrDefault(p => p.Id == this.ProcessId.Value);
			if (esProcess != null)
			{
				Console.WriteLine($"Killing elasticsearch PID {this.ProcessId}");
				esProcess.Kill();
				esProcess.Dispose();
			}
			base.OnBeforeStop();
		}
	}
}
