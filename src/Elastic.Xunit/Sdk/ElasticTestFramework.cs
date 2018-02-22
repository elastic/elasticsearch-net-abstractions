using System;
using System.Reflection;
using Elastic.Xunit.Configuration;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elastic.Xunit.Sdk
{
	internal class ElasticTestFramework : XunitTestFramework
	{
		public ElasticTestFramework(IMessageSink messageSink)
			: base(messageSink)
		{
			//entry point called only once
		}

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		{

			var config = TestConfiguration.Configuration;

		    var random = new Random(config.Seed);

//			Console.WriteLine(new string('-', 20));
//            Console.WriteLine("Starting tests using config:");
//			Console.WriteLine($" - {nameof(config.TestAgainstAlreadyRunningElasticsearch)}: {config.TestAgainstAlreadyRunningElasticsearch}");
//			Console.WriteLine($" - {nameof(config.ElasticsearchVersion)}: {config.ElasticsearchVersion}");
//			Console.WriteLine($" - {nameof(config.ForceReseed)}: {config.ForceReseed}");
//			Console.WriteLine($" - {nameof(config.Mode)}: {config.Mode}");
//			Console.WriteLine($" - {nameof(config.Seed)}: {config.Seed}");
//			if (config.Mode == TestMode.Integration)
//			{
//				Console.WriteLine($" - {nameof(config.ClusterFilter)}: {config.ClusterFilter}");
//				Console.WriteLine($" - {nameof(config.TestFilter)}: {config.TestFilter}");
//
//			}
//			Console.WriteLine($" - {nameof(config.RunIntegrationTests)}: {config.RunIntegrationTests}");
//			Console.WriteLine($" - {nameof(config.RunUnitTests)}: {config.RunUnitTests}");
//			Console.WriteLine($" - Random:");
//			Console.WriteLine($" \t- {nameof(config.Random.SourceSerializer)}: {config.Random.SourceSerializer}");
//			Console.WriteLine($" \t- {nameof(config.Random.TypedKeys)}: {config.Random.TypedKeys}");
//			Console.WriteLine($" \t- {nameof(config.Random.OldConnection)}: {config.Random.OldConnection}");
//			Console.WriteLine(new string('-', 20));

			return new TestFrameworkExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
		}
	}
}
