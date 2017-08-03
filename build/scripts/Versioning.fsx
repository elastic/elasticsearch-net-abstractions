#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net40"
#r @"FakeLib.dll"
#r @"FSharp.Data.dll"
#r @"System.Xml.Linq.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Commandline.fsx"

open System
open System.Diagnostics
open System.IO
open System.Xml
open System.Text.RegularExpressions
open FSharp.Data

open Fake
open AssemblyInfoFile
open SemVerHelper
open Paths
open Projects
open SemVerHelper
open Commandline

module Versioning =
    type private GlobalJson = JsonProvider<"../../global.json">
    let globalJson = GlobalJson.Load("../../global.json");

    let private versionOf project =
        match project with
        | ObservableProcess -> globalJson.Versions.Observableprocess.Remove(0, 1)

    let private assemblyVersionOf v = sprintf "%s.0.0" (v.Major.ToString()) |> parse
    let private assemblyFileVersionOf v = sprintf "%s.%s.%s.0" (v.Major.ToString()) (v.Minor.ToString()) (v.Patch.ToString()) |> parse

    let writeVersionIntoGlobalJson project version =
        let observableProcessVersion = match project with | ObservableProcess -> version | _ -> versionOf ObservableProcess
        let versionsNode = GlobalJson.Versions(observableprocess = observableProcessVersion)

        let newGlobalJson = GlobalJson.Root (GlobalJson.Sdk(globalJson.Sdk.Version), versionsNode)
        use tw = new StreamWriter("global.json")
        newGlobalJson.JsonValue.WriteTo(tw, JsonSaveOptions.None)
        tracefn "Written (%s) to global.json as the current version will use this version from now on as current in the build" (version.ToString())

    type AssemblyVersionInfo = { Informational: SemVerInfo; Assembly: SemVerInfo; AssemblyFile: SemVerInfo; }
    let VersionInfo project =
        let currentVersion = versionOf project |> parse
        let bv = getBuildParam "version"
        let buildVersion = if (isNullOrEmpty bv) then None else Some(parse(bv))
        match (getBuildParam "target", buildVersion) with
        | ("release", None) -> failwithf "can not run release because no explicit version number was passed on the command line"
        | ("release", Some v) ->
            if (currentVersion >= v) then failwithf "tried to create release %s but current version is already at %s" (v.ToString()) (currentVersion.ToString())
            writeVersionIntoGlobalJson project v
            { Informational= v; Assembly= assemblyVersionOf v; AssemblyFile = assemblyFileVersionOf v }
        | _ ->
            tracefn "Not running 'release' target so using version in global.json (%s) as current" (currentVersion.ToString())
            { Informational= currentVersion; Assembly= assemblyVersionOf currentVersion; AssemblyFile = assemblyFileVersionOf currentVersion }
            
    let ValidateArtifacts project =
        let assemblyVersions = VersionInfo project
        let fileVersion = assemblyVersions.AssemblyFile
        let assemblyVersion = parse (sprintf "%i.0.0" fileVersion.Major)
        let tmp = "build/output/_packages/tmp"
        !! "build/output/_packages/*.nupkg"
        |> Seq.iter(fun f ->
           Unzip tmp f
           !! (sprintf "%s/**/*.dll" tmp)
           |> Seq.iter(fun f ->
                let fv = FileVersionInfo.GetVersionInfo(f)
                let a = GetAssemblyVersion f
                traceFAKE "Assembly: %A File: %s Product: %s => %s" a fv.FileVersion fv.ProductVersion f
                if (a.Minor > 0 || a.Revision > 0 || a.Build > 0) then failwith (sprintf "%s assembly version is not sticky to its major component" f)
                if (parse (fv.ProductVersion) <> fileVersion) then failwith (sprintf "Expected product info %s to match new version %s " fv.ProductVersion (fileVersion.ToString()))
           )
           DeleteDir tmp
        )
