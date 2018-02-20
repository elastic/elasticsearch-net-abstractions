using Elastic.Net.Abstractions.Clusters;

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

			using (var cluster = new EphimeralCluster("5.5.1", instanceCount: 2))
			{
				cluster.Start();

				//Console.ReadKey();
			}

			return 0;
		}

	}
}
