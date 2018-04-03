#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Versioning.fsx"
#load @"Commandline.fsx"
#load @"Tooling.fsx"
#load @"Signing.fsx"
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

Target "ChangeVersion" <| fun _ -> 
    for p in Commandline.projects do
        Versioning.writeVersionIntoGlobalJson (p.Project.project) (p.Informational.ToString())

Target "Version" <| fun _ -> 
    for v in Commandline.projects do
        traceImportant (sprintf "project %s has version %s from here on out" (v.Project.name) (v.Informational.ToString()))

Target "Release" <| fun _ -> 
    Build.CreateNugetPackage Commandline.projects
    Versioning.ValidateArtifacts Commandline.projects

Target "Dump" <| fun _ -> 
    for v in Commandline.projects do
        traceImportant (sprintf "project %s has version %s from here on out" (v.Project.name) (v.Informational.ToString()))

// Dependencies
"Clean"
    ==> "ChangeVersion"
    ==> "Version"
    ==> "Restore"
    ==> "FullBuild"
    ==> "Build"

"Build"
  ==> "Release"

"Dump"

RunTargetOrListTargets()

