# Elasticsearch .NET abstractions

You've reached the home repository for several auxiliary projects from the .NET team within Elastic.



Current projects:

### [Elastic.Managed](src/Elastic.Managed/README.md)
Provides an easy to start/stop one or more Elasticsearch instances that exists on disk already
 
### [Elastic.Managed.Ephemeral](src/Elastic.Managed.Ephemeral/README.md)
 
Bootstrap (download, install, configure) and run Elasticsearch `2.x`, `5.x` and `6.x` clusters with ease.
Started nodes are run in a new ephemeral location each time they are started and will clean up after they 
are disposed.
 
### [Elastic.Xunit](src/Elastic.Xunit/README.md)

Write integration tests against Elasticsearch `2.x`, `5.x` and `6.x`.
Works with `.NET Core` and `.NET 4.6` and up.

Supports `dotnet xunit`, `dotnet test`, `xunit.console.runner` and tests will be runnable in your IDE through VSTest and jetBrains Rider.

### [Differ](src/Differ/README.md)

Compare and Diff assemblies from different sources e.g. assemblies, directories, GitHub commit, NuGet packages.
Useful for determining what changes are introduced across versions, and if any are _breaking_.

Outputs differences in XML, Markdown or AsciiDoc. 



