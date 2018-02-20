using System;
using System.Text.RegularExpressions;

namespace Elastic.ManagedNode
{
	public static class LineOutParser
	{
/*
[2016-09-26T11:43:17,314][INFO ][o.e.n.Node               ] [readonly-node-a9c5f4] initializing ...
[2016-09-26T11:43:17,470][INFO ][o.e.e.NodeEnvironment    ] [readonly-node-a9c5f4] using [1] data paths, mounts [[BOOTCAMP (C:)]], net usable_space [27.7gb], net total_space [129.7gb], spins? [unknown], types [NTFS]
[2016-09-26T11:43:17,471][INFO ][o.e.e.NodeEnvironment    ] [readonly-node-a9c5f4] heap size [1.9gb], compressed ordinary object pointers [true]
[2016-09-26T11:43:17,475][INFO ][o.e.n.Node               ] [readonly-node-a9c5f4] version[5.0.0-beta1], pid[13172], build[7eb6260/2016-09-20T23:10:37.942Z], OS[Windows 10/10.0/amd64], JVM[Oracle Corporation/Java HotSpot(TM) 64-Bit Server VM/1.8.0_101/25.101-b13]
[2016-09-26T11:43:19,160][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [aggs-matrix-stats]
[2016-09-26T11:43:19,160][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [ingest-common]
[2016-09-26T11:43:19,161][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [lang-expression]
[2016-09-26T11:43:19,161][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [lang-groovy]
[2016-09-26T11:43:19,161][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [lang-mustache]
[2016-09-26T11:43:19,162][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [lang-painless]
[2016-09-26T11:43:19,162][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [percolator]
[2016-09-26T11:43:19,162][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [reindex]
[2016-09-26T11:43:19,162][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [transport-netty3]
[2016-09-26T11:43:19,163][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded module [transport-netty4]
[2016-09-26T11:43:19,163][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded plugin [ingest-attachment]
[2016-09-26T11:43:19,164][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded plugin [ingest-geoip]
[2016-09-26T11:43:19,164][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded plugin [mapper-attachments]
[2016-09-26T11:43:19,164][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded plugin [mapper-murmur3]
[2016-09-26T11:43:19,164][INFO ][o.e.p.PluginsService     ] [readonly-node-a9c5f4] loaded plugin [x-pack]
[2016-09-26T11:43:19,374][WARN ][d.m.attachment           ] [mapper-attachments] plugin has been deprecated and will be replaced by [ingest-attachment] plugin.
[2016-09-26T11:43:22,179][INFO ][o.e.n.Node               ] [readonly-node-a9c5f4] initialized
[2016-09-26T11:43:22,180][INFO ][o.e.n.Node               ] [readonly-node-a9c5f4] starting ...
*/
		private static readonly Regex ConsoleLineParser = new Regex(@"\[(?<date>.*?)\]\[(?<level>.*?)\](?:\[(?<section>.*?)\])(?: \[(?<node>.*?)\])? (?<message>.+)");

		public static bool TryParse(string line,
			out string date, out string level, out string section, out string node, out string message, out bool started)
		{
			date = level = section = node = message = null;
			started = false;
			if (string.IsNullOrEmpty(line)) return false;

			var match = ConsoleLineParser.Match(line);
			if (!match.Success) return false;
			date = match.Groups["date"].Value.Trim();
			level = match.Groups["level"].Value.Trim();
			section = match.Groups["section"].Value.Trim().Replace("org.elasticsearch.", "");
			node = match.Groups["node"].Value.Trim();
			message = match.Groups["message"].Value.Trim();
			started = TryGetStartedConfirmation(section, message);
			return true;
		}

		private static bool TryGetStartedConfirmation(string section, string message)
		{
			var inNodeSection = section == "o.e.n.Node" || section == "node";
			return inNodeSection && message == "started";
		}

		private static readonly Regex PortParser = new Regex(@"bound_address(es)? {.+\:(?<port>\d+)}");

		public static bool TryGetPortNumber(string section, string message, out int port)
		{
			port = 0;
            var inHttpSection = section == "o.e.h.HttpServer" || section == "http";
			if (!inHttpSection) return false;

			if (string.IsNullOrWhiteSpace(message)) return false;

			var match = PortParser.Match(message);
			if (!match.Success) return false;

			var portString = match.Groups["port"].Value.Trim();
			port = int.Parse(portString);
			return true;
		}

		//[2016-09-26T11:43:17,475][INFO ][o.e.n.Node               ] [readonly-node-a9c5f4] version[5.0.0-beta1], pid[13172], build[7eb6260/2016-09-20T23:10:37.942Z], OS[Windows 10/10.0/amd64], JVM[Oracle Corporation/Java HotSpot(TM) 64-Bit Server VM/1.8.0_101/25.101-b13]
		private static readonly Regex InfoParser =
			new Regex(@"version\[(?<version>.*)\], pid\[(?<pid>.*)\], build\[(?<build>.+)\]");

		public static bool TryParseNodeInfo(string section, string  message, out string version, out int? pid)
		{
            var inNodeSection = section == "o.e.n.Node" || section == "node";

			version = null;
			pid = null;
			if (!inNodeSection) return false;

			var match = InfoParser.Match(message.Replace(Environment.NewLine, ""));
			if (!match.Success) return false;

			version = match.Groups["version"].Value.Trim();
			pid = int.Parse(match.Groups["pid"].Value.Trim());
			return true;
		}
	}
}
