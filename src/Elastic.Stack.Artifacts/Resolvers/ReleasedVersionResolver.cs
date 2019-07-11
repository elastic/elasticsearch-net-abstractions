using System.Runtime.InteropServices;
using Elastic.Stack.Artifacts.Platform;
using Elastic.Stack.Artifacts.Products;
using SemVer;

namespace Elastic.Stack.Artifacts.Resolvers
{
	public static class ReleasedVersionResolver
	{
		//https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-7.1.0-linux-x86_64.tar.gz
		private const string ArtifactsUrl = "https://artifacts.elastic.co";
		public static bool TryResolve(Product product, Version version, OSPlatform os, out Artifact artifact)
		{
			var p = product.Moniker;
			var downloadPath = $"{ArtifactsUrl}/downloads/{product}";
			var archive = $"{p}-{version}-{OsMonikers.CurrentPlatformPackageSuffix()}.{product.Extension}";
			if (!product.PlatformDependant || version <= product.PlatformSuffixAfter)
				archive = $"{p}-{version}.{product.Extension}";

			var downloadUrl = $"{downloadPath}/{archive}";
			artifact = new Artifact(product, version, downloadUrl, ArtifactBuildState.Released, null);
			return true;
		}
	}
}
