#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net45"
#r @"FakeLib.dll"
#r @"FSharp.Data.dll"
#r @"System.Xml.Linq.dll"

#load @"Paths.fsx"

open System.Diagnostics
open System.IO
open System
open FSharp.Data

open Fake
open SemVerHelper
open Projects
open Paths
open Fake.Git

module Versioning =
    type AssemblyVersionInfo = { Informational: SemVerInfo; Assembly: SemVerInfo; AssemblyFile: SemVerInfo; Project: ProjectInfo; }
    type private GlobalJson = JsonProvider<"../../global.json">

    let private canaryVersionOrCurrent version = 
        match getBuildParam "target" with
        | "canary" ->
            let timestampedVersion = (sprintf "ci%s" (DateTime.UtcNow.ToString("yyyyMMddTHHmmss")))
            tracefn "Canary suffix %s " timestampedVersion
            let v = version |> parse
            let canaryVersion = parse ((sprintf "%d.%d.0-%s" v.Major (v.Minor + 1) timestampedVersion).Trim())
            canaryVersion
        | _ -> version |> parse

    let private versionOf project =
        let globalJson = GlobalJson.Load("../../global.json");
        match project with
        | Managed -> canaryVersionOrCurrent <| globalJson.Versions.Managed.Remove(0, 1)
        | Ephemeral -> canaryVersionOrCurrent <| globalJson.Versions.Ephemeral.Remove(0, 1)
        | Xunit -> canaryVersionOrCurrent <| globalJson.Versions.Xunit.Remove(0, 1)

    let private assemblyVersionOf v = sprintf "%i.0.0" v.Major |> parse

    let private assemblyFileVersionOf v = sprintf "%i.%i.%i.0" v.Major v.Minor v.Patch |> parse

    //write it with a leading v in the json, needed for the json type provider to keep things strings
    let writeGlobalJson reposVersion managedVersion ephemeralVersion xunitVersion =
        let globalJson = GlobalJson.Load("../../global.json");
        let versionsNode = GlobalJson.Versions(reposVersion, managedVersion, ephemeralVersion, xunitVersion)

        let newGlobalJson = GlobalJson.Root (GlobalJson.Sdk(globalJson.Sdk.Version), versionsNode)
        use tw = new StreamWriter("global.json")
        newGlobalJson.JsonValue.WriteTo(tw, JsonSaveOptions.None)

    let private pre (v: string) = match (v.StartsWith("v")) with | true -> v | _ -> sprintf "v%s" v
    let private bumpVersion project version = 
        let globalJson = GlobalJson.Load("../../global.json");
        let reposVersion = pre <| globalJson.Versions.Repos
        let managedVersion = match project with | Managed -> pre version | _ -> pre <| globalJson.Versions.Managed
        let ephemeralVersion = match project with | Ephemeral -> pre version | _ -> pre <| globalJson.Versions.Ephemeral
        let xunitVersion = match project with | Xunit -> pre version | _ -> pre <| globalJson.Versions.Xunit
        writeGlobalJson reposVersion managedVersion ephemeralVersion xunitVersion
        traceImportant <| sprintf "%s bumped version to (%O) in global.json " (nameOf project) version

    let writeVersionIntoGlobalJson project version =
        let globalJson = GlobalJson.Load("../../global.json");
        let pv = pre version
        let changed = 
            match project with
            | Managed -> pv <> (pre <| globalJson.Versions.Managed)
            | Ephemeral -> pv <> (pre <| globalJson.Versions.Ephemeral)
            | Xunit -> pv <> (pre <| globalJson.Versions.Xunit)

        match changed with 
        | true -> bumpVersion project version 
        | _ -> 
            traceImportant <| sprintf "%s did not change version (%O)" (nameOf project) version
            ignore()
        changed

    let BumpGlobalVersion (projects: AssemblyVersionInfo list) = 
        let globalJson = GlobalJson.Load("../../global.json");
        let v = globalJson.Versions.Repos.Remove(0, 1) |> parse
        let bumpedVersion = sprintf "v%i.%i.%i" v.Major v.Minor (v.Patch + 1)

        let managedVersion = pre <| globalJson.Versions.Managed
        let ephemeralVersion = pre <| globalJson.Versions.Ephemeral
        let xunitVersion = pre <| globalJson.Versions.Xunit
        writeGlobalJson bumpedVersion managedVersion ephemeralVersion xunitVersion
        traceImportant <| sprintf "bumped repos version to (%s) in global.json"  bumpedVersion

        let header p = sprintf "%s %O" p.Project.name p.Informational
        let projectVersions = projects |> List.map header |> String.concat ", "

        directRunGitCommandAndFail "." (sprintf "commit -am \"release: %s of %s\" " bumpedVersion projectVersions)
        directRunGitCommandAndFail "." (sprintf "tag -a %s -m \"release: %s of %s\" " bumpedVersion bumpedVersion projectVersions)

    let FullVersionInfo project v = 
        { Informational= v; Assembly= assemblyVersionOf v; AssemblyFile = assemblyFileVersionOf v; Project = infoOf project }
    let VersionInfo project = 
        let v = versionOf project
        { Informational= v; Assembly= assemblyVersionOf v; AssemblyFile = assemblyFileVersionOf v; Project = infoOf project }

    let MsBuildArgs info =
        let m = info.Project.moniker
        let p k v= sprintf "/p:%s%O=%O" m k v
        [
            p "Version" <| info.Informational;
            p "AssemblyVersion" <| info.Assembly;
            p "AssemblyFileVersion" <| info.AssemblyFile;
        ]

    let private validateNugetPackage (pi: AssemblyVersionInfo) = 
        traceFAKE "Assembly: %O AssemblyFile %O Informational: %O => project %s" 
            pi.Assembly pi.AssemblyFile pi.Informational pi.Project.name 

        let fileName = sprintf "%s.%O" pi.Project.name pi.Informational
        let tmpFolder = sprintf "%s/tmp-%s" Paths.NugetOutput fileName
        let nupkg = sprintf "%s/%s.nupkg" Paths.NugetOutput fileName
        
        Unzip tmpFolder nupkg

        !! (sprintf "%s/**/*.dll" tmpFolder)
        |> Seq.iter(fun dll ->
            let fv = FileVersionInfo.GetVersionInfo(dll)
            let a = GetAssemblyVersion dll
            traceFAKE "Assembly: %A AssemblyFile: %s Informational: %s => %s" a fv.FileVersion fv.ProductVersion dll
            if (a.Minor > 0 || a.Revision > 0 || a.Build > 0) then failwith (sprintf "%s assembly version is not sticky to its major component" dll)
            if (parse (fv.ProductVersion) <> pi.Informational) then 
                failwith <| sprintf "Expected product info %s to match new version %O " fv.ProductVersion pi.Informational

            let assemblyName = System.Reflection.AssemblyName.GetAssemblyName(dll);
            if not <| assemblyName.FullName.Contains("PublicKeyToken=96c599bbe3e70f5d") then
                failwith <| sprintf "%s should have PublicKeyToken=96c599bbe3e70f5d" assemblyName.FullName
        )
        DeleteDir tmpFolder

    let ValidateArtifacts (projects: AssemblyVersionInfo list) = for info in projects do validateNugetPackage info
    
