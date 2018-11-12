#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net45"
#r @"FakeLib.dll"
#r @"FSharp.Data.dll"

#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open System 
open Fake 
open FSharp.Data 

open Paths
open Versioning
open Tooling

module Build =

    type private GlobalJson = JsonProvider<"../../global.json", InferTypesFromValues = false>
    let private pinnedSdkVersion = GlobalJson.GetSample().Sdk.Version

    let msBuildProperties (projects: Versioning.AssemblyVersionInfo list) = 
        let props = (projects |> List.collect Versioning.MsBuildArgs)
        let v = Versioning.reposVersion()
        [(sprintf "/p:ReposVersion=v%s" v)] |> List.append props

    let Compile (projects: Versioning.AssemblyVersionInfo list) = 
        if not (DotNetCli.isInstalled()) then failwith  "You need to install the dotnet command line SDK to build for .NET Core"
        let runningSdkVersion = DotNetCli.getVersion()
        if (runningSdkVersion <> pinnedSdkVersion) then failwithf "Attempting to run with dotnet.exe with %s but global.json mandates %s" runningSdkVersion pinnedSdkVersion

        let props = projects |> msBuildProperties
        DotNetCli.Build
            (fun p -> 
                { p with 
                    Configuration = "Release" 
                    Project = Paths.SolutionFile
                    TimeOut = TimeSpan.FromMinutes(3.)
                    AdditionalArgs = props
                }
            ) |> ignore
            
    let RewriteBenchmarkDotNetExporter () = 
        let assemblyRewriter = Paths.PaketDotNetGlobalTool "assembly-rewriter" @"tools\netcoreapp2.1\any\assembly-rewriter.dll"
        let bdOutput = sprintf @"%s\%s" (Paths.Source @"Elastic.BenchmarkDotNetExporter") @"bin\Release\netstandard2.0"
        let outDllName s = match s with | "Elastic.BenchmarkDotNetExporter" -> s | _ -> sprintf "Elastic.Internal.%s" s
        let dllName s = sprintf @"%s\%s.dll" bdOutput s
        let names = [@"Elastic.BenchmarkDotNetExporter"; "Elasticsearch.Net"; "Nest"] 
        let dlls = 
            names
            |> Seq.map (fun s -> sprintf @"-i ""%s"" -o ""%s"" " (dllName s) (dllName <| outDllName s))
            |> Seq.fold (+) " "
        let mergeCommand = sprintf @"%s %s" assemblyRewriter dlls
        DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(3.) }) mergeCommand |> ignore
        
        let keyFile = Paths.Keys "keypair.snk"
        let ilMergeArgs = ["/internalize"; (sprintf "/keyfile:%s" keyFile); (sprintf "/out:%s" (dllName (names |> Seq.head)))]
        let mergeDlls = names |> Seq.map (fun s -> dllName <| outDllName s)
        Tooling.ILRepack.Exec (ilMergeArgs |> Seq.append mergeDlls) |> ignore

    let Restore () =
        DotNetCli.Restore
            (fun p -> 
                { p with 
                    Project = Paths.SolutionFile
                    TimeOut = TimeSpan.FromMinutes(3.)
                }
            ) |> ignore

    let Clean() =
        CleanDir Paths.BuildOutput
        let cleanCommand = sprintf "clean %s -c Release" Paths.SolutionFile
        DotNetCli.RunCommand (fun p -> { p with TimeOut = TimeSpan.FromMinutes(3.) }) cleanCommand |> ignore

    let CreateNugetPackage (projects: Versioning.AssemblyVersionInfo list) = 

        let props = projects |> msBuildProperties |> List.append ["--no-build"]
        DotNetCli.Pack(fun p -> 
        {
            p with 
                Configuration = "Release"
                OutputPath = sprintf @"..\..\%s" Paths.NugetOutput
                TimeOut = TimeSpan.FromMinutes(3.)
                Project = Paths.SolutionFile
                AdditionalArgs = props
                
        })