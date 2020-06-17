// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ShellProgressBar;

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
			using (var pbar = new ProgressBar(2, "reading NEST's REST API spec folder"))
			{

				RestSpecificationFolder = Path.GetFullPath(Path.Combine(nestSourceFolder, "..", "ApiGenerator", "RestSpecification"));

				var jsonFiles = Directory.GetFiles(RestSpecificationFolder, $"*.json", SearchOption.AllDirectories)
					.Where(f => !f.EndsWith(".patch.json") && !f.EndsWith("_common.json"))
					.Select(f => new FileInfo(f))
					.ToList();

				SpecificationFiles = jsonFiles.ToDictionary(f => Path.GetFileNameWithoutExtension(f.Name), f => f);
				pbar.Tick("read all json files");

				Requests = Directory.GetFiles(nestSourceFolder, $"*Request.cs", SearchOption.AllDirectories)
					.Select(f => new FileInfo(f))
					.Select(CreateMapping)
					.Where(m => m != null)
					.ToDictionary(m => m.TypeName);

				pbar.Tick("mapped all json files to their *Request.cs counterparts in NEST's codebase");

				var requestFileNames = Requests.Values.Select(v => v.Json.Name).ToList();
				var notFound = SpecificationFiles.Values
					.Select(v => v.Name)
					.ToList()
					.Except(requestFileNames)
					.Except(_ignoredApis)
					.ToList();
			}

		}

		public Dictionary<string, RestSpecMapping> Requests { get; }

		private readonly Regex _mapsApiRe = new Regex(@"MapsApi\(""(?<descriptor>.+?)(?:\.json)?""");
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
			if (SkipRequestImplementation(typeName)) return null;

			var specFileName = typeName.Replace("Request", "");
			if (!_badDescriptorFors.Contains(typeName))
				specFileName = FindDescriptorForRemapping(file, specFileName);

			specFileName = specFileName.SnakeCase().Replace("_", ".").Replace("async.search", "async_search");
			do
			{
				if (SpecificationFiles.TryGetValue(specFileName, out var f))
					return new RestSpecMapping {TypeName = $"I{typeName}", Json = f};
			}
			while (TryGetSpecTarget(specFileName, out specFileName));

			throw new Exception($"{specFileName}: {typeName} is not a known request in {RestSpecificationFolder}");
		}

		public bool SkipRequestImplementation(string typeName)
		{
			if (typeName == "IRequest") return true;
			if (typeName == "ICovariantSearchRequest") return true;
			if (typeName == "ITypedSearchRequest") return true;
			if (typeName == "IProxyRequest") return true;
			if (typeName == "ISqlRequest") return true;
			if (typeName == "IUpgradeRequest") return true;
			if (typeName == "IUpgradeStatusRequest") return true;
			if (ClientTypesExporter.InterfaceRegex.IsMatch(typeName)) typeName = typeName.Substring(1);
			return !typeName.EndsWith("Request")
			       || _helperRequests.Contains(typeName)
			       || _notARequest.Contains(typeName);
		}

		private string FindDescriptorForRemapping(FileInfo file, string specFileName)
		{
			foreach (var l in File.ReadAllLines(file.FullName))
			{
				var matches = _mapsApiRe.Match(l);
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
