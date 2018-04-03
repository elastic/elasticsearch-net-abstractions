#I @"../../packages/build/FAKE/tools"
open Fake.TeamCityRESTHelper
#r @"FakeLib.dll"
#load @"Projects.fsx"
#load @"Versioning.fsx"

open System
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
* release <version>
  - 0 create a release worthy nuget packages for [version] under build\output
"""

module Commandline =

    let private args = getBuildParamOrDefault "cmdline" "" |> split ' '

    let private (|IsAVersion|_|) version = match SemVerHelper.isValidSemVer version with | true -> Some (parse version) | _ -> None

    let private (|IsATarget|_|) candidate = match candidate with | "dump" | "build" | "release" -> Some candidate | _ -> None
    let target = match args with | IsATarget t::_ -> t | _ -> "build"

    let private (|IsAProject|_|) candidate =
        let names = projectsStartingWith candidate 
        match names with 
        | [name] -> tryFind name
        | [] -> None
        | _ ->
            traceError (sprintf "'%s' yields more then one project '%A' and therefor ambiguous" candidate names)
            exit 2

    let projects = 
        let rec a args bucket = 
            match args with
            | IsATarget _::IsAProject project::IsAVersion version::tail -> 
                Versioning.FullVersionInfo project version :: a tail bucket 
            | IsAProject project::IsAVersion version::tail -> 
                Versioning.FullVersionInfo project version :: a tail bucket 
            | _ -> bucket
        let argProjects = a args []
        let allProjects = Project.All |> List.map Versioning.VersionInfo

        List.append argProjects allProjects |> List.distinctBy (fun p -> p.Project.name)

    let project = Project.Xunit

    let parse () =
        setBuildParam "target" target
