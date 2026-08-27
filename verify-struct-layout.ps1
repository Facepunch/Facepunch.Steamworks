# Enforces that the marshalled memory layout of every struct in the binding still matches
# the committed baseline in Tools/baselines/.
#
# WHY THIS EXISTS
# ---------------
# Steam writes callback structs directly into memory that managed code reads back with
# Marshal.PtrToStructure. If a managed layout disagrees with the C++ one, nothing throws -
# you silently read the wrong fields, or read past the end of Steam's buffer. That is the
# worst failure mode in this codebase: invisible, non-deterministic, and nearly impossible
# to attribute after the fact.
#
# The struct layouts are produced by the code generator, so an innocuous-looking generator
# tweak can silently re-lay-out dozens of structs at once. This pins all of them, so any
# such change surfaces as an explicit, reviewable diff.
#
# It is also the prerequisite for fixing the known Pack heuristic defect described in
# docs/audit/01-marshaling-abi.md - 20 structs are laid out incorrectly today, and that
# fix touches how every generated struct is emitted. Without this baseline the change
# would be unverifiable, especially with no Steam client available to test against.
#
# Needs no Steam client, account or network.
#
#   powershell -ExecutionPolicy Bypass -File verify-struct-layout.ps1
#   powershell -ExecutionPolicy Bypass -File verify-struct-layout.ps1 -Record   # accept changes

param(
    [string]$Configuration = "Release",
    [switch]$Record
)

$ErrorActionPreference = "Stop"
$repo = $PSScriptRoot

$tool = Join-Path $repo "Tools\Steamworks.LayoutBaseline\Steamworks.LayoutBaseline.csproj"
$assembly = Join-Path $repo "Facepunch.Steamworks\bin\$Configuration\net6.0\Facepunch.Steamworks.Win64.dll"
$baseline = Join-Path $repo "Tools\baselines\layout-win64.txt"

Write-Host "Building the library and the layout tool..."
dotnet build (Join-Path $repo "Facepunch.Steamworks\Facepunch.Steamworks.Win64.csproj") -c $Configuration -v q --nologo | Out-Null
dotnet build $tool -c $Configuration -v q --nologo | Out-Null

$runner = Join-Path $repo "Tools\Steamworks.LayoutBaseline\bin\$Configuration\net8.0\Steamworks.LayoutBaseline.dll"
if (-not (Test-Path $runner)) { throw "Layout tool was not produced at $runner" }
if (-not (Test-Path $assembly)) { throw "Managed assembly not found: $assembly" }

if ($Record) {
    Write-Host "Recording a NEW baseline. Commit it alongside the change that caused it."
    & dotnet $runner record $assembly $baseline
    exit $LASTEXITCODE
}

& dotnet $runner check $assembly $baseline
$code = $LASTEXITCODE

if ($code -ne 0) {
    Write-Host ""
    Write-Host "Struct layout changed. This is a native ABI change - review it carefully." -ForegroundColor Red
    Write-Host "If it is intended, re-run with -Record and commit the updated baseline."
}

exit $code
