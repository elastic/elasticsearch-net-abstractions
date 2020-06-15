// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Elasticsearch.Net;

namespace Nest.TypescriptGenerator
{
	public class CsharpTypeInfoProvider
	{
		private static readonly Regex BadClassRegex = new Regex(@"(SynchronizedCollection|Descriptor|Attribute)(?:Base)?(?:\`.+$|$)");

		// can not add typeof(IAggregate), because quite a few of these behave as an ICollection which trips TSLite.
		//need to manually port these.

		public static readonly Type[] ExposedInterfaces =
		{
			typeof(IRequest), typeof(IResponse), typeof(ICharFilter), typeof(ITokenFilter), typeof(IAnalyzer), typeof(ITokenizer),
			typeof(ICatRecord), typeof(IProperty)
		};

		public static readonly Type[] StringAbstractions = new[]
		{
			typeof(DateMath), typeof(Indices), typeof(IndexName), typeof(RelationName), typeof(Id), typeof(Names), typeof(Name), typeof(NodeIds),
			typeof(Metrics), typeof(IndexMetrics), typeof(Field), typeof(Fields), typeof(PropertyName), typeof(Routing), typeof(TaskId),
			typeof(DateMathExpression), typeof(IDateMath)
		};

		private static readonly Type[] ExposedInterfacesImplementations = ExposedInterfaces.Concat(new []
		{
			typeof(IDictionaryResponse<,>), typeof(IIndicesModuleSettings)
		}).ToArray();

		public CsharpTypeInfoProvider()
		{
			var nestAssembly = typeof(IRequest<>).Assembly;
			var lowLevelAssembly = typeof(IElasticLowLevelClient).Assembly;

			ExposedTypes = nestAssembly
				.GetTypes()
				.Where(TypeFilter)
				.Concat(ExposedInterfacesImplementations.Where(t=>!t.IsGenericType))
				.ToArray();

			var requestParams = lowLevelAssembly
				.GetTypes()
				.Where(t => t.IsClass && t.Name.EndsWith("RequestParameters"))
				.Where(t => t.Namespace != "Elasticsearch.Net.Specification.SlmApi")
				.ToList();

			var doubles = requestParams
				.GroupBy(t => t.Name.Replace("Parameters", ""))
				.Where(g => g.Count() > 1)
				.ToList();


			RequestParameters = requestParams
				.ToDictionary(t => t.Name.Replace("Parameters", ""));
		}

		public Dictionary<string, Type> RequestParameters { get; }
		public Type[] ExposedTypes { get; }

		private static bool TypeFilter(Type t) => TypeFilter(t, ExposedInterfacesImplementations);

		private static bool TypeFilter(Type t, IEnumerable<Type> interfaces) =>
			(t.IsEnum && !t.Namespace.StartsWith("Elasticsearch.Net.Utf8Json") && (t.Namespace.StartsWith("Nest") || t.Namespace.StartsWith("Elasticsearch.Net")))
			|| (interfaces.Any(i=> i.IsAssignableFrom(t)) && t.IsClass && !BadClassRegex.IsMatch(t.Name));
	}
}
