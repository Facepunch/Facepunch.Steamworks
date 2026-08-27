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

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamApps( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents();

			return true;
		}

		internal static void InstallEvents()
		{
			Dispatch.Install<DlcInstalled_t>( x => OnDlcInstalled?.Invoke( x.AppID ) );
			Dispatch.Install<NewUrlLaunchParameters_t>( x => OnNewLaunchParameters?.Invoke() );
		}

		/// <summary>
		/// Posted after the user gains ownership of DLC and that DLC is installed.
		/// </summary>
		public static event Action<AppId> OnDlcInstalled;

		/// <summary>
		/// Posted after the user gains executes a Steam URL with command line or query parameters
		/// such as steam://run/appid//-commandline/?param1=value1(and)param2=value2(and)param3=value3 etc
		/// while the game is already running.  The new params can be queried
		/// with GetLaunchQueryParam and GetLaunchCommandLine.
		/// </summary>
		public static event Action OnNewLaunchParameters;

		/// <summary>
		/// Gets whether or not the active user is subscribed to the current App ID.
		/// </summary>
		public static bool IsSubscribed => Internal.BIsSubscribed();

		/// <summary>
		/// Gets whether or not the user borrowed this game via Family Sharing. If true, call GetAppOwner() to get the lender SteamID.
		/// </summary>
		public static bool IsSubscribedFromFamilySharing => Internal.BIsSubscribedFromFamilySharing();

		/// <summary>
		/// Gets whether or not the license owned by the user provides low violence depots.
		/// Low violence depots are useful for copies sold in countries that have content restrictions
		/// </summary>
		public static bool IsLowViolence => Internal.BIsLowViolence();

		/// <summary>
		/// Gets whether or not the current App ID license is for Cyber Cafes.
		/// </summary>
		public static bool IsCybercafe => Internal.BIsCybercafe();

		/// <summary>
		/// Gets whether or not the user has a VAC ban on their account.
		/// </summary>
		public static bool IsVACBanned => Internal.BIsVACBanned();

		/// <summary>
		/// Gets the current language that the user has set.
		/// This falls back to the Steam UI language if the user hasn't explicitly picked a language for the title.
		/// </summary>
		/// <returns>
		/// A Steam API language code such as <c>"english"</c>, <c>"french"</c> or <c>"schinese"</c>
		/// &#8212; <b>not</b> an ISO 639 code, so do not feed it straight to
		/// <c>CultureInfo</c>. Empty if Steam has no answer.
		/// </returns>
		/// <remarks>
		/// The value can only be one of the languages you declared on the Steamworks partner site. A
		/// player whose Steam UI is set to a language your app does not list falls back to your
		/// default rather than reporting their real preference.
		/// </remarks>
		public static string GameLanguage => Internal.GetCurrentGameLanguage();

		/// <summary>
		/// Gets a list of the languages the current app supports, as configured on the Steamworks
		/// partner site. Use it to populate an in-game language picker that cannot offer a language
		/// Steam will not serve.
		/// </summary>
		/// <returns>
		/// The supported Steam API language codes. Empty if Steam returned nothing &#8212; which is
		/// what an app with no languages configured yields, rather than an error.
		/// </returns>
		public static string[] AvailableLanguages => Internal.GetAvailableGameLanguages().Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

		/// <summary>
		/// Gets whether or not the active user is subscribed to a specified App ID.
		/// Only use this if you need to check ownership of another game related to yours, a demo for example.
		/// </summary>
		/// <param name="appid">The App ID of the DLC to check.</param>
		public static bool IsSubscribedToApp( AppId appid ) => Internal.BIsSubscribedApp( appid.Value );

		/// <summary>
		/// Gets whether or not the user owns a specific DLC and if the DLC is installed.
		/// </summary>
		/// <param name="appid">The App ID of the DLC to check.</param>
		public static bool IsDlcInstalled( AppId appid ) => Internal.BIsDlcInstalled( appid.Value );

		/// <summary>
		/// Returns the time of the purchase of the app.
		/// </summary>
		/// <param>The App ID to check the purchase time for.</param>
		public static DateTime PurchaseTime( AppId appid = default )
		{
			if ( appid == 0 )
				appid = SteamClient.AppId;

			return Epoch.ToDateTime(Internal.GetEarliestPurchaseUnixTime(appid.Value ) );
		}

		/// <summary>
		/// Checks if the user is subscribed to the current app through a free weekend.
		/// This function will return false for users who have a retail or other type of license.
		/// Before using, please ask your Valve technical contact how to package and secure your free weekened.
		/// </summary>
		public static bool IsSubscribedFromFreeWeekend => Internal.BIsSubscribedFromFreeWeekend();

		/// <summary>
		/// Returns metadata for all available DLC.
		/// </summary>
		public static IEnumerable<DlcInformation> DlcInformation()
		{
			var appid = default( AppId );
			var available = false;

			// Count hoisted out of the loop condition: it was re-queried through the native
			// boundary on every iteration, doubling the interop transitions for no benefit.
			// The DLC count is fixed for the lifetime of the app.
			var count = Internal.GetDLCCount();

			for ( int i = 0; i < count; i++ )
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
		/// Install control for optional DLC.
		/// </summary>
		/// <param name="appid">The App ID of the DLC to install.</param>
		public static void InstallDlc( AppId appid ) => Internal.InstallDLC( appid.Value );

		/// <summary>
		/// Uninstall control for optional DLC.
		/// </summary>
		/// <param name="appid">The App ID of the DLC to uninstall.</param>
		public static void UninstallDlc( AppId appid ) => Internal.UninstallDLC( appid.Value );

		/// <summary>
		/// Gets the name of the beta branch that is launched, or <see langword="null"/> if the application is not running on a beta branch.
		/// Use it to gate experimental features, or to stamp bug reports with the branch they came
		/// from.
		/// </summary>
		/// <returns>
		/// The branch name as configured on the partner site, or <see langword="null"/> for the
		/// default public branch. Note that the default branch reports null rather than
		/// <c>"public"</c>.
		/// </returns>
		/// <remarks>
		/// A player can switch branches from the Steam client, so this is not a build-time constant.
		/// It is also not a security boundary &#8212; anyone with the branch password can be on a
		/// beta branch.
		/// </remarks>
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
		/// Force verify game content on next launch.
		/// <para>
		/// If you detect the game is out-of-date (for example, by having the client detect a version mismatch with a server),
		/// you can call MarkContentCorrupt to force a verify, show a message to the user, and then quit.
		/// </para>
		/// </summary>
		/// <param name="missingFilesOnly">Whether or not to only verify missing files.</param>
		public static void MarkContentCorrupt( bool missingFilesOnly ) => Internal.MarkContentCorrupt( missingFilesOnly );

		/// <summary>
		/// Gets a list of all installed depots for a given App ID in mount order.
		/// </summary>
		/// <param name="appid">The App ID. Defaults to the running app.</param>
		/// <returns>
		/// The installed depots, in the order Steam mounts them, which is the order in which later
		/// depots override files from earlier ones. Empty for an app that is not installed.
		/// </returns>
		/// <remarks>
		/// <b>Capped at 32 depots by this binding</b>, which allocates a fixed buffer of that size.
		/// An app with more installed depots is silently truncated &#8212; there is no error and no
		/// way to tell from the result that anything was dropped. Valve's native call has no such
		/// limit.
		/// </remarks>
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
		/// Gets the install folder for a specific App ID.
		/// This works even if the application is not installed, based on where the game would be installed with the default Steam library location.
		/// </summary>
		/// <param name="appid">The App ID. Defaults to the running app.</param>
		/// <returns>
		/// The absolute install folder, or <see langword="null"/> if Steam could not answer. Because
		/// this succeeds for apps that are <b>not installed</b>, a non-null path is not evidence that
		/// anything exists on disk &#8212; use <see cref="IsAppInstalled"/> for that.
		/// </returns>
		/// <remarks>
		/// Do not cache this across sessions. Steam moves content between library folders, and the
		/// path changes when a user relocates an install.
		/// </remarks>
		public static string AppInstallDir( AppId appid = default )
		{
			if ( appid == 0 )
				appid = SteamClient.AppId;

			if ( Internal.GetAppInstallDir( appid.Value, out var strVal ) == 0 )
				return null;

			return strVal;
		}

		/// <summary>
		/// Gets whether another app is currently installed on this machine. Intended for checking on
		/// a related app of your own &#8212; the full version alongside a demo, or a companion tool.
		/// </summary>
		/// <param name="appid">The App ID to check.</param>
		/// <returns>
		/// <see langword="true"/> if the app's content is installed locally. Installed is not the
		/// same as owned; use <see cref="IsSubscribedToApp"/> for ownership.
		/// </returns>
		/// <remarks>
		/// Valve intends this for apps related to yours rather than as a general library scanner.
		/// </remarks>
		public static bool IsAppInstalled( AppId appid ) => Internal.BIsAppInstalled( appid.Value );

		/// <summary>
		/// Gets the Steam ID of the original owner of the current app. If it's different from the current user then it is borrowed.
		/// Borrowed here means Family Sharing &#8212; someone else bought the game and the current
		/// user is playing it from their library.
		/// </summary>
		/// <returns>
		/// The owning account's <see cref="SteamId"/>. Equal to <see cref="SteamClient.SteamId"/>
		/// when the player owns the game themselves.
		/// </returns>
		/// <remarks>
		/// Comparing this against the logged in user is how you detect Family Sharing. Bear in mind
		/// that a shared session can be revoked at any moment if the owner starts playing, so treat
		/// it as a reason to be careful with long-lived progression rather than as a licence check.
		/// </remarks>
		public static SteamId AppOwner => Internal.GetAppOwner().Value;

		/// <summary>
		/// Gets the associated launch parameter if the game is run via steam://run/appid/?param1=value1;param2=value2;param3=value3 etc.
		/// <para>
		/// Parameter names starting with the character '<c>@</c>' are reserved for internal use and will always return an empty string.
		/// Parameter names starting with an underscore '<c>_</c>' are reserved for steam features -- they can be queried by the game, 
		/// but it is advised that you not param names beginning with an underscore for your own features.
		/// </para>
		/// </summary>
		/// <param name="param">The name of the parameter.</param>
		/// <returns>The launch parameter value.</returns>
		public static string GetLaunchParam( string param ) => Internal.GetLaunchQueryParam( param );

		/// <summary>
		/// Gets the download progress for optional DLC.
		/// </summary>
		/// <param name="appid">The App ID to check the progress for.</param>
		public static DownloadProgress DlcDownloadProgress( AppId appid )
		{
			ulong punBytesDownloaded = 0;
			ulong punBytesTotal = 0;

			if ( !Internal.GetDlcDownloadProgress( appid.Value, ref punBytesDownloaded, ref punBytesTotal ) )
				return default;

			return new DownloadProgress { BytesDownloaded = punBytesDownloaded, BytesTotal = punBytesTotal, Active = true };
		}

		/// <summary>
		/// Gets the Build ID of this app, which can change at any time based on backend updates to the game.
		/// Defaults to <c>0</c> if you're not running a build downloaded from steam.
		/// </summary>
		public static int BuildId => Internal.GetAppBuildId();


		/// <summary>
		/// Asynchronously retrieves metadata details about a specific file in the depot manifest.
		/// </summary>
		/// <param name="filename">The name of the file.</param>
		public static async Task<FileDetails?> GetFileDetailsAsync( string filename )
		{
			var r = await Internal.GetFileDetails( filename );

			if ( !r.HasValue || r.Value.Result != Result.OK )
				return null;

			var details = r.Value;

			return new FileDetails
			{
				SizeInBytes = details.FileSize,
				Flags = details.Flags,
				Sha1 = Sha1ToString( ref details )
			};
		}

		/// <summary>
		/// m_FileSHA is a raw SHA-1 digest, so always exactly this many bytes.
		/// </summary>
		private const int FileShaLength = 20;

		/// <summary>
		/// Formats the digest the same way it always was. m_FileSHA is a fixed size buffer
		/// held inline in the struct, so it has to be pinned before it can be read - and
		/// <paramref name="result"/> is taken by reference precisely so it can be.
		/// </summary>
		private static unsafe string Sha1ToString( ref FileDetailsResult_t result )
		{
			var str = new StringBuilder( FileShaLength * 2 );

			fixed ( byte* sha = result.FileSHA )
			{
				for ( var i = 0; i < FileShaLength; i++ )
					str.Append( sha[i].ToString( "x" ) );
			}

			return str.ToString();
		}

		/// <summary>
		/// Get command line if game was launched via Steam URL, e.g. steam://run/appid//command line/.
		/// This method of passing a connect string (used when joining via rich presence, accepting an
		/// invite, etc) is preferable to passing the connect string on the operating system command
		/// line, which is a security risk. In order for rich presence joins to go through this
		/// path and not be placed on the OS command line, you must set a value in your app's
		/// configuration on Steam. Ask Valve for help with this.
		/// </summary>
		public static string CommandLine
		{
			get
			{
				Internal.GetLaunchCommandLine( out var strVal );
				return strVal;
			}
		}

		/// <summary>
		/// Check if game is a timed trial with limited playtime.
		/// </summary>
		/// <param name="secondsAllowed">The amount of seconds left on the timed trial.</param>
		/// <param name="secondsPlayed">The amount of seconds played on the timed trial.</param>
		public static bool IsTimedTrial( out int secondsAllowed, out int secondsPlayed )
        {
			uint a = 0;
			uint b = 0;
			secondsAllowed = 0;
			secondsPlayed = 0;

			if ( !Internal.BIsTimedTrial( ref a, ref b ) )
				return false;

			secondsAllowed = (int) a;
			secondsPlayed = (int) b;

			return true;
        }

	}
}
