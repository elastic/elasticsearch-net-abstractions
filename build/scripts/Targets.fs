namespace Scripts

open System
open Bullseye

module Main =

    let private target name action = Targets.Target(name, new Action(action)) 
    let private skip name = printfn "SKIPPED target '%s' evaluated not to run" name |> ignore
    let private conditional optional name action = target name (if optional then action else (fun _ -> skip name)) 
    let private command name dependencies action = Targets.Target(name, dependencies, new Action(action))
    
    let [<EntryPoint>] main args = 
        let parsed = Commandline.parse (args |> Array.toList)
        
        let isRelease = parsed.Target = "release"
        
        conditional isRelease "VerifyClean" <| fun _ -> 
            match isCleanWorkingCopy "." with 
            | true -> traceHeader "Current checkout is clean, proceeding with release" 
            | false -> 
                traceError "Current working dir is NOT clean aborting"
                failwithf "Current working dir is NOT clean aborting"

        conditional isRelease "VerifyVersionChange" <| fun _ -> 
            match getBuildParam "versionchanged" with
            | "1" -> Versioning.BumpGlobalVersion Commandline.providedProjects
            | _ ->
                traceError "None of the packages seem to have bumped versions so we can not release at this time"
                failwithf "None of the packages seem to have bumped versions so we can not release at this time"
            
        target "Clean" Build.Clean

        target "Restore" Build.Restore

        target "RewriteBenchmarkDotNetExporter" Build.RewriteBenchmarkDotNetExporter

        target "FullBuild"  <| fun _ -> 
            Build.Compile Commandline.projects

        conditional isRelease "Version" <| fun _ -> 
            let changedResults = 
                Commandline.projects
                |> List.map (fun p -> Versioning.writeVersionIntoVersionsJson (p.Project.project) (p.Informational.ToString()))
                |> List.contains true

            setBuildParam "versionchanged" (if changedResults then "1" else "0")

        command "Build" [ "VerifyClean"; "Version"; "VerifyVersionChange"; "Restore"; "FullBuild"] <| fun _ -> printfn "Finished Build %O" artifactsVersion

        command "Pack" [ "Build"; "RewriteBenchmark"] <| fun _ -> 
            Build.CreateNugetPackage Commandline.projects
            Versioning.ValidateArtifacts Commandline.projects
            
        command "Release" ["Pack";] <| fun _ -> printfn "Ran release"
        
        command "Canary" ["Pack";] <| fun _ -> printfn "Ran canary"
        
        0
        


