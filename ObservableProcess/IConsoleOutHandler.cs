namespace Elastic.ProcessManagement
{
	public interface IConsoleOutHandler
	{
		void Handle(ConsoleOut consoleOut);
		void Write(ConsoleOut consoleOut);
	}
}