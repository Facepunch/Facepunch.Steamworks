# Live-Steam runtime proof for the fork (Pass-3 CEO/CTO directive #1).
#
# Runs the Facepunch.Steamworks.Test NUnit suite against a RUNNING, LOGGED-IN
# Steam client. This is the ONLY proof that the upstream-merge re-marshaling
# (25+ interfaces, ICustomMarshaler removal, the NetIdentity + NetworkingMessages
# changes) actually works at the native callback boundary — CI/unit/fake-server
# coverage cannot reach it.
#
# REQUIRES: Steam running + logged in on this machine. The suite calls
# SteamClient.Init(1442910) once for the whole run (AppTest.AssemblyInitialize).
# It reads Steam state (friends, apps, user, inventory, server list); it does not
# purchase, delete, or post anything. It DOES advertise presence on app 1442910
# for the duration of the run.
#
#   powershell -ExecutionPolicy Bypass -File run-live-steam-tests.ps1            # safe read-only subset
#   powershell -ExecutionPolicy Bypass -File run-live-steam-tests.ps1 -Full      # entire suite

param([switch]$Full)

$ErrorActionPreference = "Stop"
$repo = $PSScriptRoot
$proj = Join-Path $repo "Facepunch.Steamworks.Test\Facepunch.Steamworks.TestWin64.csproj"

# 1. Steam must be running.
$steam = Get-Process steam -ErrorAction SilentlyContinue
if (-not $steam) {
    throw "Steam is not running. Start Steam and sign in, then re-run. (The suite calls SteamClient.Init(1442910).)"
}
Write-Host "Steam process found (pid $($steam.Id | Select-Object -First 1))."

# 2. steam_appid.txt beside the test binary so Init succeeds without the launcher.
$outDir = Join-Path $repo "Facepunch.Steamworks.Test\bin\Release\net6.0"
New-Item -ItemType Directory -Force $outDir | Out-Null
Set-Content -Path (Join-Path $outDir "steam_appid.txt") -Value "1442910" -NoNewline -Encoding ascii

# 3. Read-only + NetworkingMessages subset by default (no server-list network
#    hammering, no input VDF, no encrypted-app-ticket key requirement, no random
#    avatar-image callbacks). Includes NetworkingMessagesTest so the zero-alloc
#    receive + Span/IntPtr send overloads are exercised live, not just compiled.
$filter = if ($Full) { "" } else {
    'FullyQualifiedName~AppTest|FullyQualifiedName~FriendsTest&FullyQualifiedName!~Avatar|FullyQualifiedName~NetworkingMessagesTest'
}

# --blame-hang-timeout so no single environment-dependent test can hang the run.
$args = @("test", $proj, "-c", "Release", "--blame-hang-timeout", "90s", "--logger", "trx;LogFileName=live-steam.trx")
if ($filter) { $args += @("--filter", $filter) }

Write-Host "Running $($(if($Full){'FULL'}else{'read-only subset'})) live-Steam suite..."
& dotnet @args
$code = $LASTEXITCODE
Write-Host "dotnet test exit: $code   (results: $outDir\..\..\TestResults\live-steam.trx)"
exit $code
