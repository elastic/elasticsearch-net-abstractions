# Elastic.Managed.Ephemeral

Bootstrap (download, install, configure) and run Elasticsearch `2.x`, `5.x` and `6.x` clusters with ease.
Started nodes are run in a new ephemeral location each time they are started and will clean up after they 
are disposed.


## EphemeralCluster 

A `ClusterBase` implementation from `Elastic.Managed` that can:

* download elasticsearch `2.x`, `5.x` and `6.x` versions (stable releases, snapshots, build candidates)
* download elasticsearch `2.x`, `5.x` and `6.x` plugins (stable releases, snapshots, build candidates)
* install elasticsearch and desired plugins in an ephemeral location. The source downloaded zips are cached 
on disk (LocalAppData). 
* Ships with builtin knowledge on how to enable XPack, SSL, Security on the running cluster.
* Start elasticsearch using ephemeral locations for ES_HOME and conf/logs/data paths.


#### Examples:

The easiest way to get started is by simply passing the version you want to be bootstrapped to `EphemeralCluster`.
`Start` starts the `ElasticsearchNode`'s and waits for them to be started. The default overload waits `2 minutes`.

```csharp
using (var cluster = new EphemeralCluster("6.0.0"))
{
	cluster.Start();
}
```

If you want the full configuration possibilities inject a `EphemeralClusterConfiguration` instead:


```csharp
var plugins = new ElasticsearchPlugins(ElasticsearchPlugin.RepositoryAzure, ElasticsearchPlugin.IngestAttachment);
var config = new EphemeralClusterConfiguration("6.2.3", ClusterFeatures.XPack, plugins, numberOfNodes: 2);
using (var cluster = new EphemeralCluster(config))
{
	cluster.Start();

	var nodes = cluster.NodesUris();
	var connectionPool = new StaticConnectionPool(nodes);
	var settings = new ConnectionSettings(connectionPool).EnableDebugMode();
	var client = new ElasticClient(settings);
				
	Console.Write(client.CatPlugins().DebugInformation);
}
```
Here we first create a `ElasticsearchPlugins` collection of the plugins that we want to bootstrap.
Then we create an instance of `EphemeralClusterConfiguration` that dictates we want a 2 node cluster
running elasticsearch `6.2.3` with XPack enabled using the previous declared `plugins`.

We then Start the node and after its up create a `NEST` client using the `NodeUris()` that the cluster
started.

We call `/_cat/plugins` and write `NEST`'s debug information to the console.

When the cluster exits the using block and disposes the cluster all nodes will be shutdown gracefully.

