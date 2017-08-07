using System;
using Elastic.ProcessManagement;
using Elastic.ProcessManagement.Std;

namespace ScratchPad
{
    public static class Program
    {
        public static int Main()
        {
	        var es = @"c:\Data\elasticsearch-5.4.1\bin\elasticsearch.bat";
	        using (var elasticsearch = new ElasticsearchNode(es))
	        {
		        elasticsearch.Start();
		        Console.ReadKey();
	        }
	        return 0;
        }
    }

}
