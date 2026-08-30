param(
    [Parameter(Mandatory = $true)]
    [string]$Version
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$repoRoot = $PSScriptRoot
Set-Location $repoRoot

if ([string]::IsNullOrWhiteSpace($env:NUGET_GITHUB_TOKEN)) {
    throw 'NUGET_GITHUB_TOKEN is required to publish packages.'
}

$catalogPath = Join-Path $repoRoot 'artifacts/package-catalog.json'
$packagesPath = Join-Path $repoRoot 'artifacts/packages'

& (Join-Path $repoRoot 'Build-GitHubPackages.ps1') -Version $Version
if ($LASTEXITCODE -ne 0) { throw "Build-GitHubPackages.ps1 failed with exit code $LASTEXITCODE." }

if (-not (Test-Path $catalogPath)) { throw "Package catalog not found: $catalogPath" }
$catalog = Get-Content $catalogPath -Raw | ConvertFrom-Json
if ($null -eq $catalog.packages -or @($catalog.packages).Count -eq 0) { throw 'Package catalog contains no packages.' }

foreach ($package in @($catalog.packages)) {
    $packagePath = Join-Path $packagesPath $package.file
    if (-not (Test-Path $packagePath)) { throw "Package file not found: $packagePath" }

    Write-Host "Publishing $($package.id) $($package.version)..."
    dotnet nuget push $packagePath --source nuviot --api-key $env:NUGET_GITHUB_TOKEN --skip-duplicate
    if ($LASTEXITCODE -ne 0) { throw "dotnet nuget push failed for '$($package.id)' with exit code $LASTEXITCODE." }
}

$verifyRoot = Join-Path $repoRoot 'artifacts/verify'
if (Test-Path $verifyRoot) { Remove-Item -Recurse -Force $verifyRoot }
New-Item -ItemType Directory -Force -Path $verifyRoot | Out-Null

foreach ($package in @($catalog.packages)) {
    $packageVerifyRoot = Join-Path $verifyRoot $package.id
    New-Item -ItemType Directory -Force -Path $packageVerifyRoot | Out-Null
    Push-Location $packageVerifyRoot
    try {
        dotnet new classlib --framework net9.0 --no-restore | Out-Null
        if ($LASTEXITCODE -ne 0) { throw "dotnet new failed while verifying '$($package.id)'." }

        dotnet add package $package.id --version $package.version --source 'https://nuget.pkg.github.com/nuviot/index.json' --no-restore
        if ($LASTEXITCODE -ne 0) { throw "dotnet add package failed while verifying '$($package.id)'." }

        dotnet restore --configfile (Join-Path $repoRoot 'NuGet.config') --force --no-cache
        if ($LASTEXITCODE -ne 0) { throw "Remote restore verification failed for '$($package.id)' $($package.version)." }
    }
    finally {
        Pop-Location
    }

    Write-Host "Verified $($package.id) $($package.version) from GitHub Packages."
}

Write-Host "Published and verified $(@($catalog.packages).Count) packages at version $Version."
