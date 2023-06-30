// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Ephemeral;
using static Elastic.Elasticsearch.Ephemeral.ClusterFeatures;


var config = new EphemeralClusterConfiguration("8.7.0", XPack | Security | SSL);
using var cluster = new EphemeralCluster(config);

var exitEvent = new ManualResetEvent(false);
Console.CancelKeyPress += (sender, eventArgs) => {
	cluster.Dispose();
	eventArgs.Cancel = true;
	exitEvent.Set();
};
using var started = cluster.Start();
exitEvent.WaitOne();
