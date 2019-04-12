using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks
{
	/// <summary>
	/// Exposes a wide range of information and actions for applications and Downloadable Content (DLC).
	/// </summary>
	public static class Apps
	{
		static Internal.ISteamApps _steamapps;
		internal static Internal.ISteamApps steamapps
		{
			get
			{
				if ( _steamapps == null )
					_steamapps = new Internal.ISteamApps();

				return _steamapps;
			}
		}

		/// <summary>
		/// Checks if the active user is subscribed to the current App ID
		/// </summary>
		public static bool IsSubscribed => steamapps.BIsSubscribed();

		/// <summary>
		/// Checks if the license owned by the user provides low violence depots.
		/// Low violence depots are useful for copies sold in countries that have content restrictions
		/// </summary>
		public static bool IsLowVoilence => steamapps.BIsLowViolence();

		/// <summary>
		/// Checks whether the current App ID license is for Cyber Cafes.
		/// </summary>
		public static bool IsCybercafe => steamapps.BIsCybercafe();

		/// <summary>
		/// CChecks if the user has a VAC ban on their account
		/// </summary>
		public static bool IsVACBanned => steamapps.BIsVACBanned();

		/// <summary>
		/// Gets the current language that the user has set.
		/// This falls back to the Steam UI language if the user hasn't explicitly picked a language for the title.
		/// </summary>
		public static string GameLangauge => steamapps.GetCurrentGameLanguage();

		/// <summary>
		/// Gets a list of the languages the current app supports.
		/// </summary>
		public static string[] AvailablLanguages => steamapps.GetAvailableGameLanguages().Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
	}
}