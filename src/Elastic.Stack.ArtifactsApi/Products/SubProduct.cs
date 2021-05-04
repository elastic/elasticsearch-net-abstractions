// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Elastic.Stack.ArtifactsApi.Products
{
	public class SubProduct
	{
		private readonly Func<ElasticVersion, string> _getExistsMoniker;

		private readonly Func<ElasticVersion, bool> _isValid;

		public SubProduct(string subProject, Func<ElasticVersion, bool> isValid = null,
			Func<ElasticVersion, string> listName = null)
		{
			SubProductName = subProject;
			_isValid = isValid ?? (v => true);
			_getExistsMoniker = listName ?? (v => subProject);
		}

		public string SubProductName { get; }

		public ElasticVersion ShippedByDefaultAsOf { get; set; }

		/// <summary>
		///     Temporary, snapshot API reports bad plugin download urls
		/// </summary>
		public Func<string, string> PatchDownloadUrl { get; set; } = s => s;

		public bool PlatformDependent { get; protected set; }

		/// <summary> what moniker to use when asserting the sub product is already present</summary>
		public string GetExistsMoniker(ElasticVersion version) => _getExistsMoniker(version);

		/// <summary>Whether the sub project is included in the distribution out of the box for the given version</summary>
		public bool IsIncludedOutOfTheBox(ElasticVersion version) =>
			ShippedByDefaultAsOf != null && version >= ShippedByDefaultAsOf;

		/// <summary>Whether the subProject is valid for the given version</summary>
		public bool IsValid(ElasticVersion version) => IsIncludedOutOfTheBox(version) || _isValid(version);

		public override string ToString() => SubProductName;
	}
}
