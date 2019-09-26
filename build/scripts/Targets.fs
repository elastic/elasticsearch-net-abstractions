namespace Scripts

open System
open Bullseye
open Fake.Tools.Git

module Main =

    let private target name action = Targets.Target(name, new Action(action)) 
    let private skip name = printfn "SKIPPED target '%s' evaluated not to run" name |> ignore
    let private conditional optional name action = target name (if optional then action else (fun _ -> skip name)) 
    let private command name dependencies action = Targets.Target(name, dependencies, new Action(action))
    
    let versionChanged = ref false
    
    let [<EntryPoint>] main args = 
        let parsed = Commandline.parse (args |> Array.toList)
        
        let isRelease = parsed.Target = "release"
        
        conditional isRelease "VerifyClean" <| fun _ -> 
            match Information.isCleanWorkingCopy "." with 
            | true -> printfn "Current checkout is clean, proceeding with release" 
            | false -> 
                failwithf "Current working dir is NOT clean aborting"

        conditional isRelease "VerifyVersionChange" <| fun _ -> 
            match !versionChanged with
            | true -> Versioning.BumpGlobalVersion parsed.Projects
            | _ ->
                failwithf "None of the packages seem to have bumped versions so we can not release at this time"
            
        target "Clean" Build.Clean

        target "Restore" Build.Restore

        target "RewriteBenchmarkDotNetExporter" Build.RewriteBenchmarkDotNetExporter

        target "FullBuild"  <| fun _ -> 
            Build.Compile parsed.Projects

        conditional isRelease "Version" <| fun _ -> 
            let changedResults = 
                parsed.Projects
                |> List.map (fun p -> Versioning.writeVersionIntoVersionsJson (p.Project.project) (p.Informational.ToString()))
                |> List.contains true

            versionChanged := changedResults

        command "Build" [ "VerifyClean"; "Version"; "VerifyVersionChange"; "Restore"; "FullBuild"] <| fun _ -> printfn "Finished Build" 

        command "Pack" [ "Build"; "RewriteBenchmark"] <| fun _ -> 
            Build.CreateNugetPackage parsed.Projects
            Versioning.ValidateArtifacts parsed.Projects
            
        command "Release" ["Pack";] <| fun _ -> printfn "Ran release"
        
        command "Canary" ["Pack";] <| fun _ -> printfn "Ran canary"
        
        0
        


