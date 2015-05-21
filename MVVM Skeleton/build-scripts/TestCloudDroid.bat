@echo off

SET XTC_API_KEY=fdd8ffdd0f98818b7dbf418bbe6699ee
SET USER=luis.lafer-sousa@parivedasolutions.com
SET DEVICE_ID=ea440f93
SET TESTCLOUD="../packages/Xamarin.UITest.0.7.2.1812-dev/tools/test-cloud.exe"
SET TEST_ASSEMBLIES="../CompassMobile.UITests.Phone/bin/Debug/"
SET APK="../CompassMobile.Droid.Phone/bin/Debug/com.compassphs.healthprocloud.android.apk"
SET msbuild="C:\Program Files (x86)\MSBuild\12.0\Bin\msbuild.exe"

echo Cleaning Projects
%msbuild% ../CompassMobile.UITests.Phone/CompassMobile.UITests.Phone.csproj /p:Configuration=Debug /t:Clean /fileLogger >nul
%msbuild% ../CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj /p:Configuration=Debug /t:Clean /fileLogger >nul
echo done
echo.

echo Building Test Project
%msbuild% /p:Configuration=Debug ../CompassMobile.UITests.Phone/CompassMobile.UITests.Phone.csproj  /fileLogger >nul
echo done
echo.

echo Building Package
%msbuild% /t:Package /p:Configuration=TestCloud /p:Outputpath="../CompassMobile.Droid.Phone/bin/Debug/" /p:AndroidUseSharedRuntime=false /p:EmbedAssembliesIntoApk=true ../CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj /fileLogger 
echo done
echo.

%TESTCLOUD% submit %APK% %XTC_API_KEY% --user %USER% --devices %DEVICE_ID% --series "Android" --locale "en_US" --assembly-dir %TEST_ASSEMBLIES% --app-name "Health Pro Cloud"

pause

