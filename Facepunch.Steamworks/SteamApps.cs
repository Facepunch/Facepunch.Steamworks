using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Exposes a wide range of information and actions for applications and Downloadable Content (DLC).
	/// </summary>
	public class SteamApps : SteamSharedClass<SteamApps>
	{
		internal static ISteamApps Internal => Interface as ISteamApps;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamApps( server ) );
		}

		internal static void InstallEvents()
		{
			Dispatch.Install<DlcInstalled_t>( x => OnDlcInstalled?.Invoke( x.AppID ) );
			Dispatch.Install<NewUrlLaunchParameters_t>( x => OnNewLaunchParameters?.Invoke() );
		}

		/// <summary>
		/// posted after the user gains ownership of DLC and that DLC is installed
		/// </summary>
		public static event Action<AppId> OnDlcInstalled;

		/// <summary>
		/// posted after the user gains executes a Steam URL with command line or query parameters
		/// such as steam://run/appid//-commandline/?param1=value1(and)param2=value2(and)param3=value3 etc
		/// while the game is already running.  The new params can be queried
		/// with GetLaunchQueryParam and GetLaunchCommandLine
		/// </summary>
		public static event Action OnNewLaunchParameters;

		/// <summary>
		/// Checks if the active user is subscribed to the current App ID
		/// </summary>
		public static bool IsSubscribed => Internal.BIsSubscribed();

		/// <summary>
		/// Check if user borrowed this game via Family Sharing, If true, call GetAppOwner() to get the lender SteamID
		/// </summary>
		public static bool IsSubscribedFromFamilySharing => Internal.BIsSubscribedFromFamilySharing();

		/// <summary>
		/// Checks if the license owned by the user provides low violence depots.
		/// Low violence depots are useful for copies sold in countries that have content restrictions
		/// </summary>
		public static bool IsLowVoilence => Internal.BIsLowViolence();

		/// <summary>
		/// Checks whether the current App ID license is for Cyber Cafes.
		/// </summary>
		public static bool IsCybercafe => Internal.BIsCybercafe();

		/// <summary>
		/// CChecks if the user has a VAC ban on their account
		/// </summary>
		public static bool IsVACBanned => Internal.BIsVACBanned();

		/// <summary>
		/// Gets the current language that the user has set.
		/// This falls back to the Steam UI language if the user hasn't explicitly picked a language for the title.
		/// </summary>
		public static string GameLanguage => Internal.GetCurrentGameLanguage();

		/// <summary>
		/// Gets a list of the languages the current app supports.
		/// </summary>
		public static string[] AvailableLanguages => Internal.GetAvailableGameLanguages().Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

		/// <summary>
		/// Checks if the active user is subscribed to a specified AppId.
		/// Only use this if you need to check ownership of another game related to yours, a demo for example.
		/// </summary>
		public static bool IsSubscribedToApp( AppId appid ) => Internal.BIsSubscribedApp( appid.Value );

		/// <summary>
		/// Checks if the user owns a specific DLC and if the DLC is installed
		/// </summary>
		public static bool IsDlcInstalled( AppId appid ) => Internal.BIsDlcInstalled( appid.Value );

		/// <summary>
		/// Returns the time of the purchase of the app
		/// </summary>
		public static DateTime PurchaseTime( AppId appid = default )
		{
			if ( appid == 0 )
				appid = SteamClient.AppId;

			return Epoch.ToDateTime(Internal.GetEarliestPurchaseUnixTime(appid.Value ) );
		}

		/// <summary>
		/// Checks if the user is subscribed to the current app through a free weekend
		/// This function will return false for users who have a retail or other type of license
		/// Before using, please ask your Valve technical contact how to package and secure your free weekened
		/// </summary>
		public static bool IsSubscribedFromFreeWeekend => Internal.BIsSubscribedFromFreeWeekend();

		/// <summary>
		/// Returns metadata for all available DLC
		/// </summary>
		public static IEnumerable<DlcInformation> DlcInformation()
		{
			var appid = default( AppId );
			var available = false;

			for ( int i = 0; i < Internal.GetDLCCount(); i++ )
			{
				if ( !Internal.BGetDLCDataByIndex( i, ref appid, ref available, out var strVal ) )
					continue;

				yield return new DlcInformation
				{
					AppId = appid.Value,
					Name = strVal,
					Available = available
				};
			}
		}

		/// <summary>
		/// Install/Uninstall control for optional DLC
		/// </summary>
		public static void InstallDlc( AppId appid ) => Internal.InstallDLC( appid.Value );

		/// <summary>
		/// Install/Uninstall control for optional DLC
		/// </summary>
		public static void UninstallDlc( AppId appid ) => Internal.UninstallDLC( appid.Value );

		/// <summary>
		/// Returns null if we're not on a beta branch, else the name of the branch
		/// </summary>
		public static string CurrentBetaName
		{
			get
			{
				if ( !Internal.GetCurrentBetaName( out var strVal ) )
					return null;

				return strVal;
			}
		}

		/// <summary>
		/// Allows you to force verify game content on next launch.
		///
		/// If you detect the game is out-of-date(for example, by having the client detect a version mismatch with a server),
		/// you can call use MarkContentCorrupt to force a verify, show a message to the user, and then quit.
		/// </summary>
		public static void MarkContentCorrupt( bool missingFilesOnly ) => Internal.MarkContentCorrupt( missingFilesOnly );

		/// <summary>
		/// Gets a list of all installed depots for a given App ID in mount order
		/// </summary>
		public static IEnumerable<DepotId> InstalledDepots( AppId appid = default )
		{
			if ( appid == 0 )
				appid = SteamClient.AppId;

			var depots = new DepotId_t[32];
			uint count = Internal.GetInstalledDepots( appid.Value, depots, (uint) depots.Length );

			for ( int i = 0; i < count; i++ )
			{
				yield return new DepotId { Value = depots[i].Value };
			}
		}

		/// <summary>
		/// Gets the install folder for a specific AppID.
		/// This works even if the application is not installed, based on where the game would be installed with the default Steam library location.
		/// </summary>
		public static string AppInstallDir( AppId appid = default )
		{
			if ( appid == 0 )
				appid = SteamClient.AppId;

			if ( Internal.GetAppInstallDir( appid.Value, out var strVal ) == 0 )
				return null;

			return strVal;
		}

		/// <summary>
		/// The app may not actually be owned by the current user, they may have it left over from a free weekend, etc.
		/// </summary>
		public static bool IsAppInstalled( AppId appid ) => Internal.BIsAppInstalled( appid.Value );

		/// <summary>
		/// Gets the Steam ID of the original owner of the current app. If it's different from the current user then it is borrowed..
		/// </summary>
		public static SteamId AppOwner => Internal.GetAppOwner().Value;

		/// <summary>
		/// Gets the associated launch parameter if the game is run via steam://run/appid/?param1=value1;param2=value2;param3=value3 etc.
		/// Parameter names starting with the character '@' are reserved for internal use and will always return an empty string.
		/// Parameter names starting with an underscore '_' are reserved for steam features -- they can be queried by the game, 
		/// but it is advised that you not param names beginning with an underscore for your own features.
		/// </summary>
		public static string GetLaunchParam( string param ) => Internal.GetLaunchQueryParam( param );

		/// <summary>
		/// Gets the download progress for optional DLC.
		/// </summary>
		public static DownloadProgress DlcDownloadProgress( AppId appid )
		{
			ulong punBytesDownloaded = 0;
			ulong punBytesTotal = 0;

			if ( !Internal.GetDlcDownloadProgress( appid.Value, ref punBytesDownloaded, ref punBytesTotal ) )
				return default;

			return new DownloadProgress { BytesDownloaded = punBytesDownloaded, BytesTotal = punBytesTotal, Active = true };
		}

		/// <summary>
		/// Gets the buildid of this app, may change at any time based on backend updates to the game.
		/// Defaults to 0 if you're not running a build downloaded from steam.
		/// </summary>
		public static int BuildId => Internal.GetAppBuildId();


		/// <summary>
		/// Asynchronously retrieves metadata details about a specific file in the depot manifest.
		/// Currently provides:
		/// </summary>
		public static async Task<FileDetails?> GetFileDetailsAsync( string filename )
		{
			var r = await Internal.GetFileDetails( filename );

			if ( !r.HasValue || r.Value.Result != Result.OK )
				return null;

			return new FileDetails
			{
				SizeInBytes = r.Value.FileSize,
				Flags = r.Value.Flags,
				Sha1 = string.Join( "", r.Value.FileSHA.Select( x => x.ToString( "x" ) ) )
			};
		}

		/// <summary>
		/// Get command line if game was launched via Steam URL, e.g. steam://run/appid//command line/.
		/// This method of passing a connect string (used when joining via rich presence, accepting an
		/// invite, etc) is preferable to passing the connect string on the operating system command
		/// line, which is a security risk.  In order for rich presence joins to go through this
		/// path and not be placed on the OS command line, you must set a value in your app's
		/// configuration on Steam.  Ask Valve for help with this.
		/// </summary>
		public static string CommandLine
		{
			get
			{
				Internal.GetLaunchCommandLine( out var strVal );
				return strVal;
			}
		}

	}
}