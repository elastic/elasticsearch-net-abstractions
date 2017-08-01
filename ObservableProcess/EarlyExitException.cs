using System;

namespace Elastic.ProcessManagement
{
	public class EarlyExitException : Exception
	{
		public EarlyExitException(string message) : base(message) { }
		public EarlyExitException(string message, Exception innerException) : base(message, innerException) { }

		public EarlyExitException(string message, string helpText) : base(message)
		{
			this.HelpText = helpText;
		}
		public EarlyExitException(string message, string helpText, Exception innerException) : base(message, innerException)
		{
			this.HelpText = helpText;
		}

		public string HelpText { get; private set; }
	}
}