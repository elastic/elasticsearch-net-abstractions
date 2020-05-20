# Elasticsearch .NET abstractions

You've reached the home repository for several auxiliary projects from the .NET team within Elastic.

Current projects:

### [Elastic.Elasticsearch.Managed](src/Elastic.Elasticsearch.Managed/README.md)

Provides an easy to start/stop one or more Elasticsearch instances that exists on disk already
 
### [Elastic.Elasticsearch.Ephemeral](src/Elastic.Elasticsearch.Ephemeral/README.md)
 
Bootstrap (download, install, configure) and run Elasticsearch `2.x`, `5.x`, `6.x` and `7.x` clusters with ease.
Started nodes are run in a new ephemeral location each time they are started and will clean up after they 
are disposed.
 
### [Elastic.Elasticsearch.Xunit](src/Elastic.Elasticsearch.Xunit/README.md)

Write integration tests against Elasticsearch `2.x`, `5.x`, `6.x` and `7.x`.
Works with `.NET Core` and `.NET 4.6` and up.

Supports `dotnet xunit`, `dotnet test`, `xunit.console.runner` and tests will be runnable in your IDE through VSTest and jetBrains Rider.

### [Elastic.Stack.ArtifactsApi](src/Elastic.Stack.ArtifactsApi/README.md)

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
    

