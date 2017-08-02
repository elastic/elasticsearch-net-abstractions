using System;

namespace Elastic.ProcessManagement.Std
{
	public class ConsoleOut
	{
		private char[] _characters;
		public bool Error { get; }

		public string Line { get; }

		private bool IsString { get; }

		private ConsoleOut(bool error, string line)
		{
			this.Error = error;
			this.Line = line;
			this.IsString = true;
		}
		private ConsoleOut(bool error, char[] characters)
		{
			this.Error = error;
			this._characters = characters;
		}

		public void OutOrErrrorCharacters(Action<char[]> outChararchters, Action<char[]>  errorCharacters)
		{
			if (this.Error) errorCharacters(this._characters);
			else outChararchters(this._characters);

		}
		public void CharsOrString(Action<char[]> doCharacters, Action<string> doLine)
		{
			if (this.IsString) doLine(this.Line);
			else doCharacters(this._characters);
		}

		public static ConsoleOut ErrorOut(string data) => new ConsoleOut(true, data);
		public static ConsoleOut Out(string data) => new ConsoleOut(false, data);

		public static ConsoleOut ErrorOut(char[] characters) => new ConsoleOut(true, characters);
		public static ConsoleOut Out(char[] characters) => new ConsoleOut(false, characters);
	}
}
