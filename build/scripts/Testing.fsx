#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"
#load @"Commandline.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"

open System.IO
open Fake 
open Paths
open Projects
open Tooling
open Commandline

module Tests =
    open System

    let private buildingOnTravis = getEnvironmentVarAsBool "TRAVIS"

    let private setLocalEnvVars() = 
        let clusterFilter =  getBuildParamOrDefault "clusterfilter" ""
        let testFilter = getBuildParamOrDefault "testfilter" ""
        let numberOfConnections = getBuildParamOrDefault "numberOfConnections" ""
        setProcessEnvironVar "NEST_INTEGRATION_CLUSTER" clusterFilter
        setProcessEnvironVar "NEST_TEST_FILTER" testFilter
        setProcessEnvironVar "NEST_NUMBER_OF_CONNECTIONS" numberOfConnections

    let private dotnetTest() =
        CreateDir Paths.BuildOutput
        let command = ["xunit"; "-parallel"; "all"; "-xml"; "../.." @@ Paths.Output("TestResults-Desktop-Clr.xml")] 

        let dotnet = Tooling.BuildTooling("dotnet")
        dotnet.ExecIn "src/Tests" command |> ignore

    let RunUnitTests() = ignore()

