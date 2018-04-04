#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"
#load @"Projects.fsx"
#load @"Versioning.fsx"

open Fake
open Projects
open Versioning
open SemVerHelper

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

module Commandline =

    let private args = getBuildParamOrDefault "cmdline" "" |> split ' '

    let private (|IsAVersion|_|) version = match SemVerHelper.isValidSemVer version with | true -> Some (parse version) | _ -> None

    let private (|IsATarget|_|) candidate = match candidate with | "pack" | "build" | "release" | "canary" -> Some candidate | _ -> None

    let target = 
        let t = match args with | IsATarget t::_ -> t | _ -> "build"
        setBuildParam "target" t
        t

    let private (|IsAProject|_|) candidate =
        let names = projectsStartingWith candidate 
        match names with 
        | [name] -> tryFind name
        | [] -> None
        | _ ->
            traceError (sprintf "'%s' yields more then one project '%A' and therefor ambiguous" candidate names)
            exit 2

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
        let allProjects = Project.All |> List.map Versioning.VersionInfo

        List.append providedProjects allProjects |> List.distinctBy (fun p -> p.Project.name)

    let parse () = ignore()
