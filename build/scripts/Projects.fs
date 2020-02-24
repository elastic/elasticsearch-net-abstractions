namespace Scripts

open System
open Fake.Core

[<AutoOpen>]
module Projects = 

    type Project =
        | Managed
        | Ephemeral
        | Xunit
        | Stack

        static member All = [Managed; Ephemeral; Xunit; Stack;]

    type ProjectInfo = { name: string; moniker: string; project: Project}

    let nameOf project = 
        match project with
        | Managed -> "Elastic.Managed"
        | Ephemeral -> "Elastic.Managed.Ephemeral"
        | Xunit -> "Elastic.Xunit"
        | Stack -> "Elastic.Stack.Artifacts"

    let monikerOf project = 
        match project with
        | Managed -> "Managed"
        | Ephemeral -> "Ephemeral"
        | Xunit -> "Xunit"
        | Stack -> "Stack"

    let infoOf project = { name = project |> nameOf; moniker = project |> monikerOf; project = project }

    let projectsStartingWith partial =
        Project.All 
        |> Seq.map monikerOf 
        |> Seq.filter (fun s -> s |> String.toLower |> String.startsWith (partial |> String.toLower) && partial |> String.isNotNullOrEmpty) 
        |> Seq.toList

    let tryFind partial =
        let projectsStartingWith = 
            Project.All 
            |> Seq.map infoOf
            |> Seq.filter (fun s -> partial |> String.isNotNullOrEmpty && s.moniker |> String.toLower |> String.startsWith (partial |> String.toLower)) 
            |> Seq.toList

        match projectsStartingWith with 
        | [i] -> Some i.project
        | _ -> None