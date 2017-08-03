#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"

open System
open Fake
open Projects

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
    let skipTests = args |> List.exists (fun x -> x = "skiptests")
    let private arguments = args |> List.filter (fun x -> x <> "skiptests")

    let private (|IsAProject|_|) candidate =
        let projectNames = Project.All |> Seq.map nameOf  
        let names = projectNames |> Seq.filter (fun s -> s |> toLower |> startsWith (candidate |> toLower)) |> Seq.toList
        match names with 
        | [name] -> Some name
        | [] ->
            traceError (sprintf "'%s' did not match any of our known projects '%A'" candidate projectNames)
            exit 2
            None
        | _ ->
            traceError (sprintf "'%s' yield more then one project '%A'" candidate names)
            exit 2
            None
        
    let target = 
        match arguments with
        | [IsAProject project] -> "build"
        | [IsAProject project; t] -> t
        | IsAProject project::t::tail -> t
        | _ ->
            traceError usage
            exit 2

    let parse () =
        printfn "%A" arguments
        match arguments with
        | [IsAProject project] -> ignore()
        | [IsAProject project; "release"; version] -> 
            setBuildParam "version" version
        | [IsAProject project; t] when target |> isNotNullOrEmpty -> ignore()
        | _ ->
            traceError usage
            exit 2

        setBuildParam "target" target
        traceHeader target
