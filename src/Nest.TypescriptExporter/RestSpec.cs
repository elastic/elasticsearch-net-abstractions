using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nest.TypescriptGenerator
{
	public class RestSpec
	{
		public Dictionary<string, FileInfo> SpecificationFiles { get; }
		private string RestSpecificationFolder { get; }

		public class RestSpecMapping
		{
			public FileInfo Json { get; set; }
			public string TypeName { get; set; }
		}

		public RestSpec(string nestSourceFolder)
		{
			this.RestSpecificationFolder = Path.GetFullPath(Path.Combine(nestSourceFolder, "..", "CodeGeneration", "ApiGenerator", "RestSpecification"));

			var jsonFiles = Directory.GetFiles(this.RestSpecificationFolder, $"*.json", SearchOption.AllDirectories)
				.Where(f=>!f.EndsWith(".patch.json") && !f.EndsWith("_common.json"))
				.Select(f => new FileInfo(f))
				.ToList();

			this.SpecificationFiles = jsonFiles
				.ToDictionary(f=>Path.GetFileNameWithoutExtension(f.Name), f=>f);

			this.Requests = Directory.GetFiles(nestSourceFolder, $"*Request.cs", SearchOption.AllDirectories)
				.Select(f => new FileInfo(f))
				.Select(CreateMapping)
				.Where(m=>m != null)
				.ToDictionary(m=> m.TypeName);

			var requestFileNames = this.Requests.Values.Select(v => v.Json.Name).ToList();
			var notFound = this.SpecificationFiles.Values
				.Select(v => v.Name)
				.ToList()
				.Except(requestFileNames)
				.Except(_ignoredApis)
				.ToList();

			//TODO expose and report on notFound
		}

		public Dictionary<string, RestSpecMapping> Requests { get; }

		private readonly Regex _descriptorRe = new Regex(@"DescriptorFor\(""(?<descriptor>.+?)""");
		private readonly string[] _ignoredApis =
		{
			//not mapped un purpose in NEST
			"xpack.ml.delete_filter.json",
			"xpack.ml.get_filters.json",
			"xpack.ml.put_filter.json",
			"xpack.monitoring.bulk.json",
			"xpack.ml.get_buckets.replace.json",
			// not mapped yet
			"rank_eval.json"
		};
		private readonly string[] _helperRequests =
		{
			"BulkAllRequest", "ScrollAllRequest",
		};

		private readonly string[] _notARequest = {"HttpInputRequest", "SearchInputRequest"};
		private readonly string[] _badDescriptorFors =
		{
			"DeleteScriptRequest", "GetScriptRequest", "PutScriptRequest", "SnapshotStatusRequest"
		};
		private RestSpecMapping CreateMapping(FileInfo file)
		{
			var typeName = Path.GetFileNameWithoutExtension(file.Name);
			if (SkipRequest(typeName)) return null;

			var specFileName = typeName.Replace("Request", "");
			if (!_badDescriptorFors.Contains(typeName))
				specFileName = FindDescriptorForRemapping(file, specFileName);

			specFileName = specFileName.SnakeCase().Replace("_", ".");
			do
			{
				if (this.SpecificationFiles.TryGetValue(specFileName, out var f))
					return new RestSpecMapping {TypeName = $"I{typeName}", Json = f};
			}
			while (TryGetSpecTarget(specFileName, out specFileName));


			throw new Exception($"{typeName} is not a known request in {this.RestSpecificationFolder}");
		}

		public bool SkipRequest(string typeName)
		{
			if (ClientTypesExporter.InterfaceRegex.IsMatch(typeName)) typeName = typeName.Substring(1);
			return !typeName.EndsWith("Request")
			       || _helperRequests.Contains(typeName)
			       || _notARequest.Contains(typeName);
		}

		private string FindDescriptorForRemapping(FileInfo file, string specFileName)
		{
			foreach (var l in File.ReadAllLines(file.FullName))
			{
				var matches = _descriptorRe.Match(l);
				if (!matches.Success) continue;
				specFileName = matches.Groups["descriptor"].Value;
				break;
			}

			return specFileName;
		}

		private bool TryGetSpecTarget(string specFileName, out string newSpecFileName)
		{
			newSpecFileName = null;
			var i = specFileName.LastIndexOf('.');
			if (i < 0) return false;
			newSpecFileName = specFileName.Remove(i, 1).Insert(i, "_");
			return true;

		}
	}
}
