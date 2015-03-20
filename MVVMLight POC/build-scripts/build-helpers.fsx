module BuildHelpers

open Fake
open Fake.XamarinHelper
open Fake.EnvironmentHelper
open Fake.FileSystemHelper
open System
open System.IO
open System.Linq

let Exec command args =
    let result = Shell.Exec(command, args)

    if result <> 0 then failwithf "%s exited with error %d" command result

let RestorePackages solutionFile =
    Exec "build-scripts/tools/NuGet/NuGet.exe" ("restore " + solutionFile)
    solutionFile |> RestoreComponents (fun defaults -> {defaults with ToolPath = "build-scripts/tools/xpkg/xamarin-component.exe" })

let RunNUnitTests dllPath xmlPath =
    Exec "/Library/Frameworks/Mono.framework/Versions/Current/bin/nunit-console4" (dllPath + " -xml=" + xmlPath)
    TeamCityHelper.sendTeamCityNUnitImport xmlPath

let RunUITests platform =

    RestorePackages "Demo.UITests/Demo.UITests.csproj"

    MSBuild "Demo.UITests/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "Demo.UITests/Demo.UITests.csproj" ] |> ignore
    setEnvironVar "ps_platform" platform

    RunNUnitTests "Demo.UITests/bin/Debug/Demo.UITests.dll" "Demo.UITests/bin/Debug/testresults.xml"

    let files = filesInDirMatching (platform + "-*.png") (directoryInfo "Demo.UITests/bin/Debug")

    for file in files do
        TeamCityHelper.PublishArtifact file.FullName


let RunTestCloudTests appFile deviceList =
    MSBuild "Demo.UITests/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "Demo.UITests/Demo.UITests.csproj" ] |> ignore
    
    //TODO: have to set this when we have a TestCloud subscription
    let testCloudToken = Environment.GetEnvironmentVariable("TestCloudApiToken")

    let args = String.Format(@"submit ""{0}"" {1} --devices {2} --series ""master"" --locale ""en_US"" --assembly-dir ""Demo.UITests/bin/Debug"" --nunit-xml Demo.UITests/bin/Debug/testresults.xml", appFile, testCloudToken, deviceList)

    //TODO: ensure proper UITest version is set
    Exec "packages/Xamarin.UITest.0.6.6/tools/test-cloud.exe" args

    TeamCityHelper.sendTeamCityNUnitImport "Demo.UITests/bin/Debug/testresults.xml"
