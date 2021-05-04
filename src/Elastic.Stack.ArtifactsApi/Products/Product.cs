// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Concurrent;
using Elastic.Stack.ArtifactsApi.Platform;

namespace Elastic.Stack.ArtifactsApi.Products
{
	public class Product
	{
		private static readonly ConcurrentDictionary<string, Product> CachedProducts =
			new ConcurrentDictionary<string, Product>();

		private static readonly ElasticVersion DefaultIncludePlatformSuffix = ElasticVersion.From("7.0.0-alpha1");

		private Product(string productName) => ProductName = productName;

		protected Product(string productName, SubProduct relatedProduct,
			ElasticVersion platformVersionSuffixAfter = null) : this(productName)
		{
			SubProduct = relatedProduct;
			PlatformSuffixAfter = platformVersionSuffixAfter ?? DefaultIncludePlatformSuffix;
		}

		public SubProduct SubProduct { get; }

		public string Moniker => SubProduct?.SubProductName ?? ProductName;

		public string Extension => PlatformDependent ? OsMonikers.CurrentPlatformArchiveExtension() : "zip";

		public string ProductName { get; }

		public bool PlatformDependent => SubProduct?.PlatformDependent ?? true;

		public ElasticVersion PlatformSuffixAfter { get; }

		public static Product Elasticsearch { get; } = From("elasticsearch");

		public static Product Kibana { get; } = From("kibana", platformInZipAfter: "1.0.0");

		public static Product From(string product, SubProduct subProduct = null,
			ElasticVersion platformInZipAfter = null) =>
			CachedProducts.GetOrAdd(subProduct == null ? $"{product}" : $"{product}/{subProduct.SubProductName}",
				k => new Product(product, subProduct, platformInZipAfter));

		public static Product ElasticsearchPlugin(ElasticsearchPlugin plugin) => From("elasticsearch-plugins", plugin);

		public override string ToString() =>
			SubProduct != null ? $"{ProductName}/{SubProduct.SubProductName}" : ProductName;

		public string PatchDownloadUrl(string downloadUrl) => SubProduct?.PatchDownloadUrl(downloadUrl) ?? downloadUrl;
	}
}
