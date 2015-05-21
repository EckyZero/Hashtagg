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
	try
		Exec "/Library/Frameworks/Mono.framework/Versions/Current/bin/nunit-console4" (dllPath + " -xml=" + xmlPath)
	with
		| exn ->
            printfn "Issue running unit tests (if positive exit code, these are failed tests"
            printfn "Exception message: %s" exn.Message 
    TeamCityHelper.sendTeamCityNUnitImport xmlPath

let RunUITests platform =

    RestorePackages "CompassMobile.UITests.Phone/CompassMobile.UITests.Phone.csproj"

    MSBuild "CompassMobile.UITests.Phone/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "CompassMobile.UITests.Phone/CompassMobile.UITests.Phone.csproj" ] |> ignore
    setEnvironVar "ps_platform" platform

    RunNUnitTests "CompassMobile.UITests.Phone/bin/Debug/CompassMobile.UITests.Phone.dll" "CompassMobile.UITests.Phone/bin/Debug/testresults.xml"

    let files = filesInDirMatching (platform + "-*.png") (directoryInfo "CompassMobile.UITests.Phone/bin/Debug")

    for file in files do
        TeamCityHelper.PublishArtifact file.FullName


let RunTestCloudTests appFile deviceList =
    
    let testCloudToken = "fdd8ffdd0f98818b7dbf418bbe6699ee"

    let args = String.Format(@"submit ""{0}"" {1} --devices {2} --series ""dev"" --locale ""en_US"" --user ""luis.lafer-sousa@parivedasolutions.com"" --fixture-chunk --assembly-dir ""CompassMobile.UITests.Phone/bin/debug"" --nunit-xml CompassMobile.UITests.Phone/bin/debug/testresults.xml", appFile, testCloudToken, deviceList)

    //TODO: ensure proper UITest version is set
    Exec "packages/Xamarin.UITest.0.7.2.1812-dev/tools/test-cloud.exe" args

    TeamCityHelper.sendTeamCityNUnitImport "CompassMobile.UITests.Phone/bin/debug/testresults.xml"
