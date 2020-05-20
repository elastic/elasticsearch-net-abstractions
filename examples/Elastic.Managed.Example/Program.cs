// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Managed;
using Elastic.Elasticsearch.Managed.Configuration;
using Elastic.Elasticsearch.Managed.ConsoleWriters;

namespace Elastic.Managed.Example
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var version = "6.3.0";
			var esHome = Environment.ExpandEnvironmentVariables($@"%LOCALAPPDATA%\ElasticManaged\{version}\elasticsearch-{version}");

			var clusterConfiguration = new ClusterConfiguration(version, esHome, numberOfNodes: 2);
			using (var cluster = new ElasticsearchCluster(clusterConfiguration))
			{
				cluster.Start(new ConsoleLineWriter(), TimeSpan.FromMinutes(2));
			}

			Console.WriteLine("Program ended");
		}
	}
}
