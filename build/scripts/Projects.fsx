#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

open System
open Fake

[<AutoOpen>]
module Projects = 

    type Project =
        | Managed
        | Ephemeral
        | Xunit
        | BenchmarkDotNetExporter
        | Stack

        static member All = [Managed; Ephemeral; Xunit; BenchmarkDotNetExporter; Stack;]

    type ProjectInfo = { name: string; moniker: string; project: Project}

    let nameOf project = 
        match project with
        | Managed -> "Elastic.Managed"
        | Ephemeral -> "Elastic.Managed.Ephemeral"
        | Xunit -> "Elastic.Xunit"
        | BenchmarkDotNetExporter -> "Elastic.BenchmarkDotNetExporter"
        | Stack -> "Elastic.Stack.Artifacts"

    let monikerOf project = 
        match project with
        | Managed -> "Managed"
        | Ephemeral -> "Ephemeral"
        | Xunit -> "Xunit"
        | BenchmarkDotNetExporter -> "BDNetExporter"
        | Stack -> "Stack"

    let infoOf project = { name = project |> nameOf; moniker = project |> monikerOf; project = project }

    let projectsStartingWith partial =
        Project.All 
        |> Seq.map monikerOf 
        |> Seq.filter (fun s -> s |> toLower |> startsWith (partial |> toLower) && partial |> isNotNullOrEmpty) 
        |> Seq.toList

    let tryFind partial =
        let projectsStartingWith = 
            Project.All 
            |> Seq.map infoOf
            |> Seq.filter (fun s -> partial |> isNotNullOrEmpty && s.moniker |> toLower |> startsWith (partial |> toLower)) 
            |> Seq.toList

        match projectsStartingWith with 
        | [i] -> Some i.project
        | _ -> None