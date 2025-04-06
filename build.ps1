#!/usr/bin/env powershell

# & dotnet tool install --global GitVersion.Tool --version 5.*
# & brew install gitversion

param(
    [switch]$interactive
)

$name = "MongoDbGenericRepository"
$project = "./MongoDBGenericRepository/MongoDBGenericRepository.csproj"

Write-Output "Checking if GitVersion is installed..."

$exists = Get-Command gitversion -ErrorAction Ignore
$exists2 = Get-Command dotnet-gitversion -ErrorAction Ignore

if (!$exists) {
    if ($exists2) {
        Write-Output "GitVersion is installed, but as 'dotnet-gitversion'. Setting alias..."
        Set-Alias -Name gitversion -Value dotnet-gitversion
    }
    else {
        Write-Error "GitVersion is not installed. Please install it from https://gitversion.net/docs/installation."
        exit 1
    }
}

Write-Output "Using GitVersion to determine version..."
$json = & gitversion | ConvertFrom-Json

Write-Output $json

$source = "https://rise-x.pkgs.visualstudio.com/3e51f2c5-532a-4292-a53d-eaa48473daf9/_packaging/Diana.Services/nuget/v3/index.json"

Write-Output "Building and publishing Nuget package..."

$NuGetVersionV2 = $($json.NuGetVersionV2)
$FullSemVer = $($json.FullSemVer)
$AssemblySemVer = $($json.AssemblySemVer)
$MajorMinorPatch = $($json.AssemblySemVer)

$AssemblySemFileVer = "3.3.0"

Write-Output "Version: $($AssemblySemFileVer)"

Write-Output "Build Project $($project)"

& dotnet build "$($project)" --configuration "Release" $($interactive ? '--interactive' : '')

Write-Host "dotnet pack `"$($project)`" --configuration Release -p:Version=$($AssemblySemFileVer) -p:FileVersion=$($AssemblySemFileVer) -p:InformationalVersion=$($AssemblySemFileVer) -p:PackageVersion=$($AssemblySemFileVer) --output `"_publish`" $($interactive ? '--interactive' : '')"

& dotnet pack "$($project)" --no-build --no-restore --configuration "Release" -p:Version="$($AssemblySemFileVer)" -p:FileVersion="$($AssemblySemFileVer)" -p:InformationalVersion="$($AssemblySemFileVer)" -p:PackageVersion="$($AssemblySemFileVer)" --output "_publish" $($interactive ? '--interactive' : '')

$packageName = "$($name).$($AssemblySemFileVer).nupkg"
$pushCommand = "dotnet nuget push ./_publish/$($packageName) --source $source --timeout 900 --api-key ""Cli\"""

if ($interactive) {
    $pushCommand += " --interactive"
}

Write-Output $pushCommand

Invoke-Expression $pushCommand

Write-Output "Successfully published Nuget package. Add to your project with:"
Write-Output "dotnet add package $($name) --version $($AssemblySemFileVer)"
