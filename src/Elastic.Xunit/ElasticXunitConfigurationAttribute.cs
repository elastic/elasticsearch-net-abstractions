using System;

namespace Elastic.Xunit
{
	/// <summary>
	/// An assembly attribute that specifies the <see cref="ElasticXunitRunOptions"/>
	/// for Xunit tests within the assembly.
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	public class ElasticXunitConfigurationAttribute : Attribute
	{
		/// <summary>
		/// Creates a new instance of <see cref="ElasticXunitConfigurationAttribute"/>
		/// </summary>
		/// <param name="type">
		/// A type deriving from <see cref="ElasticXunitRunOptions"/> that specifies the run options
		/// </param>
		public ElasticXunitConfigurationAttribute(Type type)
		{
			var options = Activator.CreateInstance(type) as ElasticXunitRunOptions;
			this.Options = options ?? new ElasticXunitRunOptions();
		}

		/// <summary>
		/// The run options
		/// </summary>
		public ElasticXunitRunOptions Options { get;  }
	}
}
