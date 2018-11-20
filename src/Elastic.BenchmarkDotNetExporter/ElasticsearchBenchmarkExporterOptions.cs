using System;
using System.Linq;
using Elasticsearch.Net;
using Nest;

namespace Elastic.BenchmarkDotNetExporter
{
	/// <summary> Configure the Elasticsearch BenchmarkDotNet exporter</summary>
	public class ElasticsearchBenchmarkExporterOptions
	{
		/// <summary>
		/// Configure the exporter options, the <see cref="commaSeparatedListOfUrls"> parameter is required.
		/// <para>The other options can be specified in the property initializer</para>
		/// </summary>
		/// <param name="commaSeparatedListOfUrls">
		/// A list of comma separated Elasticsearch nodes of the cluster you want to report into.
		/// <para>You need to specify at least one node, if you enable sniffing the exporter will find the rest of the nodes</para>
		/// </param>
		/// <exception cref="ArgumentException">If none of the urls specified parse into a <see cref="Uri"/></exception>
		public ElasticsearchBenchmarkExporterOptions(string commaSeparatedListOfUrls) : this(Parse(commaSeparatedListOfUrls)) { }

		/// <summary>
		/// Configure the exporter options, the <see cref="nodes"> parameter is required.
		/// <para>The other options can be specified in the property initializer</para>
		/// </summary>
		/// <param name="nodes">
		/// A list of Elasticsearch nodes of the cluster you want to report into.
		/// <para>You need to specify at least one node, if you enable sniffing the exporter will find the rest of the nodes</para>
		/// </param>
		public ElasticsearchBenchmarkExporterOptions(params Uri[] nodes)
		{
			this.Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
			if (this.Nodes.Length == 0)
				throw new ArgumentException($"No nodes were passed to {nameof(ElasticsearchBenchmarkExporterOptions)}", nameof(nodes));
		}


		public Uri[] Nodes { get; }

		/// <summary> If Elasticsearch security is enabled (it should!) this sets the username to be used. </summary>
		public string Username { get; set; }
		/// <summary> If Elasticsearch security is enabled (it should!) this sets the password to be used. </summary>
		public string Password { get; set; }
		/// <summary> Instructs the exporter to use a sniffing connection pool, which will discover the rest of the cluster</summary>
		public bool UseSniffingConnectionPool { get; set; }

		/// <summary> (Optional) Report the sha of the commit we are benchmarking</summary>
		public string GitCommitSha { get; set; }
		/// <summary> (Optional) Report the message of the commit we are benchmarking</summary>
		public string GitCommitMessage { get; set; }
		/// <summary> (Optional) Report the branch of the commit we are benchmarking</summary>
		public string GitBranch { get; set; }
		/// <summary> (Optional) Report the repository, does not have to be a complete URI</summary>
		public string GitRepositoryIdentifier { get; set; }

		internal static readonly string DefaultMonniker = "benchmarks-dotnet";
		/// <summary>
		/// The prefix for the indices being created, indices will be suffixed with <code>-DATE</code>
		/// see <see cref="IndexStrategy"/> how to control the DATE rounding.
		/// </summary>
		public string IndexName { get; set; } = DefaultMonniker;
		public string TemplateName { get; set; } = DefaultMonniker;
		public string PipelineName { get; set; } = DefaultMonniker;
		public TimeSeriesStrategy IndexStrategy { get; set; } = TimeSeriesStrategy.Default;

		private static Uri[] Parse(string urls)
		{
			if (string.IsNullOrWhiteSpace(urls)) throw new ArgumentException("no urls provided, empty string or null", nameof(urls));
			var uris = urls.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
				.Select(u => u.Trim())
				.Select(u => Uri.TryCreate(u, UriKind.Absolute, out var url) ? url : null)
				.Where(u => u != null)
				.ToList();
			if (uris.Count == 0) throw new ArgumentException($"'{urls}' can not be parsed to a list of Uri", nameof(urls));
			return uris.ToArray();
		}
		internal IConnectionPool CreateConnectionPool()
		{
			var uris = this.Nodes;
			if (uris.Length == 1)
				return this.UseSniffingConnectionPool
					? new SniffingConnectionPool(uris)
					: (IConnectionPool)new SingleNodeConnectionPool(uris[0]);

			return this.UseSniffingConnectionPool
				? new SniffingConnectionPool(uris)
				: new StaticConnectionPool(uris);

		}

		internal ConnectionSettings CreateConnectionSettings()
		{
			var settings = new ConnectionSettings(CreateConnectionPool());
			if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
				settings = settings.BasicAuthentication(Username, Password);
			return settings;
		}




		/// <summary>
		/// If <see cref="ElasticsearchBenchmarkExporterOptions.PipelineName"/> is set, which it by default. This controls
		/// how the pipeline rewrites the <see cref="ElasticsearchBenchmarkExporterOptions.IndexName"/> to a time series index.
		/// <para>NOTE: this only controls what happens when the pipeline gets created initially</para>
		/// </summary>
		public enum TimeSeriesStrategy
		{
			/// <summary> The default rounding is to create <see cref="Monthly"/> benchmark indices </summary>
			Default,
			Hourly,
			Dayly,
			Weekly,
			Monthly,
			Yearly,
		}
	}
}
