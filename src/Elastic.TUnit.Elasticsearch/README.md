# Elastic.TUnit.Elasticsearch

Write integration tests against Elasticsearch using [TUnit](https://tunit.dev) and
[`Elastic.Clients.Elasticsearch`](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch/).

This is the recommended package for most users — it builds on
[`Elastic.TUnit`](https://www.nuget.org/packages/Elastic.TUnit/) and adds a convenience
`ElasticsearchCluster` base class with a pre-configured `ElasticsearchClient`.

## Getting started

### Install

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="TUnit" Version="1.15.0" />
    <PackageReference Include="Elastic.TUnit.Elasticsearch" Version="<latest>" />
  </ItemGroup>
</Project>
```

`Elastic.Clients.Elasticsearch` and `Elastic.TUnit` are included as transitive dependencies.

### Define a cluster

A one-liner is all you need. The base class provides a default `Client` with debug mode enabled
and per-request diagnostics routed to whichever TUnit test is currently executing:

```csharp
public class MyTestCluster() : ElasticsearchCluster("latest-9");
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
        var info = await cluster.Client.InfoAsync();

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

### Custom client configuration

Override the `Client` property when you need custom connection settings, authentication,
or serialization:

```csharp
public class MyTestCluster() : ElasticsearchCluster("latest-9")
{
    public override ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
    {
        var pool = new StaticNodePool(c.NodesUris());
        var settings = new ElasticsearchClientSettings(pool)
            .WireTUnitOutput(output)
            .Authentication(new BasicAuthentication("user", "pass"));
        return new ElasticsearchClient(settings);
    });
}
```

The `.WireTUnitOutput(output)` extension enables debug mode and routes per-request diagnostics
to the current test's output.

For multiple clusters that share the same client setup, use a base class:

```csharp
public abstract class MyClusterBase : ElasticsearchCluster
{
    protected MyClusterBase() : base(new ElasticsearchConfiguration("latest-9")
    {
        ShowElasticsearchOutputAfterStarted = false,
    }) { }
}

public class ClusterA : MyClusterBase { }
public class ClusterB : MyClusterBase
{
    protected override void SeedCluster()
    {
        Client.Indices.Create("my-index");
    }
}
```

Both `ClusterA` and `ClusterB` inherit the default `Client` from `ElasticsearchCluster`.

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

The default `Client` on `ElasticsearchCluster` routes per-request diagnostics to TUnit's
test output. The client is created once, but each test's request/response diagnostics appear
in that test's output via `TestContext.Current`.

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

### Concurrency

**Cluster startup is serialized.** Only one cluster starts at a time across the entire test run,
regardless of how many cluster types exist. Elasticsearch is resource-intensive, so startups are
gated by an internal semaphore to avoid overwhelming the machine.

**Tests run with unlimited parallelism by default.** Once a cluster is up, TUnit runs all tests
against it concurrently with no limit. For integration tests that hit Elasticsearch this can be
too aggressive — use `[ParallelLimiter<T>]` to cap concurrency:

```csharp
[ParallelLimiter<ElasticsearchParallelLimit>]
[ClassDataSource<MyTestCluster>(Shared = SharedType.Keyed, Key = nameof(MyTestCluster))]
public class HeavyTests(MyTestCluster cluster) { }
```

`ElasticsearchParallelLimit` defaults to `Environment.ProcessorCount`. Implement your own
`IParallelLimit` for different concurrency.

### Full configuration

For multi-node clusters, plugins, or XPack features use `ElasticsearchConfiguration` directly.
When extending the generic `ElasticsearchCluster<TConfiguration>`, define your own `Client`
property since the default is only on the non-generic base:

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
        var settings = new ElasticsearchClientSettings(new StaticNodePool(c.NodesUris()))
            .WireTUnitOutput(output);
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
| Client access | `cluster.GetOrAddClient(...)` in test | `cluster.Client` on cluster |
| Parallel control | `ElasticXunitRunOptions.MaxConcurrency` | `[ParallelLimiter<T>]` |
| Cluster partitioning | `Nullean.Xunit.Partitions` | `SharedType.Keyed` |
