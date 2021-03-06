#!/bin/bash

# -e  Exit immediately if a command exits with a non-zero status.
set -e

echo "Building project"

# Final output directory of NuPkgs.
NUPKG_DIR=$1

# Location of the nusepc file
NUSPEC_PATH="$2/.nuspec"

# Build configuration to build/pack.
BUILD_CONFIGURATION=Release

echo "Loading nuspec from: $NUSPEC_PATH"
echo "Output NuGet package to: $NUPKG_DIR"

# Forces sourcelink to work during the build.
export CI=true

dotnet build -c $BUILD_CONFIGURATION src/Google.Analytics.SDK.Core
dotnet build -c $BUILD_CONFIGURATION tests/Google.Analytics.SDK.Tests
dotnet test -c $BUILD_CONFIGURATION tests/Google.Analytics.SDK.Tests 
dotnet pack src/Google.Analytics.SDK.Core --configuration $BUILD_CONFIGURATION --no-restore --no-build --output $NUPKG_DIR  /p:NuspecFile=$NUSPEC_PATH
