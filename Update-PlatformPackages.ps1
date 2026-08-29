param(
    [Parameter(Mandatory = $false)]
    [string]$CatalogPath,

    [Parameter(Mandatory = $false)]
    [string]$PropsPath = './Directory.Packages.props'
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$repoRoot = $PSScriptRoot
Set-Location $repoRoot

$propsFullPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $PropsPath))
if (-not (Test-Path $propsFullPath)) {
    throw "Directory.Packages.props not found: $propsFullPath"
}

if ([string]::IsNullOrWhiteSpace($CatalogPath)) {
    if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
        throw 'GitHub CLI (gh) is required when -CatalogPath is not supplied.'
    }

    Write-Host 'Reading package catalog from nuviot/platform main...'
    $catalogJson = gh api `
        -H 'Accept: application/vnd.github.raw+json' `
        '/repos/nuviot/platform/contents/catalog/packages.json?ref=main'

    if ($LASTEXITCODE -ne 0) {
        throw "Unable to read nuviot/platform catalog with GitHub CLI. Exit code: $LASTEXITCODE"
    }
}
else {
    $catalogFullPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $CatalogPath))
    if (-not (Test-Path $catalogFullPath)) {
        throw "Catalog not found: $catalogFullPath"
    }

    Write-Host "Reading package catalog from $catalogFullPath..."
    $catalogJson = Get-Content $catalogFullPath -Raw
}

$catalog = $catalogJson | ConvertFrom-Json
$versions = @{}
foreach ($package in @($catalog.packages)) {
    if ([string]::IsNullOrWhiteSpace($package.id) -or [string]::IsNullOrWhiteSpace($package.version)) {
        throw 'Catalog contains a package without an id or version.'
    }
    $versions[$package.id] = $package.version
}

[xml]$props = Get-Content $propsFullPath -Raw
$packageVersionNodes = @($props.SelectNodes('//PackageVersion'))
$internalNodes = @($packageVersionNodes | Where-Object {
    $id = $_.Include
    $id -and ($id.StartsWith('LagoVista.', [System.StringComparison]::OrdinalIgnoreCase) -or
              $id.StartsWith('NuvIoT.', [System.StringComparison]::OrdinalIgnoreCase))
})

if ($internalNodes.Count -eq 0) {
    Write-Host 'No platform-managed package versions found.'
    exit 0
}

$missing = @($internalNodes | Where-Object { -not $versions.ContainsKey($_.Include) } | ForEach-Object { $_.Include })
if ($missing.Count -gt 0) {
    throw "Platform catalog is missing required internal package(s): $($missing -join ', ')"
}

$changes = @()
foreach ($node in $internalNodes) {
    $id = $node.Include
    $oldVersion = [string]$node.Version
    $newVersion = [string]$versions[$id]

    if ($oldVersion -ne $newVersion) {
        $changes += [pscustomobject]@{
            Id = $id
            OldVersion = $oldVersion
            NewVersion = $newVersion
        }
        $node.SetAttribute('Version', $newVersion)
    }
}

if ($changes.Count -eq 0) {
    Write-Host 'Platform package versions are already current.'
    exit 0
}

$settings = New-Object System.Xml.XmlWriterSettings
$settings.Indent = $true
$settings.Encoding = New-Object System.Text.UTF8Encoding($false)
$settings.NewLineChars = "`n"
$settings.NewLineHandling = [System.Xml.NewLineHandling]::Replace

$writer = [System.Xml.XmlWriter]::Create($propsFullPath, $settings)
try {
    $props.Save($writer)
}
finally {
    $writer.Dispose()
}

Write-Host ''
Write-Host 'Platform package updates:'
foreach ($change in $changes) {
    Write-Host "  $($change.Id): $($change.OldVersion) -> $($change.NewVersion)"
}
Write-Host ''
Write-Host "$($changes.Count) package version(s) updated in Directory.Packages.props."
