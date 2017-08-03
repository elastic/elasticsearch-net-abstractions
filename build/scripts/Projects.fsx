#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Data/lib/net40"
#r @"FakeLib.dll"
#r @"FSharp.Data.dll"
#r @"System.IO.Compression.FileSystem.dll"

open System
open System.IO
open System.Diagnostics
open System.Net

open Fake
open FSharp.Data 

[<AutoOpen>]
module Projects = 

    type private GlobalJson = JsonProvider<"../../global.json">
    let private globalJson = GlobalJson.Load("../../global.json");
    type DotNetFrameworkIdentifier = { MSBuild: string; Nuget: string; DefineConstants: string; }

    type Project =
        | ObservableProcess

        static member All = seq[ObservableProcess]
        
    let nameOf project = 
        match project with
        | ObservableProcess -> "ObservableProcess"
    

