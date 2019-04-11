using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamParentalSettings : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamParentalSettings( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid => platform != null && platform.IsValid;
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// bool
		public bool BIsAppBlocked( AppId_t nAppID /*AppId_t*/ )
		{
			return platform.ISteamParentalSettings_BIsAppBlocked( nAppID.Value );
		}
		
		// bool
		public bool BIsAppInBlockList( AppId_t nAppID /*AppId_t*/ )
		{
			return platform.ISteamParentalSettings_BIsAppInBlockList( nAppID.Value );
		}
		
		// bool
		public bool BIsFeatureBlocked( ParentalFeature eFeature /*EParentalFeature*/ )
		{
			return platform.ISteamParentalSettings_BIsFeatureBlocked( eFeature );
		}
		
		// bool
		public bool BIsFeatureInBlockList( ParentalFeature eFeature /*EParentalFeature*/ )
		{
			return platform.ISteamParentalSettings_BIsFeatureInBlockList( eFeature );
		}
		
		// bool
		public bool BIsParentalLockEnabled()
		{
			return platform.ISteamParentalSettings_BIsParentalLockEnabled();
		}
		
		// bool
		public bool BIsParentalLockLocked()
		{
			return platform.ISteamParentalSettings_BIsParentalLockLocked();
		}
		
	}
}
