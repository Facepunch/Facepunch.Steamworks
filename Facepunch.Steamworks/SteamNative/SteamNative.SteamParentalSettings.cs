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
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
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
