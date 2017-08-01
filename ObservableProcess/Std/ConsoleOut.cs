namespace Elastic.ProcessManagement.Std
{
	public struct ConsoleOut
	{
		public static IConsoleOutWriter DefaultWriter = new ConsoleOutWriter();
		public static IConsoleOutWriter ErrorsInRedWriter = new ConsoleOutColorWriter();

		public bool Error { get; }
		public string Data { get; }

		private ConsoleOut(bool error, string data)
		{
			this.Error = error;
			this.Data = data;
		}

		public static ConsoleOut ErrorOut(string data) => new ConsoleOut(true, data);
		public static ConsoleOut Out(string data) => new ConsoleOut(false, data);
	}
}
