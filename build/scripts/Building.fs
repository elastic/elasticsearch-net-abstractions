namespace Scripts

open Fake.IO
open Tooling

module Build =

    let msBuildProperties (projects: Versioning.AssemblyVersionInfo list) = 
        let props = (projects |> List.collect Versioning.MsBuildArgs)
        let v = Versioning.reposVersion()
        [(sprintf "/p:ReposVersion=v%s" v)] |> List.append props

    let Compile (projects: Versioning.AssemblyVersionInfo list) = 
        let props = projects |> msBuildProperties
        DotNet.Exec (["build"; Paths.SolutionFile; "-c"; "Release"] @ props) |> ignore
            
    let RewriteBenchmarkDotNetExporter () =
        DotNet.Exec ["tool"; "restore"]
        
        let assemblyRewriter = "assembly-rewriter"
        let bdOutput = sprintf @"%s/%s" (Paths.Source @"Elastic.BenchmarkDotNetExporter") @"bin/Release/netstandard2.0"
        let outDllName s = match s with | "Elastic.BenchmarkDotNetExporter" -> s | _ -> sprintf "Elastic.Internal.%s" s
        let dllName s = sprintf @"%s/%s.dll" bdOutput s
        let names = [@"Elastic.BenchmarkDotNetExporter"; "Elasticsearch.Net"; "Nest"] 
        let dlls = 
            names
            |> Seq.map (fun s -> sprintf @"-i ""%s"" -o ""%s"" " (dllName s) (dllName <| outDllName s))
            |> Seq.fold (+) " "
        let mergeCommand = sprintf @"%s %s" assemblyRewriter dlls
        DotNet.Exec [mergeCommand]
        
        let keyFile = Paths.Keys "keypair.snk"
        let ilMergeArgs = ["/internalize"; (sprintf "/keyfile:%s" keyFile); (sprintf "/out:%s" (dllName (names |> Seq.head)))]
        let mergeDlls = names |> List.map (fun s -> dllName <| outDllName s)
        Tooling.ILRepack.Exec (ilMergeArgs @ mergeDlls) |> ignore

    let Restore () = DotNet.Exec <| ["restore"; Paths.SolutionFile; ] 

    let Clean() =
        Directory.delete Paths.BuildOutput
        DotNet.Exec <| ["clean"; Paths.SolutionFile; "-c"; "release";] 

    let CreateNugetPackage (projects: Versioning.AssemblyVersionInfo list) = 

        let props = projects |> msBuildProperties |> List.append ["--no-build"]
        let output = sprintf "%s" Paths.NugetOutput
        DotNet.Exec <| ["pack"; Paths.SolutionFile; "-c"; "release"; "-o"; output] @ props
