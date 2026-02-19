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
- `ExternalClusterConfiguration` — connection details for remote Elasticsearch clusters
- `ElasticsearchClusterExtensions` — `GetOrAddClient` helpers for caching any client type
- `ElasticsearchTestHooks` — global `[BeforeEvery(Test)]` hook for skip evaluation
- `ElasticsearchTestBase<TCluster>` — optional convenience base class
- `ElasticsearchParallelLimit` — default parallel limiter
- `SkipVersionAttribute` / `SkipTestAttribute` — version-based and custom skip conditions
- Bootstrap diagnostics writers (ANSI console, progress heartbeat)

## External cluster support

`ElasticsearchCluster<TConfiguration>` supports skipping ephemeral cluster startup when a
remote Elasticsearch instance is already available. This significantly speeds up development
feedback loops since you don't need to wait for Elasticsearch to bootstrap on every test run.

The resolution order during `InitializeAsync()` is:

1. **Programmatic hook** — override `TryUseExternalCluster()` to return an
   `ExternalClusterConfiguration`, or `null` to fall through
2. **Environment variables** — set `TEST_ELASTICSEARCH_URL` (and optionally
   `TEST_ELASTICSEARCH_API_KEY`)
3. **Ephemeral startup** — download, install, and start Elasticsearch locally

When using an external cluster:

- `NodesUris()` returns the external URI
- `IsExternal` is `true`
- `ExternalApiKey` contains the API key (if provided)
- `Dispose()` is a no-op (the remote cluster is not managed)
- Connectivity is validated with a GET `/` before tests run

## Dependencies

- `TUnit.Core`
- `Elastic.Elasticsearch.Ephemeral`

No dependency on `Elastic.Clients.Elasticsearch`.
