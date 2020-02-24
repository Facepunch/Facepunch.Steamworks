using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingUtils : SteamInterface
	{
		
		internal ISteamNetworkingUtils( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamNetworkingUtils_v003", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamNetworkingUtils_v003();
		public override IntPtr GetGlobalInterfacePointer() => SteamAPI_SteamNetworkingUtils_v003();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_AllocateMessage", CallingConvention = Platform.CC)]
		private static extern IntPtr _AllocateMessage( IntPtr self, int cbAllocateBuffer );
		
		#endregion
		internal NetMsg AllocateMessage( int cbAllocateBuffer )
		{
			var returnValue = _AllocateMessage( Self, cbAllocateBuffer );
			return returnValue.ToType<NetMsg>();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_InitRelayNetworkAccess", CallingConvention = Platform.CC)]
		private static extern void _InitRelayNetworkAccess( IntPtr self );
		
		#endregion
		internal void InitRelayNetworkAccess()
		{
			_InitRelayNetworkAccess( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetRelayNetworkStatus", CallingConvention = Platform.CC)]
		private static extern SteamNetworkingAvailability _GetRelayNetworkStatus( IntPtr self, ref SteamRelayNetworkStatus_t pDetails );
		
		#endregion
		internal SteamNetworkingAvailability GetRelayNetworkStatus( ref SteamRelayNetworkStatus_t pDetails )
		{
			var returnValue = _GetRelayNetworkStatus( Self, ref pDetails );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalPingLocation", CallingConvention = Platform.CC)]
		private static extern float _GetLocalPingLocation( IntPtr self, ref NetPingLocation result );
		
		#endregion
		internal float GetLocalPingLocation( ref NetPingLocation result )
		{
			var returnValue = _GetLocalPingLocation( Self, ref result );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeBetweenTwoLocations", CallingConvention = Platform.CC)]
		private static extern int _EstimatePingTimeBetweenTwoLocations( IntPtr self, ref NetPingLocation location1, ref NetPingLocation location2 );
		
		#endregion
		internal int EstimatePingTimeBetweenTwoLocations( ref NetPingLocation location1, ref NetPingLocation location2 )
		{
			var returnValue = _EstimatePingTimeBetweenTwoLocations( Self, ref location1, ref location2 );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_EstimatePingTimeFromLocalHost", CallingConvention = Platform.CC)]
		private static extern int _EstimatePingTimeFromLocalHost( IntPtr self, ref NetPingLocation remoteLocation );
		
		#endregion
		internal int EstimatePingTimeFromLocalHost( ref NetPingLocation remoteLocation )
		{
			var returnValue = _EstimatePingTimeFromLocalHost( Self, ref remoteLocation );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString", CallingConvention = Platform.CC)]
		private static extern void _ConvertPingLocationToString( IntPtr self, ref NetPingLocation location, IntPtr pszBuf, int cchBufSize );
		
		#endregion
		internal void ConvertPingLocationToString( ref NetPingLocation location, out string pszBuf )
		{
			IntPtr mempszBuf = Helpers.TakeMemory();
			_ConvertPingLocationToString( Self, ref location, mempszBuf, (1024 * 32) );
			pszBuf = Helpers.MemoryToString( mempszBuf );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_ParsePingLocationString", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ParsePingLocationString( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszString, ref NetPingLocation result );
		
		#endregion
		internal bool ParsePingLocationString( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszString, ref NetPingLocation result )
		{
			var returnValue = _ParsePingLocationString( Self, pszString, ref result );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_CheckPingDataUpToDate", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CheckPingDataUpToDate( IntPtr self, float flMaxAgeSeconds );
		
		#endregion
		internal bool CheckPingDataUpToDate( float flMaxAgeSeconds )
		{
			var returnValue = _CheckPingDataUpToDate( Self, flMaxAgeSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPingToDataCenter", CallingConvention = Platform.CC)]
		private static extern int _GetPingToDataCenter( IntPtr self, SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP );
		
		#endregion
		internal int GetPingToDataCenter( SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP )
		{
			var returnValue = _GetPingToDataCenter( Self, popID, ref pViaRelayPoP );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetDirectPingToPOP", CallingConvention = Platform.CC)]
		private static extern int _GetDirectPingToPOP( IntPtr self, SteamNetworkingPOPID popID );
		
		#endregion
		internal int GetDirectPingToPOP( SteamNetworkingPOPID popID )
		{
			var returnValue = _GetDirectPingToPOP( Self, popID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPCount", CallingConvention = Platform.CC)]
		private static extern int _GetPOPCount( IntPtr self );
		
		#endregion
		internal int GetPOPCount()
		{
			var returnValue = _GetPOPCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetPOPList", CallingConvention = Platform.CC)]
		private static extern int _GetPOPList( IntPtr self, ref SteamNetworkingPOPID list, int nListSz );
		
		#endregion
		internal int GetPOPList( ref SteamNetworkingPOPID list, int nListSz )
		{
			var returnValue = _GetPOPList( Self, ref list, nListSz );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetLocalTimestamp", CallingConvention = Platform.CC)]
		private static extern long _GetLocalTimestamp( IntPtr self );
		
		#endregion
		internal long GetLocalTimestamp()
		{
			var returnValue = _GetLocalTimestamp( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetDebugOutputFunction", CallingConvention = Platform.CC)]
		private static extern void _SetDebugOutputFunction( IntPtr self, NetDebugOutput eDetailLevel, NetDebugFunc pfnFunc );
		
		#endregion
		internal void SetDebugOutputFunction( NetDebugOutput eDetailLevel, NetDebugFunc pfnFunc )
		{
			_SetDebugOutputFunction( Self, eDetailLevel, pfnFunc );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueInt32", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetGlobalConfigValueInt32( IntPtr self, NetConfig eValue, int val );
		
		#endregion
		internal bool SetGlobalConfigValueInt32( NetConfig eValue, int val )
		{
			var returnValue = _SetGlobalConfigValueInt32( Self, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetGlobalConfigValueFloat( IntPtr self, NetConfig eValue, float val );
		
		#endregion
		internal bool SetGlobalConfigValueFloat( NetConfig eValue, float val )
		{
			var returnValue = _SetGlobalConfigValueFloat( Self, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetGlobalConfigValueString", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetGlobalConfigValueString( IntPtr self, NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val );
		
		#endregion
		internal bool SetGlobalConfigValueString( NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val )
		{
			var returnValue = _SetGlobalConfigValueString( Self, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueInt32", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConnectionConfigValueInt32( IntPtr self, Connection hConn, NetConfig eValue, int val );
		
		#endregion
		internal bool SetConnectionConfigValueInt32( Connection hConn, NetConfig eValue, int val )
		{
			var returnValue = _SetConnectionConfigValueInt32( Self, hConn, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConnectionConfigValueFloat( IntPtr self, Connection hConn, NetConfig eValue, float val );
		
		#endregion
		internal bool SetConnectionConfigValueFloat( Connection hConn, NetConfig eValue, float val )
		{
			var returnValue = _SetConnectionConfigValueFloat( Self, hConn, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConnectionConfigValueString", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConnectionConfigValueString( IntPtr self, Connection hConn, NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val );
		
		#endregion
		internal bool SetConnectionConfigValueString( Connection hConn, NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string val )
		{
			var returnValue = _SetConnectionConfigValueString( Self, hConn, eValue, val );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConfigValue", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConfigValue( IntPtr self, NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, NetConfigType eDataType, IntPtr pArg );
		
		#endregion
		internal bool SetConfigValue( NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, NetConfigType eDataType, IntPtr pArg )
		{
			var returnValue = _SetConfigValue( Self, eValue, eScopeType, scopeObj, eDataType, pArg );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SetConfigValueStruct", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetConfigValueStruct( IntPtr self, ref NetKeyValue opt, NetConfigScope eScopeType, IntPtr scopeObj );
		
		#endregion
		internal bool SetConfigValueStruct( ref NetKeyValue opt, NetConfigScope eScopeType, IntPtr scopeObj )
		{
			var returnValue = _SetConfigValueStruct( Self, ref opt, eScopeType, scopeObj );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValue", CallingConvention = Platform.CC)]
		private static extern NetConfigResult _GetConfigValue( IntPtr self, NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref UIntPtr cbResult );
		
		#endregion
		internal NetConfigResult GetConfigValue( NetConfig eValue, NetConfigScope eScopeType, IntPtr scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref UIntPtr cbResult )
		{
			var returnValue = _GetConfigValue( Self, eValue, eScopeType, scopeObj, ref pOutDataType, pResult, ref cbResult );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetConfigValueInfo", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetConfigValueInfo( IntPtr self, NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pOutName, ref NetConfigType pOutDataType, [In,Out] NetConfigScope[]  pOutScope, [In,Out] NetConfig[]  pOutNextValue );
		
		#endregion
		internal bool GetConfigValueInfo( NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pOutName, ref NetConfigType pOutDataType, [In,Out] NetConfigScope[]  pOutScope, [In,Out] NetConfig[]  pOutNextValue )
		{
			var returnValue = _GetConfigValueInfo( Self, eValue, pOutName, ref pOutDataType, pOutScope, pOutNextValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_GetFirstConfigValue", CallingConvention = Platform.CC)]
		private static extern NetConfig _GetFirstConfigValue( IntPtr self );
		
		#endregion
		internal NetConfig GetFirstConfigValue()
		{
			var returnValue = _GetFirstConfigValue( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ToString", CallingConvention = Platform.CC)]
		private static extern void _SteamNetworkingIPAddr_ToString( IntPtr self, ref NetAddress addr, IntPtr buf, uint cbBuf, [MarshalAs( UnmanagedType.U1 )] bool bWithPort );
		
		#endregion
		internal void SteamNetworkingIPAddr_ToString( ref NetAddress addr, out string buf, [MarshalAs( UnmanagedType.U1 )] bool bWithPort )
		{
			IntPtr membuf = Helpers.TakeMemory();
			_SteamNetworkingIPAddr_ToString( Self, ref addr, membuf, (1024 * 32), bWithPort );
			buf = Helpers.MemoryToString( membuf );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIPAddr_ParseString", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SteamNetworkingIPAddr_ParseString( IntPtr self, ref NetAddress pAddr, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszStr );
		
		#endregion
		internal bool SteamNetworkingIPAddr_ParseString( ref NetAddress pAddr, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszStr )
		{
			var returnValue = _SteamNetworkingIPAddr_ParseString( Self, ref pAddr, pszStr );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ToString", CallingConvention = Platform.CC)]
		private static extern void _SteamNetworkingIdentity_ToString( IntPtr self, ref NetIdentity identity, IntPtr buf, uint cbBuf );
		
		#endregion
		internal void SteamNetworkingIdentity_ToString( ref NetIdentity identity, out string buf )
		{
			IntPtr membuf = Helpers.TakeMemory();
			_SteamNetworkingIdentity_ToString( Self, ref identity, membuf, (1024 * 32) );
			buf = Helpers.MemoryToString( membuf );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingUtils_SteamNetworkingIdentity_ParseString", CallingConvention = Platform.CC)]
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
