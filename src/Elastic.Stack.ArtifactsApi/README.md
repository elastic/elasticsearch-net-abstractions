# Elastic.Stack.ArtifactsApi

Library to fetch the url and metadata for released artifacts.

Supports:

1. Snapshots builds
    * `7.4.0-SNAPSHOT`
    * `latest-MAJOR` where `MAJOR` is a single integer representing the major you want a snapshot for
    * `latest` latest greatest 

2. BuildCandidates
    * `commit_hash:version` a build candidate for a version

3. Released versions
    * `MAJOR.MINOR.PATH` where `MAJOR` is still supported as defined by the EOL policy of Elastic.
    * Note if the version exists but is not yet released it will resolve as a build candidate
    

## Usage

First create an elastic version 

```csharp
var version = ElasticVersion.From(versionString);
```

Where `versionString` is a string in the aforementioned formats. `version.ArtifactBuildState` represents the type of version parsed.

```csharp
var version = ElasticVersion.From(versionString);
```

To go from a version to an artifact do the following

```csharp
var product = Product.From("elasticsearch");
var artifact = version.Artifact(product);
```
By first creating a `product` we can then pass that `product` to `version.Artifact` to get an artifact to that product's version.

A product can be a main product such as `elasticsearch` or a related product e.g

```csharp
var product = Product.From("elasticsearch-plugins", "analysis-icu");
var artifact = version.Artifact(product);
```

To aid with discoverability we ship with some statics so you do not need to guess the right monikers.

```csharp
Product.Elasticsearch;
Product.Kibana;
Product.ElasticsearchPlugin(ElasticsearchPlugin.AnalysisIcu);
```






