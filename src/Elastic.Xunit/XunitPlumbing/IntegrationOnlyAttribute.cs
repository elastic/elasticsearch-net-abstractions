using System;

namespace Elastic.Xunit.XunitPlumbing
{
	/// <summary>
	/// Ignores all unit tests on a given class, allowing a subclass to define integration methods without running inherited
	/// unit tests.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class IntegrationOnlyAttribute : Attribute { }
}
