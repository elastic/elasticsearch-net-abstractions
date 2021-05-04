// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Elasticsearch.Xunit.XunitPlumbing
{
	/// <summary>
	///     Base test discoverer used to discover tests cases attached
	///     to test methods that are attributed with <see cref="T:Xunit.FactAttribute" /> (or a subclass).
	/// </summary>
	public abstract class ElasticTestCaseDiscoverer : IXunitTestCaseDiscoverer
	{
		protected readonly IMessageSink DiagnosticMessageSink;

		protected ElasticTestCaseDiscoverer(IMessageSink diagnosticMessageSink) =>
			DiagnosticMessageSink = diagnosticMessageSink;

		/// <inheritdoc />
		public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo factAttribute) =>
			SkipMethod(discoveryOptions, testMethod, out var skipReason)
				? string.IsNullOrEmpty(skipReason)
					? new IXunitTestCase[] { }
					: new IXunitTestCase[] {new SkippingTestCase(skipReason, testMethod, null)}
				: new[]
				{
					new XunitTestCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod)
				};

		/// <summary>
		///     Detemines whether a test method should be skipped, and the reason why
		/// </summary>
		/// <param name="discoveryOptions">The discovery options</param>
		/// <param name="testMethod">The test method</param>
		/// <param name="skipReason">The reason to skip</param>
		/// <returns></returns>
		protected virtual bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod,
			out string skipReason)
		{
			skipReason = null;
			return false;
		}

		protected static TValue GetAttribute<TAttribute, TValue>(ITestMethod testMethod, string propertyName)
			where TAttribute : Attribute
		{
			var classAttributes = testMethod.TestClass.Class.GetCustomAttributes(typeof(TAttribute)) ??
			                      Enumerable.Empty<IAttributeInfo>();
			var methodAttributes = testMethod.Method.GetCustomAttributes(typeof(TAttribute)) ??
			                       Enumerable.Empty<IAttributeInfo>();
			var attribute = classAttributes.Concat(methodAttributes).FirstOrDefault();
			return attribute == null ? default(TValue) : attribute.GetNamedArgument<TValue>(propertyName);
		}

		protected static IList<IAttributeInfo> GetAttributes<TAttribute>(ITestMethod testMethod)
			where TAttribute : Attribute
		{
			var classAttributes = testMethod.TestClass.Class.GetCustomAttributes(typeof(TAttribute)) ??
			                      Enumerable.Empty<IAttributeInfo>();
			var methodAttributes = testMethod.Method.GetCustomAttributes(typeof(TAttribute)) ??
			                       Enumerable.Empty<IAttributeInfo>();
			return classAttributes.Concat(methodAttributes).ToList();
		}

		protected static IEnumerable<TValue> GetAttributes<TAttribute, TValue>(ITestMethod testMethod,
			string propertyName)
			where TAttribute : Attribute
		{
			var classAttributes = testMethod.TestClass.Class.GetCustomAttributes(typeof(TAttribute));
			var methodAttributes = testMethod.Method.GetCustomAttributes(typeof(TAttribute));
			return classAttributes
				.Concat(methodAttributes)
				.Select(a => a.GetNamedArgument<TValue>(propertyName));
		}
	}
}
