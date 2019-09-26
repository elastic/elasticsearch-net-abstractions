namespace Scripts

open Fake.Core
open System.IO

module Paths =

    let Repository = "https://github.com/mpdreamz/elasticsearch-net-abstraction"

    let BuildFolder = "build"
    let BuildOutput = sprintf "%s/output" BuildFolder
    let VersionsJson = Path.GetFullPath "versions.json"

    let Tool tool = sprintf "packages/build/%s" tool
    let CheckedInToolsFolder = "build/Tools"
    let KeysFolder = sprintf "%s/keys" BuildFolder
    let NugetOutput = sprintf "%s/_packages" BuildOutput
    let SourceFolder = "src"
    
    let CheckedInTool(tool) = sprintf "%s/%s" CheckedInToolsFolder tool
    let PaketDotNetGlobalTool tool subPath = sprintf "%s/%s" (Tool tool) subPath
    let Keys(keyFile) = sprintf "%s/%s" KeysFolder keyFile
    let Output(folder) = sprintf "%s/%s" BuildOutput folder
    let Source(folder) = sprintf "%s/%s" SourceFolder folder
    let Build(folder) = sprintf "%s/%s" BuildFolder folder

    let BinFolder(folder) = 
        let f = String.replace @"\" "/" folder
        sprintf "%s/%s/bin/Release" SourceFolder f

    let SolutionFile = sprintf "%s/Elastic.Abstractions.sln" SourceFolder
    
    let PackageOutFolder = Output("_packages")
