#r @"FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

Target "common-build" (fun () ->
    RestorePackages "Demo.sln"

    MSBuild "Demo.Shared/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "Demo.Shared/Demo.Shared.csproj" ] |> ignore
)

Target "common-tests-build" (fun () ->
    RestorePackages "Demo.Shared.Tests/Demo.Shared.Tests.csproj"

    MSBuild "Demo.Shared.Tests/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] ["Demo.Shared.Tests/Demo.Shared.Tests.csproj" ] |> ignore
)

Target "common-tests" (fun () ->
    RunNUnitTests "Demo.Shared.Tests/bin/Debug/Demo.Shared.Tests.dll" "Demo.Shared.Tests/bin/Debug/testresults.xml"
)

Target "ios-build" (fun () ->
    RestorePackages "iOS/Demo.iOS.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "Demo.sln"
            Configuration = "Debug|iPhoneSimulator"
            Target = "Build"
        })
)

Target "org-ios-build" (fun () ->
    RestorePackages "iOS/Demo.iOS.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "iOS/Demo.iOS.csproj"
            Configuration = "Debug|iPhoneSimulator"
            Target = "Build"
        })
)

Target "ios-unit-tests" (fun () ->
    RestorePackages "Demo.iOS.Tests/Demo.iOS.Tests.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "Demo.iOS.Tests/Demo.iOS.Tests.csproj"
            Configuration = "Debug|iPhoneSimulator"
            Target = "Build"
        })
)

Target "ios-uitests" (fun () ->
    RunUITests "ios"
)


Target "ios-appstore" (fun () ->
    RestorePackages "iOS/Demo.iOS.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "Demo.sln"
            Configuration = "AppStore|iPhone"
            Target = "Build"
        })

    let outputFolder = Path.Combine("iOS", "bin", "iPhone", "AppStore")
    let appPath = Directory.EnumerateDirectories(outputFolder, "*.app").First()
    let zipFilePath = Path.Combine(outputFolder, "Demo.iOS.zip")
    let zipArgs = String.Format("-r -y '{0}' '{1}'", zipFilePath, appPath)

    Exec "zip" zipArgs

    TeamCityHelper.PublishArtifact zipFilePath
)

Target "ios-testcloud" (fun () ->

    RestorePackages "iOS/Demo.iOS.csproj"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "iOS/Demo.iOS.csproj"
            Configuration = "Debug|iPhone"
            Target = "Build"
        })

    let appPath = Directory.EnumerateFiles(Path.Combine("iOS","bin", "iPhone", "Debug"), "*.ipa").First()

    getBuildParam "devices" |> RunTestCloudTests appPath
)


Target "android-build" (fun () ->
    RestorePackages "Android/Demo.Android.csproj"

    MSBuild "Android/bin/Debug" "Build" [ ("Configuration", "Debug") ] [ "Android/Demo.Android.csproj" ] |> ignore
)

Target "android-uitests" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "Android/Demo.Android.csproj"
            Configuration = "Debug"
            OutputPath = "Android/bin/Debug"
        }) |> ignore

    AndroidPackage (fun defaults ->
            {defaults with
                ProjectPath = "Demo.Android.Tests/Demo.Android.Tests.csproj"
                Configuration = "Debug"
                OutputPath = "Demo.Android.Tests/bin/Debug"
        }) |> ignore

    RunUITests "android"
)


Target "android-testcloud" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "Android/Demo.Android.csproj"
            Configuration = "Release"
            OutputPath = "Android/bin/Release"
        }) |> ignore

    let appPath = Directory.EnumerateFiles(Path.Combine("Android","bin", "Release"), "*.apk", SearchOption.AllDirectories).First()

    getBuildParam "devices" |> RunTestCloudTests appPath
)


Target "android-package" (fun () ->
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = "Android/Demo.Android.csproj"
            Configuration = "Release"
            OutputPath = "Android/bin/Release"
        })
        |> AndroidSignAndAlign (fun defaults ->
            {defaults with
                KeystorePath = "demo.keystore" //TODO: replace with real values
                KeystorePassword = "demo" // TODO: don't store this in the build script for a real app!
                KeystoreAlias = "demo"
            })
            |> fun file -> TeamCityHelper.PublishArtifact file.FullName
)


Target "clean-all" ( fun() ->
    CleanDirs ["Android/bin"; "Demo.Shared/bin"; "Demo.Shared.Tests/bin"; "Demo.UITests/bin"; "iOS/bin"; "Demo.iOS.Tests/bin"]

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
