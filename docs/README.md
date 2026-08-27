# Documentation

Steam's own documentation is thin and, in several places, wrong or unfollowable. These
docs aim to be the opposite: explain the concept, show a worked example, and be explicit
about what we had to infer rather than read stated.

**Where we assume rather than know, we say so.** Valve leaves 30.9% of SDK methods with no
comment at all, and 91.7% of functions returning `const char*` document no pointer
lifetime. Silence in Valve's headers is not a reason for silence here — it is a reason to
state the assumption we are relying on, so it can be challenged.

The same applies to *our own* code. Where this library's XML documentation contradicts what
the code does, the guides say so and follow the code. There are two such cases today, both
in [Achievements & Stats](guides/02-achievements-and-stats.md), and both would otherwise cost
you an afternoon.

---

## Guides

Task-oriented, written for someone who has not shipped a Steam game before. Read
[Getting Started](guides/01-getting-started.md) first; the rest are independent.

| Guide | Covers |
|---|---|
| [1. Getting Started](guides/01-getting-started.md) | The IPC mental model, why `Init` throwing is normal, `steam_appid.txt` traps, reading init failures, the `asyncCallbacks` decision, shutdown, dedicated servers, which native binary to ship |
| [2. Achievements & Stats](guides/02-achievements-and-stats.md) | Why your first write disappears, `StatsReceived` gating, why `SetStat` does *not* store despite its own docs, `Trigger` vs `Clear`, progress toasts that record nothing, leaderboards returning `null` rather than empty |
| [3. Lobbies & Matchmaking](guides/03-lobbies-and-matchmaking.md) | Why a new lobby is invisible, `Join()` vs `JoinLobbyAsync`, the query builder and its exact-match-only string filters, lobby vs member data, chat, invites, and the `+connect_lobby` cold start you must handle yourself |
| [4. Networking transports](guides/04-networking-transports.md) | Choosing between `NetworkingSockets`, `NetworkingMessages` and legacy P2P; why SDR is not a fourth API; zero-allocation receive; poll groups; the `messageNum`/`recvTime` fix; tuning and simulated packet loss |
| [5. Workshop & Steam Cloud](guides/05-workshop-and-cloud.md) | Loading subscribed items, the query builder, publishing with the editor, and Cloud reads/writes including delete-vs-forget and quota |
| [6. Dedicated servers](guides/06-dedicated-servers.md) | End to end: init, logon (which `Init` does *not* do for you), the browser, auth tickets, server stats, and the four silent ways a server can die |

## Steam Datagram Relay (SDR)

Valve's relay network — routing game traffic through Valve's backbone for lower latency,
DDoS protection and IP privacy. These are design notes rather than tutorials: they record
*why* each decision went the way it did, and which behaviours were inferred from the headers
rather than documented.

| Document | Covers |
|---|---|
| [Poll groups & hosted servers](sdr/poll-groups-and-hosted-servers.md) | Draining many connections in one call; running a dedicated server inside the relay network, ticketed and ticketless |
| [Ping locations & certificates](sdr/ping-locations-and-certificates.md) | Latency estimation without pinging, POP enumeration, running SDR without a Steam login |

For *when* to reach for SDR at all, start with
[Networking transports §6](guides/04-networking-transports.md#6-sdr--when-and-how).

## Audits

Evidence-backed analysis of the codebase. Every finding cites a header quote, a
measurement, or a reproduction. Suspicions that did not survive verification are recorded as
such rather than quietly dropped.

Start with the [audit index](audit/README.md) — it ranks all open findings and summarises
what has already been fixed.

| # | Report |
|---|---|
| 00 | [Native conformance](audit/00-native-conformance.md) — P/Invoke entry points vs. the shipped binaries |
| 01 | [Marshaling & ABI](audit/01-marshaling-abi.md) — struct layout and marshalling vs. the C++ headers |
| 02 | [Dispatch, memory & threading](audit/02-dispatch-memory-threading.md) — the callback pump and lifetimes |
| 03 | [API coverage & missing features](audit/03-api-coverage-gaps.md) — what the SDK offers vs. what we expose |
| 04 | [GameServer](audit/04-gameserver.md) — the dedicated-server path |
| 05 | [Tests & documentation coverage](audit/05-tests-and-docs.md) — measured coverage and the offline test strategy |
| 06 | [Performance](audit/06-performance.md) — measured allocation and interop costs |

The [README's known-limitations section](../README.md#known-limitations--open-issues) is the
short version of the audit index's open list, ranked by severity.

---

## Verifying the library without Steam

Steam does not need to be installed to check a great deal of this library's correctness.
**Run this locally before every commit** — it is the primary gate, and it runs all of the
checks below:

```bash
powershell -ExecutionPolicy Bypass -File verify.ps1
```

CI runs the same scripts, but treat CI as a backstop rather than the source of truth: if
`verify.ps1` passes on your machine the change is good, and if it fails, CI will not save
you. Nothing here needs a Steam client, a Steam account, a login or a network connection —
which is the only reason it actually gets run.

It builds every target framework of every platform project, then runs two gates. Exit code 0
means everything passed.

### Gate 1 — native export conformance

```bash
powershell -ExecutionPolicy Bypass -File verify-native-conformance.ps1
```

Asserts every `DllImport` entry point exists in all 17 committed native binaries, across
Windows x86/x64, Linux x86/x64 and macOS. Entry points are plain strings the compiler never
checks — this is what stops a symbol Valve removed from reaching a player's machine. It is
how the six dead `ISteamAppList` bindings were found, and how eight stale native binaries
were found, one of which would have prevented a Linux dedicated server from starting at all.

### Gate 2 — struct layout baseline

```bash
powershell -ExecutionPolicy Bypass -File verify-struct-layout.ps1
```

Pins the marshalled size, pack and every field offset of all 343 ABI structs against
`Tools/baselines/layout-win64.txt`. Steam writes these directly into memory we read back, so
a mismatch silently returns wrong data instead of throwing — the worst failure mode in this
codebase. The layouts come out of the code generator, where one small change can move dozens
of structs at once, so the baseline turns that into an explicit, reviewable diff.

Pass `-Record` to deliberately accept an intended layout change, and commit the new baseline
alongside the change that caused it.

**What it does not prove:** the baseline pins what the layout *is*, not what it *should be*.
It catches drift but would accept a wrong layout that was recorded deliberately. The proof
that the current layouts are correct comes from a header-derived MSVC-x64 layout model
diffed against `Marshal.SizeOf`/`OffsetOf`, which still lives outside this repository.

### Runtime behaviour

Anything requiring a live Steam client is in `run-live-steam-tests.ps1`, which needs Steam
running and signed in. Be aware that its default filter excludes every server test — the
`GameServerTest`, `GameServerStatsTest` and `ServerListTest` suites run only under `-Full`.
