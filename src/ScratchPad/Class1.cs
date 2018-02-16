using System;
using Elastic.ManagedNode;
using Elastic.ManagedNode.Configuration;
using Elastic.Net.Abstractions.Clusters;
using Elasticsearch.Net;
using ProcNet.Std;

namespace ScratchPad
{
	public static class Program
	{
		public static int Main()
		{
//			var es = @"c:\Data\elasticsearch-5.4.1\bin\elasticsearch.bat";
//			using (var elasticsearch = new ElasticsearchNode(es))
//			{
//				elasticsearch.Start();
//				Console.ReadKey();
//			}

//			using (var p = new ObservableProcess("ipconfig", "/all"))
//			{
//				p.Subscribe(c => Console.Write(c.Characters));
//
//				p.WaitForCompletion(TimeSpan.FromSeconds(2));
//			}

//			var handle = new ManualResetEvent(false);
//
//			var process = new Process()
//			{
//				StartInfo = new ProcessStartInfo
//				{
//					Arguments = "/all",
//					FileName = "ipconfig",
//					RedirectStandardError = true,
//					RedirectStandardOutput = true,
//					CreateNoWindow = true,
//					UseShellExecute = false
//				},
//				EnableRaisingEvents = true
//			};
//			process.OutputDataReceived += (e,s) => Console.WriteLine(s.Data);
//			process.ErrorDataReceived += (e, s) => Console.Error.WriteLine(s.Data);
//			process.Exited += (e, s) =>
//			{
//				process.WaitForExit(5000);
//				process.WaitForExit();
//				process.Dispose();
//				handle.Set();
//			};
//			process.Start();
//			process.BeginOutputReadLine();
//			process.BeginErrorReadLine();
//
//			handle.WaitOne();

			using (var node = new ElasticsearchNode(new NodeConfiguration("5.5.1", "cluster-x", "node-y")))
			{
				node.Start(new ElasticsearchConsoleOutWriter());
				node.WaitForStarted(TimeSpan.FromMinutes(2));
			}

//			using (var cluster = new ElasticsearchCluster("5.5.1", instanceCount: 3))
//			{
//				cluster.Start();
//
//				Console.ReadKey();
//			}

			return 0;
		}

	}
}
