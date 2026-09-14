# Facepunch Steamworks for Unity

This package wraps the Facepunch.Steamworks managed assemblies and the Steam API
redistributables as a Unity Package Manager (UPM) package.

Unity 2021.2 or newer is required because the managed assemblies target .NET
Standard 2.1.

## Install from Git

In Unity's Package Manager, choose **Add package from git URL** and enter:

```
https://github.com/michael-tola-denis/Facepunch.Steamworks.Unity.git?path=/Packages/com.michaeltoladenis.facepunch-steamworks#v2.5.3
```

Keep the `#v2.5.3` suffix. Without a tag, Unity follows `master`, so a later
change to this repository can break your project the next time packages resolve.

## Do not combine with other Facepunch.Steamworks copies

Only one copy of Facepunch.Steamworks can be in a Unity project. Do **not** install
this package alongside anything that already ships the assemblies, such as:

- `com.community.netcode.transport.facepunch` (the Netcode for GameObjects Facepunch
  transport bundles its own copy in `Runtime/Facepunch/`)
- a `Facepunch.Steamworks.*.dll` under `Assets/Plugins`

Two copies produce "Multiple precompiled assemblies with the same name" errors. If
you use the NGO transport, rely on its bundled copy and skip this package.

For a local checkout, add this to your project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.michaeltoladenis.facepunch-steamworks": "file:../Facepunch.Steamworks.Unity/Packages/com.michaeltoladenis.facepunch-steamworks"
  }
}
```

## Included binaries

The package supplies the correct managed assembly for Windows x86/x64, Linux, and
macOS, plus Valve's platform-specific Steam API binary. Their Unity plugin importer
settings are retained, so Unity selects them for the active editor and build target.

## Usage

Create a `steam_appid.txt` next to the built executable (or use your test App ID
while developing), then initialize and run callbacks from your game code:

```csharp
using Steamworks;

SteamClient.Init(480); // Replace 480 with your own Steam App ID.
// Call SteamClient.RunCallbacks() each frame.
```

See the [Facepunch Steamworks wiki](https://wiki.facepunch.com/steamworks/) for the
full API and initialization guidance. The package is MIT licensed; Steam's native
redistributables remain subject to Valve's Steamworks SDK terms.
