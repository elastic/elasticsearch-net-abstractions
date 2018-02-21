using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.Configuration;
using Xunit.Abstractions;

namespace Elastic.Xunit.XunitPlumbing
{
	public class IntegrationTestDiscoverer : NestTestDiscoverer
	{
		private bool RunningOnTeamCity { get; } = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));

		public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestConfiguration.Configuration.RunIntegrationTests) { }

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
			var method = classOfMethod.GetMethod(testMethod.Method.Name, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public)
				?? testMethod.Method.ToRuntimeMethod();

			return TypeSkipVersionAttributeSatisfies(classOfMethod)
			       || MethodSkipVersionAttributeSatisfies(method)
			       || SkipWhenRunOnTeamCity(classOfMethod, method)
			       || SkipWhenNeedingTypedKeys(classOfMethod);
		}

		private bool SkipWhenRunOnTeamCity(Type classOfMethod, MethodInfo info)
		{
			if (!this.RunningOnTeamCity) return false;

			var attributes = classOfMethod.GetTypeInfo().GetCustomAttributes<SkipOnTeamCityAttribute>()
				.Concat(info.GetCustomAttributes<SkipOnTeamCityAttribute>());
			return attributes.Any();
		}

		private static bool TypeSkipVersionAttributeSatisfies(Type classOfMethod) =>
			VersionUnderTestMatchesAttribute(classOfMethod.GetTypeInfo().GetCustomAttributes<SkipVersionAttribute>());

		private static bool MethodSkipVersionAttributeSatisfies(MethodInfo methodInfo) =>
			VersionUnderTestMatchesAttribute(methodInfo.GetCustomAttributes<SkipVersionAttribute>());

		private static bool VersionUnderTestMatchesAttribute(IEnumerable<SkipVersionAttribute> attributes)
		{
			if (!attributes.Any()) return false;

			return attributes
				.SelectMany(a => a.Ranges)
				.Any(range => TestConfiguration.VersionUnderTestSatisfiedBy(range.ToString()));
		}

	}
}
