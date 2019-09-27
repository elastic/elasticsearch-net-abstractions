namespace Scripts

open Fake.Core

module Commandline =

    let private usage = """
USAGE:

build [project] <target> [params] [skiptests]

Targets:

* build
  - default target if non provided. 
* pack [project version]*
  - builds nuget packages for the provided project e.g
    build pack xunit 1.1.1 managed 1.0.0

"""


    let private (|IsAVersion|_|) version = match SemVer.isValid version with | true -> Some (SemVer.parse version) | _ -> None

    let private (|IsATarget|_|) candidate = match candidate with | "pack" | "build" | "release" | "canary" -> Some candidate | _ -> None

    let private (|IsAProject|_|) candidate =
        let names = projectsStartingWith candidate 
        match names with 
        | [name] -> tryFind name
        | [] -> None
        | _ ->
            failwithf "'%s' yields more then one project '%A' and therefor ambiguous" candidate names
            exit 2


    type PassedArguments = {
        Target: string
        Projects: Versioning.AssemblyVersionInfo list
    }
    let parse (args: string list) =
        
        let target = match args with | IsATarget t::_ -> t | _ -> "build"
        let providedProjects =
            let rec a args bucket = 
                match args with
                | IsATarget _::IsAProject project::IsAVersion version::tail -> 
                    Versioning.FullVersionInfo project version :: a tail bucket 
                | IsAProject project::IsAVersion version::tail -> 
                    Versioning.FullVersionInfo project version :: a tail bucket 
                | _ -> bucket
            a args []
        let projects =
            let vInfo = Versioning.VersionInfo target
            let allProjects = Project.All |> List.map vInfo 
            List.append providedProjects allProjects |> List.distinctBy (fun p -> p.Project.name)
        
        { Target = target; Projects = projects }
        
        
        
        
