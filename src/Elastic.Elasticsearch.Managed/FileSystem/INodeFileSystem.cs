// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elastic.Elasticsearch.Managed.FileSystem
{
	/// <summary>
	///     The file system for an Elasticsearch node
	/// </summary>
	public interface INodeFileSystem
	{
		/// <summary>
		///     The path to the script to start Elasticsearch
		/// </summary>
		string Binary { get; }

		/// <summary>
		///     The path to the script to manage plugins
		/// </summary>
		string PluginBinary { get; }

		/// <summary>
		///     The path to the home directory
		/// </summary>
		string ElasticsearchHome { get; }

		/// <summary>
		///     The path to the config directory
		/// </summary>
		string ConfigPath { get; }

		/// <summary>
		///     The path to the data directory
		/// </summary>
		string DataPath { get; }

		/// <summary>
		///     The path to the logs directory
		/// </summary>
		string LogsPath { get; }

		/// <summary>
		///     The path to the repository directory
		/// </summary>
		string RepositoryPath { get; }

		/// <summary>
		///     The path to the directory in which this node resides
		/// </summary>
		string LocalFolder { get; }

		/// <summary> The config environment variable to use for this version</summary>
		string ConfigEnvironmentVariableName { get; }
	}
}
