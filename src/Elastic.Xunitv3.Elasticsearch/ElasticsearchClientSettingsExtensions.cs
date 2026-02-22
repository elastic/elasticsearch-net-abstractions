// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Clients.Elasticsearch;

namespace Elastic.Xunitv3.Elasticsearch;

/// <summary>
///     Extension methods for <see cref="ElasticsearchClientSettings" />.
/// </summary>
public static class ElasticsearchClientSettingsExtensions
{
	/// <summary>
	///     Enables debug mode and routes per-request diagnostics to the given
	///     <paramref name="output" /> writer. Typically used with the
	///     <c>XunitTestOutputWriter</c> so that request/response logs appear
	///     in the current test's output.
	/// </summary>
	public static ElasticsearchClientSettings WireXunitOutput(
		this ElasticsearchClientSettings settings, TextWriter output) =>
		settings
			.EnableDebugMode()
			.OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
}
