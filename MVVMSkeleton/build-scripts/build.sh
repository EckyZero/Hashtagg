#!/bin/bash

mono --runtime=v4.0 build-scripts/tools/NuGet/NuGet.exe install FAKE -Version 3.26.7 -Output packages
mono --runtime=v4.0 packages/FAKE.3.26.7/tools/FAKE.exe build-scripts/build.fsx $@
