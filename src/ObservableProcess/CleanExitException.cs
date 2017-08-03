using System;

namespace Elastic.ProcessManagement
{
	public class CleanExitException : Exception
	{
		public CleanExitException(string message) : base(message) { }
		public CleanExitException(string message, Exception innerException) : base(message, innerException) { }

		public CleanExitException(string message, string helpText) : base(message)
		{
			this.HelpText = helpText;
		}
		public CleanExitException(string message, string helpText, Exception innerException) : base(message, innerException)
		{
			this.HelpText = helpText;
		}

		public string HelpText { get; private set; }
	}
}
