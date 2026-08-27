using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Steam Family View (parental controls): whether the account is running under a parental lock,
	/// and which features a parent has restricted. Use it to hide or disable parts of your game that
	/// would take a child somewhere their parent has blocked &#8212; a store link, a chat window, a
	/// web browser panel.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>Valve documents none of this.</b> <c>isteamparentalsettings.h</c> contains six method
	/// signatures and not one comment &#8212; it is one of two headers in the SDK at 0% comment
	/// coverage. Everything below is inferred from the method names, the
	/// <see cref="ParentalFeature"/> enum, and how Family View behaves in the Steam client. Treat it
	/// as a well-informed reading rather than a specification, and prefer failing open (showing your
	/// own safe UI) over relying on exact semantics.
	/// </para>
	/// <para>
	/// This is a <b>courtesy signal, not a security boundary.</b> Steam enforces its own restrictions
	/// on its own features; nothing here prevents your game from doing anything. It exists so you can
	/// avoid dead ends, not so you can implement access control.
	/// </para>
	/// <para>
	/// Most accounts have Family View switched off entirely, in which case
	/// <see cref="IsParentalLockEnabled"/> is <see langword="false"/> and nothing is blocked. Check
	/// that first and skip the rest.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// if ( SteamParental.IsParentalLockEnabled )
	/// {
	///     // Don't offer a route the parent has closed off
	///     if ( SteamParental.IsFeatureBlocked( ParentalFeature.Store ) )
	///         HideDlcStoreButton();
	///
	///     if ( SteamParental.IsFeatureBlocked( ParentalFeature.Friends ) )
	///         HideFriendsPanel();
	/// }
	///
	/// // Settings can change while the game runs
	/// SteamParental.OnSettingsChanged += RefreshRestrictedUi;
	/// </code>
	/// </example>
	public class SteamParental : SteamSharedClass<SteamParental>
	{
		internal static ISteamParentalSettings Internal => Interface as ISteamParentalSettings;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamParentalSettings( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			return true;
		}

		internal static void InstallEvents( bool server )
		{
			Dispatch.Install<SteamParentalSettingsChanged_t>( x => OnSettingsChanged?.Invoke(), server );
		}

		/// <summary>
		/// Raised when the account's Family View configuration changes while your game is running
		/// &#8212; a parent unlocking the session, or changing what is restricted. Re-read anything
		/// you cached and refresh your UI.
		/// </summary>
		/// <remarks>
		/// Carries no payload, so you must re-query the properties you care about. Requires callbacks
		/// to be pumped like any other Steam event.
		/// </remarks>
		public static event Action OnSettingsChanged;


		/// <summary>
		/// Whether Family View is switched on for this account at all. When
		/// <see langword="false"/> &#8212; which is the common case &#8212; nothing is restricted and
		/// the rest of this class will report nothing blocked.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the account has Family View configured. Check this before
		/// bothering with any of the other members.
		/// </returns>
		public static bool IsParentalLockEnabled => Internal.BIsParentalLockEnabled();

		/// <summary>
		/// Whether the parental lock is currently <b>engaged</b>, as opposed to merely configured. A
		/// parent can temporarily unlock a session by entering their PIN, which leaves Family View
		/// enabled but not currently restricting anything.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if restrictions are actively in force right now. When Family View
		/// is enabled but unlocked this is <see langword="false"/> and the user has full access.
		/// </returns>
		/// <remarks>
		/// This distinction &#8212; enabled versus locked &#8212; is not documented by Valve and is
		/// inferred from the two method names and Family View's behaviour in the Steam client. It is
		/// the pair most likely to be misread, so if you only test one, test this one.
		/// </remarks>
		public static bool IsParentalLockLocked => Internal.BIsParentalLockLocked();

		/// <summary>
		/// Whether a specific app is currently blocked for this user by Family View. The effective
		/// answer: it accounts for whether the lock is engaged, not just what is on the list.
		/// </summary>
		/// <param name="app">The application to test.</param>
		/// <returns>
		/// <see langword="true"/> if the user is currently barred from that app. Valve documents no
		/// behaviour for an app id that does not exist.
		/// </returns>
		/// <remarks>
		/// The difference between this and <see cref="BIsAppInBlockList"/> is not documented by
		/// Valve; it is inferred from the names. Read this one for "can they play it right now" and
		/// the other for "is it on the parent's list".
		/// </remarks>
		public static bool IsAppBlocked( AppId app ) => Internal.BIsAppBlocked( app.Value );

		/// <summary>
		/// Whether a specific app appears on the parent's block list, regardless of whether the lock
		/// is currently engaged. Use <see cref="IsAppBlocked"/> for the effective answer.
		/// </summary>
		/// <param name="app">The application to test.</param>
		/// <returns><see langword="true"/> if the app is on the configured block list.</returns>
		/// <remarks>
		/// Keeps Valve's <c>B</c> prefix in its name, unlike its sibling <see cref="IsAppBlocked"/>.
		/// That is a naming inconsistency in this binding rather than a difference in meaning.
		/// </remarks>
		public static bool BIsAppInBlockList( AppId app ) => Internal.BIsAppInBlockList( app.Value );

		/// <summary>
		/// Whether a Steam feature &#8212; the store, community, friends, the in-game browser and so
		/// on &#8212; is currently blocked for this user. This is the member most games want: it
		/// tells you not to offer a button that will lead nowhere.
		/// </summary>
		/// <param name="feature">
		/// The Steam feature to test. <see cref="ParentalFeature.Invalid"/> and
		/// <see cref="ParentalFeature.Max"/> are not real features; Valve documents no behaviour for
		/// passing them.
		/// </param>
		/// <returns><see langword="true"/> if the user is currently barred from that feature.</returns>
		/// <remarks>
		/// Blocking is Steam's to enforce on Steam's own surfaces. If your game implements its own
		/// chat or its own web view, nothing here restricts it &#8212; honouring
		/// <see cref="ParentalFeature.Community"/> or <see cref="ParentalFeature.Browser"/> in that
		/// case is your decision, and a considerate one.
		/// </remarks>
		public static bool IsFeatureBlocked( ParentalFeature feature ) => Internal.BIsFeatureBlocked( feature );

		/// <summary>
		/// Whether a feature appears on the parent's block list, regardless of whether the lock is
		/// currently engaged. Use <see cref="IsFeatureBlocked"/> for the effective answer.
		/// </summary>
		/// <param name="feature">The Steam feature to test.</param>
		/// <returns><see langword="true"/> if the feature is on the configured block list.</returns>
		/// <remarks>
		/// As with <see cref="BIsAppInBlockList"/>, the <c>B</c> prefix here is inherited from Valve's
		/// naming and is inconsistent with its sibling.
		/// </remarks>
		public static bool BIsFeatureInBlockList( ParentalFeature feature ) => Internal.BIsFeatureInBlockList( feature );
	}
}