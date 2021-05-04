// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using Elastic.Stack.ArtifactsApi.Products;
using Elastic.Stack.ArtifactsApi.Resolvers;
using Version = SemVer.Version;

namespace Elastic.Stack.ArtifactsApi
{
	public class Artifact
	{
		private static readonly Uri BaseUri = new Uri("http://localhost");

		internal Artifact(Product product, Version version, string downloadUrl, ArtifactBuildState state,
			string buildHash)
		{
			ProductName = product.ProductName;
			Version = version;
			DownloadUrl = product?.PatchDownloadUrl(downloadUrl);
			State = state;
			BuildHash = buildHash;
		}

		internal Artifact(Product product, Version version, SearchPackage package, string buildHash = null)
		{
			ProductName = product.ProductName;
			Version = version;
			State = ArtifactBuildState.Snapshot;
			DownloadUrl = product?.PatchDownloadUrl(package.DownloadUrl);
			ShaUrl = package.ShaUrl;
			AscUrl = package.AscUrl;
			BuildHash = buildHash;
		}

		public string LocalFolderName
		{
			get
			{
				var hashed = string.IsNullOrWhiteSpace(BuildHash) ? string.Empty : $"-build-{BuildHash}";
				switch (State)
				{
					case ArtifactBuildState.Released:
						return $"{ProductName}-{Version}";
					case ArtifactBuildState.Snapshot:
						return $"{ProductName}-{Version}{hashed}";
					case ArtifactBuildState.BuildCandidate:
						return $"{ProductName}-{Version}{hashed}";
					default:
						throw new ArgumentOutOfRangeException(nameof(State), $"{State} not expected here");
				}
			}
		}

		public string FolderInZip => $"{ProductName}-{Version}";

		public string Archive
		{
			get
			{
				if (!Uri.TryCreate(DownloadUrl, UriKind.Absolute, out var uri))
					uri = new Uri(BaseUri, DownloadUrl);

				return Path.GetFileName(uri.LocalPath);
			}
		}

		// ReSharper disable UnusedAutoPropertyAccessor.Global
		public string ProductName { get; }
		public Version Version { get; }
		public string DownloadUrl { get; }

		public ArtifactBuildState State { get; }
		public string BuildHash { get; }
		public string ShaUrl { get; }

		public string AscUrl { get; }
		// ReSharper restore UnusedAutoPropertyAccessor.Global
	}
}
