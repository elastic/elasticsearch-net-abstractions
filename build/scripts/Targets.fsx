#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Versioning.fsx"
#load @"Commandline.fsx"
#load @"Tooling.fsx"
#load @"Building.fsx"

open Fake

open Building
open Versioning
open Commandline

Commandline.parse()

Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" Build.Clean

Target "Restore" Build.Restore

Target "FullBuild"  <| fun _ -> 
    Build.Compile Commandline.projects

Target "Version" <| fun _ -> 
    let changedResults = 
        Commandline.projects
        |> List.map (fun p -> Versioning.writeVersionIntoGlobalJson (p.Project.project) (p.Informational.ToString()))
        |> List.contains true

    setBuildParam "versionchanged" (if changedResults then "1" else "0")

Target "Pack" <| fun _ -> 
    Build.CreateNugetPackage Commandline.projects
    Versioning.ValidateArtifacts Commandline.projects

Target "Release" <| fun _ -> 
    match getBuildParam "versionchanged" with
    | "1" -> Versioning.BumpGlobalVersion Commandline.projects
    | _ ->
        traceError "none of the packages seem to have bumped versions so we can not release at this time"

// Dependencies
"Clean"
    ==> "Version"
    ==> "Restore"
    ==> "FullBuild"
    ==> "Build"

"Build"
  ==> "Pack"

"Pack"
  ==> "Release"

"Dump"

RunTargetOrListTargets()

