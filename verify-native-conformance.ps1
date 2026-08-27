# Verifies that every native symbol this library P/Invokes actually exists in EVERY
# Steamworks binary committed to this repository - for Windows, Linux and macOS.
#
# WHY THIS EXISTS
# ---------------
# A DllImport entry point is just a string. Nothing in the C# compiler checks it. When
# Valve renames or removes a function - which they do between SDK releases - the binding
# still compiles and only blows up as an EntryPointNotFoundException the first time a game
# calls it, typically in a shipped build on a player's machine.
#
# This check catches that class of defect statically. It needs NO Steam client, NO Steam
# account, NO Steam login and NO network, so it runs on any build agent. For a P/Invoke
# binding library it is the single strongest correctness guarantee available without a
# live Steam environment.
#
# It has already caught two real defects:
#   * ISteamAppList - 6 bindings to functions Valve deleted from the SDK.
#   * A whole set of stale native binaries (win64/, linux32/, linux64/, osx/) that were
#     ~11 SDK releases behind and missing 36 entry points, including
#     SteamInternal_GameServer_Init_V2 - meaning a Linux dedicated server built against
#     them could not even initialise.
#
#   powershell -ExecutionPolicy Bypass -File verify-native-conformance.ps1
#   powershell -ExecutionPolicy Bypass -File verify-native-conformance.ps1 -Configuration Debug

param(
    [string]$Configuration = "Release",
    [switch]$Verbose
)

$ErrorActionPreference = "Stop"
$repo = $PSScriptRoot

# The checker itself targets net8.0; the library under test is read as metadata only, so
# its own target framework does not matter. net6.0 is used simply because all three
# platform projects produce it.
$tool = Join-Path $repo "Tools\Steamworks.Conformance\Steamworks.Conformance.csproj"
$managedDir = Join-Path $repo "Facepunch.Steamworks\bin\$Configuration\net6.0"

Write-Host "Building the library and the conformance checker..."
dotnet build (Join-Path $repo "Facepunch.Steamworks\Facepunch.Steamworks.Win64.csproj") -c $Configuration -v q --nologo | Out-Null
dotnet build (Join-Path $repo "Facepunch.Steamworks\Facepunch.Steamworks.Win32.csproj") -c $Configuration -v q --nologo | Out-Null
dotnet build (Join-Path $repo "Facepunch.Steamworks\Facepunch.Steamworks.Posix.csproj") -c $Configuration -v q --nologo | Out-Null
dotnet build $tool -c $Configuration -v q --nologo | Out-Null

$checker = Join-Path $repo "Tools\Steamworks.Conformance\bin\$Configuration\net8.0\Steamworks.Conformance.dll"
if (-not (Test-Path $checker)) { throw "Conformance checker was not produced at $checker" }

# Map each native binary to the managed assembly whose bindings it must satisfy.
#   steam_api64.dll   -> the Win64 assembly
#   steam_api.dll     -> the Win32 assembly
#   .so / .dylib      -> the Posix assembly
$win64 = Join-Path $managedDir "Facepunch.Steamworks.Win64.dll"
$win32 = Join-Path $managedDir "Facepunch.Steamworks.Win32.dll"
$posix = Join-Path $managedDir "Facepunch.Steamworks.Posix.dll"

foreach ($a in @($win64, $win32, $posix)) {
    if (-not (Test-Path $a)) { throw "Managed assembly not found: $a" }
}

# Every committed native binary, excluding build output and any tooling directory.
#
# The dot-directory exclusion matters: .claude/worktrees/ can hold complete checkouts of
# this same repo while other agent sessions are running, and each contributes its own copy
# of all 17 binaries. Without this the script silently checks 34 or 51 files and reports a
# pass count that looks wrong (and would keep passing even if the real tree regressed,
# since the copies are checked too). Only the working tree should be verified.
$natives = Get-ChildItem -Path $repo -Recurse -File -Include "steam_api.dll", "steam_api64.dll", "libsteam_api.so", "libsteam_api.dylib" |
    Where-Object {
        $_.FullName -notmatch '\\(bin|obj)\\' -and
        $_.FullName -notmatch '\\\.[^\\]+\\'      # .git, .claude, .vs, ...
    } |
    Sort-Object FullName

if ($natives.Count -eq 0) { throw "No native Steamworks binaries found under $repo" }

Write-Host ""
Write-Host "Checking $($natives.Count) native binaries..."
Write-Host ""

$failed = 0
foreach ($n in $natives) {
    $managed = switch -Wildcard ($n.Name) {
        "steam_api64.dll"    { $win64 }
        "steam_api.dll"      { $win32 }
        default              { $posix }
    }

    $relative = $n.FullName.Substring($repo.Length).TrimStart('\')
    $args = @($checker, $managed, $n.FullName)
    if ($Verbose) { $args += "-v" }

    $output = & dotnet @args 2>&1
    if ($LASTEXITCODE -ne 0) {
        $failed++
        Write-Host "  FAIL  $relative" -ForegroundColor Red
        $output | ForEach-Object { Write-Host "        $_" }
    }
    else {
        Write-Host "  PASS  $relative" -ForegroundColor Green
        if ($Verbose) { $output | ForEach-Object { Write-Host "        $_" } }
    }
}

Write-Host ""
if ($failed -gt 0) {
    Write-Host "$failed of $($natives.Count) native binaries FAILED conformance." -ForegroundColor Red
    Write-Host "Either a binding refers to a symbol Valve removed, or a committed binary is stale."
    exit 1
}

Write-Host "All $($natives.Count) native binaries passed conformance." -ForegroundColor Green
exit 0
