using System;

namespace Elastic.Stack.Artifacts.Products
{
	public class SubProduct
	{
		public SubProduct(string subProject, Func<ElasticVersion, bool> isValid = null, Func<ElasticVersion, string> listName = null)
		{
			SubProductName = subProject;
			_isValid = isValid ?? (v => true);
			_getExistsMoniker = listName ?? (v => subProject);
		}

		public string SubProductName { get; }

		public ElasticVersion ShippedByDefaultAsOf { get; set; }
		/// <summary>
		/// Temporary, snapshot API reports bad plugin download urls
		/// </summary>
		public Func<string, string> PatchDownloadUrl { get; set; } = s => s;
		public bool PlatformDependent { get; protected set; }

		private readonly Func<ElasticVersion, bool> _isValid;
		private readonly Func<ElasticVersion, string> _getExistsMoniker;

		/// <summary> what moniker to use when asserting the sub product is already present</summary>
		public string GetExistsMoniker(ElasticVersion version) => _getExistsMoniker(version);

		/// <summary>Whether the sub project is included in the distribution out of the box for the given version</summary>
		public bool IsIncludedOutOfTheBox(ElasticVersion version) => ShippedByDefaultAsOf != null && version >= ShippedByDefaultAsOf;

		/// <summary>Whether the subProject is valid for the given version</summary>
		public bool IsValid(ElasticVersion version) => IsIncludedOutOfTheBox(version) || _isValid(version);

		public override string ToString() => SubProductName;
	}
}
