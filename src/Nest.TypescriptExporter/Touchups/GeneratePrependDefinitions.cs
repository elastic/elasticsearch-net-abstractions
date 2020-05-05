// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;

namespace Nest.TypescriptGenerator.Touchups
{
	public static class GeneratePrependDefinitions
	{
		public static void PrependDefinitions(string file)
		{
			var hack = @"
function class_serializer(ns: string) {return function (ns: any){}}
function rest_spec_name(ns: string) {return function (ns: any){}}
function prop_serializer(ns: string) {return function (ns: any, x:any){}}
function request_parameter() {return function (ns: any, x:any){}}
function namespace(ns: string) {return function (ns: any){}}

interface Uri {}
interface Date {}
interface TimeSpan {}
interface SourceDocument {}
";
			var contents = File.ReadAllText(file);
			contents = contents
				.Replace("class Error extends ErrorCause", $"class {nameof(MainError)} extends ErrorCause");

			File.WriteAllText(file, hack);
			File.AppendAllText(file, contents);
		}

	}
}
