using System;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.Configuration;
using Xunit.Abstractions;

namespace Elastic.Xunit.XunitPlumbing
{
	public class UnitTestDiscoverer : NestTestDiscoverer
	{
		public UnitTestDiscoverer(IMessageSink diagnosticMessageSink)
			: base(diagnosticMessageSink, TestConfiguration.Configuration.RunUnitTests)
		{
		}

		protected override bool SkipMethod(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			var classOfMethod = Type.GetType(testMethod.TestClass.Class.Name, true, true);
			return !TestConfiguration.Configuration.RunUnitTests
			       || ClassShouldSkipWhenPackageReference(classOfMethod)
			       || ClassIsIntegrationOnly(classOfMethod)
			       || SkipWhenNeedingTypedKeys(classOfMethod);
		}


		private static bool ClassShouldSkipWhenPackageReference(Type classOfMethod)
		{
#if TESTINGNUGETPACKAGE
			var attributes = classOfMethod.GetAttributes<ProjectReferenceOnlyAttribute>();
			return (attributes.Any());
#else
			return false;
#endif
		}

		private static bool ClassIsIntegrationOnly(Type classOfMethod)
		{
			var attributes = classOfMethod.GetTypeInfo().GetCustomAttributes<IntegrationOnlyAttribute>();
			return attributes.Any();
		}
	}
}
