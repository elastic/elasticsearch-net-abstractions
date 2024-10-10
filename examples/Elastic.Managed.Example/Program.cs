// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using Elastic.Elasticsearch.Managed;
using Elastic.Elasticsearch.Managed.Configuration;
using Elastic.Elasticsearch.Managed.ConsoleWriters;
using Elastic.Stack.ArtifactsApi;
using Elastic.Stack.ArtifactsApi.Products;

namespace Elastic.Managed.Example
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			ElasticVersion version = "latest-8";
			var folderName = version.Artifact(Product.Elasticsearch).LocalFolderName;

			var temp = Path.Combine(Path.GetTempPath(), "elastic", folderName, "my-cluster");
			var home = Path.Combine(temp, "home");

			var clusterConfiguration = new ClusterConfiguration(version, home, 2);
			using (var cluster = new ElasticsearchCluster(clusterConfiguration))
				cluster.Start(new ConsoleLineWriter(), TimeSpan.FromMinutes(2));

			Console.WriteLine("Program ended");
		}
	}
}
