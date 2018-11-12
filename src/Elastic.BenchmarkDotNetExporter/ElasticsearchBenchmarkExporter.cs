using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Reports;
using Elasticsearch.Net;
using Nest;

namespace Elastic.BenchmarkDotNetExporter
{

	public class ElasticsearchBenchmarkExporter : BenchmarkDotNet.Exporters.ExporterBase
	{
		public ElasticsearchBenchmarkExporter(
			string urls,
			string username,
			string password,
			string commitSha = "unknown",
			string currentBranch = "unknown",
			string indexName = "benchmarks-dotnet",
			string templateName = "benchmarks-dotnet",
			string pipelineName = "benchmarks-dotnet"
		)
		{
			if (string.IsNullOrWhiteSpace(urls)) throw new ArgumentException("no urls provided, empty string or null", nameof(urls));
			var uris = urls.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
				.Select(u => u.Trim())
				.Select(u => Uri.TryCreate(u, UriKind.Absolute, out var url) ? url : null)
				.Where(u => u != null)
				.ToList();
			if (uris.Count == 0) throw new ArgumentException($"'{urls}' can not be parsed to a list of Uri", nameof(urls));

			var cp = uris.Count == 1 ? new SingleNodeConnectionPool(uris[0]) : (IConnectionPool)new SniffingConnectionPool(uris);
			var settings = new ConnectionSettings(cp);
			if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
				settings = settings.BasicAuthentication(username, password);

			this.Client = new ElasticClient(settings);
			this.CommitSha = commitSha;
			this.CurrentBranchName = currentBranch;
			this.IndexName = indexName;
			this.PipelineName = pipelineName;
			this.TemplateName = templateName;
		}

		private ElasticClient Client { get; }
		private string CommitSha { get; }
		private string CurrentBranchName { get; }
		private string IndexName { get; }
		private string PipelineName { get; }
		private string TemplateName { get; }

		//we only log when we can not write to Elasticsearch
		protected override string FileExtension => "log";
		protected override string FileNameSuffix => "-elasticsearch-error";

		public override void ExportToLog(Summary summary, ILogger logger)
		{
			if (TryPutIndexTemplate(logger)) return;
			if (!TryPutPipeline(logger)) return;

			var environmentInfo = CreateHostEnvironmentInformation(summary);
			var benchmarks = CreateBenchmarkDocuments(summary, environmentInfo);

			IndexBenchmarksIntoElasticsearch(logger, benchmarks);
		}

		private void IndexBenchmarksIntoElasticsearch(ILogger logger, List<BenchmarkDocument> benchmarks)
		{
			var bulk = this.Client.Bulk(b => b
				.Index(this.IndexName)
				.Type("_doc")
				.Pipeline(this.PipelineName)
				.IndexMany(benchmarks)
			);
			if (!bulk.IsValid) logger.WriteLine(bulk.DebugInformation);
		}

		private bool TryPutIndexTemplate(ILogger logger)
		{
			var indexTemplateExists = this.Client.IndexTemplateExists(this.TemplateName).Exists;
			if (indexTemplateExists) return true;

			var putIndexTemplate = this.Client.PutIndexTemplate(this.TemplateName, r => r
				.Settings(s => s.NumberOfShards(1))
				.Mappings(m => m.Map<BenchmarkDocument>(mm => mm.AutoMap()))
				.IndexPatterns(this.IndexName, $"{this.IndexName}-*")
			);
			if (putIndexTemplate.IsValid) return true;

			logger.WriteLine(putIndexTemplate.DebugInformation);
			return false;

		}

		private bool TryPutPipeline(ILogger logger)
		{
			if (string.IsNullOrWhiteSpace(this.PipelineName)) return true;

			var getPipelineResponse = this.Client.GetPipeline(p => p.Id(this.PipelineName));
			if (getPipelineResponse.IsValid) return true;

			var putPipeline = this.Client.PutPipeline(this.PipelineName, r => r
				.Description("Enriches the benchmark exports from BenchmarkDotNet")
				.Processors(procs => procs
					.DateIndexName<BenchmarkDocument>(din => din
						.Field(p => p.Date)
						.IndexNamePrefix($"{this.IndexName}-")
						.DateRounding(DateRounding.Month)
						.DateFormats("yyyy-MM-dd'T'HH:mm:ss.SSSSSSSZ")
					)
				)
			);
			if (putPipeline.IsValid) return true;

			logger.WriteLine(putPipeline.DebugInformation);
			return false;

		}

		private List<BenchmarkDocument> CreateBenchmarkDocuments(Summary summary, HostEnv environmentInfo)
		{
			var benchmarks = summary.Reports.Select(r =>
			{
				var data = new BenchmarkDocument
				{
					Date = DateTime.UtcNow,
					Commit = this.CommitSha,
					BranchName = this.CurrentBranchName,
					Title = summary.Title,
					TotalBenchmarkRunTime = summary.TotalTime,
					HostEnvironmentInfo = environmentInfo,

					DisplayInfo = r.BenchmarkCase.DisplayInfo,
					Namespace = r.BenchmarkCase.Descriptor.Type.Namespace,
					Type = FullNameProvider.GetTypeName(r.BenchmarkCase.Descriptor.Type),
					Method = r.BenchmarkCase.Descriptor.WorkloadMethod.Name,
					MethodTitle = r.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo,
					Parameters = r.BenchmarkCase.Parameters.PrintInfo,
					// do NOT remove this property, it is used for xunit-performance migration
					FullName = FullNameProvider.GetBenchmarkName(r.BenchmarkCase),
					Statistics = r.ResultStatistics
				};

				if (summary.Config.HasMemoryDiagnoser()) data.Memory = r.GcStats;

				data.MeasurementStages = r.AllMeasurements
					.GroupBy(m => $"{m.IterationStage.ToString()}-{m.IterationMode.ToString()}")
					.Where(g => g.Any())
					.Select(g => new BenchmarkMeasurementStage
					{
						IterationMode = g.First().IterationMode.ToString(),
						IterationStage = g.First().IterationStage.ToString(),
						Operations = g.First().Operations,
					});

				return data;
			}).ToList();
			return benchmarks;
		}

		private static HostEnv CreateHostEnvironmentInformation(Summary summary)
		{
			var environmentInfo = new HostEnv
			{
				BenchmarkDotNetCaption = HostEnvironmentInfo.BenchmarkDotNetCaption,
				BenchmarkDotNetVersion = summary.HostEnvironmentInfo.BenchmarkDotNetVersion,
				OsVersion = summary.HostEnvironmentInfo.OsVersion.Value,
				ProcessorName = summary.HostEnvironmentInfo.CpuInfo.Value.ProcessorName,
				PhysicalProcessorCount = summary.HostEnvironmentInfo.CpuInfo.Value?.PhysicalProcessorCount,
				PhysicalCoreCount = summary.HostEnvironmentInfo.CpuInfo.Value?.PhysicalCoreCount,
				LogicalCoreCount = summary.HostEnvironmentInfo.CpuInfo.Value?.LogicalCoreCount,
				RuntimeVersion = summary.HostEnvironmentInfo.RuntimeVersion,
				Architecture = summary.HostEnvironmentInfo.Architecture,
				HasAttachedDebugger = summary.HostEnvironmentInfo.HasAttachedDebugger,
				HasRyuJit = summary.HostEnvironmentInfo.HasRyuJit,
				Configuration = summary.HostEnvironmentInfo.Configuration,
				JitModules = summary.HostEnvironmentInfo.JitModules,
				DotNetSdkVersion = summary.HostEnvironmentInfo.DotNetSdkVersion.Value,
				ChronometerFrequency = new ChronometerFrequency {Hertz = summary.HostEnvironmentInfo.ChronometerFrequency.Hertz},
				HardwareTimerKind = summary.HostEnvironmentInfo.HardwareTimerKind.ToString()
			};
			return environmentInfo;
		}

		internal class BenchmarkMeasurementStage
		{
			public string IterationMode { get; set; }
			public string IterationStage { get; set; }
			public long Operations { get; set; }
		}

		internal class BenchmarkData
		{
			public string DisplayInfo { get; set; }
			public string Namespace { get; set; }
			public string Type { get; set; }
			public string Method { get; set; }
			public string MethodTitle { get; set; }
			public string Parameters { get; set; }
			public string FullName { get; set; }
			public Statistics Statistics { get; set; }
			public GcStats Memory { get; set; }
			public IEnumerable<BenchmarkMeasurementStage> MeasurementStages { get; set; }
		}

		/// <summary> Represents a benchmark case with information of the overall benchmark run as well. </summary>
		internal class BenchmarkDocument : BenchmarkData
		{
			public string Title { get; set; }
			public string BranchName { get; set; }
			public string Commit { get; set; }
			public DateTime Date { get; set; }
			public TimeSpan TotalBenchmarkRunTime { get; set; }
			public HostEnv HostEnvironmentInfo { get; set; }
			public BenchmarkData Benchmark { get; set; }
		}

		internal class HostEnv
		{
			public string BenchmarkDotNetCaption { get; set; }
			public string BenchmarkDotNetVersion { get; set; }
			public string OsVersion { get; set; }
			public string ProcessorName { get; set; }
			public int? PhysicalProcessorCount { get; set; }
			public int? PhysicalCoreCount { get; set; }
			public int? LogicalCoreCount { get; set; }
			public string RuntimeVersion { get; set; }
			public string Architecture { get; set; }
			public bool HasAttachedDebugger { get; set; }
			public bool HasRyuJit { get; set; }
			public string Configuration { get; set; }
			public string JitModules { get; set; }
			public string DotNetSdkVersion { get; set; }
			public ChronometerFrequency ChronometerFrequency { get; set; }
			public string HardwareTimerKind { get; set; }
		}

		internal class ChronometerFrequency { public double Hertz { get; set; } }

	}


	// copy pasta from https://raw.githubusercontent.com/dotnet/BenchmarkDotNet/04a71586206a822bca56f0abdacefdc2e5fc1b01/src/BenchmarkDotNet/Exporters/FullNameProvider.cs
	// needs a PR to open this up to the outside
}
