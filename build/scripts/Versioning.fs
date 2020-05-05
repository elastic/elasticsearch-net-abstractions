// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Scripts

open System.Diagnostics
open System.IO
open System
open System.Reflection

open Fake.Core
open Fake.IO
open FSharp.Data
open Fake.IO.Globbing.Operators
open Fake.Tools.Git

module Versioning =
    type AssemblyVersionInfo = { Informational: SemVerInfo; Assembly: SemVerInfo; AssemblyFile: SemVerInfo; Project: ProjectInfo; }
    type private VersionsJson = JsonProvider<"versions.example.json">

    let defaultCanaryDate = DateTime.UtcNow.ToString("yyyyMMddTHHmmss")

    let private canaryVersionOrCurrent target version = 
        match target with
        | "canary" ->
            let timestampedVersion = (sprintf "ci%s" (defaultCanaryDate))
            printfn "Canary suffix %s " timestampedVersion
            let v = version |> SemVer.parse
            let canaryVersion = SemVer.parse ((sprintf "%d.%d.0-%s" v.Major (v.Minor + 1u) timestampedVersion).Trim())
            canaryVersion
        | _ -> version |> SemVer.parse

    let private versionOf target project =
        let globalJson = VersionsJson.Load(Paths.VersionsJson);
        match project with
        | Managed -> canaryVersionOrCurrent target <| globalJson.Versions.Managed.Remove(0, 1)
        | Ephemeral -> canaryVersionOrCurrent target <| globalJson.Versions.Ephemeral.Remove(0, 1)
        | Xunit -> canaryVersionOrCurrent target <| globalJson.Versions.Xunit.Remove(0, 1)
        | Stack -> canaryVersionOrCurrent target <| globalJson.Versions.Stack.Remove(0, 1)
        
    let reposVersion () =
        let globalJson = VersionsJson.Load(Paths.VersionsJson);
        globalJson.Versions.Repos.Remove(0, 1);

    let private assemblyVersionOf v = sprintf "%i.0.0" v.Major |> SemVer.parse

    let private assemblyFileVersionOf v = sprintf "%i.%i.%i.0" v.Major v.Minor v.Patch |> SemVer.parse

    //write it with a leading v in the json, needed for the json type provider to keep things strings
    let writeVersionsJson reposVersion managedVersion ephemeralVersion xunitVersion stackVersion =
        let versionsNode = VersionsJson.Versions(reposVersion, managedVersion, ephemeralVersion, xunitVersion, stackVersion)

        let newVersionsJson = VersionsJson.Root (versionsNode)
        use tw = new StreamWriter(Paths.VersionsJson)
        newVersionsJson.JsonValue.WriteTo(tw, JsonSaveOptions.None)

    let private pre (v: string) = match (v.StartsWith("v")) with | true -> v | _ -> sprintf "v%s" v
    let private bumpVersion project version = 
        let globalJson = VersionsJson.Load(Paths.VersionsJson);
        let reposVersion = pre <| globalJson.Versions.Repos
        let managedVersion = match project with | Managed -> pre version | _ -> pre <| globalJson.Versions.Managed
        let ephemeralVersion = match project with | Ephemeral -> pre version | _ -> pre <| globalJson.Versions.Ephemeral
        let xunitVersion = match project with | Xunit -> pre version | _ -> pre <| globalJson.Versions.Xunit
        let stack = match project with | Stack -> pre version | _ -> pre <| globalJson.Versions.Stack
        
        writeVersionsJson reposVersion managedVersion ephemeralVersion xunitVersion stack
        printfn "%s bumped version to (%O) in global.json " (nameOf project) version

    let writeVersionIntoVersionsJson project version =
        let globalJson = VersionsJson.Load(Paths.VersionsJson);
        let pv = pre version
        let changed = 
            match project with
            | Managed -> pv <> (pre <| globalJson.Versions.Managed)
            | Ephemeral -> pv <> (pre <| globalJson.Versions.Ephemeral)
            | Xunit -> pv <> (pre <| globalJson.Versions.Xunit)
            | Stack -> pv <> (pre <| globalJson.Versions.Stack)

        match changed with 
        | true -> bumpVersion project version 
        | _ -> 
            printfn "%s did not change version (%O)" (nameOf project) version
            ignore()
        changed

    let BumpGlobalVersion (projects: AssemblyVersionInfo list) = 
        let globalJson = VersionsJson.Load(Paths.VersionsJson);
        let v = globalJson.Versions.Repos.Remove(0, 1) |> SemVer.parse
        let bumpedVersion = sprintf "v%i.%i.%i" v.Major v.Minor (v.Patch + 1u)

        let managedVersion = pre <| globalJson.Versions.Managed
        let ephemeralVersion = pre <| globalJson.Versions.Ephemeral
        let xunitVersion = pre <| globalJson.Versions.Xunit
        let stackVersion = pre <| globalJson.Versions.Stack
        writeVersionsJson bumpedVersion managedVersion ephemeralVersion xunitVersion stackVersion
        printfn "bumped repos version to (%s) in global.json"  bumpedVersion

        let header p = sprintf "%s %O" p.Project.name p.Informational
        let projectVersions = projects |> List.map header |> String.concat ", "

        CommandHelper.gitCommand "." (sprintf "commit -am \"release: %s of %s\" " bumpedVersion projectVersions)
        CommandHelper.gitCommand "." (sprintf "tag -a %s -m \"release: %s of %s\" " bumpedVersion bumpedVersion projectVersions)

    let FullVersionInfo project v = 
        { Informational= v; Assembly= assemblyVersionOf v; AssemblyFile = assemblyFileVersionOf v; Project = infoOf project }
    let VersionInfo target project = 
        let v = versionOf target project
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
        printfn "Assembly: %O AssemblyFile %O Informational: %O => project %s" 
            pi.Assembly pi.AssemblyFile pi.Informational pi.Project.name 

        let fileName = sprintf "%s.%O" pi.Project.name pi.Informational
        let tmpFolder = sprintf "%s/tmp-%s" Paths.NugetOutput fileName
        let nupkg = sprintf "%s/%s.nupkg" Paths.NugetOutput fileName
        
        Zip.unzip tmpFolder nupkg

        !! (sprintf "%s/**/*.dll" tmpFolder)
        |> Seq.iter(fun dll ->
            let fv = FileVersionInfo.GetVersionInfo(dll)
            let a = AssemblyName.GetAssemblyName(dll).Version
            printfn "Assembly: %A AssemblyFile: %s Informational: %s => %s" a fv.FileVersion fv.ProductVersion dll
            if (a.Minor > 0 || a.Revision > 0 || a.Build > 0) then failwith (sprintf "%s assembly version is not sticky to its major component" dll)
            if (SemVer.parse (fv.ProductVersion) <> pi.Informational) then 
                failwith <| sprintf "Expected product info %s to match new version %O " fv.ProductVersion pi.Informational

            let assemblyName = System.Reflection.AssemblyName.GetAssemblyName(dll);
            if not <| assemblyName.FullName.Contains("PublicKeyToken=96c599bbe3e70f5d") then
                failwith <| sprintf "%s should have PublicKeyToken=96c599bbe3e70f5d" assemblyName.FullName
        )

    let ValidateArtifacts (projects: AssemblyVersionInfo list) = 
        for info in projects do validateNugetPackage info
        for info in projects do 
            let fileName = sprintf "%s.%O" info.Project.name info.Informational
            let tmpFolder = sprintf "%s/tmp-%s" Paths.NugetOutput fileName
            Directory.delete tmpFolder
    
