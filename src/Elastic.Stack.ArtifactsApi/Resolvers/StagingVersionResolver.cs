// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Stack.ArtifactsApi.Platform;
using Elastic.Stack.ArtifactsApi.Products;
using SemVer;

namespace Elastic.Stack.ArtifactsApi.Resolvers
{
	public static class StagingVersionResolver
	{
		private const string StagingUrlFormat = "https://staging.elastic.co/{0}-{1}";

		// https://staging.elastic.co/7.2.0-957e3089/downloads/elasticsearch/elasticsearch-7.2.0-linux-x86_64.tar.gz
		// https://staging.elastic.co/7.2.0-957e3089/downloads/elasticsearch-plugins/analysis-icu/analysis-icu-7.2.0.zip
		public static bool TryResolve(Product product, Version version, string buildHash, out Artifact artifact)
		{
			artifact = null;
			if (string.IsNullOrWhiteSpace(buildHash)) return false;

			var p = product.Moniker;
			var stagingRoot = string.Format(StagingUrlFormat, version, buildHash);
			var archive = $"{p}-{version}-{OsMonikers.CurrentPlatformPackageSuffix()}.{product.Extension}";
			if (!product.PlatformDependent || version <= product.PlatformSuffixAfter)
				archive = $"{p}-{version}.{product.Extension}";

			var downloadUrl = $"{stagingRoot}/downloads/{product}/{archive}";
			artifact = new Artifact(product, version, downloadUrl, ArtifactBuildState.BuildCandidate, buildHash);
			return true;
		}
	}
}
