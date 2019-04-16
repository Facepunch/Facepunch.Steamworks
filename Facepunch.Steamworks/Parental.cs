using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public static class Parental
	{
		static Internal.ISteamParentalSettings _internal;
		internal static Internal.ISteamParentalSettings Internal
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
		public static bool IsParentalLockEnabled => Internal.BIsParentalLockEnabled();

		/// <summary>
		/// 
		/// </summary>
		public static bool IsParentalLockLocked => Internal.BIsParentalLockLocked();

		/// <summary>
		/// 
		/// </summary>
		public static bool IsAppBlocked( AppId app ) => Internal.BIsAppBlocked( app.Value );

		/// <summary>
		/// 
		/// </summary>
		public static bool BIsAppInBlockList( AppId app ) => Internal.BIsAppInBlockList( app.Value );

		/// <summary>
		/// 
		/// </summary>
		public static bool IsFeatureBlocked( ParentalFeature feature ) => Internal.BIsFeatureBlocked( feature );

		/// <summary>
		/// 
		/// </summary>
		public static bool BIsFeatureInBlockList( ParentalFeature feature ) => Internal.BIsFeatureInBlockList( feature );
	}
}