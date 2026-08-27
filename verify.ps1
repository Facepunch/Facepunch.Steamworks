# Runs every check that does NOT need Steam. This is the primary gate - run it locally
# before every commit. CI runs the same scripts, but CI is the backstop, not the source of
# truth: if this passes on your machine the change is good, and if it fails, CI will not
# save you.
#
# Nothing here needs a Steam client, a Steam account, a Steam login, or a network
# connection. That is deliberate - it means the whole gate is runnable by anyone, on any
# machine, at any time, which is the only reason it actually gets run.
#
#   powershell -ExecutionPolicy Bypass -File verify.ps1
#   powershell -ExecutionPolicy Bypass -File verify.ps1 -Configuration Debug
#
# Exit code 0 = everything passed. Non-zero = at least one gate failed.

param(
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Continue"
$repo = $PSScriptRoot
$failures = @()

function Section($name) {
    Write-Host ""
    Write-Host "=== $name " -NoNewline
    Write-Host ("=" * [Math]::Max(0, 60 - $name.Length))
}

# ---------------------------------------------------------------------------------------
# 1. Build. Every target framework of every platform project, plus the tools and tests.
# ---------------------------------------------------------------------------------------
Section "Build ($Configuration)"
$build = & dotnet build (Join-Path $repo "Facepunch.Steamworks.sln") -c $Configuration --nologo 2>&1
$buildOk = $LASTEXITCODE -eq 0

$errors   = @($build | Select-String -Pattern " error " -SimpleMatch)
$warnings = @($build | Select-String -Pattern " warning " -SimpleMatch)

if ($buildOk) {
    Write-Host "PASS  build succeeded ($($warnings.Count) warning lines)" -ForegroundColor Green
}
else {
    Write-Host "FAIL  build failed" -ForegroundColor Red
    $errors | Select-Object -First 20 | ForEach-Object { Write-Host "      $_" }
    $failures += "build"
}

# ---------------------------------------------------------------------------------------
# 2. Native export conformance.
#    Asserts every DllImport entry point exists in all 17 committed native binaries.
#    Entry points are plain strings the compiler never checks, so this is the only thing
#    standing between a symbol Valve removed and an EntryPointNotFoundException on a
#    player's machine.
# ---------------------------------------------------------------------------------------
Section "Native export conformance"
if ($buildOk) {
    & powershell -ExecutionPolicy Bypass -File (Join-Path $repo "verify-native-conformance.ps1") -Configuration $Configuration |
        Select-Object -Last 1 | ForEach-Object { Write-Host "      $_" }
    if ($LASTEXITCODE -ne 0) { $failures += "native-conformance" }
}
else {
    Write-Host "SKIP  build failed" -ForegroundColor Yellow
    $failures += "native-conformance (skipped)"
}

# ---------------------------------------------------------------------------------------
# 3. Struct layout baseline.
#    Steam writes these structs directly into memory we read back, so a layout mismatch
#    silently returns wrong data rather than throwing. The layouts are produced by the code
#    generator, where one small change can move dozens of structs at once.
# ---------------------------------------------------------------------------------------
Section "Struct layout baseline"
if ($buildOk) {
    & powershell -ExecutionPolicy Bypass -File (Join-Path $repo "verify-struct-layout.ps1") -Configuration $Configuration |
        Select-Object -Last 1 | ForEach-Object { Write-Host "      $_" }
    if ($LASTEXITCODE -ne 0) {
        $failures += "struct-layout"
        Write-Host "      A layout change is a native ABI change. If it is intended, re-run" -ForegroundColor Yellow
        Write-Host "      verify-struct-layout.ps1 -Record and commit the new baseline with it." -ForegroundColor Yellow
    }
}
else {
    Write-Host "SKIP  build failed" -ForegroundColor Yellow
    $failures += "struct-layout (skipped)"
}

# ---------------------------------------------------------------------------------------
# Summary
# ---------------------------------------------------------------------------------------
Section "Summary"
if ($failures.Count -eq 0) {
    Write-Host "ALL GATES PASSED" -ForegroundColor Green
    Write-Host ""
    Write-Host "Note: this covers everything checkable without Steam. It does NOT prove"
    Write-Host "runtime behaviour against a live Steam client - see run-live-steam-tests.ps1"
    Write-Host "for that, which needs Steam running and signed in."
    exit 0
}

Write-Host "FAILED: $($failures -join ', ')" -ForegroundColor Red
exit 1
