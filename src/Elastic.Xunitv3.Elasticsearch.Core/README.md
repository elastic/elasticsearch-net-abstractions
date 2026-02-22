# Elastic.Xunitv3.Elasticsearch.Core

Core xUnit v3 integration for ephemeral Elasticsearch clusters, using [Nullean.Xunit.Partitions.v3](https://github.com/nullean/xunit-partitions) for cluster lifecycle management.

**Most users should depend on `Elastic.Xunitv3.Elasticsearch` instead**, which adds a pre-configured `ElasticsearchClient`.

This package is for users who want to bring their own Elasticsearch client or use a different client library.
