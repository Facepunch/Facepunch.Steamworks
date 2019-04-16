using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;

namespace Steamworks.Data
{
	internal struct GID_t
	{
		public ulong Value;
		
		public static implicit operator GID_t( ulong value ) => new GID_t(){ Value = value };
		public static implicit operator ulong( GID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct JobID_t
	{
		public ulong Value;
		
		public static implicit operator JobID_t( ulong value ) => new JobID_t(){ Value = value };
		public static implicit operator ulong( JobID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct TxnID_t
	{
		public GID_t Value;
		
		public static implicit operator TxnID_t( GID_t value ) => new TxnID_t(){ Value = value };
		public static implicit operator GID_t( TxnID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct PackageId_t
	{
		public uint Value;
		
		public static implicit operator PackageId_t( uint value ) => new PackageId_t(){ Value = value };
		public static implicit operator uint( PackageId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct BundleId_t
	{
		public uint Value;
		
		public static implicit operator BundleId_t( uint value ) => new BundleId_t(){ Value = value };
		public static implicit operator uint( BundleId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct AppId_t
	{
		public uint Value;
		
		public static implicit operator AppId_t( uint value ) => new AppId_t(){ Value = value };
		public static implicit operator uint( AppId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct AssetClassId_t
	{
		public ulong Value;
		
		public static implicit operator AssetClassId_t( ulong value ) => new AssetClassId_t(){ Value = value };
		public static implicit operator ulong( AssetClassId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct PhysicalItemId_t
	{
		public uint Value;
		
		public static implicit operator PhysicalItemId_t( uint value ) => new PhysicalItemId_t(){ Value = value };
		public static implicit operator uint( PhysicalItemId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct DepotId_t
	{
		public uint Value;
		
		public static implicit operator DepotId_t( uint value ) => new DepotId_t(){ Value = value };
		public static implicit operator uint( DepotId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct RTime32
	{
		public uint Value;
		
		public static implicit operator RTime32( uint value ) => new RTime32(){ Value = value };
		public static implicit operator uint( RTime32 value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct CellID_t
	{
		public uint Value;
		
		public static implicit operator CellID_t( uint value ) => new CellID_t(){ Value = value };
		public static implicit operator uint( CellID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SteamAPICall_t
	{
		public ulong Value;
		
		public static implicit operator SteamAPICall_t( ulong value ) => new SteamAPICall_t(){ Value = value };
		public static implicit operator ulong( SteamAPICall_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct AccountID_t
	{
		public uint Value;
		
		public static implicit operator AccountID_t( uint value ) => new AccountID_t(){ Value = value };
		public static implicit operator uint( AccountID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct PartnerId_t
	{
		public uint Value;
		
		public static implicit operator PartnerId_t( uint value ) => new PartnerId_t(){ Value = value };
		public static implicit operator uint( PartnerId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct ManifestId_t
	{
		public ulong Value;
		
		public static implicit operator ManifestId_t( ulong value ) => new ManifestId_t(){ Value = value };
		public static implicit operator ulong( ManifestId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SiteId_t
	{
		public ulong Value;
		
		public static implicit operator SiteId_t( ulong value ) => new SiteId_t(){ Value = value };
		public static implicit operator ulong( SiteId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct PartyBeaconID_t
	{
		public ulong Value;
		
		public static implicit operator PartyBeaconID_t( ulong value ) => new PartyBeaconID_t(){ Value = value };
		public static implicit operator ulong( PartyBeaconID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HAuthTicket
	{
		public uint Value;
		
		public static implicit operator HAuthTicket( uint value ) => new HAuthTicket(){ Value = value };
		public static implicit operator uint( HAuthTicket value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct BREAKPAD_HANDLE
	{
		public IntPtr Value;
		
		public static implicit operator BREAKPAD_HANDLE( IntPtr value ) => new BREAKPAD_HANDLE(){ Value = value };
		public static implicit operator IntPtr( BREAKPAD_HANDLE value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HSteamPipe
	{
		public int Value;
		
		public static implicit operator HSteamPipe( int value ) => new HSteamPipe(){ Value = value };
		public static implicit operator int( HSteamPipe value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HSteamUser
	{
		public int Value;
		
		public static implicit operator HSteamUser( int value ) => new HSteamUser(){ Value = value };
		public static implicit operator int( HSteamUser value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct FriendsGroupID_t
	{
		public short Value;
		
		public static implicit operator FriendsGroupID_t( short value ) => new FriendsGroupID_t(){ Value = value };
		public static implicit operator short( FriendsGroupID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HServerListRequest
	{
		public IntPtr Value;
		
		public static implicit operator HServerListRequest( IntPtr value ) => new HServerListRequest(){ Value = value };
		public static implicit operator IntPtr( HServerListRequest value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HServerQuery
	{
		public int Value;
		
		public static implicit operator HServerQuery( int value ) => new HServerQuery(){ Value = value };
		public static implicit operator int( HServerQuery value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct UGCHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCHandle_t( ulong value ) => new UGCHandle_t(){ Value = value };
		public static implicit operator ulong( UGCHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct PublishedFileUpdateHandle_t
	{
		public ulong Value;
		
		public static implicit operator PublishedFileUpdateHandle_t( ulong value ) => new PublishedFileUpdateHandle_t(){ Value = value };
		public static implicit operator ulong( PublishedFileUpdateHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	public struct PublishedFileId
	{
		public ulong Value;
		
		public static implicit operator PublishedFileId( ulong value ) => new PublishedFileId(){ Value = value };
		public static implicit operator ulong( PublishedFileId value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct UGCFileWriteStreamHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCFileWriteStreamHandle_t( ulong value ) => new UGCFileWriteStreamHandle_t(){ Value = value };
		public static implicit operator ulong( UGCFileWriteStreamHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SteamLeaderboard_t
	{
		public ulong Value;
		
		public static implicit operator SteamLeaderboard_t( ulong value ) => new SteamLeaderboard_t(){ Value = value };
		public static implicit operator ulong( SteamLeaderboard_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SteamLeaderboardEntries_t
	{
		public ulong Value;
		
		public static implicit operator SteamLeaderboardEntries_t( ulong value ) => new SteamLeaderboardEntries_t(){ Value = value };
		public static implicit operator ulong( SteamLeaderboardEntries_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SNetSocket_t
	{
		public uint Value;
		
		public static implicit operator SNetSocket_t( uint value ) => new SNetSocket_t(){ Value = value };
		public static implicit operator uint( SNetSocket_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SNetListenSocket_t
	{
		public uint Value;
		
		public static implicit operator SNetListenSocket_t( uint value ) => new SNetListenSocket_t(){ Value = value };
		public static implicit operator uint( SNetListenSocket_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct ScreenshotHandle
	{
		public uint Value;
		
		public static implicit operator ScreenshotHandle( uint value ) => new ScreenshotHandle(){ Value = value };
		public static implicit operator uint( ScreenshotHandle value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HTTPRequestHandle
	{
		public uint Value;
		
		public static implicit operator HTTPRequestHandle( uint value ) => new HTTPRequestHandle(){ Value = value };
		public static implicit operator uint( HTTPRequestHandle value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HTTPCookieContainerHandle
	{
		public uint Value;
		
		public static implicit operator HTTPCookieContainerHandle( uint value ) => new HTTPCookieContainerHandle(){ Value = value };
		public static implicit operator uint( HTTPCookieContainerHandle value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct InputHandle_t
	{
		public ulong Value;
		
		public static implicit operator InputHandle_t( ulong value ) => new InputHandle_t(){ Value = value };
		public static implicit operator ulong( InputHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct InputActionSetHandle_t
	{
		public ulong Value;
		
		public static implicit operator InputActionSetHandle_t( ulong value ) => new InputActionSetHandle_t(){ Value = value };
		public static implicit operator ulong( InputActionSetHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct InputDigitalActionHandle_t
	{
		public ulong Value;
		
		public static implicit operator InputDigitalActionHandle_t( ulong value ) => new InputDigitalActionHandle_t(){ Value = value };
		public static implicit operator ulong( InputDigitalActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct InputAnalogActionHandle_t
	{
		public ulong Value;
		
		public static implicit operator InputAnalogActionHandle_t( ulong value ) => new InputAnalogActionHandle_t(){ Value = value };
		public static implicit operator ulong( InputAnalogActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct ControllerHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerHandle_t( ulong value ) => new ControllerHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct ControllerActionSetHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerActionSetHandle_t( ulong value ) => new ControllerActionSetHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerActionSetHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct ControllerDigitalActionHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerDigitalActionHandle_t( ulong value ) => new ControllerDigitalActionHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerDigitalActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct ControllerAnalogActionHandle_t
	{
		public ulong Value;
		
		public static implicit operator ControllerAnalogActionHandle_t( ulong value ) => new ControllerAnalogActionHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerAnalogActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct UGCQueryHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCQueryHandle_t( ulong value ) => new UGCQueryHandle_t(){ Value = value };
		public static implicit operator ulong( UGCQueryHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct UGCUpdateHandle_t
	{
		public ulong Value;
		
		public static implicit operator UGCUpdateHandle_t( ulong value ) => new UGCUpdateHandle_t(){ Value = value };
		public static implicit operator ulong( UGCUpdateHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct HHTMLBrowser
	{
		public uint Value;
		
		public static implicit operator HHTMLBrowser( uint value ) => new HHTMLBrowser(){ Value = value };
		public static implicit operator uint( HHTMLBrowser value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SteamItemInstanceID_t
	{
		public ulong Value;
		
		public static implicit operator SteamItemInstanceID_t( ulong value ) => new SteamItemInstanceID_t(){ Value = value };
		public static implicit operator ulong( SteamItemInstanceID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SteamItemDef_t
	{
		public int Value;
		
		public static implicit operator SteamItemDef_t( int value ) => new SteamItemDef_t(){ Value = value };
		public static implicit operator int( SteamItemDef_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SteamInventoryResult_t
	{
		public int Value;
		
		public static implicit operator SteamInventoryResult_t( int value ) => new SteamInventoryResult_t(){ Value = value };
		public static implicit operator int( SteamInventoryResult_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
	internal struct SteamInventoryUpdateHandle_t
	{
		public ulong Value;
		
		public static implicit operator SteamInventoryUpdateHandle_t( ulong value ) => new SteamInventoryUpdateHandle_t(){ Value = value };
		public static implicit operator ulong( SteamInventoryUpdateHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
	}
	
}
