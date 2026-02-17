# Elastic.TUnit.Elasticsearch.Core

> **Most users should depend on [`Elastic.TUnit.Elasticsearch`](https://www.nuget.org/packages/Elastic.TUnit.Elasticsearch/) instead.**

This is the barebones core package. It provides cluster lifecycle, skip attributes, test hooks,
and bootstrap diagnostics for TUnit — but **does not include `Elastic.Clients.Elasticsearch`**.

You should only depend on this package directly if you are **sure** you will not use
`Elastic.Clients.Elasticsearch`. For example:

- You are using a different Elasticsearch client (e.g. the low-level `Elastic.Transport` client,
  a custom HTTP client, or a third-party library)
- You are testing cluster infrastructure itself and never need a typed client
- You are building your own convenience layer on top of this package

For everyone else — which is the vast majority of users — use
[`Elastic.TUnit.Elasticsearch`](https://www.nuget.org/packages/Elastic.TUnit.Elasticsearch/).
It depends on this package transitively and adds:

- A non-generic `ElasticsearchCluster` base class with a default `ElasticsearchClient`
- The `.WireTUnitOutput()` extension for `ElasticsearchClientSettings`
- One-liner cluster definitions like `public class MyCluster() : ElasticsearchCluster("latest-9");`

## What's in this package

- `ElasticsearchCluster<TConfiguration>` — generic cluster base with TUnit lifecycle integration
- `ElasticsearchConfiguration` — cluster configuration with bootstrap diagnostics settings
- `ElasticsearchClusterExtensions` — `GetOrAddClient` helpers for caching any client type
- `ElasticsearchTestHooks` — global `[BeforeEvery(Test)]` hook for skip evaluation
- `ElasticsearchTestBase<TCluster>` — optional convenience base class
- `ElasticsearchParallelLimit` — default parallel limiter
- `SkipVersionAttribute` / `SkipTestAttribute` — version-based and custom skip conditions
- Bootstrap diagnostics writers (ANSI console, progress heartbeat)

## Dependencies

- `TUnit.Core`
- `Elastic.Elasticsearch.Ephemeral`

No dependency on `Elastic.Clients.Elasticsearch`.
