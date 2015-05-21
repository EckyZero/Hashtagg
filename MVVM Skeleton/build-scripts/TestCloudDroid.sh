#!/bin/sh

export XTC_API_KEY=fdd8ffdd0f98818b7dbf418bbe6699ee
export USER=luis.lafer-sousa@parivedasolutions.com

if [ $1 == "A" ]; then
    echo "Testing all devices (142)"
    export DEVICE_ID=c8648c6a
elif [ $1 == "M" ]; then
    echo "Testing minimum device subset (7)"
    export DEVICE_ID=7ec7282e
else
    echo "Testing single device (1)"
    export DEVICE_ID=ea440f93
fi

export TESTCLOUD=./packages/Xamarin.UITest.0.7.2.1812-dev/tools/test-cloud.exe
export TEST_ASSEMBLIES=./CompassMobile.UITests.Phone/bin/Debug/
export APK=./CompassMobile.Droid.Phone/bin/Debug/com.compassphs.healthprocloud.android.apk

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

### Shared : build the tests
/usr/bin/xbuild /p:Configuration=Debug ./CompassMobile.UITests.Phone/CompassMobile.UITests.Phone.csproj

### Android: Build and submit the Android app for testing using the default keystore
/usr/bin/xbuild /t:Package /p:Configuration=TestCloud /p:Outputpath="../CompassMobile.Droid.Phone/bin/Debug/" /p:AndroidUseSharedRuntime=false /p:EmbedAssembliesIntoApk=true ./CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj

/usr/bin/mono $TESTCLOUD submit $APK $XTC_API_KEY --user $USER --devices $DEVICE_ID --series "Android" --locale "en_US" --assembly-dir $TEST_ASSEMBLIES --app-name "Health Pro Cloud"

