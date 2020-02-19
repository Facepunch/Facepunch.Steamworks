using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public class SteamParental : SteamClass
	{
		internal static ISteamParentalSettings Internal;
		internal override SteamInterface Interface => Internal;

		internal override void InitializeInterface( bool server )
		{
			Internal = new ISteamParentalSettings( server );
			InstallEvents();
		}


		internal static void InstallEvents()
		{
			Dispatch.Install<SteamParentalSettingsChanged_t>( x => OnSettingsChanged?.Invoke() );
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