using System.Collections.Concurrent;
using Elastic.Stack.Artifacts.Platform;

namespace Elastic.Stack.Artifacts.Products
{
	public class Product
	{
		private static readonly ConcurrentDictionary<string, Product> CachedProducts = new ConcurrentDictionary<string, Product>();

		public static Product From(string product, SubProduct subProduct = null, ElasticVersion platformInZipAfter = null) =>
			CachedProducts.GetOrAdd(subProduct == null? $"{product}" : $"{product}/{subProduct.SubProductName}",
				k => new Product(product, subProduct, platformInZipAfter));

		private static readonly  ElasticVersion DefaultIncludePlatformSuffix = ElasticVersion.From("7.0.0-alpha1");

		private Product(string productName) => ProductName = productName;

		protected Product(string productName, SubProduct relatedProduct, ElasticVersion platformVersionSuffixAfter = null) : this(productName)
		{
			SubProduct = relatedProduct;
			PlatformSuffixAfter = platformVersionSuffixAfter ?? DefaultIncludePlatformSuffix;
		}

		public SubProduct SubProduct{ get; }

		public string Moniker => SubProduct?.SubProductName ?? ProductName;

		public string Extension => PlatformDependent ? OsMonikers.CurrentPlatformArchiveExtension() : "zip";

		public string ProductName { get; }

		public bool PlatformDependent => SubProduct?.PlatformDependent ?? true;

		public ElasticVersion PlatformSuffixAfter { get; }

		public static Product Elasticsearch { get; } = From("elasticsearch");

		public static Product ElasticsearchPlugin(ElasticsearchPlugin plugin) => From("elasticsearch-plugins", plugin);

		public static Product Kibana { get; } = From("kibana", platformInZipAfter: "1.0.0");

		public override string ToString() => SubProduct != null ? $"{ProductName}/{SubProduct.SubProductName}" : ProductName;

		public string PatchDownloadUrl(string downloadUrl) => SubProduct?.PatchDownloadUrl(downloadUrl) ?? downloadUrl;
	}
}
