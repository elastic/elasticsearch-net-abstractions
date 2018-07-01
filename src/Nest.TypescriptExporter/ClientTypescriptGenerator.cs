using System.Collections.Generic;
using Elasticsearch.Net;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using TypeLite;
using TypeLite.TsModels;

namespace Nest.TypescriptGenerator
{
	public class ClientTypescriptGenerator : TsGenerator
	{
		private readonly CsharpTypeInfoProvider _typeInfoProvider;
		private readonly CSharpSourceDirectory _sourceDirectory;
		private readonly RestSpec _restSpec;

		public ClientTypescriptGenerator(CsharpTypeInfoProvider typeInfoProvider, CSharpSourceDirectory sourceDirectory, RestSpec restSpec)
		{
			_typeInfoProvider = typeInfoProvider;
			_sourceDirectory = sourceDirectory;
			_restSpec = restSpec;
		}

		public static Dictionary<string, string> TypeRenames => new Dictionary<string, string>
		{
			{"KeyValuePair", "Map"}
		};

		private readonly HashSet<string> _appended = new HashSet<string>();
		private readonly HashSet<Type> _propertyTypesToIgnore = new HashSet<Type>(new[]
			{
				typeof (PropertyInfo),
				typeof (Expression),
				typeof (Type),
				typeof (Exception),
				typeof (IApiCallDetails),
				typeof (Node)
			});

		private readonly HashSet<Type> _typesToIgnore = new HashSet<Type>(new[]
			{
				typeof (IApiCallDetails),
				typeof (Node),
				typeof (Audit),
				typeof (AuditEvent)
			});

		private readonly Dictionary<Type, string[]> _typesPropertiesToIgnore = new Dictionary<Type, string[]>
		{
			{ typeof(IResponse), new [] { nameof(IResponse.IsValid), nameof(IResponse.DebugInformation) } }
		};


		private bool PropertyTypesToIgnore(Type propertyType)
		{
			return _propertyTypesToIgnore.Contains(propertyType) || (propertyType.BaseType != null && propertyType.BaseType == typeof (MulticastDelegate));
		}

		protected override void AppendClassDefinition(TsClass classModel, ScriptBuilder sb, TsGeneratorOutput generatorOutput)
		{
			AddNamespaceHeader(classModel.Name, sb);

			var typeName = this.GetTypeName(classModel);
			var visibility = this.GetTypeVisibility(classModel, typeName) ? "export " : "";

			AddRequestRenameInformation(sb, classModel);

			AddDocCommentForCustomJsonConverter(sb, classModel);
			_docAppender.AppendClassDoc(sb, classModel, typeName);

			sb.AppendFormatIndented("{0}class {1}", visibility, typeName);
			if (classModel.BaseType != null)
				sb.AppendFormat(" extends {0}", this.GetFullyQualifiedTypeName(classModel.BaseType));

			if (classModel.Interfaces.Count > 0)
			{
				var implementations = classModel.Interfaces.Select(GetFullyQualifiedTypeName).ToArray();

				var prefixFormat = classModel.Type.IsInterface ? " extends {0}"
					: classModel.BaseType != null ? " , {0}"
					: " extends {0}";

				sb.AppendFormat(prefixFormat, string.Join(" ,", implementations));
			}

			sb.AppendLine(" {");

			var members = new List<TsProperty>();
			if ((generatorOutput & TsGeneratorOutput.Properties) == TsGeneratorOutput.Properties)
			{
				members.AddRange(classModel.Properties);
			}
			if ((generatorOutput & TsGeneratorOutput.Fields) == TsGeneratorOutput.Fields)
			{
				members.AddRange(classModel.Fields);
			}
			using (sb.IncreaseIndentation())
			{
				foreach (var property in members)
				{
					if (property.IsIgnored ||
						PropertyTypesToIgnore(property.PropertyType.Type) ||
						(_typesPropertiesToIgnore.ContainsKey(classModel.Type) && _typesPropertiesToIgnore[classModel.Type].Contains(property.Name)))
						continue;

					AddDocCommentForCustomJsonConverter(sb, property);
					_docAppender.AppendPropertyDoc(sb, property, this.GetPropertyName(property), this.GetPropertyType(property));
					sb.AppendLineIndented($"{this.GetPropertyName(property)}: {this.GetPropertyType(property)};");
				}
			}

			sb.AppendLineIndented("}");
			_generatedClasses.Add(classModel);
		}
		private bool AddNamespaceHeaderEnum(string name, string ns, ScriptBuilder sb)
		{
			if (!ns.StartsWith("Nest") && !ns.StartsWith("Elasticsearch.Net")) return false;

			var n = "common";
			if (this._sourceDirectory.TypeNameToNamespaceMapping.TryGetValue(name, out var fullNs))
				n = string.Join('.', fullNs.Split(".").Select(StringExtensions.SnakeCase));

			sb.AppendLineIndented($"/** namespace:{n} **/");
			return true;
		}

		private void AddNamespaceHeader(string name, ScriptBuilder sb)
		{
			var n = "common";
			if (this._sourceDirectory.TypeNameToNamespaceMapping.TryGetValue(name, out var ns))
				n = string.Join('.', ns.Split(".").Select(StringExtensions.SnakeCase));

			sb.AppendLineIndented($"@namespace(\"{n}\")");
		}

		private void AddDocCommentForCustomJsonConverter(ScriptBuilder sb, TsProperty property)
		{
			var declaringType = property.MemberInfo.DeclaringType;
			var propertyName = property.MemberInfo.Name;

			var iface = declaringType.GetInterfaces().FirstOrDefault(ii => ii.Name == "I" + declaringType.Name);
			var ifaceProperty = iface?.GetProperty(propertyName);

			var attributes = new List<Attribute>();
			if (ifaceProperty != null) attributes.AddRange(ifaceProperty.GetCustomAttributes());
			attributes.AddRange(property.MemberInfo.GetCustomAttributes());

			var isRequest = declaringType.Name.Contains("Request");
			var nonGenericTypeName = ClientTypesExporter.RemoveGeneric.Replace(declaringType.Name, "$1");
			if (ClientTypesExporter.InterfaceRegex.IsMatch(nonGenericTypeName)) nonGenericTypeName = nonGenericTypeName.Substring(1);

			if (isRequest && this._typeInfoProvider.RequestParameters.ContainsKey(nonGenericTypeName))
			{
				var rp = this._typeInfoProvider.RequestParameters[nonGenericTypeName];
				var prop = rp.GetProperty(propertyName);
				if (prop != null)
					sb.AppendLineIndented("@request_parameter()");
			}

			var converter = attributes.FirstOrDefault(a => a.TypeId.ToString() == "Nest.Json.JsonConverterAttribute");
			if (converter != null)
			{
				if (GetConverter(converter, out var type)) return;
				sb.AppendLineIndented($"@prop_serializer(\"{type.Name}\")");
			}
		}

		private void AddRequestRenameInformation(ScriptBuilder sb, TsClass classModel)
		{
			if (this._restSpec.SkipRequest(classModel.Name)) return;

			var i = classModel.Name;
			if (!ClientTypesExporter.InterfaceRegex.IsMatch(i)) i = $"I{i}";
			if (!this._restSpec.Requests.TryGetValue(i, out var mapping))
			{
				throw new Exception($"Could not get {i} original rest spec file name");
			}

			var originalSpec = Path.GetFileNameWithoutExtension(mapping.Json.Name);
			sb.AppendLineIndented($"@rest_spec_name(\"{originalSpec}\")");
		}

		private static IEnumerable<Attribute> GetDescriptorAttributes(Type requestInterface)
		{
			var descriptorName = Regex.Replace(requestInterface.Name, "^I(.+)Request$", "$1Descriptor");
			var type = requestInterface.Assembly.GetType($"Nest.{descriptorName}");
			return type?.GetCustomAttributes() ?? Enumerable.Empty<Attribute>();
		}

		private static void AddDocCommentForCustomJsonConverter(ScriptBuilder sb, TsClass classModel)
		{
			var iface = classModel.Type.GetInterfaces().FirstOrDefault(i => i.Name == "I" + classModel.Type.Name);

			var attributes = new List<Attribute>();
			if (iface != null) attributes.AddRange(iface.GetCustomAttributes());
			attributes.AddRange(classModel.Type.GetCustomAttributes());

			var converter = attributes.FirstOrDefault(a => a.TypeId.ToString() == "Nest.Json.JsonConverterAttribute");
			if (converter != null)
			{
				if (GetConverter(converter, out var type)) return;
				sb.AppendLineIndented($"@class_serializer(\"{type.Name}\")");
			}
		}

		private static string GetDescriptorFor(Attribute attribute, string classModelName)
		{
			if (attribute == null) return classModelName.SnakeCase().Replace("_","");
			return classModelName;
		}

		private static bool GetConverter(Attribute converter, out Type type)
		{
			type = (Type) converter.GetType().GetProperty("ConverterType").GetGetMethod().Invoke(converter, new object[] { });
			if (type.Name.StartsWith("ReadAsTypeJsonConverter")) return true;
			if (type.Name.StartsWith("VerbatimDictionary")) return true;
			if (type.Name.Contains("DictionaryResponse")) return true;
			if (type.Name.StartsWith("StringEnum")) return true;
			if (type.Name.StartsWith("Reserialize")) return true;
			return false;
		}

		protected override void AppendEnumDefinition(TsEnum enumModel, ScriptBuilder sb, TsGeneratorOutput output)
		{
			if (!AddNamespaceHeaderEnum(enumModel.Name, enumModel.Type.Assembly.FullName, sb)) return;
			if (_typesToIgnore.Contains(enumModel.Type)) return;

			var typeName = this.GetTypeName(enumModel);
			var visibility = string.Empty;

			_docAppender.AppendEnumDoc(sb, enumModel, typeName);

			var constSpecifier = this.GenerateConstEnums ? "const " : string.Empty;
			sb.AppendLineIndented(string.Format("{0}{2}enum {1} {{", visibility, typeName, constSpecifier));

			using (sb.IncreaseIndentation())
			{
				var i = 1;
				foreach (var v in enumModel.Values)
				{
					_docAppender.AppendEnumValueDoc(sb, v);
					var enumMemberAttribute = v.Field.GetCustomAttribute<EnumMemberAttribute>();
					var name = (!string.IsNullOrEmpty(enumMemberAttribute?.Value) ? enumMemberAttribute.Value : v.Name).QuoteMaybe();

					sb.AppendLineIndented(string.Format(i < enumModel.Values.Count ? "{0} = {1}," : "{0} = {1}", name, v.Value));
					i++;
				}
			}

			sb.AppendLineIndented("}");

			_generatedEnums.Add(enumModel);
		}

		protected override void AppendModule(TsModule module, ScriptBuilder sb, TsGeneratorOutput generatorOutput)
		{
			var classes = module.Classes.Where(c => !_typeConvertors.IsConvertorRegistered(c.Type) && !c.IsIgnored).ToList();
			var enums = module.Enums.Where(e => !_typeConvertors.IsConvertorRegistered(e.Type) && !e.IsIgnored).ToList();
			if ((generatorOutput == TsGeneratorOutput.Enums && enums.Count == 0) ||
				(generatorOutput == TsGeneratorOutput.Properties && classes.Count == 0) ||
				(enums.Count == 0 && classes.Count == 0))
			{
				return;
			}

			if ((generatorOutput & TsGeneratorOutput.Enums) == TsGeneratorOutput.Enums)
			{
				foreach (var enumModel in enums)
				{
					if (Ignore(enumModel)) continue;
					this.AppendEnumDefinition(enumModel, sb, generatorOutput);
				}
			}

			if (((generatorOutput & TsGeneratorOutput.Properties) == TsGeneratorOutput.Properties)
				|| (generatorOutput & TsGeneratorOutput.Fields) == TsGeneratorOutput.Fields)
			{
				var cc = classes.Select(c => new {c, order = OrderTypes(c)}).ToList();

				var orderedClasses = cc.OrderBy(c=>c.order).Select(c=>c.c).ToList();
				var temp = cc.OrderBy(c=>c.order).Select(c => $"{c.order} - {c.c.Type.FullName}").ToList();
				foreach (var classModel in orderedClasses)
				{
					var c = ReMapClass(classModel);
					if (Ignore(c)) continue;
					if (this._appended.Contains(c.Name)) continue;
					if (this._appended.Contains("I" + c.Name)) continue;
					this.AppendClassDefinition(c, sb, generatorOutput);
					this._appended.Add(c.Name);
				}
			}

			if ((generatorOutput & TsGeneratorOutput.Constants) == TsGeneratorOutput.Constants)
			{
				foreach (var classModel in classes)
				{
					if (classModel.IsIgnored) continue;

					this.AppendConstantModule(classModel, sb);
				}
			}
		}
		public static IEnumerable<Type> GetParentTypes(Type type)
		{
			// is there any base type?
			if (type == null)
			{
				yield break;
			}

			// return all implemented or inherited interfaces
			foreach (var i in type.GetInterfaces())
			{
				yield return i;
			}

			// return all inherited types
			var currentBaseType = type.BaseType;
			while (currentBaseType != null)
			{
				yield return currentBaseType;
				currentBaseType= currentBaseType.BaseType;
			}
		}

		private static int OrderTypes(TsClass c)
		{
			var weigth = 0;
			if (c.Type.Namespace.StartsWith("Nest")) weigth += 100;
			if (!c.Type.IsInterface) weigth += 50;

			var baseTypesCount = GetParentTypes(c.Type).Count();
			weigth += baseTypesCount;

			return weigth;
		}

		protected bool Ignore(TsClass classModel)
		{
			if (TypeRenames.ContainsKey(classModel.Name)) return false;
			if (typeof(IRequestParameters).IsAssignableFrom(classModel.Type)) return true;
			if (IsClrType(classModel.Type)) return true;
			if (_typesToIgnore.Contains(classModel.Type)) return true;
			return false;
		}

		private bool Ignore(TsEnum enumModel) => IsClrType(enumModel.Type);

		private bool IsClrType(Type type)
		{
			var name = type.FullName ?? type.DeclaringType?.FullName;
			return name != null && !name.StartsWith("Nest.") && !name.StartsWith("Elasticsearch.Net.");
		}

		private static TsClass ReMapClass(TsClass classModel)
		{
			if (typeof(RequestBase<>) == classModel.Type) return new TsClass(typeof(RequestBase));

			if (classModel.BaseType == null) return classModel;

			var baseType = classModel.BaseType.Type;
			if (typeof(IRequest).IsAssignableFrom(baseType))
				classModel.BaseType = new TsClass(typeof(RequestBase));

			if (typeof(IResponse).IsAssignableFrom(baseType))
			{
				if (baseType.IsGenericType)
				{
					var t = baseType.GetGenericTypeDefinition();
					if (t == typeof(DictionaryResponseBase<,>)) return classModel;
				}

				if (baseType == typeof(ShardsOperationResponseBase)) return classModel;
				if (baseType == typeof(AcknowledgedResponseBase)) return classModel;
				if (baseType == typeof(IndicesResponseBase)) return classModel;

				classModel.BaseType = new TsClass(typeof(ResponseBase));
			}

			return classModel;
		}
	}
	public class RequestBase { }

	public class MainError { }
}
