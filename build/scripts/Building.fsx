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

module Build =

    type private GlobalJson = JsonProvider<"../../global.json">
    let private pinnedSdkVersion = GlobalJson.GetSample().Sdk.Version

    let Compile (projects: Versioning.AssemblyVersionInfo list) = 
        if not (DotNetCli.isInstalled()) then failwith  "You need to install the dotnet command line SDK to build for .NET Core"
        let runningSdkVersion = DotNetCli.getVersion()
        if (runningSdkVersion <> pinnedSdkVersion) then failwithf "Attempting to run with dotnet.exe with %s but global.json mandates %s" runningSdkVersion pinnedSdkVersion

        let props = (projects |> List.collect Versioning.MsBuildArgs)
        DotNetCli.Build
            (fun p -> 
                { p with 
                    Configuration = "Release" 
                    Project = Paths.SolutionFile
                    TimeOut = TimeSpan.FromMinutes(3.)
                    AdditionalArgs = props
                }
            ) |> ignore
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

        let args = (projects |> List.collect Versioning.MsBuildArgs)
        DotNetCli.Pack(fun p -> 
        {
            p with 
                Configuration = "Release"
                OutputPath = sprintf @"..\..\%s" Paths.NugetOutput
                TimeOut = TimeSpan.FromMinutes(3.)
                Project = Paths.SolutionFile
                AdditionalArgs = args
        })