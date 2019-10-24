using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Elasticsearch.Net;
using TypeLite;
using TypeLite.TsModels;

namespace Nest.TypescriptGenerator
{
	public class ClientTypesExporter
	{
		private readonly ClientTypescriptGenerator _scriptGenerator;
		private readonly TypeScriptFluent _definitions;
		public static readonly Regex InterfaceRegex = new Regex("(?-i)^I[A-Z].*$");
		public static Regex RemoveGeneric { get; } = new Regex(@"^(.+)(?:\`.+)$");

		public ClientTypesExporter(CsharpTypeInfoProvider typeInfoProvider, ClientTypescriptGenerator scriptGenerator)
		{
			this._scriptGenerator = scriptGenerator;
			var d = TypeScript.Definitions(scriptGenerator)
				.WithTypeFormatter(FormatType)
				.WithMemberFormatter(FormatMember)
				.WithMemberTypeFormatter(FormatMemberType)
				.WithVisibility((@class, name) => false)
				.AsConstEnums(false)
				.For<DateInterval>()
				.For<AnalyzerBase>()
				.WithModuleNameFormatter(module => string.Empty);

			this._definitions = typeInfoProvider.ExposedTypes.Aggregate(d, (def, t) => def.For(t));
		}

		public string Generate() => this._definitions.Generate();

		private static string FormatMemberType(TsProperty tsProperty, string memberTypeName)
		{
			if (memberTypeName == nameof(Error)) return nameof(MainError);

			var asCollection = tsProperty.PropertyType as TsCollection;
			var isCollection = asCollection != null;
			if (typeof(IIsADictionary).IsAssignableFrom(tsProperty.PropertyType.Type)) return memberTypeName;

			return memberTypeName.StartsWith("Dictionary<")
				? memberTypeName
				: memberTypeName + (isCollection ? string.Concat(Enumerable.Repeat("[]", asCollection.Dimension)) : "");
		}

		private static string FormatMember(TsProperty property)
		{
			var declaringType = property.MemberInfo.DeclaringType;
			var propertyName = property.MemberInfo.Name;

			if (declaringType == null) return propertyName.SnakeCase().QuoteMaybe();
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
				return ((string) v ?? propertyName.SnakeCase()).QuoteMaybe();
			}
			return propertyName.SnakeCase().QuoteMaybe();
		}

		private string FormatType(TsType type, ITsTypeFormatter formatter) => GenerateTypeName(type);

		private string GenerateTypeName(TsType type)
		{
			var tsClass = (TsClass)type;

			var name = ClientTypescriptGenerator.TypeRenames.ContainsKey(tsClass.Name)
				? ClientTypescriptGenerator.TypeRenames[tsClass.Name]
				: tsClass.Name;

			if (type.Type == typeof(HistogramOrder)) return "HistogramOrder";

			if (InterfaceRegex.IsMatch(name) && !CsharpTypeInfoProvider.ExposedInterfaces.Contains(type.Type)) name = name.Substring(1);

			if (!tsClass.GenericArguments.Any()) return name;

			return name + "<" + string.Join(", ", tsClass.GenericArguments.Select(WriteArrayIfCollection)) + ">";
		}

		private string WriteArrayIfCollection(TsType a)
		{
			var fullyQualifiedTypeName = _scriptGenerator.GetFullyQualifiedTypeName(a);
			if (typeof(IIsADictionary).IsAssignableFrom(a.Type)) return fullyQualifiedTypeName;
			if (a is TsCollection)
			{

			}

			return a is TsCollection && !fullyQualifiedTypeName.StartsWith("Dictionary<")
				? fullyQualifiedTypeName + "[]"
				: fullyQualifiedTypeName;
		}
	}
}
