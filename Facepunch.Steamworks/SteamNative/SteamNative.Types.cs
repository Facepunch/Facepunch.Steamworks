using Facepunch.Steamworks;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SteamNative
{
	internal struct GID_t
	{
		public ulong Value;
		
		public static implicit operator GID_t( ulong value )
		{
			return new GID_t(){ Value = value };
		}
		
		public static implicit operator ulong( GID_t value )
		{
			return value.Value;
		}
	}
	
	internal struct JobID_t
	{
		public ulong Value;
		
		public static implicit operator JobID_t( ulong value )
		{
			return new JobID_t(){ Value = value };
		}
		
		public static implicit operator ulong( JobID_t value )
		{
			return value.Value;
		}
	}
	
	internal struct TxnID_t
	{
		public GID_t Value;
		
		public static implicit operator TxnID_t( GID_t value )
		{
			return new TxnID_t(){ Value = value };
		}
		
		public static implicit operator GID_t( TxnID_t value )
		{
			return value.Value;
		}
	}
	
	internal struct PackageId_t
	{
		public uint Value;
		
		public static implicit operator PackageId_t( uint value )
		{
			return new PackageId_t(){ Value = value };
		}
		
		public static implicit operator uint( PackageId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct BundleId_t
	{
		public uint Value;
		
		public static implicit operator BundleId_t( uint value )
		{
			return new BundleId_t(){ Value = value };
		}
		
		public static implicit operator uint( BundleId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct AppId_t
	{
		public uint Value;
		
		public static implicit operator AppId_t( uint value )
		{
			return new AppId_t(){ Value = value };
		}
		
		public static implicit operator uint( AppId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct AssetClassId_t
	{
		public ulong Value;
		
		public static implicit operator AssetClassId_t( ulong value )
		{
			return new AssetClassId_t(){ Value = value };
		}
		
		public static implicit operator ulong( AssetClassId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct PhysicalItemId_t
	{
		public uint Value;
		
		public static implicit operator PhysicalItemId_t( uint value )
		{
			return new PhysicalItemId_t(){ Value = value };
		}
		
		public static implicit operator uint( PhysicalItemId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct DepotId_t
	{
		public uint Value;
		
		public static implicit operator DepotId_t( uint value )
		{
			return new DepotId_t(){ Value = value };
		}
		
		public static implicit operator uint( DepotId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct RTime32
	{
		public uint Value;
		
		public static implicit operator RTime32( uint value )
		{
			return new RTime32(){ Value = value };
		}
		
		public static implicit operator uint( RTime32 value )
		{
			return value.Value;
		}
	}
	
	internal struct CellID_t
	{
		public uint Value;
		
		public static implicit operator CellID_t( uint value )
		{
			return new CellID_t(){ Value = value };
		}
		
		public static implicit operator uint( CellID_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SteamAPICall_t
	{
		public ulong Value;
		
		public static implicit operator SteamAPICall_t( ulong value )
		{
			return new SteamAPICall_t(){ Value = value };
		}
		
		public static implicit operator ulong( SteamAPICall_t value )
		{
			return value.Value;
		}
	}
	
	internal struct AccountID_t
	{
		public uint Value;
		
		public static implicit operator AccountID_t( uint value )
		{
			return new AccountID_t(){ Value = value };
		}
		
		public static implicit operator uint( AccountID_t value )
		{
			return value.Value;
		}
	}
	
	internal struct PartnerId_t
	{
		public uint Value;
		
		public static implicit operator PartnerId_t( uint value )
		{
			return new PartnerId_t(){ Value = value };
		}
		
		public static implicit operator uint( PartnerId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct ManifestId_t
	{
		public ulong Value;
		
		public static implicit operator ManifestId_t( ulong value )
		{
			return new ManifestId_t(){ Value = value };
		}
		
		public static implicit operator ulong( ManifestId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SiteId_t
	{
		public ulong Value;
		
		public static implicit operator SiteId_t( ulong value )
		{
			return new SiteId_t(){ Value = value };
		}
		
		public static implicit operator ulong( SiteId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct HAuthTicket
	{
		public uint Value;
		
		public static implicit operator HAuthTicket( uint value )
		{
			return new HAuthTicket(){ Value = value };
		}
		
		public static implicit operator uint( HAuthTicket value )
		{
			return value.Value;
		}
	}
	
	internal struct BREAKPAD_HANDLE
	{
		public IntPtr Value;
		
		public static implicit operator BREAKPAD_HANDLE( IntPtr value )
		{
			return new BREAKPAD_HANDLE(){ Value = value };
		}
		
		public static implicit operator IntPtr( BREAKPAD_HANDLE value )
		{
			return value.Value;
		}
	}
	
	internal struct HSteamPipe
	{
		public int Value;
		
		public static implicit operator HSteamPipe( int value )
		{
			return new HSteamPipe(){ Value = value };
		}
		
		public static implicit operator int( HSteamPipe value )
		{
			return value.Value;
		}
	}
	
	internal struct HSteamUser
	{
		public int Value;
		
		public static implicit operator HSteamUser( int value )
		{
			return new HSteamUser(){ Value = value };
		}
		
		public static implicit operator int( HSteamUser value )
		{
			return value.Value;
		}
	}
	
	internal struct FriendsGroupID_t
	{
		public short Value;
		
		public static implicit operator FriendsGroupID_t( short value )
		{
			return new FriendsGroupID_t(){ Value = value };
		}
		
		public static implicit operator short( FriendsGroupID_t value )
		{
			return value.Value;
		}
	}
	
	internal struct HServerListRequest
	{
		public IntPtr Value;
		
		public static implicit operator HServerListRequest( IntPtr value )
		{
			return new HServerListRequest(){ Value = value };
		}
		
		public static implicit operator IntPtr( HServerListRequest value )
		{
			return value.Value;
		}
	}
	
	internal struct HServerQuery
	{
		public int Value;
		
		public static implicit operator HServerQuery( int value )
		{
			return new HServerQuery(){ Value = value };
		}
		
		public static implicit operator int( HServerQuery value )
		{
			return value.Value;
		}
	}
	
	internal struct UGCHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCHandle_t( ulong value )
		{
			return new UGCHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( UGCHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct PublishedFileUpdateHandle_t
	{
		public ulong Value;
		
		public static implicit operator PublishedFileUpdateHandle_t( ulong value )
		{
			return new PublishedFileUpdateHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( PublishedFileUpdateHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct PublishedFileId_t
	{
		public ulong Value;
		
		public static implicit operator PublishedFileId_t( ulong value )
		{
			return new PublishedFileId_t(){ Value = value };
		}
		
		public static implicit operator ulong( PublishedFileId_t value )
		{
			return value.Value;
		}
	}
	
	internal struct UGCFileWriteStreamHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCFileWriteStreamHandle_t( ulong value )
		{
			return new UGCFileWriteStreamHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( UGCFileWriteStreamHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SteamLeaderboard_t
	{
		public ulong Value;
		
		public static implicit operator SteamLeaderboard_t( ulong value )
		{
			return new SteamLeaderboard_t(){ Value = value };
		}
		
		public static implicit operator ulong( SteamLeaderboard_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SteamLeaderboardEntries_t
	{
		public ulong Value;
		
		public static implicit operator SteamLeaderboardEntries_t( ulong value )
		{
			return new SteamLeaderboardEntries_t(){ Value = value };
		}
		
		public static implicit operator ulong( SteamLeaderboardEntries_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SNetSocket_t
	{
		public uint Value;
		
		public static implicit operator SNetSocket_t( uint value )
		{
			return new SNetSocket_t(){ Value = value };
		}
		
		public static implicit operator uint( SNetSocket_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SNetListenSocket_t
	{
		public uint Value;
		
		public static implicit operator SNetListenSocket_t( uint value )
		{
			return new SNetListenSocket_t(){ Value = value };
		}
		
		public static implicit operator uint( SNetListenSocket_t value )
		{
			return value.Value;
		}
	}
	
	internal struct ScreenshotHandle
	{
		public uint Value;
		
		public static implicit operator ScreenshotHandle( uint value )
		{
			return new ScreenshotHandle(){ Value = value };
		}
		
		public static implicit operator uint( ScreenshotHandle value )
		{
			return value.Value;
		}
	}
	
	internal struct HTTPRequestHandle
	{
		public uint Value;
		
		public static implicit operator HTTPRequestHandle( uint value )
		{
			return new HTTPRequestHandle(){ Value = value };
		}
		
		public static implicit operator uint( HTTPRequestHandle value )
		{
			return value.Value;
		}
	}
	
	internal struct HTTPCookieContainerHandle
	{
		public uint Value;
		
		public static implicit operator HTTPCookieContainerHandle( uint value )
		{
			return new HTTPCookieContainerHandle(){ Value = value };
		}
		
		public static implicit operator uint( HTTPCookieContainerHandle value )
		{
			return value.Value;
		}
	}
	
	internal struct ControllerHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerHandle_t( ulong value )
		{
			return new ControllerHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( ControllerHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct ControllerActionSetHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerActionSetHandle_t( ulong value )
		{
			return new ControllerActionSetHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( ControllerActionSetHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct ControllerDigitalActionHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerDigitalActionHandle_t( ulong value )
		{
			return new ControllerDigitalActionHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( ControllerDigitalActionHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct ControllerAnalogActionHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerAnalogActionHandle_t( ulong value )
		{
			return new ControllerAnalogActionHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( ControllerAnalogActionHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct UGCQueryHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCQueryHandle_t( ulong value )
		{
			return new UGCQueryHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( UGCQueryHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct UGCUpdateHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCUpdateHandle_t( ulong value )
		{
			return new UGCUpdateHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( UGCUpdateHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct HHTMLBrowser
	{
		public uint Value;
		
		public static implicit operator HHTMLBrowser( uint value )
		{
			return new HHTMLBrowser(){ Value = value };
		}
		
		public static implicit operator uint( HHTMLBrowser value )
		{
			return value.Value;
		}
	}
	
	internal struct SteamItemInstanceID_t
	{
		public ulong Value;
		
		public static implicit operator SteamItemInstanceID_t( ulong value )
		{
			return new SteamItemInstanceID_t(){ Value = value };
		}
		
		public static implicit operator ulong( SteamItemInstanceID_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SteamItemDef_t
	{
		public int Value;
		
		public static implicit operator SteamItemDef_t( int value )
		{
			return new SteamItemDef_t(){ Value = value };
		}
		
		public static implicit operator int( SteamItemDef_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SteamInventoryResult_t
	{
		public int Value;
		
		public static implicit operator SteamInventoryResult_t( int value )
		{
			return new SteamInventoryResult_t(){ Value = value };
		}
		
		public static implicit operator int( SteamInventoryResult_t value )
		{
			return value.Value;
		}
	}
	
	internal struct SteamInventoryUpdateHandle_t
	{
		public ulong Value;
		
		public static implicit operator SteamInventoryUpdateHandle_t( ulong value )
		{
			return new SteamInventoryUpdateHandle_t(){ Value = value };
		}
		
		public static implicit operator ulong( SteamInventoryUpdateHandle_t value )
		{
			return value.Value;
		}
	}
	
	internal struct CGameID
	{
		public ulong Value;
		
		public static implicit operator CGameID( ulong value )
		{
			return new CGameID(){ Value = value };
		}
		
		public static implicit operator ulong( CGameID value )
		{
			return value.Value;
		}
	}
	
	internal struct CSteamID
	{
		public ulong Value;
		
		public static implicit operator CSteamID( ulong value )
		{
			return new CSteamID(){ Value = value };
		}
		
		public static implicit operator ulong( CSteamID value )
		{
			return value.Value;
		}
	}
	
}
