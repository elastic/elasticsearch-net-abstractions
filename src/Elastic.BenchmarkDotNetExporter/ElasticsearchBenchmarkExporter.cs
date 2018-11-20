using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
		public ElasticsearchBenchmarkExporter(string commaSeparatedListOfUrls)
			: this(new ElasticsearchBenchmarkExporterOptions(commaSeparatedListOfUrls)) {}

		public ElasticsearchBenchmarkExporter(ElasticsearchBenchmarkExporterOptions options)
		{
			Options = options;
			Client = new ElasticClient(Options.CreateConnectionSettings()
				.DefaultMappingFor<BenchmarkDocument>(m=>m
					.TypeName("_doc")
				)
				.DefaultFieldNameInferrer(p=>ToUnderscoreCase(p))
			);
		}

		private static string ToUnderscoreCase(string str) {
			return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
		}


		private ElasticsearchBenchmarkExporterOptions Options { get; }
		private IElasticClient Client { get; set; }

		//we only log when we can not write to Elasticsearch
		protected override string FileExtension => "log";
		protected override string FileNameSuffix => "-elasticsearch-error";

		public override void ExportToLog(Summary summary, ILogger logger)
		{
			if (!TryPutIndexTemplate(logger)) return;
			if (!TryPutPipeline(logger)) return;

			var benchmarks = CreateBenchmarkDocuments(summary);

			IndexBenchmarksIntoElasticsearch(logger, benchmarks);
		}

		private void IndexBenchmarksIntoElasticsearch(ILogger logger, List<BenchmarkDocument> benchmarks)
		{
			var bulk = Client.Bulk(b => b
				.Index(Options.IndexName)
				.Type("_doc")
				.Pipeline(Options.PipelineName)
				.IndexMany(benchmarks)
			);
			if (!bulk.IsValid) logger.WriteLine(bulk.DebugInformation);
		}

		private bool TryPutIndexTemplate(ILogger logger)
		{
			var indexTemplateExists = Client.IndexTemplateExists(Options.TemplateName).Exists;
			if (indexTemplateExists) return true;

			var putIndexTemplate = Client.PutIndexTemplate(Options.TemplateName, r => r
				.Settings(s => s.NumberOfShards(1))
				.Mappings(m => m.Map<BenchmarkDocument>(mm => mm.AutoMap()))
				.IndexPatterns(Options.IndexName, $"{Options.IndexName}-*")
			);
			if (putIndexTemplate.IsValid) return true;

			logger.WriteLine(putIndexTemplate.DebugInformation);
			return false;

		}

		private bool TryPutPipeline(ILogger logger)
		{
			if (string.IsNullOrWhiteSpace(Options.PipelineName)) return true;

			var getPipelineResponse = Client.GetPipeline(p => p.Id(Options.PipelineName));
			if (getPipelineResponse.IsValid) return true;

			var putPipeline = Client.PutPipeline(Options.PipelineName, r => r
				.Description("Enriches the benchmark exports from BenchmarkDotNet")
				.Processors(procs => procs
					.DateIndexName<BenchmarkDocument>(din => din
						.Field(p => p.Timestamp)
						.IndexNamePrefix($"{Options.IndexName}-")
						.DateRounding(DateRounding.Month)
						.DateFormats("yyyy-MM-dd'T'HH:mm:ss.SSSSSSSZ")
					)
				)
			);
			if (putPipeline.IsValid) return true;

			logger.WriteLine(putPipeline.DebugInformation);
			return false;

		}

		private List<BenchmarkDocument> CreateBenchmarkDocuments(Summary summary)
		{
			var host = CreateHostEnvironmentInformation(summary);
			var git = new BenchmarkGit
			{
				Sha = Options.GitCommitSha,
				BranchName = Options.GitBranch,
				CommitMessage = Options.GitCommitMessage,
				Repository = Options.GitRepositoryIdentifier
			};
			var runtimeVersion = new BenchmarkLanguage
			{
				Version = summary.HostEnvironmentInfo.RuntimeVersion,
				DotNetSdkVersion = summary.HostEnvironmentInfo.DotNetSdkVersion.Value,
				HasRyuJit = summary.HostEnvironmentInfo.HasRyuJit,
				JitModules = summary.HostEnvironmentInfo.JitModules,
				BuildConfiguration = summary.HostEnvironmentInfo.Configuration,
				BenchmarkDotNetCaption = HostEnvironmentInfo.BenchmarkDotNetCaption,
				BenchmarkDotNetVersion = summary.HostEnvironmentInfo.BenchmarkDotNetVersion,
			};
			var os = new BenchmarkOperatingSystem
			{
				Version = summary.HostEnvironmentInfo.OsVersion.Value,
				Name = OsName(),
				Platform = OsPlatform()
			};
			var agent = new BenchmarkAgent
			{
				OperatingSystem = os,
				Git = git,
				Language = runtimeVersion,
			};
			var benchmarks = summary.Reports.Select(r =>
			{
				var @event = new BenchmarkEvent
				{
					Description = r.BenchmarkCase.Descriptor.WorkloadMethodDisplayInfo,

					Action = r.BenchmarkCase.Descriptor.WorkloadMethod.Name,
					Module = r.BenchmarkCase.Descriptor.Type.Namespace,
					Category = summary.Title,
					Type = FullNameProvider.GetTypeName(r.BenchmarkCase.Descriptor.Type),
					Duration = summary.TotalTime,
					Original = r.BenchmarkCase.DisplayInfo,

					Method = FullNameProvider.GetBenchmarkName(r.BenchmarkCase),
					Parameters = r.BenchmarkCase.Parameters.PrintInfo,
				};

				var data = new BenchmarkDocument
				{
					Timestamp = DateTime.UtcNow,
					Host = host,
					Agent = agent,
					Event = @event,
					Benchmark = new BenchmarkData(r.ResultStatistics),
				};

				if (summary.Config.HasMemoryDiagnoser()) data.Benchmark.Memory = r.GcStats;

				var grouped = r.AllMeasurements
					.GroupBy(m => $"{m.IterationStage.ToString()}-{m.IterationMode.ToString()}")
					.Where(g => g.Any())
					.ToList();

				@event.MeasurementStages = grouped
					.Select(g => new BenchmarkMeasurementStage
					{
						IterationMode = g.First().IterationMode.ToString(),
						IterationStage = g.First().IterationStage.ToString(),
						Operations = g.First().Operations,
					});

				var warmupCount = grouped.Select(g=>g.First())
					.FirstOrDefault(s => s.IterationStage == IterationStage.Warmup && s.IterationMode == IterationMode.Workload)
					.Operations;
				var measuredCount = grouped.Select(g=>g.First())
					.FirstOrDefault(s => s.IterationStage == IterationStage.Result && s.IterationMode == IterationMode.Workload)
					.Operations;
				@event.Repetitions = new BenchmarkSimplifiedWorkloadCounts
				{
					Warmup = warmupCount,
					Measured = measuredCount
				};
				return data;
			}).ToList();
			return benchmarks;
		}

		private string OsName()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX: return "Max OS X";
				case PlatformID.Unix: return "Linux";
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE: return "Windows";
				case PlatformID.Xbox: return "XBox";
				default: return "Unknown";
			}
		}

		private string OsPlatform()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX: return "darwin";
				case PlatformID.Unix: return "unix";
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE: return "windows";
				case PlatformID.Xbox: return "xbox";
				default: return "unknown";
			}
		}

		private static BenchmarkHost CreateHostEnvironmentInformation(Summary summary)
		{
			var environmentInfo = new BenchmarkHost
			{
				ProcessorName = summary.HostEnvironmentInfo.CpuInfo.Value.ProcessorName,
				PhysicalProcessorCount = summary.HostEnvironmentInfo.CpuInfo.Value?.PhysicalProcessorCount,
				PhysicalCoreCount = summary.HostEnvironmentInfo.CpuInfo.Value?.PhysicalCoreCount,
				LogicalCoreCount = summary.HostEnvironmentInfo.CpuInfo.Value?.LogicalCoreCount,
				Architecture = summary.HostEnvironmentInfo.Architecture,
				HasAttachedDebugger = summary.HostEnvironmentInfo.HasAttachedDebugger,
				ChronometerFrequencyHerz = summary.HostEnvironmentInfo.ChronometerFrequency.Hertz,
				HardwareTimerKind = summary.HostEnvironmentInfo.HardwareTimerKind.ToString()
			};
			return environmentInfo;
		}

		internal class BenchmarkMeasurementStage
		{
			[Keyword]public string IterationMode { get; set; }
			[Keyword]public string IterationStage { get; set; }
			public long Operations { get; set; }
		}

		internal class BenchmarkSimplifiedWorkloadCounts
		{
			public long Warmup { get; set; }
			public long Measured { get; set; }
		}

		/// <summary> Represents a benchmark case with information of the overall benchmark run as well. </summary>
		internal class BenchmarkDocument
		{
			[Date(Name = "@timestamp")]
			public DateTime Timestamp { get; set; }

			public BenchmarkAgent Agent { get; set; }

			public BenchmarkHost Host { get; set; }
			public BenchmarkData Benchmark { get; set; }
			public BenchmarkEvent Event { get; set; }
		}

		internal class BenchmarkEvent
		{
			[Keyword]public string Category { get; set; }

			[Text]public string Original { get; set; }
			[Keyword]public string Module { get; set; }
			[Keyword]public string Type { get; set; }
			[Keyword]public string Action { get; set; }
			[Text]public string Description { get; set; }
			[Text]public string Parameters { get; set; }
			[Keyword]public string Method { get; set; }
			public IEnumerable<BenchmarkMeasurementStage> MeasurementStages { get; set; }
			public TimeSpan Duration { get; set; }
			public BenchmarkSimplifiedWorkloadCounts Repetitions { get; set; }
		}


		internal class BenchmarkData
		{
			public GcStats Memory { get; set; }
			public int N { get; }
			public double Min { get; }
			public double LowerFence { get; }
			public double Q1 { get; }
			public double Median { get; }
			public double Mean { get; }
			public double Q3 { get; }
			public double UpperFence { get; }
			public double Max { get; }
			public double InterquartileRange { get; }
			public double[] LowerOutliers { get; }
			public double[] UpperOutliers { get; }
			public double[] AllOutliers { get; }
			public double StandardError { get; }
			public double Variance { get; }
			public double StandardDeviation { get; }
			public double Skewness { get; }
			public double Kurtosis { get; }
			public ConfidenceInterval ConfidenceInterval { get; }
			public PercentileValues Percentiles { get; }

			public BenchmarkData(Statistics statistics)
			{
				this.N = statistics.N;
				this.Min = statistics.Min;
				this.LowerFence = statistics.LowerFence;
				this.Q1 = statistics.Q1;
				this.Median = statistics.Median;
				this.Mean = statistics.Mean;
				this.Q3 = statistics.Q3;
				this.UpperFence = statistics.UpperFence;
				this.Max = statistics.Max;
				this.InterquartileRange = statistics.InterquartileRange;
				this.LowerOutliers = statistics.LowerOutliers;
				this.UpperOutliers = statistics.UpperOutliers;
				this.AllOutliers = statistics.AllOutliers;
				this.StandardError = statistics.StandardError;
				this.Variance = statistics.Variance;
				this.StandardDeviation = statistics.StandardDeviation;
				this.Skewness = statistics.Skewness;
				this.Kurtosis = statistics.Kurtosis;
				this.ConfidenceInterval = statistics.ConfidenceInterval;
				this.Percentiles = statistics.Percentiles;

			}
		}

		internal class BenchmarkAgent
		{
			private static readonly AssemblyName Reference = typeof(BenchmarkAgent).Assembly.GetName();
			private static readonly AssemblyInformationalVersionAttribute Attribute =
				typeof(BenchmarkAgent)
					.Assembly
					.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
					.FirstOrDefault() as AssemblyInformationalVersionAttribute;

			[Keyword]public string Type => Reference.Name;
			[Keyword]public string Version => Attribute?.InformationalVersion ?? Reference.Version.ToString();
			[Keyword]public string Name { get; set; }

			public BenchmarkGit Git { get; set; }
			public BenchmarkLanguage Language { get; set; }
			[Object(Name="os")]
			public BenchmarkOperatingSystem OperatingSystem { get; set; }
		}

		internal class BenchmarkOperatingSystem
		{
			[Keyword]public string Version { get; set; }
			[Keyword]public string Platform { get; set; }
			[Keyword]public string Name { get; set; }
			[Keyword]public string Family { get; set; }
			[Keyword]public string Kernel { get; set; }
		}

		internal class BenchmarkLanguage
		{
			[Keyword] public string Version { get; set; }
			[Keyword]public string DotNetSdkVersion { get; set; }
			public bool HasRyuJit { get; set; }
			[Text]public string JitModules { get; set; }
			[Keyword]public string BuildConfiguration { get; set; }
			public bool HasDebuggerAttached { get; set; }
			[Keyword]public string BenchmarkDotNetVersion { get; set; }
			[Keyword]public string BenchmarkDotNetCaption { get; set; }
		}

		internal class BenchmarkHost
		{
			[Keyword]public string ProcessorName { get; set; }
			public int? PhysicalProcessorCount { get; set; }
			public int? PhysicalCoreCount { get; set; }
			public int? LogicalCoreCount { get; set; }
			[Keyword]public string Architecture { get; set; }
			public bool HasAttachedDebugger { get; set; }
			public string HardwareTimerKind { get; set; }
			public double ChronometerFrequencyHerz { get; set; }

		}

		internal class BenchmarkGit
		{
			[Keyword(Name="branch")] public string BranchName { get; set; }
			[Keyword(Name="sha")] public string Sha { get; set; }
			[Text(Name="commit_message")] public string CommitMessage { get; set; }
			[Keyword(Name="repos")] public string Repository { get; set; }
		}


	}

}
