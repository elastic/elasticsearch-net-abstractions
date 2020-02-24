namespace Scripts

open Fake.IO
open Tooling

module Build =

    let msBuildProperties (projects: Versioning.AssemblyVersionInfo list) = 
        let props = (projects |> List.collect Versioning.MsBuildArgs)
        let v = Versioning.reposVersion()
        [(sprintf "/p:ReposVersion=v%s" v)] |> List.append props

    let Compile (projects: Versioning.AssemblyVersionInfo list) = 
        let props = projects |> msBuildProperties
        DotNet.Exec (["build"; Paths.SolutionFile; "-c"; "Release"] @ props) |> ignore
            
    let Restore () = DotNet.Exec <| ["restore"; Paths.SolutionFile; ] 

    let Clean() =
        Directory.delete Paths.BuildOutput
        DotNet.Exec <| ["clean"; Paths.SolutionFile; "-c"; "release";] 

    let CreateNugetPackage (projects: Versioning.AssemblyVersionInfo list) = 

        let props = projects |> msBuildProperties |> List.append ["--no-build"]
        let output = sprintf "%s" Paths.NugetOutput
        DotNet.Exec <| ["pack"; Paths.SolutionFile; "-c"; "release"; "-o"; output] @ props
