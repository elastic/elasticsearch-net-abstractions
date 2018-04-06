namespace Elastic.Managed.Configuration
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
