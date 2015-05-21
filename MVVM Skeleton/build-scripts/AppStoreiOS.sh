#!/bin/sh

### Grab all the nuget packages
/usr/bin/nuget restore CompassMobile.sln

### Clean out any old builds
rm -rf ./CompassMobile.Droid/bin
rm -rf ./CompassMobile.Droid/obj
rm -rf ./CompassMobile.Droid.Phone/bin
rm -rf ./CompassMobile.Droid.Phone/obj
rm -rf ./CompassMobile.Droid.Tests/bin
rm -rf ./CompassMobile.Droid.Tests/obj
rm -rf ./CompassMobile.iOS/bin
rm -rf ./CompassMobile.iOS/obj
rm -rf ./CompassMobile.iOS.Phone/bin
rm -rf ./CompassMobile.iOS.Phone/obj
rm -rf ./CompassMobile.iOS.Tests/bin
rm -rf ./CompassMobile.iOS.Tests/obj
rm -rf ./CompassMobile.Shared.BL/bin
rm -rf ./CompassMobile.Shared.BL/obj
rm -rf ./CompassMobile.Shared.Bootstrapper/bin
rm -rf ./CompassMobile.Shared.Bootstrapper/obj
rm -rf ./CompassMobile.Shared.Common/bin
rm -rf ./CompassMobile.Shared.Common/obj
rm -rf ./CompassMobile.Shared.DAL/bin
rm -rf ./CompassMobile.Shared.DAL/obj
rm -rf ./CompassMobile.Shared.SAL/bin
rm -rf ./CompassMobile.Shared.SAL/obj
rm -rf ./CompassMobile.Shared.Tests/bin
rm -rf ./CompassMobile.Shared.Tests/obj
rm -rf ./CompassMobile.Shared.VM/bin
rm -rf ./CompassMobile.Shared.VM/obj
rm -rf ./CompassMobile.UITests.Phone/bin
rm -rf ./CompassMobile.UITests.Phone/obj

### iOS : build the iOS app
/Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool -v build "--configuration:AppStore|iPhone" ./CompassMobile.sln
