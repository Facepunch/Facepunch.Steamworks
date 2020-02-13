using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingUtils : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamNetworkingUtils();
		
		
		internal ISteamNetworkingUtils()
		{
			SetupInterface();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_AllocateMessage")]
		private static extern IntPtr _AllocateMessage( IntPtr self, int cbAllocateBuffer );
		
		#endregion
		internal NetMsg AllocateMessage( int cbAllocateBuffer )
		{
			var returnValue = _AllocateMessage( Self, cbAllocateBuffer );
			return NetMsg.Fill( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_InitRelayNetworkAccess")]
		private static extern void _InitRelayNetworkAccess( IntPtr self );
		
		#endregion
		internal void InitRelayNetworkAccess()
		{
			_InitRelayNetworkAccess( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus")]
		private static extern SteamNetworkingAvailability _GetRelayNetworkStatus( IntPtr self, ref SteamRelayNetworkStatus_t pDetails );
		
		#endregion
		internal SteamNetworkingAvailability GetRelayNetworkStatus( ref SteamRelayNetworkStatus_t pDetails )
		{
			var returnValue = _GetRelayNetworkStatus( Self, ref pDetails );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalPingLocation")]
		private static extern float _GetLocalPingLocation( IntPtr self, ref PingLocation result );
		
		#endregion
		internal float GetLocalPingLocation( ref PingLocation result )
		{
			var returnValue = _GetLocalPingLocation( Self, ref result );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations")]
		private static extern int _EstimatePingTimeBetweenTwoLocations( IntPtr self, ref PingLocation location1, ref PingLocation location2 );
		
		#endregion
		internal int EstimatePingTimeBetweenTwoLocations( ref PingLocation location1, ref PingLocation location2 )
		{
			var returnValue = _EstimatePingTimeBetweenTwoLocations( Self, ref location1, ref location2 );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost")]
		private static extern int _EstimatePingTimeFromLocalHost( IntPtr self, ref PingLocation remoteLocation );
		
		#endregion
		internal int EstimatePingTimeFromLocalHost( ref PingLocation remoteLocation )
		{
			var returnValue = _EstimatePingTimeFromLocalHost( Self, ref remoteLocation );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString")]
		private static extern void _ConvertPingLocationToString( IntPtr self, ref PingLocation location, IntPtr pszBuf, int cchBufSize );
		
		#endregion
		internal void ConvertPingLocationToString( ref PingLocation location, out string pszBuf )
		{
			IntPtr mempszBuf = Helpers.TakeMemory();
			_ConvertPingLocationToString( Self, ref location, mempszBuf, (1024 * 32) );
			pszBuf = Helpers.MemoryToString( mempszBuf );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ParsePingLocationString")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ParsePingLocationString( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszString, ref PingLocation result );
		
		#endregion
		internal bool ParsePingLocationString( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszString, ref PingLocation result )
		{
			var returnValue = _ParsePingLocationString( Self, pszString, ref result );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_CheckPingDataUpToDate")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CheckPingDataUpToDate( IntPtr self, float flMaxAgeSeconds );
		
		#endregion
		internal bool CheckPingDataUpToDate( float flMaxAgeSeconds )
		{
			var returnValue = _CheckPingDataUpToDate( Self, flMaxAgeSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPingToDataCenter")]
		private static extern int _GetPingToDataCenter( IntPtr self, SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP );
		
		#endregion
		internal int GetPingToDataCenter( SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP )
		{
			var returnValue = _GetPingToDataCenter( Self, popID, ref pViaRelayPoP );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetDirectPingToPOP")]
		private static extern int _GetDirectPingToPOP( IntPtr self, SteamNetworkingPOPID popID );
		
		#endregion
		internal int GetDirectPingToPOP( SteamNetworkingPOPID popID )
		{
			var returnValue = _GetDirectPingToPOP( Self, popID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPCount")]
		private static extern int _GetPOPCount( IntPtr self );
		
		#endregion
		internal int GetPOPCount()
		{
			var returnValue = _GetPOPCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPList")]
		private static extern int _GetPOPList( IntPtr self, ref SteamNetworkingPOPID list, int nListSz );
		
		#endregion
		internal int GetPOPList( ref SteamNetworkingPOPID list, int nListSz )
		{
			var returnValue = _GetPOPList( Self, ref list, nListSz );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalTimestamp")]
		private static extern long _GetLocalTimestamp( IntPtr self );
		
		#endregion
		internal long GetLocalTimestamp()
		{
			var returnValue = _GetLocalTimestamp( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction")]
		private static extern void _SetDebugOutputFunction( IntPtr self, DebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc );
		
		#endregion
		internal void SetDebugOutputFunction( DebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc )
		{
			_SetDebugOutputFunction( Self, eDetailLevel, pfnFunc );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueInt32")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetGlobalConfigValueInt32( IntPtr self, SteamNetworkingConfigValue eValue, int val );
		
		#endregion
		internal bool SetGlobalConfigValueInt32( SteamNetworkingConfigValue eValue, int val )
		{
			var returnValue = _SetGlobalConfigValueInt32( Self, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueFloat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetGlobalConfigValueFloat( IntPtr self, SteamNetworkingConfigValue eValue, float val );
		
		#endregion
		internal bool SetGlobalConfigValueFloat( SteamNetworkingConfigValue eValue, float val )
		{
			var returnValue = _SetGlobalConfigValueFloat( Self, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueString")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetGlobalConfigValueString( IntPtr self, SteamNetworkingConfigValue eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val );
		
		#endregion
		internal bool SetGlobalConfigValueString( SteamNetworkingConfigValue eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val )
		{
			var returnValue = _SetGlobalConfigValueString( Self, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueInt32")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConnectionConfigValueInt32( IntPtr self, Connection hConn, SteamNetworkingConfigValue eValue, int val );
		
		#endregion
		internal bool SetConnectionConfigValueInt32( Connection hConn, SteamNetworkingConfigValue eValue, int val )
		{
			var returnValue = _SetConnectionConfigValueInt32( Self, hConn, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueFloat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConnectionConfigValueFloat( IntPtr self, Connection hConn, SteamNetworkingConfigValue eValue, float val );
		
		#endregion
		internal bool SetConnectionConfigValueFloat( Connection hConn, SteamNetworkingConfigValue eValue, float val )
		{
			var returnValue = _SetConnectionConfigValueFloat( Self, hConn, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueString")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConnectionConfigValueString( IntPtr self, Connection hConn, SteamNetworkingConfigValue eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val );
		
		#endregion
		internal bool SetConnectionConfigValueString( Connection hConn, SteamNetworkingConfigValue eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val )
		{
			var returnValue = _SetConnectionConfigValueString( Self, hConn, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConfigValue")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConfigValue( IntPtr self, SteamNetworkingConfigValue eValue, SteamNetworkingConfigScope eScopeType, long scopeObj, SteamNetworkingConfigDataType eDataType, IntPtr pArg );
		
		#endregion
		internal bool SetConfigValue( SteamNetworkingConfigValue eValue, SteamNetworkingConfigScope eScopeType, long scopeObj, SteamNetworkingConfigDataType eDataType, IntPtr pArg )
		{
			var returnValue = _SetConfigValue( Self, eValue, eScopeType, scopeObj, eDataType, pArg );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConfigValueStruct")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConfigValueStruct( IntPtr self, ref SteamNetworkingConfigValue_t opt, SteamNetworkingConfigScope eScopeType, long scopeObj );
		
		#endregion
		internal bool SetConfigValueStruct( ref SteamNetworkingConfigValue_t opt, SteamNetworkingConfigScope eScopeType, long scopeObj )
		{
			var returnValue = _SetConfigValueStruct( Self, ref opt, eScopeType, scopeObj );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValue")]
		private static extern NetConfigResult _GetConfigValue( IntPtr self, SteamNetworkingConfigValue eValue, SteamNetworkingConfigScope eScopeType, long scopeObj, ref SteamNetworkingConfigDataType pOutDataType, IntPtr pResult, ref UIntPtr cbResult );
		
		#endregion
		internal NetConfigResult GetConfigValue( SteamNetworkingConfigValue eValue, SteamNetworkingConfigScope eScopeType, long scopeObj, ref SteamNetworkingConfigDataType pOutDataType, IntPtr pResult, ref UIntPtr cbResult )
		{
			var returnValue = _GetConfigValue( Self, eValue, eScopeType, scopeObj, ref pOutDataType, pResult, ref cbResult );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValueInfo")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetConfigValueInfo( IntPtr self, SteamNetworkingConfigValue eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pOutName, ref SteamNetworkingConfigDataType pOutDataType, [In,Out] SteamNetworkingConfigScope[]  pOutScope, [In,Out] SteamNetworkingConfigValue[]  pOutNextValue );
		
		#endregion
		internal bool GetConfigValueInfo( SteamNetworkingConfigValue eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pOutName, ref SteamNetworkingConfigDataType pOutDataType, [In,Out] SteamNetworkingConfigScope[]  pOutScope, [In,Out] SteamNetworkingConfigValue[]  pOutNextValue )
		{
			var returnValue = _GetConfigValueInfo( Self, eValue, pOutName, ref pOutDataType, pOutScope, pOutNextValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetFirstConfigValue")]
		private static extern SteamNetworkingConfigValue _GetFirstConfigValue( IntPtr self );
		
		#endregion
		internal SteamNetworkingConfigValue GetFirstConfigValue()
		{
			var returnValue = _GetFirstConfigValue( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString")]
		private static extern void _SteamNetworkingIPAddr_ToString( IntPtr self, ref NetAddress addr, IntPtr buf, uint cbBuf, [MarshalAs( UnmanagedType.U1 )] bool bWithPort );
		
		#endregion
		internal void SteamNetworkingIPAddr_ToString( ref NetAddress addr, out string buf, [MarshalAs( UnmanagedType.U1 )] bool bWithPort )
		{
			IntPtr membuf = Helpers.TakeMemory();
			_SteamNetworkingIPAddr_ToString( Self, ref addr, membuf, (1024 * 32), bWithPort );
			buf = Helpers.MemoryToString( membuf );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SteamNetworkingIPAddr_ParseString( IntPtr self, ref NetAddress pAddr, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszStr );
		
		#endregion
		internal bool SteamNetworkingIPAddr_ParseString( ref NetAddress pAddr, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszStr )
		{
			var returnValue = _SteamNetworkingIPAddr_ParseString( Self, ref pAddr, pszStr );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString")]
		private static extern void _SteamNetworkingIdentity_ToString( IntPtr self, ref NetIdentity identity, IntPtr buf, uint cbBuf );
		
		#endregion
		internal void SteamNetworkingIdentity_ToString( ref NetIdentity identity, out string buf )
		{
			IntPtr membuf = Helpers.TakeMemory();
			_SteamNetworkingIdentity_ToString( Self, ref identity, membuf, (1024 * 32) );
			buf = Helpers.MemoryToString( membuf );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SteamNetworkingIdentity_ParseString( IntPtr self, ref NetIdentity pIdentity, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszStr );
		
		#endregion
		internal bool SteamNetworkingIdentity_ParseString( ref NetIdentity pIdentity, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszStr )
		{
			var returnValue = _SteamNetworkingIdentity_ParseString( Self, ref pIdentity, pszStr );
			return returnValue;
		}
		
	}
}
