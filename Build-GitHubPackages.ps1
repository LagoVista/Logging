param(
    [Parameter(Mandatory = $false)]
    [string]$Version = '5.0.1',

    [Parameter(Mandatory = $false)]
    [string]$OutputDirectory = './artifacts/packages',

    [Parameter(Mandatory = $false)]
    [string]$CatalogPath = './artifacts/package-catalog.json'
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$repoRoot = $PSScriptRoot
Set-Location $repoRoot

$solutionPath = Join-Path $repoRoot 'Logging.sln'
$propsPath = Join-Path $repoRoot 'Directory.Packages.props'
$outputPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $OutputDirectory))
$catalogFullPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $CatalogPath))

if (-not (Test-Path $solutionPath)) { throw "Solution not found: $solutionPath" }
if (-not (Test-Path $propsPath)) { throw "Directory.Packages.props not found: $propsPath" }

if (Test-Path $outputPath) {
    Remove-Item -Recurse -Force $outputPath
}
New-Item -ItemType Directory -Force -Path $outputPath | Out-Null
New-Item -ItemType Directory -Force -Path (Split-Path $catalogFullPath -Parent) | Out-Null

[xml]$props = Get-Content $propsPath -Raw
$centralVersions = @{}
foreach ($node in @($props.SelectNodes('//PackageVersion'))) {
    $id = [string]$node.Include
    $versionValue = [string]$node.Version
    if (-not [string]::IsNullOrWhiteSpace($id) -and -not [string]::IsNullOrWhiteSpace($versionValue)) {
        $centralVersions[$id] = $versionValue
    }
}

$packageDefinitions = @(
    [pscustomobject]@{
        Id = 'LagoVista.IoT.Logging'
        Project = 'src/LagoVista.IoT.Logging/LagoVista.IoT.Logging.csproj'
        Nuspec = 'src/LagoVista.IoT.Logging/Package.nuspec'
    },
    [pscustomobject]@{
        Id = 'LagoVista.LogZIO'
        Project = 'src/LagoVista.LogZIO/LagoVista.LogZIO.csproj'
        Nuspec = 'src/LagoVista.LogZIO/Package.nuspec'
    }
)

$internalDependencies = @{}
$internalPackagePrefix = 'LagoVista.'

foreach ($package in $packageDefinitions) {
    $projectPath = Join-Path $repoRoot $package.Project
    $nuspecPath = Join-Path $repoRoot $package.Nuspec

    if (-not (Test-Path $projectPath)) { throw "Project not found: $projectPath" }
    if (-not (Test-Path $nuspecPath)) { throw "NuSpec not found: $nuspecPath" }

    [xml]$projectXml = Get-Content $projectPath -Raw
    [xml]$nuspecXml = Get-Content $nuspecPath -Raw

    $nuspecId = [string]$nuspecXml.package.metadata.id
    if ($nuspecId -ne $package.Id) {
        throw "NuSpec id '$nuspecId' does not match expected package id '$($package.Id)'."
    }

    $directInternalDependencies = @()
    foreach ($reference in @($projectXml.SelectNodes('//PackageReference'))) {
        $id = [string]$reference.Include
        if ($id.StartsWith($internalPackagePrefix, [System.StringComparison]::OrdinalIgnoreCase)) {
            $resolvedVersion = [string]$reference.Version
            if ([string]::IsNullOrWhiteSpace($resolvedVersion)) {
                if (-not $centralVersions.ContainsKey($id)) {
                    throw "Internal package '$id' in '$($package.Project)' does not have a centrally managed version."
                }
                $resolvedVersion = $centralVersions[$id]
            }

            $directInternalDependencies += [pscustomobject]@{
                id = $id
                version = $resolvedVersion
                source = 'PackageReference'
            }
        }
    }

    foreach ($reference in @($projectXml.SelectNodes('//ProjectReference'))) {
        $include = [string]$reference.Include
        $referencedPath = [System.IO.Path]::GetFullPath((Join-Path (Split-Path $projectPath -Parent) $include))
        $referencedDefinition = $packageDefinitions | Where-Object {
            [System.IO.Path]::GetFullPath((Join-Path $repoRoot $_.Project)) -eq $referencedPath
        } | Select-Object -First 1

        if ($null -ne $referencedDefinition) {
            $directInternalDependencies += [pscustomobject]@{
                id = $referencedDefinition.Id
                version = $Version
                source = 'ProjectReference'
            }
        }
    }

    $internalDependencies[$package.Id] = @($directInternalDependencies | Sort-Object id -Unique)

    $nuspecInternalVersions = @{}
    foreach ($dependency in @($nuspecXml.SelectNodes('//dependency'))) {
        $dependencyId = [string]$dependency.id
        if ($dependencyId.StartsWith($internalPackagePrefix, [System.StringComparison]::OrdinalIgnoreCase)) {
            $nuspecInternalVersions[$dependencyId] = [string]$dependency.version
        }
    }

    foreach ($dependency in $internalDependencies[$package.Id]) {
        if (-not $nuspecInternalVersions.ContainsKey($dependency.id)) {
            throw "NuSpec '$($package.Nuspec)' is missing internal dependency '$($dependency.id)'."
        }
    }

    foreach ($dependencyId in $nuspecInternalVersions.Keys) {
        $expected = $internalDependencies[$package.Id] | Where-Object id -eq $dependencyId | Select-Object -First 1
        if ($null -eq $expected) {
            throw "NuSpec '$($package.Nuspec)' declares internal dependency '$dependencyId' that is not present in the project dependency graph."
        }
    }

    $nuspecXml.package.metadata.version = $Version
    foreach ($dependency in @($nuspecXml.SelectNodes('//dependency'))) {
        $dependencyId = [string]$dependency.id
        $expected = $internalDependencies[$package.Id] | Where-Object id -eq $dependencyId | Select-Object -First 1
        if ($null -ne $expected) {
            $dependency.version = $expected.version
        }
    }

    $settings = New-Object System.Xml.XmlWriterSettings
    $settings.Indent = $true
    $settings.Encoding = New-Object System.Text.UTF8Encoding($false)
    $writer = [System.Xml.XmlWriter]::Create($nuspecPath, $settings)
    try {
        $nuspecXml.Save($writer)
    }
    finally {
        $writer.Dispose()
    }
}

Write-Host 'Restoring Logging solution'
dotnet restore $solutionPath --configfile NuGet.config
if ($LASTEXITCODE -ne 0) { throw 'dotnet restore failed.' }

Write-Host 'Building Logging solution'
dotnet build $solutionPath -c Release --no-restore
if ($LASTEXITCODE -ne 0) { throw 'dotnet build failed.' }

foreach ($package in $packageDefinitions) {
    $nuspecPath = Join-Path $repoRoot $package.Nuspec
    Write-Host "Packing $($package.Id) $Version"
    nuget pack $nuspecPath -OutputDirectory $outputPath -Version $Version -Properties Configuration=Release
    if ($LASTEXITCODE -ne 0) { throw "nuget pack failed for '$($package.Id)'." }

    $expectedPackage = Join-Path $outputPath "$($package.Id).$Version.nupkg"
    if (-not (Test-Path $expectedPackage)) {
        throw "Expected package was not created: $expectedPackage"
    }
}

$sourceRepository = if ($env:GITHUB_REPOSITORY) { $env:GITHUB_REPOSITORY } else { 'LagoVista/Logging' }
$sourceCommit = if ($env:GITHUB_SHA) { $env:GITHUB_SHA } else { (git rev-parse HEAD).Trim() }
$sourceRef = if ($env:GITHUB_REF_NAME) { $env:GITHUB_REF_NAME } else { (git branch --show-current).Trim() }

$catalogPackages = @()
foreach ($package in $packageDefinitions) {
    $catalogPackages += [ordered]@{
        id = $package.Id
        version = $Version
        file = "$($package.Id).$Version.nupkg"
        targetFrameworks = @('netstandard2.1')
        internalDependencies = @($internalDependencies[$package.Id])
    }
}

$catalog = [ordered]@{
    schemaVersion = 1
    generatedUtc = [DateTime]::UtcNow.ToString('o')
    source = [ordered]@{
        repository = $sourceRepository
        commit = $sourceCommit
        ref = $sourceRef
    }
    packages = $catalogPackages
}

$catalog | ConvertTo-Json -Depth 8 | Set-Content -Path $catalogFullPath -Encoding UTF8
Write-Host "Package catalog written to $catalogFullPath"
Write-Host "Logging package set $Version built successfully."
