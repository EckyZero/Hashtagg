#!/bin/bash

build-scripts/genymotion.sh 19

mono --runtime=v4.0 build-scripts/tools/NuGet/NuGet.exe install FAKE -Version 3.9.9 -Output packages
mono --runtime=v4.0 packages/FAKE.3.9.9/tools/FAKE.exe build-scripts/build.fsx $@
