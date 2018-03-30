namespace Elastic.Managed.Configuration
{
	public struct NodeSetting
	{
		public string Key { get; }
		public string Value { get; }

		public NodeSetting(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		public override string ToString() => $"{Key}={Value}";
	}
}