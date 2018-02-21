using Elastic.Managed.Ephemeral;

namespace ScratchPad
{
	public static class Program
	{
		public static int Main()
		{

//			using (var node = new ElasticsearchProcess(new NodeConfiguration("5.5.1")))
//			{
//				node.Subscribe(new ElasticsearchConsoleOutWriter());
//				node.WaitForStarted(TimeSpan.FromMinutes(2));
//			}

			//using (var node = new ElasticsearchNode("6.0.0-beta2", @"c:\Data\elasticsearch-6.0.0-beta2"))
			//using (var node = new ElasticsearchNode(new NodeConfiguration("5.5.1")))
//			using (var node = new ElasticsearchNode(new NodeConfiguration(new EphemeralFileSystem("5.5.1"))))
//			{
//				node.Subscribe();
//				node.WaitForStarted(TimeSpan.FromMinutes(2));
//				Console.ReadKey();
//			}

			using (var cluster = new EphemeralCluster("5.5.4", instanceCount: 1))
			{
				cluster.Start();

				//Console.ReadKey();
			}

			return 0;
		}

	}

}
