# Elastic.Managed
Provides an easy to start/stop one or more Elasticsearch instances that exists on disk already


## ElasticsearchNode 

A `Proc.ObservableProcess` implementation that starts an Elasticsearch instance from the specified
location. It is able to optionally block untill the node goes into started state and it sniffs the output 
to expose useful information such as the java process id, port number and others.

Because its a subclass of `Proc.ObservableProcess` its completely reactive and allows you to seperate the act 
of listening to the output and proxying stdout/err.

#### Examples:

All the examples assume the following are defined. `esHome` points to a local folder where `Elasticsearch` is installed/unzipped.

```csharp
var version = "6.2.0";
var esHome = Environment.ExpandEnvironmentVariables($@"%LOCALAPPDATA%\ElasticManaged\{version}\elasticsearch-{version}");
```

The easiest way to get going pass the `version` and `esHome` to `ElasticsearchNode`.
`ElasticsearchNode` implements `IDisposable` and will try to shutdown gracefully when disposed.
Simply new'ing `ElasticsearchNode` won't actually start the node. We need to explicitly call `Start()`.
`Start()` has several overloads but the default waits `2 minutes` for a started confirmation and proxies 
the consoleout using `HighlightWriter` which pretty prints the elasticsearch output.


```csharp
using (var node = new ElasticsearchNode(version, esHome))
{
	node.Start();
}
```

`Start` is simply sugar over 

```csharp
using (var node = new ElasticsearchNode(version, esHome))
{
	node.SubscribeLines(new HighlightWriter());

	if (!node.WaitForStarted(TimeSpan.FromMinutes(2))) throw new Exception();
}
```

As mentioned before `ElasticsearchNode` is really an `IObservable<CharactersOut>` by virtue of being an 
subclass of `Proc.ObservableProcess`. `SubscribeLines` is a specialized 
`Subscribe` that buffers `CharactersOut` untill a line is formed and emits a `LineOut`. Overloads exists that 
take additional `onNext/onError/onCompleted` handlers.

A  node can also be started using a `NodeConfiguration`

```csharp
var clusterConfiguration = new ClusterConfiguration(version, esHome);
var nodeConfiguration = new NodeConfiguration(clusterConfiguration, 9200)
{
	ShowElasticsearchOutputAfterStarted = false,
	Settings = { "node.attr.x", "y" }
};
using (var node = new ElasticsearchNode(nodeConfiguration))
{
	node.Start();
}
```

Which exposes the full range of options e.g here `ShowElasticsearchOutputAfterStarted` will dispose 
of the console out subscriptions as soon as we've parsed the started message to minimize the resources we consume.
`Settings` here allows you to pass elasticsearch node settings to use for the node.

## ElasticsearchCluster

A simple abstraction that can can start and stop one or more `ElasticsearchNodes` and wait for all of them to
be started

```csharp
var clusterConfiguration = new ClusterConfiguration(version, esHome, numberOfNodes: 2);
using (var cluster = new ElasticsearchCluster(clusterConfiguration))
{
	cluster.Start();
}
```

`ElasticsearchCluster` is simply a barebones `ClusterBase` implementation, which is more powerful then it seems
and serves as the base for `Elastic.Managed.Ephemeral`.
