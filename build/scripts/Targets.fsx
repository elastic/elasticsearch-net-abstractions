#load @"Commandline.fsx"
#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"
#load @"Testing.fsx"
#load @"Signing.fsx"
#load @"Building.fsx"
#load @"Releasing.fsx"

open System
open Fake

open Paths
open Building
open Testing
open Versioning
open Releasing
open Signing
open Commandline

Commandline.parse()

Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" Build.Clean

Target "Restore" Build.Restore

Target "FullBuild" <| fun _ -> Build.Compile false
    
Target "Test" Tests.RunUnitTests

Target "Version" <| fun _ -> 
    tracefn "Current Version: %s" (Versioning.CurrentVersion.ToString())

Target "Release" <| fun _ -> 
    Release.NugetPack()   
    Versioning.ValidateArtifacts()
    StrongName.ValidateDllsInNugetPackage()

// Dependencies
"Clean" 
  =?> ("Version", hasBuildParam "version")
  ==> "Restore"
  =?> ("FullBuild", Commandline.needsFullBuild)
  =?> ("Test", (not Commandline.skipTests))
  ==> "Build"

"Build"
  ==> "Release"

RunTargetOrListTargets()

