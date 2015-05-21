#!/bin/sh

export XTC_API_KEY=fdd8ffdd0f98818b7dbf418bbe6699ee
export USER=luis.lafer-sousa@parivedasolutions.com

if [[ $1 == "A" ]]; then
    echo "Testing all devices (42)"
    export DEVICE_ID=8b5a73a1
elif [[ $1 == "M" ]]; then
    echo "Testing minimum device subset (5)"
    export DEVICE_ID=f1fe2498
else
    echo "Testing single device (1)"
    export DEVICE_ID=8951141d
fi

export TESTCLOUD=./packages/Xamarin.UITest.0.7.2.1812-dev/tools/test-cloud.exe
export TEST_ASSEMBLIES=./CompassMobile.UITests.Phone/bin/Debug/
export IPA=./CompassMobile.iOS.Phone/bin/iPhone/Debug/CompassMobile.iOS.Phone.ipa

### Grab all the nuget packages
/usr/bin/nuget restore CompassMobile.sln

### Uploading the dSYM files is optional - but it can help with troubleshooting
export DSYM=./CompassMobile.iOS.Phone/bin/iPhone/Debug/CompassMobileiOSPhone.app.dSYM

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

### iOS : build and submit the iOS app for testing
/Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool -v build "--configuration:TestCloud|iPhone" ./CompassMobile.sln

/usr/bin/mono $TESTCLOUD submit $IPA $XTC_API_KEY --user $USER --devices $DEVICE_ID --series "iOS" --locale "en_US" --assembly-dir $TEST_ASSEMBLIES --app-name "Health Pro Cloud" --dsym $DSYM
