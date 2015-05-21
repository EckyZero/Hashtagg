@echo off

SET msbuild="C:\Program Files (x86)\MSBuild\12.0\Bin\msbuild.exe"

echo Cleaning Projects
%msbuild% ../CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj /p:Configuration=Release /t:Clean /fileLogger >nul
echo done
echo.

echo Building Package
%msbuild% /t:Package /p:Configuration=Release /p:AndroidUseSharedRuntime=false /p:EmbedAssembliesIntoApk=true ../CompassMobile.Droid.Phone/CompassMobile.Droid.Phone.csproj /fileLogger >nul
echo done
echo.

pause


