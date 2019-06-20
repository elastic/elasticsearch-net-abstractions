using System;

namespace Elastic.Managed
{
	public class ElasticsearchCleanExitException : Exception
	{
		public ElasticsearchCleanExitException(string message) : base(message) { }
		public ElasticsearchCleanExitException(string message, Exception innerException) : base(message, innerException) { }

	}
}
