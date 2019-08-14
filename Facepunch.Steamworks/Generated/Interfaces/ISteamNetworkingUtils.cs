using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingUtils : SteamInterface
	{
		public override string InterfaceName => "SteamNetworkingUtils001";
		
		public override void InitInternals()
		{
			_GetLocalPingLocation = Marshal.GetDelegateForFunctionPointer<FGetLocalPingLocation>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_EstimatePingTimeBetweenTwoLocations = Marshal.GetDelegateForFunctionPointer<FEstimatePingTimeBetweenTwoLocations>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_EstimatePingTimeFromLocalHost = Marshal.GetDelegateForFunctionPointer<FEstimatePingTimeFromLocalHost>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_ConvertPingLocationToString = Marshal.GetDelegateForFunctionPointer<FConvertPingLocationToString>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_ParsePingLocationString = Marshal.GetDelegateForFunctionPointer<FParsePingLocationString>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_CheckPingDataUpToDate = Marshal.GetDelegateForFunctionPointer<FCheckPingDataUpToDate>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_IsPingMeasurementInProgress = Marshal.GetDelegateForFunctionPointer<FIsPingMeasurementInProgress>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_GetPingToDataCenter = Marshal.GetDelegateForFunctionPointer<FGetPingToDataCenter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_GetDirectPingToPOP = Marshal.GetDelegateForFunctionPointer<FGetDirectPingToPOP>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_GetPOPCount = Marshal.GetDelegateForFunctionPointer<FGetPOPCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_GetPOPList = Marshal.GetDelegateForFunctionPointer<FGetPOPList>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_GetLocalTimestamp = Marshal.GetDelegateForFunctionPointer<FGetLocalTimestamp>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_SetDebugOutputFunction = Marshal.GetDelegateForFunctionPointer<FSetDebugOutputFunction>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_SetConfigValue = Marshal.GetDelegateForFunctionPointer<FSetConfigValue>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_GetConfigValue = Marshal.GetDelegateForFunctionPointer<FGetConfigValue>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_GetConfigValueInfo = Marshal.GetDelegateForFunctionPointer<FGetConfigValueInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_GetFirstConfigValue = Marshal.GetDelegateForFunctionPointer<FGetFirstConfigValue>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetLocalPingLocation = null;
			_EstimatePingTimeBetweenTwoLocations = null;
			_EstimatePingTimeFromLocalHost = null;
			_ConvertPingLocationToString = null;
			_ParsePingLocationString = null;
			_CheckPingDataUpToDate = null;
			_IsPingMeasurementInProgress = null;
			_GetPingToDataCenter = null;
			_GetDirectPingToPOP = null;
			_GetPOPCount = null;
			_GetPOPList = null;
			_GetLocalTimestamp = null;
			_SetDebugOutputFunction = null;
			_SetConfigValue = null;
			_GetConfigValue = null;
			_GetConfigValueInfo = null;
			_GetFirstConfigValue = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate float FGetLocalPingLocation( IntPtr self, ref PingLocation result );
		private FGetLocalPingLocation _GetLocalPingLocation;
		
		#endregion
		internal float GetLocalPingLocation( ref PingLocation result )
		{
			var returnValue = _GetLocalPingLocation( Self, ref result );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FEstimatePingTimeBetweenTwoLocations( IntPtr self, ref PingLocation location1, ref PingLocation location2 );
		private FEstimatePingTimeBetweenTwoLocations _EstimatePingTimeBetweenTwoLocations;
		
		#endregion
		internal int EstimatePingTimeBetweenTwoLocations( ref PingLocation location1, ref PingLocation location2 )
		{
			var returnValue = _EstimatePingTimeBetweenTwoLocations( Self, ref location1, ref location2 );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FEstimatePingTimeFromLocalHost( IntPtr self, ref PingLocation remoteLocation );
		private FEstimatePingTimeFromLocalHost _EstimatePingTimeFromLocalHost;
		
		#endregion
		internal int EstimatePingTimeFromLocalHost( ref PingLocation remoteLocation )
		{
			var returnValue = _EstimatePingTimeFromLocalHost( Self, ref remoteLocation );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FConvertPingLocationToString( IntPtr self, ref PingLocation location, IntPtr pszBuf, int cchBufSize );
		private FConvertPingLocationToString _ConvertPingLocationToString;
		
		#endregion
		internal void ConvertPingLocationToString( ref PingLocation location, out string pszBuf )
		{
			IntPtr mempszBuf = Helpers.TakeMemory();
			_ConvertPingLocationToString( Self, ref location, mempszBuf, (1024 * 32) );
			pszBuf = Helpers.MemoryToString( mempszBuf );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FParsePingLocationString( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszString, ref PingLocation result );
		private FParsePingLocationString _ParsePingLocationString;
		
		#endregion
		internal bool ParsePingLocationString( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszString, ref PingLocation result )
		{
			var returnValue = _ParsePingLocationString( Self, pszString, ref result );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCheckPingDataUpToDate( IntPtr self, float flMaxAgeSeconds );
		private FCheckPingDataUpToDate _CheckPingDataUpToDate;
		
		#endregion
		internal bool CheckPingDataUpToDate( float flMaxAgeSeconds )
		{
			var returnValue = _CheckPingDataUpToDate( Self, flMaxAgeSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsPingMeasurementInProgress( IntPtr self );
		private FIsPingMeasurementInProgress _IsPingMeasurementInProgress;
		
		#endregion
		internal bool IsPingMeasurementInProgress()
		{
			var returnValue = _IsPingMeasurementInProgress( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetPingToDataCenter( IntPtr self, SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP );
		private FGetPingToDataCenter _GetPingToDataCenter;
		
		#endregion
		internal int GetPingToDataCenter( SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP )
		{
			var returnValue = _GetPingToDataCenter( Self, popID, ref pViaRelayPoP );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetDirectPingToPOP( IntPtr self, SteamNetworkingPOPID popID );
		private FGetDirectPingToPOP _GetDirectPingToPOP;
		
		#endregion
		internal int GetDirectPingToPOP( SteamNetworkingPOPID popID )
		{
			var returnValue = _GetDirectPingToPOP( Self, popID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetPOPCount( IntPtr self );
		private FGetPOPCount _GetPOPCount;
		
		#endregion
		internal int GetPOPCount()
		{
			var returnValue = _GetPOPCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetPOPList( IntPtr self, ref SteamNetworkingPOPID list, int nListSz );
		private FGetPOPList _GetPOPList;
		
		#endregion
		internal int GetPOPList( ref SteamNetworkingPOPID list, int nListSz )
		{
			var returnValue = _GetPOPList( Self, ref list, nListSz );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate long FGetLocalTimestamp( IntPtr self );
		private FGetLocalTimestamp _GetLocalTimestamp;
		
		#endregion
		internal long GetLocalTimestamp()
		{
			var returnValue = _GetLocalTimestamp( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetDebugOutputFunction( IntPtr self, DebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc );
		private FSetDebugOutputFunction _SetDebugOutputFunction;
		
		#endregion
		internal void SetDebugOutputFunction( DebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc )
		{
			_SetDebugOutputFunction( Self, eDetailLevel, pfnFunc );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetConfigValue( IntPtr self, NetConfig eValue, NetScope eScopeType, long scopeObj, NetConfigType eDataType, IntPtr pArg );
		private FSetConfigValue _SetConfigValue;
		
		#endregion
		internal bool SetConfigValue( NetConfig eValue, NetScope eScopeType, long scopeObj, NetConfigType eDataType, IntPtr pArg )
		{
			var returnValue = _SetConfigValue( Self, eValue, eScopeType, scopeObj, eDataType, pArg );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate NetConfigResult FGetConfigValue( IntPtr self, NetConfig eValue, NetScope eScopeType, long scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref ulong cbResult );
		private FGetConfigValue _GetConfigValue;
		
		#endregion
		internal NetConfigResult GetConfigValue( NetConfig eValue, NetScope eScopeType, long scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref ulong cbResult )
		{
			var returnValue = _GetConfigValue( Self, eValue, eScopeType, scopeObj, ref pOutDataType, pResult, ref cbResult );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetConfigValueInfo( IntPtr self, NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pOutName, ref NetConfigType pOutDataType, [In,Out] NetScope[]  pOutScope, [In,Out] NetConfig[]  pOutNextValue );
		private FGetConfigValueInfo _GetConfigValueInfo;
		
		#endregion
		internal bool GetConfigValueInfo( NetConfig eValue, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pOutName, ref NetConfigType pOutDataType, [In,Out] NetScope[]  pOutScope, [In,Out] NetConfig[]  pOutNextValue )
		{
			var returnValue = _GetConfigValueInfo( Self, eValue, pOutName, ref pOutDataType, pOutScope, pOutNextValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate NetConfig FGetFirstConfigValue( IntPtr self );
		private FGetFirstConfigValue _GetFirstConfigValue;
		
		#endregion
		internal NetConfig GetFirstConfigValue()
		{
			var returnValue = _GetFirstConfigValue( Self );
			return returnValue;
		}
		
	}
}
