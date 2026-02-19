// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.TUnit.Elasticsearch.Core;

/// <summary>
///     Connection details for an externally managed Elasticsearch cluster.
///     Used by <see cref="ElasticsearchCluster{TConfiguration}" /> to skip
///     ephemeral cluster startup when a remote cluster is available.
/// </summary>
public class ExternalClusterConfiguration
{
	public ExternalClusterConfiguration(Uri uri, string apiKey = null) =>
		(Uri, ApiKey) = (uri ?? throw new ArgumentNullException(nameof(uri)), apiKey);

	/// <summary> The base URI of the external Elasticsearch cluster. </summary>
	public Uri Uri { get; }

	/// <summary> An optional API key for authenticating with the cluster. </summary>
	public string ApiKey { get; }

	/// <summary>
	///     Validates that the external cluster is reachable by issuing a GET request to the root endpoint.
	///     Throws <see cref="InvalidOperationException" /> with a descriptive message on failure.
	/// </summary>
	public async Task ValidateAsync(TimeSpan? timeout = null, CancellationToken ctx = default)
	{
		timeout ??= TimeSpan.FromSeconds(10);

		using var handler = new HttpClientHandler
		{
			ServerCertificateCustomValidationCallback = (_, _, _, _) => true
		};
		using var http = new HttpClient(handler) { Timeout = timeout.Value };

		if (!string.IsNullOrEmpty(ApiKey))
			http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", ApiKey);

		try
		{
			var response = await http.GetAsync(Uri, ctx).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException(
				$"External Elasticsearch cluster at {Uri} is not reachable: {ex.Message}", ex);
		}
	}
}
