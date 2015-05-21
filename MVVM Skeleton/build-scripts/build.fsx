#r @"FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

Target "common-build" (fun () ->
    RestorePackages "CompassMobile.sln"
)

Target "common-tests-build" (fun () ->
    RestorePackages "CompassMobile.Shared.Tests/CompassMobile.Shared.Tests.csproj"

    MSBuild "CompassMobile.Shared.Tests/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] ["CompassMobile.Shared.Tests/CompassMobile.Shared.Tests.csproj" ] |> ignore
)

Target "common-tests" (fun () ->
    RunNUnitTests "CompassMobile.Shared.Tests/bin/Debug/CompassMobile.Shared.Tests.dll" "CompassMobile.Shared.Tests/bin/Debug/testresults.xml"
)

Target "ios-build" (fun () ->
    RestorePackages "CompassMobile.iOS.Phone/CompassMobile.iOS.Phone.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.sln"
            Configuration = "Debug|iPhone"
            Target = "Build"
        })
)

Target "ios-appstore-build" (fun () ->
    RestorePackages "CompassMobile.iOS.Phone/CompassMobile.iOS.Phone.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.sln"
            Configuration = "AppStore|iPhone"
            Target = "Build"
        })
)

Target "org-ios-build" (fun () ->
    RestorePackages "CompassMobile.iOS.Phone/CompassMobile.iOS.Phone.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.iOS.Phone/CompassMobile.iOS.Phone.csproj"
            Configuration = "Debug|iPhone"
            Target = "Build"
        })
)

Target "ios-unit-tests" (fun () ->
    RestorePackages "CompassMobile.iOS.Tests/CompassMobile.iOS.Tests.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.iOS.Tests/CompassMobile.iOS.Tests.csproj"
            Configuration = "Debug|iPhone"
            Target = "Build"
        })
)

Target "ios-uitests" (fun () ->
    RunUITests "ios"
)


Target "ios-appstore" (fun () ->
    RestorePackages "CompassMobile.iOS.Phone/CompassMobile.iOS.Phone.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.sln"
            Configuration = "AppStore|iPhone"
            Target = "Build"
        })

    let outputFolder = Path.Combine("CompassMobile.iOS.Phone", "bin", "iPhone", "AppStore")
    let appPath = Directory.EnumerateDirectories(outputFolder, "*.app").First()
    let zipFilePath = Path.Combine(outputFolder, "CompassMobile.iOS.Phone.zip")
    let zipArgs = String.Format("-r -y '{0}' '{1}'", zipFilePath, appPath)

    Exec "zip" zipArgs

    TeamCityHelper.PublishArtifact zipFilePath
)

Target "ios-testcloud" (fun () ->
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.sln"
            Configuration = "AppStore|iPhone"
            Target = "Build"
        })

    let outputFolder = Path.Combine("CompassMobile.iOS.Phone", "bin", "iPhone", "AppStore")
    let appPath = Directory.EnumerateDirectories(outputFolder, "*.app").First()
    let zipFilePath = Path.Combine(outputFolder, "CompassMobile.iOS.Phone.zip")
    let zipArgs = String.Format("-r -y '{0}' '{1}'", zipFilePath, appPath)

    Exec "zip" zipArgs

    TeamCityHelper.PublishArtifact zipFilePath

    let appPath = Directory.EnumerateFiles(Path.Combine("CompassMobile.iOS.Phone","bin", "iPhone", "AppStore"), "*.ipa").First()

    "5781a530" |> RunTestCloudTests appPath
)


Target "android-build" (fun () ->
    RestorePackages "CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj"

    MSBuild "CompassMobile.Droid.Phone/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj" ] |> ignore
)

Target "android-uitests" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj"
            Configuration = "Debug"
            OutputPath = "CompassMobile.Droid.Phone/bin/Debug"
        }) |> ignore

    AndroidPackage (fun defaults ->
            {defaults with
                ProjectPath = "CompassMobile.Droid.Tests/CompassMobile.Droid.Tests.csproj"
                Configuration = "Debug"
                OutputPath = "CompassMobile.Droid.Tests/bin/Debug"
        }) |> ignore

    RunUITests "android"
)


Target "android-testcloud" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.sln"
            Configuration = "Debug"
            OutputPath = "CompassMobile.Droid.Phone/bin/Debug"
        }) |> ignore

    let appPath = Directory.EnumerateFiles(Path.Combine("CompassMobile.Droid.Phone","bin", "Debug"), "*.apk", SearchOption.AllDirectories).First()

    "d299529d" |> RunTestCloudTests appPath
)


Target "android-package" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj"
            Configuration = "Debug"
            OutputPath = "CompassMobile.Droid.Phone/bin/Debug"
        })
        |> AndroidSignAndAlign (fun defaults ->
            {defaults with
                KeystorePath = "compassMobile.keystore" //TODO: replace with real values
                KeystorePassword = "compassMobile" // TODO: don't store this in the build script for a real app!
                KeystoreAlias = "compassMobile"
            })
            |> fun file -> TeamCityHelper.PublishArtifact file.FullName
)


Target "clean-all" ( fun() ->
    CleanDirs ["CompassMobile.Droid.Phone/bin"; "CompassMobile.Shared.Tests/bin"; "CompassMobile.UITests.Phone/bin"; "CompassMobile.iOS.Phone/bin"; "CompassMobile.iOS.Tests/bin"]

)

"clean-all"
    ==> "common-build"

"common-build"
    ==> "common-tests-build"
    ==> "common-tests"

"common-build"
    ==> "ios-build"

"ios-build"
    ==> "ios-uitests"

"common-build"
    ==> "android-build"

"android-build"
    ==> "android-uitests"

"ios-build"
    ==> "ios-appstore"


RunTarget()
