using System;
using System.Threading;
using Elastic.ProcessManagement;
using Elastic.ProcessManagement.Std;

namespace ScratchPad
{
    public static class Program
    {
        public static int Main()
        {
		    Console.WriteLine("Start:...");

			while (true)
			{
				var process = new LineByLineObservableProcess(new ObservableProcessArguments("ipconfig", "/all")
				{
				});

				process.Subscribe(new ConsoleOutColorWriter());

				//process.Subscribe(
				//     e => Console.WriteLine(e.Data),
				//     e => Console.Error.WriteLine(e.Message));

				if (!process.WaitForCompletion(TimeSpan.FromSeconds(2000)))
					Console.Error.WriteLine("Taking too long");

				Console.WriteLine();

				//		    Console.WriteLine("------------");
				//		    Console.WriteLine("------------");
				//		    Console.WriteLine("------------");
				//
				//			process = new ObservableProcess(new ObservableProcessArguments("ipconfig", "/all")
				//			{
				//				SubscribeToLines = false
				//			});
				//	        process.Start()
				//		        .Subscribe(
				//			        e =>
				//			        {
				//				        Console.Write(e.Data);
				//			        },
				//			        e => Console.Error.Write(e.Data));
				//
				//	        if (!process.WaitForCompletion(TimeSpan.FromSeconds(20)))
				//		        Console.Error.WriteLine("Taking too long");
				//

				Console.WriteLine($"- ExitCode:{process.ExitCode} Press Any Key to Quit ---");
				Console.ReadKey();
			}
            return 0;
        }
    }
}
