# Elastic.Elasticsearch.TUnit

Write integration tests against Elasticsearch using [TUnit](https://tunit.dev).
Leverages TUnit's native primitives (`ClassDataSource<T>`, `IAsyncInitializer`, `[BeforeEvery(Test)]`) so there is
no custom test framework, no marker interfaces, and no special test discoverers -- just well-named base classes
and attributes.

## Getting started

### Install

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="TUnit" Version="1.15.0" />
    <PackageReference Include="Elastic.Elasticsearch.TUnit" Version="<latest>" />
    <PackageReference Include="Elastic.Clients.Elasticsearch" Version="9.2.1" />
  </ItemGroup>
</Project>
```

### Define a cluster

Define the cluster and expose a `Client` property. The client is cached per-cluster and
per-request diagnostics are routed to whichever TUnit test is currently executing:

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Elasticsearch.TUnit;
using Elastic.Transport;

public class MyTestCluster() : ElasticsearchCluster("latest-9")
{
    public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
    {
        var pool = new StaticNodePool(c.NodesUris());
        var settings = new ElasticsearchClientSettings(pool)
            .EnableDebugMode()
            .OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
        return new ElasticsearchClient(settings);
    });
}
```

The cluster downloads, installs, starts, and tears down Elasticsearch automatically.

### Write tests

Tests receive the cluster via constructor injection. Access the client directly:

```csharp
[ClassDataSource<MyTestCluster>(Shared = SharedType.Keyed, Key = nameof(MyTestCluster))]
public class MyTests(MyTestCluster cluster)
{
    [Test]
    public async Task InfoReturnsNodeName()
    {
        var info = cluster.Client.Info();

        await Assert.That(info.Name).IsNotNull();
    }
}
```

`SharedType.Keyed` ensures the cluster is created once and shared across all test classes that
reference the same key.

### Run

```bash
dotnet run --project MyTests/
```

## Features

### Bootstrap diagnostics

During cluster startup the library writes progress to the terminal, bypassing TUnit's per-test
output capture so you always see what is happening.

| `ShowBootstrapDiagnostics` | Environment | Output |
|---|---|---|
| `null` (default) | CI | Full verbose, ANSI-colored |
| `null` (default) | Interactive terminal | Periodic heartbeat every 5 s |
| `true` | Any | Full verbose, ANSI-colored |
| `false` | Any | Silent |

Override the default:

```csharp
public class MyCluster : ElasticsearchCluster(new ElasticsearchConfiguration("latest-9")
{
    ShowBootstrapDiagnostics = true,   // force full output locally
    ProgressInterval = TimeSpan.FromSeconds(3),
});
```

On failure, node-level diagnostics (started status, port, version, last exception) are written
to both the terminal and TUnit's test output.

### Per-test client diagnostics

The `GetOrAddClient` overload that accepts a `TextWriter` provides a writer backed by
`TestContext.Current` -- the client is created once, but each test's request/response
diagnostics appear in that test's output. The recommended pattern is to expose this as a
`Client` property on your cluster class:

```csharp
public class MyTestCluster() : ElasticsearchCluster("latest-9")
{
    public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
    {
        var pool = new StaticNodePool(c.NodesUris());
        var settings = new ElasticsearchClientSettings(pool)
            .EnableDebugMode()
            .OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
        return new ElasticsearchClient(settings);
    });
}
```

For multiple clusters that share the same client setup, use a base class:

```csharp
public abstract class MyClusterBase : ElasticsearchCluster
{
    protected MyClusterBase() : base(new ElasticsearchConfiguration("latest-9")) { }

    public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
    {
        var pool = new StaticNodePool(c.NodesUris());
        var settings = new ElasticsearchClientSettings(pool)
            .EnableDebugMode()
            .OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
        return new ElasticsearchClient(settings);
    });
}

public class ClusterA : MyClusterBase { }
public class ClusterB : MyClusterBase
{
    protected override void SeedCluster()
    {
        // Seed data after the cluster is healthy
        Client.Indices.Create("my-index");
    }
}
```

### Version-based skip

```csharp
[SkipVersion("<8.0.0", "Feature requires 8.x")]
[Test]
public async Task SomeTest() { }
```

Accepts comma-separated [semver ranges](https://github.com/adamreeve/semver.net).
The attribute works on both methods and classes.

### Custom skip conditions

```csharp
public class RequiresLinuxAttribute : SkipTestAttribute
{
    public override bool Skip => !RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    public override string Reason => "Requires Linux";
}

[RequiresLinux]
[Test]
public async Task LinuxOnlyTest() { }
```

### Parallel limiting

```csharp
[ParallelLimiter<ElasticsearchParallelLimit>]
[ClassDataSource<MyTestCluster>(Shared = SharedType.Keyed, Key = nameof(MyTestCluster))]
public class HeavyTests(MyTestCluster cluster) { }
```

`ElasticsearchParallelLimit` defaults to `Environment.ProcessorCount`. Implement your own
`IParallelLimit` for different concurrency.

### Full configuration

For multi-node clusters, plugins, or XPack features use `ElasticsearchConfiguration` directly:

```csharp
public class SecurityCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
    public SecurityCluster() : base(new ElasticsearchConfiguration(
        version: "latest-9",
        features: ClusterFeatures.XPack | ClusterFeatures.Security,
        numberOfNodes: 2)
    {
        StartTimeout = TimeSpan.FromMinutes(4),
    }) { }

    public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
    {
        var pool = new StaticNodePool(c.NodesUris());
        var settings = new ElasticsearchClientSettings(pool)
            .EnableDebugMode()
            .OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
        return new ElasticsearchClient(settings);
    });

    protected override void SeedCluster()
    {
        // Called after the cluster is healthy -- create indices, seed data, etc.
    }
}
```

## Comparison with Elastic.Elasticsearch.Xunit

| Concept | Xunit | TUnit |
|---|---|---|
| Test framework registration | `[assembly: TestFramework(...)]` | Not needed |
| Cluster fixture | `IClusterFixture<T>` | `[ClassDataSource<T>]` |
| Integration test marker | `[I]` | `[Test]` |
| Unit test marker | `[U]` | `[Test]` |
| Current cluster access | `ElasticXunitRunner.CurrentCluster` | Constructor injection |
| Client access | `cluster.GetOrAddClient(...)` in test | `cluster.Client` property on cluster |
| Parallel control | `ElasticXunitRunOptions.MaxConcurrency` | `[ParallelLimiter<T>]` |
| Cluster partitioning | `Nullean.Xunit.Partitions` | `SharedType.Keyed` |
