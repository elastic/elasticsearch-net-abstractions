// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elastic.Elasticsearch.Managed.Configuration
{
	public struct NodeSetting
	{
		public string Key { get; }
		public string Value { get; }

		/// <summary>
		/// Stores for which elasticsearch version range this setting is applicable
		/// </summary>
		public string VersionRange { get; }

		public NodeSetting(string key, string value, string range)
		{
			this.Key = key;
			this.Value = value;
			this.VersionRange = range;
		}

		public override string ToString() => $"{Key}={Value}";
	}
}
