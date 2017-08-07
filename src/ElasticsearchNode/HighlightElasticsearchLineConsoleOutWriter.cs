using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	/// <summary>
	/// Passing this to <see cref="ElasticsearchNode"/> as <see cref="IConsoleOutWriter"/>
	/// will cause it to take ownership of writing to the console itself on a line per line basis.
	/// Where as <see cref="IConsoleOutWriter"/> is always characters based. Therefor this implementation
	/// itself never actually writes to the console
	/// </summary>
	public class HighlightElasticsearchLineConsoleOutWriter : ConsoleOutColorWriter
	{
		public override void Write(ConsoleOut consoleOut)
		{

		}
	}
}
