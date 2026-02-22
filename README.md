# Elasticsearch .NET abstractions

Auxiliary .NET libraries from the Elastic team for managing, bootstrapping, and integration-testing local Elasticsearch clusters. These packages let you programmatically download, configure, start, and tear down Elasticsearch instances — useful for integration test suites, CI pipelines, and local development workflows.

## Packages

### [Elastic.TUnit.Elasticsearch](src/Elastic.TUnit.Elasticsearch/README.md)

TUnit integration for Elasticsearch with a pre-configured `ElasticsearchClient`. This is the **recommended package** for writing integration tests against Elasticsearch with TUnit. Define a cluster in one line, get automatic download/bootstrap/teardown, and access a ready-to-use client in your tests.

### [Elastic.TUnit.Elasticsearch.Core](src/Elastic.TUnit.Elasticsearch.Core/README.md)

Core TUnit integration for ephemeral Elasticsearch clusters without an `Elastic.Clients.Elasticsearch` dependency. Use this only if you need a different client library or are building your own convenience layer. Most users should depend on `Elastic.TUnit.Elasticsearch` instead, which includes this package transitively.

### [Elastic.XunitV3.Elasticsearch](src/Elastic.XunitV3.Elasticsearch/README.md)

xUnit v3 integration for Elasticsearch with a pre-configured `ElasticsearchClient` and debug diagnostics routed to each test's output. Built on partition-based cluster lifecycle so clusters start serially while tests within each cluster run concurrently. This is the **recommended package** for xUnit v3 users.

### [Elastic.XunitV3.Elasticsearch.Core](src/Elastic.XunitV3.Elasticsearch.Core/README.md)

Core xUnit v3 integration for ephemeral Elasticsearch clusters without an `Elastic.Clients.Elasticsearch` dependency. Provides the cluster base class, test framework registration, version-based skip attributes, and external cluster support. Use this only if you need a different client library; most users should depend on `Elastic.XunitV3.Elasticsearch` instead.

### [Elastic.Elasticsearch.Xunit](src/Elastic.Elasticsearch.Xunit/README.md)

xUnit v2 test framework integration for running integration tests against ephemeral Elasticsearch clusters. Handles cluster lifecycle, test partitioning by cluster, and supports `dotnet test`, `xunit.console.runner`, and IDE test runners (Visual Studio, Rider).

### [Elastic.Elasticsearch.Ephemeral](src/Elastic.Elasticsearch.Ephemeral/README.md)

Bootstrap (download, install, configure) and run throwaway Elasticsearch clusters. Each run uses a fresh ephemeral location and cleans up on dispose. Supports plugins, XPack, Security/SSL configuration, and multi-node clusters.

### [Elastic.Elasticsearch.Managed](src/Elastic.Elasticsearch.Managed/README.md)

Observable abstraction for starting and stopping one or more Elasticsearch nodes that already exist on disk. Provides reactive output monitoring, node health tracking, and cluster coordination. Serves as the foundation for `Elastic.Elasticsearch.Ephemeral`.

### [Elastic.Stack.ArtifactsApi](src/Elastic.Stack.ArtifactsApi/README.md)

Resolves download URLs and metadata for Elastic Stack artifacts across released versions, snapshots (`latest`, `latest-MAJOR`, `MAJOR.MINOR.PATCH-SNAPSHOT`), and build candidates (`commit_hash:version`). Used internally by the ephemeral cluster packages to fetch the right Elasticsearch distribution.

