// See https://aka.ms/new-console-template for more information

using Elastic.Elasticsearch.Ephemeral;

var config = new EphemeralClusterConfiguration("8.7.0");
var cluster = new EphemeralCluster(config);
using var started = cluster.Start();
