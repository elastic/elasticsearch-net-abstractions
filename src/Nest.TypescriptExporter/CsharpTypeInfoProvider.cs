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
		private static readonly Type[] ExposedInterfaces =
		{
			typeof(IRequest), typeof(IResponse), typeof(ICharFilter), typeof(ITokenFilter), typeof(IAnalyzer), typeof(ITokenizer),
			typeof(IDictionaryResponse<,>),
			typeof(IIndicesModuleSettings), typeof(IProperty)
		};

		public CsharpTypeInfoProvider()
		{
			var nestAssembly = typeof(IRequest<>).Assembly;
			var lowLevelAssembly = typeof(IElasticLowLevelClient).Assembly;

			this.ExposedTypes = nestAssembly
				.GetTypes()
				.Where(TypeFilter)
				.OrderBy(TypeOrderer)
				.ToArray();

			this.RequestParameters = lowLevelAssembly
				.GetTypes()
				.Where(t => t.IsClass && t.Name.EndsWith("RequestParameters"))
				.ToDictionary(t=>t.Name.Replace("Parameters", ""));
		}

		public Dictionary<string, Type> RequestParameters { get; }
		public Type[] ExposedTypes { get; }

		private static bool TypeFilter(Type t) =>
			(t.IsEnum && !t.Namespace.StartsWith("Nest.Json") && (t.Namespace.StartsWith("Nest") || t.Namespace.StartsWith("Elasticsearch.Net")))
			|| (ExposedInterfaces.Any(i=> i.IsAssignableFrom(t)) && t.IsClass && !BadClassRegex.IsMatch(t.Name));

		private static int TypeOrderer(Type t)
		{
			var weight = 0;
			if (t.IsClass || t.IsInterface) weight += 100;
			else return weight;
			if (t.Name.StartsWith("Nest")) weight += 50;
			weight += ClientTypescriptGenerator.GetParentTypes(t).Count();
			return weight;
		}
	}
}
