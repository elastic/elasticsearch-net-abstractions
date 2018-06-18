using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TypeLite;
using TypeLite.TsModels;
using Elasticsearch.Net;

namespace Nest.TypescriptGenerator
{
	public class Program
	{
		public static Dictionary<string, Type> RequestParameters { get; set; }
		private static MachineApiGenerator _scriptGenerator;
		public static readonly Regex InterfaceRegex = new Regex("(?-i)^I[A-Z].*$");
		public static Regex RemoveGeneric { get; } = new Regex(@"^(.+)(?:\`.+)$");

		private static string NestFolder = @"..\..\..\net-master\src\Nest";

		private static readonly string[] SkipFolders = { "_Generated", "Debug", "Release" };
		public static IEnumerable<References> InputFiles() =>
			from f in Directory.GetFiles(NestFolder, $"*.cs", SearchOption.AllDirectories)
			let dir = new DirectoryInfo(f)
			where dir?.Parent != null && !SkipFolders.Contains(dir.Parent.Name)
			select new References(new FileInfo(f));

		public static void Main(string[] args)
		{
			var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

			var references = InputFiles()
				.SelectMany(r => r.Declarations, (r, s) => new { r, s });

			var dictionary = new Dictionary<string, string>();
			foreach(var r in references)
			{
				if (dictionary.ContainsKey(r.s) && (r.s.EndsWith("Request") || r.s.EndsWith("Descriptor")) )
				{
				}
				else if (dictionary.ContainsKey(r.s))
				{
				}
				else
				{
					dictionary.Add(r.s, r.r.Namespace);
				}
			}

			var isBadClassRe = new Regex(@"(SynchronizedCollection|Descriptor|Attribute)(?:Base)?(?:\`.+$|$)");

			var nestAssembly = typeof(IRequest<>).Assembly;
			var lowLevelAssembly = typeof(IElasticLowLevelClient).Assembly;

			var exposeInterfaces = new[]
			{
				typeof(IRequest), typeof(IResponse), typeof(ICharFilter), typeof(ITokenFilter), typeof(IAnalyzer), typeof(ITokenizer),
				typeof(IDictionaryResponse<,>),
				typeof(IIndicesModuleSettings), typeof(IProperty)
			};

			var nestInterfaces = nestAssembly
				.GetTypes()
				.Where(t =>
					(t.IsEnum && !t.Namespace.StartsWith("Nest.Json") && (t.Namespace.StartsWith("Nest") || t.Namespace.StartsWith("Elasticsearch.Net")))
					|| (exposeInterfaces.Any(i=> i.IsAssignableFrom(t)) && t.IsClass && !isBadClassRe.IsMatch(t.Name))
				)
				.OrderBy(t =>
				{
					var weight = 0;
					if (t.IsClass || t.IsInterface) weight += 100;
					else return weight;
					if (t.Name.StartsWith("Nest")) weight += 50;
					weight += MachineApiGenerator.GetParentTypes(t).Count();
					return weight;
				})
				.ToArray();

			RequestParameters = lowLevelAssembly
				.GetTypes()
				.Where(t => t.IsClass && t.Name.EndsWith("RequestParameters"))
				.ToDictionary(t=>t.Name.Replace("Parameters", ""));

			var x = dictionary["DynamicMapping"];

			_scriptGenerator = new MachineApiGenerator(dictionary);

			var typeScriptFluent = TypeScript.Definitions(_scriptGenerator)
				.WithTypeFormatter(FormatType)
				.WithMemberFormatter(FormatMember)
				.WithMemberTypeFormatter(FormatMemberType)
				.WithVisibility((@class, name) => false)
				.AsConstEnums(false)
				.WithModuleNameFormatter(module => string.Empty);

			//typeScriptFluent.For(typeof (Map<,>));
			typeScriptFluent.For<DateInterval>();
			var definitions = nestInterfaces.Aggregate(typeScriptFluent, (def, t) => def.For(t));

			var file = "typedefinitions.ts";
			File.WriteAllText(file, definitions.Generate());
			LineBasedHacks(file);
			PrependDefinitions(file);
		}

		private static void LineBasedHacks(string file)
		{
			var lines = File.ReadAllLines(file);
			var newLines = new List<string>();
			var skipTillNextBracket = false;
			var singleOrArray = false;
			foreach (var l in lines)
			{
				if (l.Contains("ReadSingleOrEnumerable"))
				{
					singleOrArray = true;
					continue;
				}

				if (singleOrArray)
				{
					var ll = Regex.Replace(l, @"^(.+?): (.+?)\[\];", "$1: $2 | $2[];");
					newLines.Add(ll);
					singleOrArray = false;
					continue;
				}

				if (l == "}" && skipTillNextBracket)
				{
					skipTillNextBracket = false;
					continue;
				}

				if (l.StartsWith("class ErrorCause ")
					|| l.StartsWith("class Error extends"))
				{
					newLines.RemoveAt(newLines.Count -1);
					skipTillNextBracket = true;
				}
				if (skipTillNextBracket) continue;

				newLines.Add(l);
			}
			File.WriteAllLines(file, newLines);
		}

		private static void PrependDefinitions(string file)
		{
			var errorCauseDef = @"@namespace(""common"")
class ErrorCause {
	reason: string;
	type: string;
	caused_by: ErrorCause;
	stack_trace: string;
	metadata: ErrorCauseMetadata;
}
";
			var errorDef = @"@namespace(""common"")
class Error extends ErrorCause {
	root_cause: ErrorCause[];
	headers: Map<string, string>;
}
";
			var hack = @"
function class_serializer(ns: string) {return function (ns: any){}}
function prop_serializer(ns: string) {return function (ns: any, x:any){}}
function request_parameter() {return function (ns: any, x:any){}}
function namespace(ns: string) {return function (ns: any){}}

interface Uri {}
interface Date {}
interface TimeSpan {}
interface SourceDocument {}
";
			hack += errorCauseDef;
			hack += errorDef;
			var contents = File.ReadAllText(file);
			contents = contents
				.Replace(errorCauseDef, "")
				.Replace(errorDef, "")
				.Replace("\thits: Hit<T>[];", "\t//hits: Hit<T>[];");
			File.WriteAllText(file, hack);
			File.AppendAllText(file, contents);
		}

		private static string FormatMemberType(TsProperty tsProperty, string memberTypeName)
		{
			var asCollection = tsProperty.PropertyType as TsCollection;
			var isCollection = asCollection != null;

			return memberTypeName.StartsWith("Map<")
				? memberTypeName
				: memberTypeName + (isCollection ? string.Concat(Enumerable.Repeat("[]", asCollection.Dimension)) : "");
		}

		private static string FormatMember(TsProperty property)
		{
			var declaringType = property.MemberInfo.DeclaringType;
			var propertyName = property.MemberInfo.Name;

			if (declaringType == null) return QuoteMaybe(SnakeCase(propertyName));
			var iface = declaringType.GetInterfaces().FirstOrDefault(ii => ii.Name == "I" + declaringType.Name);
			var ifaceProperty = iface?.GetProperty(propertyName);

			var attributes = new List<Attribute>();
			if (ifaceProperty != null) attributes.AddRange(ifaceProperty.GetCustomAttributes());
			attributes.AddRange(property.MemberInfo.GetCustomAttributes());
			if (attributes.Any(a => a.TypeId.ToString() == "Nest.Json.JsonIgnoreAttribute"))
				property.IsIgnored = true;

			var jsonPropertyAttribute = attributes.FirstOrDefault(a => a.TypeId.ToString() == "Nest.Json.JsonPropertyAttribute");
			if (jsonPropertyAttribute != null)
			{
				var v = jsonPropertyAttribute.GetType().GetProperty("PropertyName").GetGetMethod().Invoke(jsonPropertyAttribute, new object[] {});
				return QuoteMaybe((string) v ?? SnakeCase(propertyName));
			}
			return QuoteMaybe(SnakeCase(propertyName));
		}

		public static string QuoteMaybe(string propertyName)
		{
			//this is dumb on purpose
			if (propertyName.Contains('-')) return $"\'{propertyName}\'";
			if (propertyName.Contains('+')) return $"\'{propertyName}\'";
			if (Regex.IsMatch(propertyName, @"^\d")) return $"\'{propertyName}\'";
			return propertyName;

		}

		private static Regex SnakeCaseRe = new Regex("(?<=.)([A-Z])");
		public static string SnakeCase(string token) => SnakeCaseRe.Replace(token, "_$0").ToLowerInvariant();

		private static string FormatType(TsType type, ITsTypeFormatter formatter)
		{
			return GenerateTypeName(type);
		}

		private static string GenerateTypeName(TsType type)
		{
			var tsClass = (TsClass)type;

			var name = _scriptGenerator.TypeRenames.ContainsKey(tsClass.Name)
				? _scriptGenerator.TypeRenames[tsClass.Name]
				: tsClass.Name;

			if (InterfaceRegex.IsMatch(name)) name = name.Substring(1);

			if (!tsClass.GenericArguments.Any()) return name;

			return name + "<" + string.Join(", ", tsClass.GenericArguments.Select(WriteArrayIfCollection)) + ">";
		}

		private static string WriteArrayIfCollection(TsType a)
		{
			var fullyQualifiedTypeName = _scriptGenerator.GetFullyQualifiedTypeName(a);

			return a is TsCollection && !fullyQualifiedTypeName.StartsWith("Map<")
				? fullyQualifiedTypeName + "[]"
				: fullyQualifiedTypeName;
		}
	}
}
