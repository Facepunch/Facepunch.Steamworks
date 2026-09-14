# Changelog

## 2.5.3

Packaging fixes only; the Facepunch.Steamworks assemblies are unchanged from 2.5.2.

- Regenerated every asset GUID. The `.meta` files had been copied from upstream
  Facepunch.Steamworks, so their GUIDs matched other packages that bundle the same
  plugins (for example `com.community.netcode.transport.facepunch`). Unity dropped the
  duplicate assets, which left projects without the Steamworks assembly.
- Added the missing `.meta` files for `Runtime/`, `Runtime/UnityPlugin/`,
  `package.json`, `README.md`, `CHANGELOG.md` and `LICENSE.md`. Unity ignores assets
  without a `.meta` inside an immutable package folder.
- Removed an empty `Runtime/Plugins/` folder.

## 2.5.2

- Added the initial UPM wrapper around the bundled Facepunch.Steamworks Unity plugins.
