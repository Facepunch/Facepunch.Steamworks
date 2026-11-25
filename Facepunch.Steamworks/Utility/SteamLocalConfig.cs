using System;
using System.Collections.Generic;
using System.IO;

namespace Steamworks
{
	/// <summary>
	/// Reads the player's settings from Steam's userdata/[account id]/config/localconfig.vdf. Valve doesn't document that file, so any read can fail.
	/// </summary>
	internal static class SteamLocalConfig
	{
		/// <summary>
		/// False when the overlay is off globally or for this app, null when the file can't be read.
		/// </summary>
		internal static bool? ReadOverlaySetting( uint accountId, uint appId )
		{
			try
			{
				var path = FindLocalConfig( accountId );
				if ( path == null )
				{
					return null;
				}

				return ReadOverlaySetting( File.ReadAllText( path ), appId.ToString() );
			}
			catch ( Exception )
			{
				return null;
			}
		}

		private static string FindLocalConfig( uint accountId )
		{
			foreach ( var steamPath in GetSteamPaths() )
			{
				var path = Path.Combine( steamPath, "userdata", accountId.ToString(), "config", "localconfig.vdf" );
				if ( File.Exists( path ) )
				{
					return path;
				}
			}

			return null;
		}

		// Steam reports its folder on Windows only, Mac and Linux installs use fixed folders
		private static IEnumerable<string> GetSteamPaths()
		{
#if PLATFORM_WIN
			var installPath = SteamAPI.GetSteamInstallPath();
			if ( !string.IsNullOrEmpty( installPath ) )
			{
				yield return installPath;
			}
#else
			var home = Environment.GetEnvironmentVariable( "HOME" );
			if ( string.IsNullOrEmpty( home ) )
			{
				yield break;
			}

			yield return Path.Combine( home, "Library", "Application Support", "Steam" );
			yield return Path.Combine( home, ".steam", "steam" );
			yield return Path.Combine( home, ".local", "share", "Steam" );
#endif
		}

		// False when either overlay key in the KeyValues text is "0"
		private static bool ReadOverlaySetting( string config, string appId )
		{
			var blockNames = new List<string>();
			string key = null;
			var index = 0;

			while ( index < config.Length )
			{
				var c = config[index];
				if ( c == '"' )
				{
					var start = index + 1;
					var end = start;
					while ( end < config.Length && config[end] != '"' )
					{
						// Skips escaped quotes
						end += config[end] == '\\' ? 2 : 1;
					}

					end = Math.Min( end, config.Length );
					index = end + 1;
					var text = config.Substring( start, end - start );

					if ( key == null )
					{
						key = text;
						continue;
					}

					if ( text == "0" && IsOverlayKey( blockNames, key, appId ) )
					{
						return false;
					}

					key = null;
				}
				else if ( c == '{' )
				{
					blockNames.Add( key );
					key = null;
					index++;
				}
				else if ( c == '}' )
				{
					blockNames.RemoveAt( blockNames.Count - 1 );
					index++;
				}
				else
				{
					index++;
				}
			}

			return true;
		}

		// UserLocalConfigStore/system/EnableGameOverlay or UserLocalConfigStore/apps/[app id]/OverlayAppEnable
		private static bool IsOverlayKey( List<string> blockNames, string key, string appId )
		{
			if ( blockNames.Count == 2 && IsSameKey( blockNames[1], "system" ) )
			{
				return IsSameKey( key, "EnableGameOverlay" );
			}

			return blockNames.Count == 3 && IsSameKey( blockNames[1], "apps" ) && blockNames[2] == appId && IsSameKey( key, "OverlayAppEnable" );
		}

		private static bool IsSameKey( string a, string b )
		{
			return string.Equals( a, b, StringComparison.OrdinalIgnoreCase );
		}
	}
}
