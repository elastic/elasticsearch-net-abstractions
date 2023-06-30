// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Ephemeral;

var config = new EphemeralClusterConfiguration("8.7.0");
var cluster = new EphemeralCluster(config);
using var started = cluster.Start();
