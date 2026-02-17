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

```csharp
using Elastic.Elasticsearch.TUnit;

public class MyTestCluster() : ElasticsearchCluster("latest-9");
```

That single line gives you a one-node ephemeral cluster running the latest Elasticsearch 9.x.
The cluster downloads, installs, starts, and tears down automatically.

### Write tests

```csharp
using Elastic.Clients.Elasticsearch;
using Elastic.Elasticsearch.TUnit;
using Elastic.Transport;

[ClassDataSource<MyTestCluster>(Shared = SharedType.Keyed, Key = nameof(MyTestCluster))]
public class MyTests(MyTestCluster cluster)
{
    [Test]
    public async Task InfoReturnsNodeName()
    {
        var client = cluster.GetOrAddClient((c, output) =>
        {
            var pool = new StaticNodePool(c.NodesUris());
            var settings = new ElasticsearchClientSettings(pool)
                .EnableDebugMode()
                .OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
            return new ElasticsearchClient(settings);
        });

        var info = client.Info();

        await Assert.That(info.Name).IsNotNull();
    }
}
```

`SharedType.Keyed` ensures the cluster is created once and shared across all test classes that
reference the same key. The `output` `TextWriter` dynamically routes per-request diagnostics to
whichever TUnit test is currently executing.

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

### Client caching with per-test output

`GetOrAddClient` caches one client per cluster lifetime. The overload that accepts a
`TextWriter` provides a writer backed by `TestContext.Current` -- the client is created once, but
each test's request/response diagnostics appear in that test's output:

```csharp
var client = cluster.GetOrAddClient((c, output) =>
{
    var settings = new ElasticsearchClientSettings(new StaticNodePool(c.NodesUris()))
        .EnableDebugMode()
        .OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
    return new ElasticsearchClient(settings);
});
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
| Parallel control | `ElasticXunitRunOptions.MaxConcurrency` | `[ParallelLimiter<T>]` |
| Cluster partitioning | `Nullean.Xunit.Partitions` | `SharedType.Keyed` |
