# Elastic.XunitV3.Elasticsearch

xUnit V3 integration for running tests against ephemeral Elasticsearch clusters.
Clusters start before their tests, shut down after, and are shared across all test classes that need them.

This package includes a pre-configured `ElasticsearchClient` with debug diagnostics routed to each test's output.
If you want to bring your own client library, depend on [`Elastic.XunitV3.Elasticsearch.Core`](https://www.nuget.org/packages/Elastic.XunitV3.Elasticsearch.Core/) instead.

Built on [`Nullean.Xunit.Partitions.V3`](https://github.com/nullean/xunit-partitions) for cluster lifecycle management.

## Quick start

### 1. Create a test project

xUnit V3 test projects are executables:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <!-- Partition-based fixture injection is invisible to static analysis -->
    <NoWarn>$(NoWarn);xUnit1041;xUnit1051</NoWarn>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="xunit.v3" Version="3.2.2" />
    <PackageReference Include="Elastic.XunitV3.Elasticsearch" Version="*" />
  </ItemGroup>
</Project>
```

### 2. Register the test framework and define a cluster

```csharp
using Elastic.XunitV3.Elasticsearch;
using Elastic.XunitV3.Elasticsearch.Core;
using Xunit;

[assembly: TestFramework(typeof(ElasticTestFramework))]

// One-liner cluster — downloads, extracts, and boots Elasticsearch 9.x
public class MyCluster() : ElasticsearchCluster("latest-9");
```

### 3. Write tests

Implement `IClusterFixture<T>` to receive the shared cluster via constructor injection.
All test classes that share the same cluster type run against a single instance:

```csharp
public class SearchTests(MyCluster cluster) : IClusterFixture<MyCluster>
{
    [Fact]
    public async Task ClusterIsReachable()
    {
        var info = await cluster.Client.InfoAsync();

        Assert.True(info.IsValidResponse);
    }
}
```

That's it. The framework will:
1. Download and extract the requested Elasticsearch version (cached across runs)
2. Start the cluster before any of its tests run
3. Inject the cluster instance into every test class that implements `IClusterFixture<MyCluster>`
4. Run all tests within the cluster concurrently
5. Shut down the cluster after its tests complete

## Why partitions?

While a single machine can run multiple Elasticsearch instances, reasoning about concurrent cluster
startups becomes difficult — especially on CI where resources vary. Most test suites only need one
or two cluster configurations but have hundreds or thousands of tests. It makes more sense to
achieve parallelism over **tests** (many) rather than **clusters** (few).

This package uses the **partition** model from `Nullean.Xunit.Partitions.V3`:

- Each cluster type is a **partition**. Partitions run **serially** — only one cluster starts at
  a time, keeping startup predictable and easy to debug.
- Tests **within** a partition run **concurrently**. Once a cluster is up, all test classes that
  share it execute in parallel with no artificial concurrency barrier.
- This is fundamentally different from xUnit's built-in **collection fixtures**, which group tests
  into a collection but then run that collection's tests *sequentially*. With partitions you get
  shared state **without** sacrificing test parallelism.
- Cluster startup (`InitializeAsync`) and teardown (`DisposeAsync`) are managed by the partition
  framework — no manual lifecycle code needed.

## Customising the cluster

### Custom configuration

```csharp
public class MyCluster : ElasticsearchCluster
{
    public MyCluster() : base(new ElasticsearchConfiguration("latest-9")
    {
        // Suppress ES log output after startup
        ShowElasticsearchOutputAfterStarted = false,
        // Control test concurrency against this cluster
        MaxConcurrency = 4,
        // Increase startup timeout for slow CI
        StartTimeout = TimeSpan.FromMinutes(5),
    }) { }

    // Seed data after the cluster is ready
    protected override void SeedCluster()
    {
        Client.Indices.Create("my-index");
    }
}
```

### Using a generic cluster with a custom client

If you need full control over the `ElasticsearchClient` configuration, use the generic
`ElasticsearchCluster<TConfiguration>` base and wire up your own client:

```csharp
public class MyCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
    public MyCluster() : base(new ElasticsearchConfiguration("latest-9")) { }

    public ElasticsearchClient Client => this.GetOrAddClient((c, output) =>
    {
        var settings = new ElasticsearchClientSettings(new StaticNodePool(c.NodesUris()))
            .EnableDebugMode()
            .OnRequestCompleted(call => output.WriteLine(call.DebugInformation));
        return new ElasticsearchClient(settings);
    });
}
```

The `output` writer automatically routes per-request diagnostics to whichever test
is currently executing, so request/response logs appear in the correct test's output.

## External clusters

You can skip ephemeral startup entirely and run tests against an existing cluster.
This is useful for CI pipelines where the cluster is provisioned separately.

### Via environment variables

```bash
export TEST_ELASTICSEARCH_URL=https://my-cluster:9200
export TEST_ELASTICSEARCH_API_KEY=my-api-key   # optional
dotnet run --project MyTests/
```

The `Client` property will automatically use the API key when configured.

### Programmatically

Override `TryUseExternalCluster()` for service discovery, config files, etc.:

```csharp
public class MyCluster : ElasticsearchCluster
{
    public MyCluster() : base("latest-9") { }

    protected override ExternalClusterConfiguration TryUseExternalCluster()
    {
        var url = Environment.GetEnvironmentVariable("MY_DEV_CLUSTER_URL");
        return url != null
            ? new ExternalClusterConfiguration(new Uri(url))
            : null; // fall through to ephemeral startup
    }
}
```

## Version-based test skipping

Skip tests for specific Elasticsearch versions using semver ranges:

```csharp
[SkipVersion("<8.0.0", "Requires vector search support")]
public class VectorSearchTests(MyCluster cluster) : IClusterFixture<MyCluster>
{
    [Fact]
    public void KnnSearch() { /* ... */ }
}

// Also works on individual methods
public class MixedTests(MyCluster cluster) : IClusterFixture<MyCluster>
{
    [SkipVersion(">=9.0.0", "API removed in 9.x")]
    [Fact]
    public void LegacyApi() { /* ... */ }
}
```

## Partition and test filtering

Control which clusters and tests run via `ElasticXunitRunOptions`:

```csharp
[assembly: ElasticXunitConfiguration(typeof(MyOptions))]

public class MyOptions : ElasticXunitRunOptions
{
    public MyOptions()
    {
        // Only run clusters whose type name matches (regex)
        PartitionFilterRegex = "MyCluster";
        // Only run test classes whose name matches (regex)
        TestFilterRegex = "Search";
    }
}
```

## Bootstrap diagnostics

During cluster startup the package provides progress feedback:

- **CI / non-interactive** — full ANSI-colored Elasticsearch log output
- **Interactive terminal** — periodic heartbeat showing elapsed time and last log line
- **Explicit control** — set `ShowBootstrapDiagnostics = true/false` on the configuration

## Running tests

```bash
# Default output (failures only)
dotnet run --project MyTests/

# See per-test progress
dotnet run --project MyTests/ -- -reporter verbose

# Filter by test name
dotnet run --project MyTests/ -- -method "*Search*"
```
