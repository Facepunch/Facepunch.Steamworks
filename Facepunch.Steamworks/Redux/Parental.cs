using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public static class Parental
	{
		static Internal.ISteamParentalSettings _internal;
		internal static Internal.ISteamParentalSettings parentalsettings
		{
			get
			{
				if ( _internal == null )
					_internal = new Internal.ISteamParentalSettings();

				return _internal;
			}
		}

		internal static void InstallEvents()
		{
			new Event<SteamParentalSettingsChanged_t>( x => OnSettingsChanged?.Invoke() );
		}

		/// <summary>
		/// Parental Settings Changed
		/// </summary>
		public static event Action OnSettingsChanged;


		/// <summary>
		/// 
		/// </summary>
		public static bool IsParentalLockEnabled => parentalsettings.BIsParentalLockEnabled();

		/// <summary>
		/// 
		/// </summary>
		public static bool IsParentalLockLocked => parentalsettings.BIsParentalLockLocked();

		/// <summary>
		/// 
		/// </summary>
		public static bool IsAppBlocked( AppId app ) => parentalsettings.BIsAppBlocked( app.Value );

		/// <summary>
		/// 
		/// </summary>
		public static bool BIsAppInBlockList( AppId app ) => parentalsettings.BIsAppInBlockList( app.Value );

		/// <summary>
		/// 
		/// </summary>
		public static bool IsFeatureBlocked( ParentalFeature feature ) => parentalsettings.BIsFeatureBlocked( feature );

		/// <summary>
		/// 
		/// </summary>
		public static bool BIsFeatureInBlockList( ParentalFeature feature ) => parentalsettings.BIsFeatureInBlockList( feature );
	}
}