using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	internal struct GID_t : IEquatable<GID_t>, IComparable<GID_t>
	{
		public ulong Value;
		
		public static implicit operator GID_t( ulong value ) => new GID_t(){ Value = value };
		public static implicit operator ulong( GID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (GID_t) p );
		public bool Equals( GID_t p ) => p.Value == Value;
		public static bool operator ==( GID_t a, GID_t b ) => a.Equals( b );
		public static bool operator !=( GID_t a, GID_t b ) => !a.Equals( b );
		public int CompareTo( GID_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct JobID_t : IEquatable<JobID_t>, IComparable<JobID_t>
	{
		public ulong Value;
		
		public static implicit operator JobID_t( ulong value ) => new JobID_t(){ Value = value };
		public static implicit operator ulong( JobID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (JobID_t) p );
		public bool Equals( JobID_t p ) => p.Value == Value;
		public static bool operator ==( JobID_t a, JobID_t b ) => a.Equals( b );
		public static bool operator !=( JobID_t a, JobID_t b ) => !a.Equals( b );
		public int CompareTo( JobID_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct TxnID_t : IEquatable<TxnID_t>, IComparable<TxnID_t>
	{
		public GID_t Value;
		
		public static implicit operator TxnID_t( GID_t value ) => new TxnID_t(){ Value = value };
		public static implicit operator GID_t( TxnID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (TxnID_t) p );
		public bool Equals( TxnID_t p ) => p.Value == Value;
		public static bool operator ==( TxnID_t a, TxnID_t b ) => a.Equals( b );
		public static bool operator !=( TxnID_t a, TxnID_t b ) => !a.Equals( b );
		public int CompareTo( TxnID_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct PackageId_t : IEquatable<PackageId_t>, IComparable<PackageId_t>
	{
		public uint Value;
		
		public static implicit operator PackageId_t( uint value ) => new PackageId_t(){ Value = value };
		public static implicit operator uint( PackageId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (PackageId_t) p );
		public bool Equals( PackageId_t p ) => p.Value == Value;
		public static bool operator ==( PackageId_t a, PackageId_t b ) => a.Equals( b );
		public static bool operator !=( PackageId_t a, PackageId_t b ) => !a.Equals( b );
		public int CompareTo( PackageId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct BundleId_t : IEquatable<BundleId_t>, IComparable<BundleId_t>
	{
		public uint Value;
		
		public static implicit operator BundleId_t( uint value ) => new BundleId_t(){ Value = value };
		public static implicit operator uint( BundleId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (BundleId_t) p );
		public bool Equals( BundleId_t p ) => p.Value == Value;
		public static bool operator ==( BundleId_t a, BundleId_t b ) => a.Equals( b );
		public static bool operator !=( BundleId_t a, BundleId_t b ) => !a.Equals( b );
		public int CompareTo( BundleId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct AssetClassId_t : IEquatable<AssetClassId_t>, IComparable<AssetClassId_t>
	{
		public ulong Value;
		
		public static implicit operator AssetClassId_t( ulong value ) => new AssetClassId_t(){ Value = value };
		public static implicit operator ulong( AssetClassId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (AssetClassId_t) p );
		public bool Equals( AssetClassId_t p ) => p.Value == Value;
		public static bool operator ==( AssetClassId_t a, AssetClassId_t b ) => a.Equals( b );
		public static bool operator !=( AssetClassId_t a, AssetClassId_t b ) => !a.Equals( b );
		public int CompareTo( AssetClassId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct PhysicalItemId_t : IEquatable<PhysicalItemId_t>, IComparable<PhysicalItemId_t>
	{
		public uint Value;
		
		public static implicit operator PhysicalItemId_t( uint value ) => new PhysicalItemId_t(){ Value = value };
		public static implicit operator uint( PhysicalItemId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (PhysicalItemId_t) p );
		public bool Equals( PhysicalItemId_t p ) => p.Value == Value;
		public static bool operator ==( PhysicalItemId_t a, PhysicalItemId_t b ) => a.Equals( b );
		public static bool operator !=( PhysicalItemId_t a, PhysicalItemId_t b ) => !a.Equals( b );
		public int CompareTo( PhysicalItemId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct DepotId_t : IEquatable<DepotId_t>, IComparable<DepotId_t>
	{
		public uint Value;
		
		public static implicit operator DepotId_t( uint value ) => new DepotId_t(){ Value = value };
		public static implicit operator uint( DepotId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (DepotId_t) p );
		public bool Equals( DepotId_t p ) => p.Value == Value;
		public static bool operator ==( DepotId_t a, DepotId_t b ) => a.Equals( b );
		public static bool operator !=( DepotId_t a, DepotId_t b ) => !a.Equals( b );
		public int CompareTo( DepotId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct RTime32 : IEquatable<RTime32>, IComparable<RTime32>
	{
		public uint Value;
		
		public static implicit operator RTime32( uint value ) => new RTime32(){ Value = value };
		public static implicit operator uint( RTime32 value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (RTime32) p );
		public bool Equals( RTime32 p ) => p.Value == Value;
		public static bool operator ==( RTime32 a, RTime32 b ) => a.Equals( b );
		public static bool operator !=( RTime32 a, RTime32 b ) => !a.Equals( b );
		public int CompareTo( RTime32 other ) => Value.CompareTo( other.Value );
	}
	
	internal struct CellID_t : IEquatable<CellID_t>, IComparable<CellID_t>
	{
		public uint Value;
		
		public static implicit operator CellID_t( uint value ) => new CellID_t(){ Value = value };
		public static implicit operator uint( CellID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (CellID_t) p );
		public bool Equals( CellID_t p ) => p.Value == Value;
		public static bool operator ==( CellID_t a, CellID_t b ) => a.Equals( b );
		public static bool operator !=( CellID_t a, CellID_t b ) => !a.Equals( b );
		public int CompareTo( CellID_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		public ulong Value;
		
		public static implicit operator SteamAPICall_t( ulong value ) => new SteamAPICall_t(){ Value = value };
		public static implicit operator ulong( SteamAPICall_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SteamAPICall_t) p );
		public bool Equals( SteamAPICall_t p ) => p.Value == Value;
		public static bool operator ==( SteamAPICall_t a, SteamAPICall_t b ) => a.Equals( b );
		public static bool operator !=( SteamAPICall_t a, SteamAPICall_t b ) => !a.Equals( b );
		public int CompareTo( SteamAPICall_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		public uint Value;
		
		public static implicit operator AccountID_t( uint value ) => new AccountID_t(){ Value = value };
		public static implicit operator uint( AccountID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (AccountID_t) p );
		public bool Equals( AccountID_t p ) => p.Value == Value;
		public static bool operator ==( AccountID_t a, AccountID_t b ) => a.Equals( b );
		public static bool operator !=( AccountID_t a, AccountID_t b ) => !a.Equals( b );
		public int CompareTo( AccountID_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct PartnerId_t : IEquatable<PartnerId_t>, IComparable<PartnerId_t>
	{
		public uint Value;
		
		public static implicit operator PartnerId_t( uint value ) => new PartnerId_t(){ Value = value };
		public static implicit operator uint( PartnerId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (PartnerId_t) p );
		public bool Equals( PartnerId_t p ) => p.Value == Value;
		public static bool operator ==( PartnerId_t a, PartnerId_t b ) => a.Equals( b );
		public static bool operator !=( PartnerId_t a, PartnerId_t b ) => !a.Equals( b );
		public int CompareTo( PartnerId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct ManifestId_t : IEquatable<ManifestId_t>, IComparable<ManifestId_t>
	{
		public ulong Value;
		
		public static implicit operator ManifestId_t( ulong value ) => new ManifestId_t(){ Value = value };
		public static implicit operator ulong( ManifestId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (ManifestId_t) p );
		public bool Equals( ManifestId_t p ) => p.Value == Value;
		public static bool operator ==( ManifestId_t a, ManifestId_t b ) => a.Equals( b );
		public static bool operator !=( ManifestId_t a, ManifestId_t b ) => !a.Equals( b );
		public int CompareTo( ManifestId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SiteId_t : IEquatable<SiteId_t>, IComparable<SiteId_t>
	{
		public ulong Value;
		
		public static implicit operator SiteId_t( ulong value ) => new SiteId_t(){ Value = value };
		public static implicit operator ulong( SiteId_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SiteId_t) p );
		public bool Equals( SiteId_t p ) => p.Value == Value;
		public static bool operator ==( SiteId_t a, SiteId_t b ) => a.Equals( b );
		public static bool operator !=( SiteId_t a, SiteId_t b ) => !a.Equals( b );
		public int CompareTo( SiteId_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct PartyBeaconID_t : IEquatable<PartyBeaconID_t>, IComparable<PartyBeaconID_t>
	{
		public ulong Value;
		
		public static implicit operator PartyBeaconID_t( ulong value ) => new PartyBeaconID_t(){ Value = value };
		public static implicit operator ulong( PartyBeaconID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (PartyBeaconID_t) p );
		public bool Equals( PartyBeaconID_t p ) => p.Value == Value;
		public static bool operator ==( PartyBeaconID_t a, PartyBeaconID_t b ) => a.Equals( b );
		public static bool operator !=( PartyBeaconID_t a, PartyBeaconID_t b ) => !a.Equals( b );
		public int CompareTo( PartyBeaconID_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		public uint Value;
		
		public static implicit operator HAuthTicket( uint value ) => new HAuthTicket(){ Value = value };
		public static implicit operator uint( HAuthTicket value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HAuthTicket) p );
		public bool Equals( HAuthTicket p ) => p.Value == Value;
		public static bool operator ==( HAuthTicket a, HAuthTicket b ) => a.Equals( b );
		public static bool operator !=( HAuthTicket a, HAuthTicket b ) => !a.Equals( b );
		public int CompareTo( HAuthTicket other ) => Value.CompareTo( other.Value );
	}
	
	internal struct BREAKPAD_HANDLE : IEquatable<BREAKPAD_HANDLE>, IComparable<BREAKPAD_HANDLE>
	{
		public IntPtr Value;
		
		public static implicit operator BREAKPAD_HANDLE( IntPtr value ) => new BREAKPAD_HANDLE(){ Value = value };
		public static implicit operator IntPtr( BREAKPAD_HANDLE value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (BREAKPAD_HANDLE) p );
		public bool Equals( BREAKPAD_HANDLE p ) => p.Value == Value;
		public static bool operator ==( BREAKPAD_HANDLE a, BREAKPAD_HANDLE b ) => a.Equals( b );
		public static bool operator !=( BREAKPAD_HANDLE a, BREAKPAD_HANDLE b ) => !a.Equals( b );
		public int CompareTo( BREAKPAD_HANDLE other ) => Value.ToInt64().CompareTo( other.Value.ToInt64() );
	}
	
	internal struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		public int Value;
		
		public static implicit operator HSteamPipe( int value ) => new HSteamPipe(){ Value = value };
		public static implicit operator int( HSteamPipe value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HSteamPipe) p );
		public bool Equals( HSteamPipe p ) => p.Value == Value;
		public static bool operator ==( HSteamPipe a, HSteamPipe b ) => a.Equals( b );
		public static bool operator !=( HSteamPipe a, HSteamPipe b ) => !a.Equals( b );
		public int CompareTo( HSteamPipe other ) => Value.CompareTo( other.Value );
	}
	
	internal struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		public int Value;
		
		public static implicit operator HSteamUser( int value ) => new HSteamUser(){ Value = value };
		public static implicit operator int( HSteamUser value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HSteamUser) p );
		public bool Equals( HSteamUser p ) => p.Value == Value;
		public static bool operator ==( HSteamUser a, HSteamUser b ) => a.Equals( b );
		public static bool operator !=( HSteamUser a, HSteamUser b ) => !a.Equals( b );
		public int CompareTo( HSteamUser other ) => Value.CompareTo( other.Value );
	}
	
	internal struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		public short Value;
		
		public static implicit operator FriendsGroupID_t( short value ) => new FriendsGroupID_t(){ Value = value };
		public static implicit operator short( FriendsGroupID_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (FriendsGroupID_t) p );
		public bool Equals( FriendsGroupID_t p ) => p.Value == Value;
		public static bool operator ==( FriendsGroupID_t a, FriendsGroupID_t b ) => a.Equals( b );
		public static bool operator !=( FriendsGroupID_t a, FriendsGroupID_t b ) => !a.Equals( b );
		public int CompareTo( FriendsGroupID_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct HServerListRequest : IEquatable<HServerListRequest>, IComparable<HServerListRequest>
	{
		public IntPtr Value;
		
		public static implicit operator HServerListRequest( IntPtr value ) => new HServerListRequest(){ Value = value };
		public static implicit operator IntPtr( HServerListRequest value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HServerListRequest) p );
		public bool Equals( HServerListRequest p ) => p.Value == Value;
		public static bool operator ==( HServerListRequest a, HServerListRequest b ) => a.Equals( b );
		public static bool operator !=( HServerListRequest a, HServerListRequest b ) => !a.Equals( b );
		public int CompareTo( HServerListRequest other ) => Value.ToInt64().CompareTo( other.Value.ToInt64() );
	}
	
	internal struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		public int Value;
		
		public static implicit operator HServerQuery( int value ) => new HServerQuery(){ Value = value };
		public static implicit operator int( HServerQuery value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HServerQuery) p );
		public bool Equals( HServerQuery p ) => p.Value == Value;
		public static bool operator ==( HServerQuery a, HServerQuery b ) => a.Equals( b );
		public static bool operator !=( HServerQuery a, HServerQuery b ) => !a.Equals( b );
		public int CompareTo( HServerQuery other ) => Value.CompareTo( other.Value );
	}
	
	internal struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		public ulong Value;
		
		public static implicit operator UGCHandle_t( ulong value ) => new UGCHandle_t(){ Value = value };
		public static implicit operator ulong( UGCHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (UGCHandle_t) p );
		public bool Equals( UGCHandle_t p ) => p.Value == Value;
		public static bool operator ==( UGCHandle_t a, UGCHandle_t b ) => a.Equals( b );
		public static bool operator !=( UGCHandle_t a, UGCHandle_t b ) => !a.Equals( b );
		public int CompareTo( UGCHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		public ulong Value;
		
		public static implicit operator PublishedFileUpdateHandle_t( ulong value ) => new PublishedFileUpdateHandle_t(){ Value = value };
		public static implicit operator ulong( PublishedFileUpdateHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (PublishedFileUpdateHandle_t) p );
		public bool Equals( PublishedFileUpdateHandle_t p ) => p.Value == Value;
		public static bool operator ==( PublishedFileUpdateHandle_t a, PublishedFileUpdateHandle_t b ) => a.Equals( b );
		public static bool operator !=( PublishedFileUpdateHandle_t a, PublishedFileUpdateHandle_t b ) => !a.Equals( b );
		public int CompareTo( PublishedFileUpdateHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	public struct PublishedFileId : IEquatable<PublishedFileId>, IComparable<PublishedFileId>
	{
		public ulong Value;
		
		public static implicit operator PublishedFileId( ulong value ) => new PublishedFileId(){ Value = value };
		public static implicit operator ulong( PublishedFileId value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (PublishedFileId) p );
		public bool Equals( PublishedFileId p ) => p.Value == Value;
		public static bool operator ==( PublishedFileId a, PublishedFileId b ) => a.Equals( b );
		public static bool operator !=( PublishedFileId a, PublishedFileId b ) => !a.Equals( b );
		public int CompareTo( PublishedFileId other ) => Value.CompareTo( other.Value );
	}
	
	internal struct UGCFileWriteStreamHandle_t : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
	{
		public ulong Value;
		
		public static implicit operator UGCFileWriteStreamHandle_t( ulong value ) => new UGCFileWriteStreamHandle_t(){ Value = value };
		public static implicit operator ulong( UGCFileWriteStreamHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (UGCFileWriteStreamHandle_t) p );
		public bool Equals( UGCFileWriteStreamHandle_t p ) => p.Value == Value;
		public static bool operator ==( UGCFileWriteStreamHandle_t a, UGCFileWriteStreamHandle_t b ) => a.Equals( b );
		public static bool operator !=( UGCFileWriteStreamHandle_t a, UGCFileWriteStreamHandle_t b ) => !a.Equals( b );
		public int CompareTo( UGCFileWriteStreamHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		public ulong Value;
		
		public static implicit operator SteamLeaderboard_t( ulong value ) => new SteamLeaderboard_t(){ Value = value };
		public static implicit operator ulong( SteamLeaderboard_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SteamLeaderboard_t) p );
		public bool Equals( SteamLeaderboard_t p ) => p.Value == Value;
		public static bool operator ==( SteamLeaderboard_t a, SteamLeaderboard_t b ) => a.Equals( b );
		public static bool operator !=( SteamLeaderboard_t a, SteamLeaderboard_t b ) => !a.Equals( b );
		public int CompareTo( SteamLeaderboard_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		public ulong Value;
		
		public static implicit operator SteamLeaderboardEntries_t( ulong value ) => new SteamLeaderboardEntries_t(){ Value = value };
		public static implicit operator ulong( SteamLeaderboardEntries_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SteamLeaderboardEntries_t) p );
		public bool Equals( SteamLeaderboardEntries_t p ) => p.Value == Value;
		public static bool operator ==( SteamLeaderboardEntries_t a, SteamLeaderboardEntries_t b ) => a.Equals( b );
		public static bool operator !=( SteamLeaderboardEntries_t a, SteamLeaderboardEntries_t b ) => !a.Equals( b );
		public int CompareTo( SteamLeaderboardEntries_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		public uint Value;
		
		public static implicit operator SNetSocket_t( uint value ) => new SNetSocket_t(){ Value = value };
		public static implicit operator uint( SNetSocket_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SNetSocket_t) p );
		public bool Equals( SNetSocket_t p ) => p.Value == Value;
		public static bool operator ==( SNetSocket_t a, SNetSocket_t b ) => a.Equals( b );
		public static bool operator !=( SNetSocket_t a, SNetSocket_t b ) => !a.Equals( b );
		public int CompareTo( SNetSocket_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		public uint Value;
		
		public static implicit operator SNetListenSocket_t( uint value ) => new SNetListenSocket_t(){ Value = value };
		public static implicit operator uint( SNetListenSocket_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SNetListenSocket_t) p );
		public bool Equals( SNetListenSocket_t p ) => p.Value == Value;
		public static bool operator ==( SNetListenSocket_t a, SNetListenSocket_t b ) => a.Equals( b );
		public static bool operator !=( SNetListenSocket_t a, SNetListenSocket_t b ) => !a.Equals( b );
		public int CompareTo( SNetListenSocket_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		public uint Value;
		
		public static implicit operator ScreenshotHandle( uint value ) => new ScreenshotHandle(){ Value = value };
		public static implicit operator uint( ScreenshotHandle value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (ScreenshotHandle) p );
		public bool Equals( ScreenshotHandle p ) => p.Value == Value;
		public static bool operator ==( ScreenshotHandle a, ScreenshotHandle b ) => a.Equals( b );
		public static bool operator !=( ScreenshotHandle a, ScreenshotHandle b ) => !a.Equals( b );
		public int CompareTo( ScreenshotHandle other ) => Value.CompareTo( other.Value );
	}
	
	internal struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		public uint Value;
		
		public static implicit operator HTTPRequestHandle( uint value ) => new HTTPRequestHandle(){ Value = value };
		public static implicit operator uint( HTTPRequestHandle value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HTTPRequestHandle) p );
		public bool Equals( HTTPRequestHandle p ) => p.Value == Value;
		public static bool operator ==( HTTPRequestHandle a, HTTPRequestHandle b ) => a.Equals( b );
		public static bool operator !=( HTTPRequestHandle a, HTTPRequestHandle b ) => !a.Equals( b );
		public int CompareTo( HTTPRequestHandle other ) => Value.CompareTo( other.Value );
	}
	
	internal struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		public uint Value;
		
		public static implicit operator HTTPCookieContainerHandle( uint value ) => new HTTPCookieContainerHandle(){ Value = value };
		public static implicit operator uint( HTTPCookieContainerHandle value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HTTPCookieContainerHandle) p );
		public bool Equals( HTTPCookieContainerHandle p ) => p.Value == Value;
		public static bool operator ==( HTTPCookieContainerHandle a, HTTPCookieContainerHandle b ) => a.Equals( b );
		public static bool operator !=( HTTPCookieContainerHandle a, HTTPCookieContainerHandle b ) => !a.Equals( b );
		public int CompareTo( HTTPCookieContainerHandle other ) => Value.CompareTo( other.Value );
	}
	
	internal struct InputHandle_t : IEquatable<InputHandle_t>, IComparable<InputHandle_t>
	{
		public ulong Value;
		
		public static implicit operator InputHandle_t( ulong value ) => new InputHandle_t(){ Value = value };
		public static implicit operator ulong( InputHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (InputHandle_t) p );
		public bool Equals( InputHandle_t p ) => p.Value == Value;
		public static bool operator ==( InputHandle_t a, InputHandle_t b ) => a.Equals( b );
		public static bool operator !=( InputHandle_t a, InputHandle_t b ) => !a.Equals( b );
		public int CompareTo( InputHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct InputActionSetHandle_t : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>
	{
		public ulong Value;
		
		public static implicit operator InputActionSetHandle_t( ulong value ) => new InputActionSetHandle_t(){ Value = value };
		public static implicit operator ulong( InputActionSetHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (InputActionSetHandle_t) p );
		public bool Equals( InputActionSetHandle_t p ) => p.Value == Value;
		public static bool operator ==( InputActionSetHandle_t a, InputActionSetHandle_t b ) => a.Equals( b );
		public static bool operator !=( InputActionSetHandle_t a, InputActionSetHandle_t b ) => !a.Equals( b );
		public int CompareTo( InputActionSetHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct InputDigitalActionHandle_t : IEquatable<InputDigitalActionHandle_t>, IComparable<InputDigitalActionHandle_t>
	{
		public ulong Value;
		
		public static implicit operator InputDigitalActionHandle_t( ulong value ) => new InputDigitalActionHandle_t(){ Value = value };
		public static implicit operator ulong( InputDigitalActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (InputDigitalActionHandle_t) p );
		public bool Equals( InputDigitalActionHandle_t p ) => p.Value == Value;
		public static bool operator ==( InputDigitalActionHandle_t a, InputDigitalActionHandle_t b ) => a.Equals( b );
		public static bool operator !=( InputDigitalActionHandle_t a, InputDigitalActionHandle_t b ) => !a.Equals( b );
		public int CompareTo( InputDigitalActionHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct InputAnalogActionHandle_t : IEquatable<InputAnalogActionHandle_t>, IComparable<InputAnalogActionHandle_t>
	{
		public ulong Value;
		
		public static implicit operator InputAnalogActionHandle_t( ulong value ) => new InputAnalogActionHandle_t(){ Value = value };
		public static implicit operator ulong( InputAnalogActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (InputAnalogActionHandle_t) p );
		public bool Equals( InputAnalogActionHandle_t p ) => p.Value == Value;
		public static bool operator ==( InputAnalogActionHandle_t a, InputAnalogActionHandle_t b ) => a.Equals( b );
		public static bool operator !=( InputAnalogActionHandle_t a, InputAnalogActionHandle_t b ) => !a.Equals( b );
		public int CompareTo( InputAnalogActionHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct ControllerHandle_t : IEquatable<ControllerHandle_t>, IComparable<ControllerHandle_t>
	{
		public ulong Value;
		
		public static implicit operator ControllerHandle_t( ulong value ) => new ControllerHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (ControllerHandle_t) p );
		public bool Equals( ControllerHandle_t p ) => p.Value == Value;
		public static bool operator ==( ControllerHandle_t a, ControllerHandle_t b ) => a.Equals( b );
		public static bool operator !=( ControllerHandle_t a, ControllerHandle_t b ) => !a.Equals( b );
		public int CompareTo( ControllerHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct ControllerActionSetHandle_t : IEquatable<ControllerActionSetHandle_t>, IComparable<ControllerActionSetHandle_t>
	{
		public ulong Value;
		
		public static implicit operator ControllerActionSetHandle_t( ulong value ) => new ControllerActionSetHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerActionSetHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (ControllerActionSetHandle_t) p );
		public bool Equals( ControllerActionSetHandle_t p ) => p.Value == Value;
		public static bool operator ==( ControllerActionSetHandle_t a, ControllerActionSetHandle_t b ) => a.Equals( b );
		public static bool operator !=( ControllerActionSetHandle_t a, ControllerActionSetHandle_t b ) => !a.Equals( b );
		public int CompareTo( ControllerActionSetHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct ControllerDigitalActionHandle_t : IEquatable<ControllerDigitalActionHandle_t>, IComparable<ControllerDigitalActionHandle_t>
	{
		public ulong Value;
		
		public static implicit operator ControllerDigitalActionHandle_t( ulong value ) => new ControllerDigitalActionHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerDigitalActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (ControllerDigitalActionHandle_t) p );
		public bool Equals( ControllerDigitalActionHandle_t p ) => p.Value == Value;
		public static bool operator ==( ControllerDigitalActionHandle_t a, ControllerDigitalActionHandle_t b ) => a.Equals( b );
		public static bool operator !=( ControllerDigitalActionHandle_t a, ControllerDigitalActionHandle_t b ) => !a.Equals( b );
		public int CompareTo( ControllerDigitalActionHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct ControllerAnalogActionHandle_t : IEquatable<ControllerAnalogActionHandle_t>, IComparable<ControllerAnalogActionHandle_t>
	{
		public ulong Value;
		
		public static implicit operator ControllerAnalogActionHandle_t( ulong value ) => new ControllerAnalogActionHandle_t(){ Value = value };
		public static implicit operator ulong( ControllerAnalogActionHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (ControllerAnalogActionHandle_t) p );
		public bool Equals( ControllerAnalogActionHandle_t p ) => p.Value == Value;
		public static bool operator ==( ControllerAnalogActionHandle_t a, ControllerAnalogActionHandle_t b ) => a.Equals( b );
		public static bool operator !=( ControllerAnalogActionHandle_t a, ControllerAnalogActionHandle_t b ) => !a.Equals( b );
		public int CompareTo( ControllerAnalogActionHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>, IComparable<UGCQueryHandle_t>
	{
		public ulong Value;
		
		public static implicit operator UGCQueryHandle_t( ulong value ) => new UGCQueryHandle_t(){ Value = value };
		public static implicit operator ulong( UGCQueryHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (UGCQueryHandle_t) p );
		public bool Equals( UGCQueryHandle_t p ) => p.Value == Value;
		public static bool operator ==( UGCQueryHandle_t a, UGCQueryHandle_t b ) => a.Equals( b );
		public static bool operator !=( UGCQueryHandle_t a, UGCQueryHandle_t b ) => !a.Equals( b );
		public int CompareTo( UGCQueryHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		public ulong Value;
		
		public static implicit operator UGCUpdateHandle_t( ulong value ) => new UGCUpdateHandle_t(){ Value = value };
		public static implicit operator ulong( UGCUpdateHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (UGCUpdateHandle_t) p );
		public bool Equals( UGCUpdateHandle_t p ) => p.Value == Value;
		public static bool operator ==( UGCUpdateHandle_t a, UGCUpdateHandle_t b ) => a.Equals( b );
		public static bool operator !=( UGCUpdateHandle_t a, UGCUpdateHandle_t b ) => !a.Equals( b );
		public int CompareTo( UGCUpdateHandle_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		public uint Value;
		
		public static implicit operator HHTMLBrowser( uint value ) => new HHTMLBrowser(){ Value = value };
		public static implicit operator uint( HHTMLBrowser value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (HHTMLBrowser) p );
		public bool Equals( HHTMLBrowser p ) => p.Value == Value;
		public static bool operator ==( HHTMLBrowser a, HHTMLBrowser b ) => a.Equals( b );
		public static bool operator !=( HHTMLBrowser a, HHTMLBrowser b ) => !a.Equals( b );
		public int CompareTo( HHTMLBrowser other ) => Value.CompareTo( other.Value );
	}
	
	public struct InventoryItemId : IEquatable<InventoryItemId>, IComparable<InventoryItemId>
	{
		public ulong Value;
		
		public static implicit operator InventoryItemId( ulong value ) => new InventoryItemId(){ Value = value };
		public static implicit operator ulong( InventoryItemId value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (InventoryItemId) p );
		public bool Equals( InventoryItemId p ) => p.Value == Value;
		public static bool operator ==( InventoryItemId a, InventoryItemId b ) => a.Equals( b );
		public static bool operator !=( InventoryItemId a, InventoryItemId b ) => !a.Equals( b );
		public int CompareTo( InventoryItemId other ) => Value.CompareTo( other.Value );
	}
	
	public struct InventoryDefId : IEquatable<InventoryDefId>, IComparable<InventoryDefId>
	{
		public int Value;
		
		public static implicit operator InventoryDefId( int value ) => new InventoryDefId(){ Value = value };
		public static implicit operator int( InventoryDefId value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (InventoryDefId) p );
		public bool Equals( InventoryDefId p ) => p.Value == Value;
		public static bool operator ==( InventoryDefId a, InventoryDefId b ) => a.Equals( b );
		public static bool operator !=( InventoryDefId a, InventoryDefId b ) => !a.Equals( b );
		public int CompareTo( InventoryDefId other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SteamInventoryResult_t : IEquatable<SteamInventoryResult_t>, IComparable<SteamInventoryResult_t>
	{
		public int Value;
		
		public static implicit operator SteamInventoryResult_t( int value ) => new SteamInventoryResult_t(){ Value = value };
		public static implicit operator int( SteamInventoryResult_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SteamInventoryResult_t) p );
		public bool Equals( SteamInventoryResult_t p ) => p.Value == Value;
		public static bool operator ==( SteamInventoryResult_t a, SteamInventoryResult_t b ) => a.Equals( b );
		public static bool operator !=( SteamInventoryResult_t a, SteamInventoryResult_t b ) => !a.Equals( b );
		public int CompareTo( SteamInventoryResult_t other ) => Value.CompareTo( other.Value );
	}
	
	internal struct SteamInventoryUpdateHandle_t : IEquatable<SteamInventoryUpdateHandle_t>, IComparable<SteamInventoryUpdateHandle_t>
	{
		public ulong Value;
		
		public static implicit operator SteamInventoryUpdateHandle_t( ulong value ) => new SteamInventoryUpdateHandle_t(){ Value = value };
		public static implicit operator ulong( SteamInventoryUpdateHandle_t value ) => value.Value;
		public override string ToString() => Value.ToString();
		public override int GetHashCode() => Value.GetHashCode();
		public override bool Equals( object p ) => this.Equals( (SteamInventoryUpdateHandle_t) p );
		public bool Equals( SteamInventoryUpdateHandle_t p ) => p.Value == Value;
		public static bool operator ==( SteamInventoryUpdateHandle_t a, SteamInventoryUpdateHandle_t b ) => a.Equals( b );
		public static bool operator !=( SteamInventoryUpdateHandle_t a, SteamInventoryUpdateHandle_t b ) => !a.Equals( b );
		public int CompareTo( SteamInventoryUpdateHandle_t other ) => Value.CompareTo( other.Value );
	}
	
}
