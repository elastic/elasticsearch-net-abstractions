// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Stack.ArtifactsApi;

namespace Elastic.Elasticsearch.Managed.Configuration
{
	public class NodeSettings : List<NodeSetting>
	{
		private static readonly ElasticVersion
			LastVersionWithoutPrefixForSettings = ElasticVersion.From("5.0.0-alpha2");

		public NodeSettings()
		{
		}

		public NodeSettings(NodeSettings settings) : base(settings)
		{
		}

		public void Add(string setting)
		{
			var s = setting.Split(new[] {'='}, 2, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length != 2)
				throw new ArgumentException($"Can only add node settings in key=value from but received: {setting}");
			Add(new NodeSetting(s[0], s[1], null));
		}

		public void Add(string key, string value) => Add(new NodeSetting(key, value, null));

		public void Add(string key, string value, string versionRange) =>
			Add(new NodeSetting(key, value, versionRange));

		public string[] ToCommandLineArguments(ElasticVersion version)
		{
			var settingsPrefix = version > LastVersionWithoutPrefixForSettings ? "" : "es.";
			var settingArgument = version.Major >= 5 ? "-E " : "-D";
			return this
				//if a node setting is only applicable for a certain version make sure its filtered out
				.Where(s => string.IsNullOrEmpty(s.VersionRange) || version.InRange(s.VersionRange))
				//allow additional settings to take precedence over already DefaultNodeSettings
				//without relying on elasticsearch to dedup, 5.4.0 no longer allows passing the same setting twice
				//on the command with the latter taking precedence
				.GroupBy(setting => setting.Key)
				.Select(g => g.Last())
				.Select(s => s.Key.StartsWith(settingArgument) ? s.ToString() : $"{settingArgument}{settingsPrefix}{s}")
				.ToArray();
		}
	}
}
