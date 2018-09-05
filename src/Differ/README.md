# Differ

Compare and Diff assemblies from different sources.
Useful for determining what changes are introduced across versions, and if any are _breaking_.

Outputs differences in XML, Markdown or AsciiDoc. 

Run 

```bat
dotnet run -- --help
```

to see the support Assembly Providers and outputs

#### Examples:

Diff between two local assemblies:

```bat
dotnet run -- "assembly|C:\6.1.0\Nest.dll" "assembly|C:\6.2.0\Nest.dll"
```

Diff between all assemblies in directories, matched by name:

```bat
dotnet run -- "directory|C:\6.1.0" "directory|C:\6.2.0"
```

Diff NuGet packages:

```bat
dotnet run -- "nuget|NEST|6.1.0|net46" "nuget|NEST|6.2.0|net46"
```

Diff GitHub commits:

```bat
dotnet run -- "github|elastic/elasticsearch-net|6.1.0|cmd /C call build.bat skiptests skipdocs|build\output\Nest\net46" "github|elastic/elasticsearch-net|6.2.0|cmd /C call build.bat skiptests skipdocs|build\output\Nest\net46"
```

Any of the above can be mixed. For example, to compare GitHub HEAD against last NuGet package, and output in Markdown

```bat
dotnet run -- --format markdown "nuget|NEST|6.2.0|net46" "github|elastic/elasticsearch-net|HEAD|cmd /C call build.bat skiptests skipdocs|build\output\Nest\net46"
```