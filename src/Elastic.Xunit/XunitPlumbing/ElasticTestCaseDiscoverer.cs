using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.XunitPlumbing
{
	public abstract class ElasticTestCaseDiscoverer : IXunitTestCaseDiscoverer
	{
		protected readonly IMessageSink DiagnosticMessageSink;

		protected ElasticTestCaseDiscoverer(IMessageSink diagnosticMessageSink)
		{
			this.DiagnosticMessageSink = diagnosticMessageSink;
		}

		protected virtual bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, out string skipReason)
		{
			skipReason = null;
			return false;
		}

		public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			return SkipMethod(discoveryOptions, testMethod, out var skipReason)
				? string.IsNullOrEmpty(skipReason)
					? new IXunitTestCase[] {}
					: new IXunitTestCase[] { new SkippingTestCase(skipReason, testMethod, null)  }
				: new[] { new XunitTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };
		}

		protected static TValue GetAttribute<TAttribute, TValue>(ITestMethod testMethod, string propertyName)
		{
			var classAttributes = testMethod.TestClass.Class.GetCustomAttributes(typeof(TAttribute)) ?? Enumerable.Empty<IAttributeInfo>();
			var methodAttributes = testMethod.Method.GetCustomAttributes(typeof(TAttribute)) ?? Enumerable.Empty<IAttributeInfo>();;
			var attribute = classAttributes.Concat(methodAttributes).FirstOrDefault();
			return attribute == null ? default(TValue) : attribute.GetNamedArgument<TValue>(propertyName);
		}
		protected static IList<IAttributeInfo> GetAttributes<TAttribute>(ITestMethod testMethod)
		{
			var classAttributes = testMethod.TestClass.Class.GetCustomAttributes(typeof(TAttribute)) ?? Enumerable.Empty<IAttributeInfo>();;
			var methodAttributes = testMethod.Method.GetCustomAttributes(typeof(TAttribute)) ?? Enumerable.Empty<IAttributeInfo>();;
			return classAttributes.Concat(methodAttributes).ToList();
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
