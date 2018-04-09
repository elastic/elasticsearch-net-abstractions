using System;

namespace Elastic.Xunit
{
	[AttributeUsage(AttributeTargets.Assembly)]
	public class ElasticXunitConfigurationAttribute : Attribute
	{
		public ElasticXunitConfigurationAttribute(Type t)
		{
			var options = Activator.CreateInstance(t) as ElasticXunitRunOptions;

			this.Options = options ?? new ElasticXunitRunOptions();
		}

		public ElasticXunitRunOptions Options { get;  }
	}
}
