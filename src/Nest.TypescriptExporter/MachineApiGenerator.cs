using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TypeLite;
using TypeLite.TsModels;

namespace Nest.TypescriptGenerator
{
	public class MachineApiGenerator : TsGenerator
	{
		private readonly Dictionary<string, string> _namespaceMapping;
		public TypeConvertorCollection Converters => base._typeConvertors;

		public MachineApiGenerator(Dictionary<string, string> namespaceMapping) : base()
		{
			_namespaceMapping = namespaceMapping;
		}

		public Dictionary<string, string> TypeRenames => new Dictionary<string, string>
		{
			{"KeyValuePair", "Map"}
		};

		public HashSet<string> Appended = new HashSet<string>();
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
			

		public bool PropertyTypesToIgnore(Type propertyType)
		{
			return _propertyTypesToIgnore.Contains(propertyType) || (propertyType.BaseType != null && propertyType.BaseType == typeof (MulticastDelegate));
		}

		protected virtual void AppendClassDefinition(TsClass classModel, ScriptBuilder sb, TsGeneratorOutput generatorOutput)
		{
			AddNamespaceHeader(classModel.Name, sb);

			string typeName = this.GetTypeName(classModel);
			string visibility = this.GetTypeVisibility(classModel, typeName) ? "export " : "";

			AddDocCommentForCustomJsonConverter(sb, classModel);
			_docAppender.AppendClassDoc(sb, classModel, typeName);

			sb.AppendFormatIndented("{0}interface {1}", visibility, typeName);
			if (classModel.BaseType != null)
			{
				sb.AppendFormat(" extends {0}", this.GetFullyQualifiedTypeName(classModel.BaseType));
			}

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
					{
						continue;
					}

					AddDocCommentForCustomJsonConverter(sb, property);
					_docAppender.AppendPropertyDoc(sb, property, this.GetPropertyName(property), this.GetPropertyType(property));
					sb.AppendLineIndented($"{this.GetPropertyName(property)}: {this.GetPropertyType(property)};");
				}
			}

			sb.AppendLineIndented("}");

			_generatedClasses.Add(classModel);
		}

		private void AddNamespaceHeader(string name, ScriptBuilder sb)
		{
			string ns;
			sb.AppendLineIndented(this._namespaceMapping.TryGetValue(name, out ns)
				? $"/**namespace:{ns} */"
				: $"/**namespace:DefaultLanguageConstruct */");
		}

		private void AddDocCommentForCustomJsonConverter(ScriptBuilder sb, TsProperty property)
		{
			var declaringType = property.MemberInfo.DeclaringType;
			var propertyName = property.MemberInfo.Name;

			var nonGenericTypeName = Program.RemoveGeneric.Replace(declaringType.Name, "$1");

			if (declaringType.Name.Contains("Request") && Program.RequestParameters.ContainsKey(nonGenericTypeName))
			{
				var rp = Program.RequestParameters[nonGenericTypeName];
				var method = rp.GetMethod(propertyName);
				if (method != null)
					sb.AppendLineIndented("/**ambiguous_origin*/");
			}
			var iface = declaringType.GetInterfaces().FirstOrDefault(ii => ii.Name == "I" + declaringType.Name);
			var ifaceProperty = iface?.GetProperty(propertyName);
		
			var jsonConverterAttribute = ifaceProperty?.GetCustomAttribute<JsonConverterAttribute>() ??
			                             property.MemberInfo.GetCustomAttribute<JsonConverterAttribute>();

			if (jsonConverterAttribute != null)
				sb.AppendLineIndented("/**custom_serialization */");
		}

		private void AddDocCommentForCustomJsonConverter(ScriptBuilder sb, TsClass classModel)
		{
			var iface = classModel.Type.GetInterfaces().FirstOrDefault(i => i.Name == "I" + classModel.Type.Name);

			var jsonConverterAttribute = iface?.GetCustomAttribute<JsonConverterAttribute>() ??
										 classModel.Type.GetCustomAttribute<JsonConverterAttribute>();

			if (jsonConverterAttribute != null)
			{
				sb.AppendLineIndented("/**custom_serialization*/");
			}
		}

		protected override void AppendEnumDefinition(TsEnum enumModel, ScriptBuilder sb, TsGeneratorOutput output)
		{
			AddNamespaceHeader(enumModel.Name, sb);
			if (_typesToIgnore.Contains(enumModel.Type)) return;

			string typeName = this.GetTypeName(enumModel);
			string visibility = string.Empty;

			_docAppender.AppendEnumDoc(sb, enumModel, typeName);

			string constSpecifier = this.GenerateConstEnums ? "const " : string.Empty;
			sb.AppendLineIndented(string.Format("{0}{2}enum {1} {{", visibility, typeName, constSpecifier));

			using (sb.IncreaseIndentation())
			{
				int i = 1;
				foreach (var v in enumModel.Values)
				{
					_docAppender.AppendEnumValueDoc(sb, v);
					var enumMemberAttribute = v.Field.GetCustomAttribute<EnumMemberAttribute>();
					var name = !string.IsNullOrEmpty(enumMemberAttribute?.Value) ? enumMemberAttribute.Value : v.Name;

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
				foreach (var classModel in classes.OrderBy(c => c.Type?.IsInterface ?? false ? 0 : 1))
				{
					var c = ReMapClass(classModel);
					if (Ignore(c)) continue;
					if (this.Appended.Contains(c.Name)) continue;
					if (this.Appended.Contains("I" + c.Name)) continue;
					this.AppendClassDefinition(c, sb, generatorOutput);
					this.Appended.Add(c.Name);
				}
			}

			if ((generatorOutput & TsGeneratorOutput.Constants) == TsGeneratorOutput.Constants)
			{
				foreach (var classModel in classes)
				{
					if (classModel.IsIgnored)
					{
						continue;
					}

					this.AppendConstantModule(classModel, sb);
				}
			}
		}

		protected bool Ignore(TsClass classModel)
		{
			if (this.TypeRenames.ContainsKey(classModel.Name)) return false;
			if (typeof(IRequestParameters).IsAssignableFrom(classModel.Type)) return true;
			if (IsClrType(classModel.Type)) return true;
			if (_typesToIgnore.Contains(classModel.Type)) return true;
			return false;
		}

		protected bool Ignore(TsEnum enumModel) => IsClrType(enumModel.Type);

		protected bool IsClrType(Type type)
		{
			var name = type.FullName ?? type.DeclaringType?.FullName;
			return name != null && !name.StartsWith("Nest.") && !name.StartsWith("Elasticsearch.Net.");
		}

		protected TsClass ReMapClass(TsClass classModel)
		{
			if (typeof(RequestBase<>) == classModel.Type)
				return new TsClass(typeof(Request));

			if (typeof(ResponseBase) == classModel.Type)
				return new TsClass(typeof(Response));

			if (classModel.BaseType != null)
			{
				if (typeof(IRequest).IsAssignableFrom(classModel.BaseType.Type))
					classModel.BaseType = new TsClass(typeof(Request));

				if (typeof(IResponse).IsAssignableFrom(classModel.BaseType.Type))
					classModel.BaseType = new TsClass(typeof(Response));
			}

			return classModel;
		}
	}

	public class Request { }

	public class Response { }

	public class Map<TKey, TValue> { }
}
