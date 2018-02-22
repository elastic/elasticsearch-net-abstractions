using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.Configuration;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.XunitPlumbing
{
	public abstract class ElasticTestDiscoverer : IXunitTestCaseDiscoverer
	{
		protected readonly IMessageSink DiagnosticMessageSink;

		protected ElasticTestDiscoverer(IMessageSink diagnosticMessageSink)
		{
			this.DiagnosticMessageSink = diagnosticMessageSink;
		}

		protected virtual bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute) => false;

		public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			return SkipMethod(discoveryOptions, testMethod, factAttribute)
				? Enumerable.Empty<IXunitTestCase>()
				: new[] { new XunitTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };
		}

		protected static TValue GetAttribute<TAttribute, TValue>(ITestMethod testMethod, string propertyName)
		{
			var classAttributes = testMethod.TestClass.Class.GetCustomAttributes(typeof(TAttribute));
			var methodAttributes = testMethod.Method.GetCustomAttributes(typeof(TAttribute));
			var attribute = classAttributes.Concat(methodAttributes).FirstOrDefault();
			return attribute == null ? default(TValue) : attribute.GetNamedArgument<TValue>(propertyName);
		}

		protected static IEnumerable<TValue> GetAttributes<TAttribute, TValue>(ITestMethod testMethod, string propertyName)
		{
			var classAttributes = testMethod.TestClass.Class.GetCustomAttributes(typeof(TAttribute));
			var methodAttributes = testMethod.Method.GetCustomAttributes(typeof(TAttribute));
			return classAttributes
				.Concat(methodAttributes)
				.Select(a => a.GetNamedArgument<TValue>(propertyName));
		}


	}
}
