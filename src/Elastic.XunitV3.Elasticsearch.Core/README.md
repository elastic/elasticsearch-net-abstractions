# Elastic.XunitV3.Elasticsearch.Core

Core xUnit V3 integration for running tests against ephemeral Elasticsearch clusters, built on
[`Nullean.Xunit.Partitions.V3`](https://github.com/nullean/xunit-partitions) for cluster lifecycle management.

**Most users should depend on [`Elastic.XunitV3.Elasticsearch`](https://www.nuget.org/packages/Elastic.XunitV3.Elasticsearch/) instead**,
which adds a pre-configured `ElasticsearchClient` with debug diagnostics routed to test output.

This package is for users who want to:
- Use a different Elasticsearch client (e.g. the low-level `Elastic.Transport` client, or a raw `HttpClient`)
- Avoid pulling in the `Elastic.Clients.Elasticsearch` dependency
- Build their own higher-level integration on top of the cluster primitives

## What's included

| Type | Purpose |
|---|---|
| `ElasticsearchCluster<TConfiguration>` | Base cluster class implementing `IPartitionLifetime` — handles startup, teardown, external cluster support |
| `ElasticsearchConfiguration` | Cluster configuration (version, timeouts, concurrency, diagnostics) |
| `IClusterFixture<T>` | Marker interface for test classes — inherits `IPartitionFixture<T>` for partition grouping |
| `ElasticTestFramework` | Test framework registration — `[assembly: TestFramework(typeof(ElasticTestFramework))]` |
| `ElasticXunitRunOptions` | Partition/test filtering options |
| `SkipVersionAttribute` | Skip tests by Elasticsearch version range |
| `SkipTestAttribute` | Abstract base for custom skip conditions |
| `ExternalClusterConfiguration` | Connect to an existing cluster instead of starting an ephemeral one |
| `ElasticsearchClusterExtensions` | `GetOrAddClient` helpers with per-test output routing |

## Usage

```csharp
using Elastic.XunitV3.Elasticsearch.Core;
using Xunit;

[assembly: TestFramework(typeof(ElasticTestFramework))]

public class MyCluster : ElasticsearchCluster<ElasticsearchConfiguration>
{
    public MyCluster() : base(new ElasticsearchConfiguration("latest-9")) { }
}

public class MyTests(MyCluster cluster) : IClusterFixture<MyCluster>
{
    [Fact]
    public void ClusterIsRunning()
    {
        Assert.NotEmpty(cluster.NodesUris());
    }
}
```

See the [`Elastic.XunitV3.Elasticsearch` README](https://www.nuget.org/packages/Elastic.XunitV3.Elasticsearch/)
for full documentation on configuration, external clusters, version skipping, and filtering.
