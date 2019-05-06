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
			_GetLocalPingLocation = Marshal.GetDelegateForFunctionPointer<FGetLocalPingLocation>( Marshal.ReadIntPtr( VTable, 0) );
			_EstimatePingTimeBetweenTwoLocations = Marshal.GetDelegateForFunctionPointer<FEstimatePingTimeBetweenTwoLocations>( Marshal.ReadIntPtr( VTable, 8) );
			_EstimatePingTimeFromLocalHost = Marshal.GetDelegateForFunctionPointer<FEstimatePingTimeFromLocalHost>( Marshal.ReadIntPtr( VTable, 16) );
			_ConvertPingLocationToString = Marshal.GetDelegateForFunctionPointer<FConvertPingLocationToString>( Marshal.ReadIntPtr( VTable, 24) );
			_ParsePingLocationString = Marshal.GetDelegateForFunctionPointer<FParsePingLocationString>( Marshal.ReadIntPtr( VTable, 32) );
			_CheckPingDataUpToDate = Marshal.GetDelegateForFunctionPointer<FCheckPingDataUpToDate>( Marshal.ReadIntPtr( VTable, 40) );
			_IsPingMeasurementInProgress = Marshal.GetDelegateForFunctionPointer<FIsPingMeasurementInProgress>( Marshal.ReadIntPtr( VTable, 48) );
			_GetPingToDataCenter = Marshal.GetDelegateForFunctionPointer<FGetPingToDataCenter>( Marshal.ReadIntPtr( VTable, 56) );
			_GetDirectPingToPOP = Marshal.GetDelegateForFunctionPointer<FGetDirectPingToPOP>( Marshal.ReadIntPtr( VTable, 64) );
			_GetPOPCount = Marshal.GetDelegateForFunctionPointer<FGetPOPCount>( Marshal.ReadIntPtr( VTable, 72) );
			_GetPOPList = Marshal.GetDelegateForFunctionPointer<FGetPOPList>( Marshal.ReadIntPtr( VTable, 80) );
			_GetLocalTimestamp = Marshal.GetDelegateForFunctionPointer<FGetLocalTimestamp>( Marshal.ReadIntPtr( VTable, 88) );
			_SetDebugOutputFunction = Marshal.GetDelegateForFunctionPointer<FSetDebugOutputFunction>( Marshal.ReadIntPtr( VTable, 96) );
			_SetConfigValue = Marshal.GetDelegateForFunctionPointer<FSetConfigValue>( Marshal.ReadIntPtr( VTable, 104) );
			_GetConfigValue = Marshal.GetDelegateForFunctionPointer<FGetConfigValue>( Marshal.ReadIntPtr( VTable, 112) );
			_GetConfigValueInfo = Marshal.GetDelegateForFunctionPointer<FGetConfigValueInfo>( Marshal.ReadIntPtr( VTable, 120) );
			_GetFirstConfigValue = Marshal.GetDelegateForFunctionPointer<FGetFirstConfigValue>( Marshal.ReadIntPtr( VTable, 128) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate float FGetLocalPingLocation( IntPtr self, ref PingLocation result );
		private FGetLocalPingLocation _GetLocalPingLocation;
		
		#endregion
		internal float GetLocalPingLocation( ref PingLocation result )
		{
			return _GetLocalPingLocation( Self, ref result );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FEstimatePingTimeBetweenTwoLocations( IntPtr self, ref PingLocation location1, ref PingLocation location2 );
		private FEstimatePingTimeBetweenTwoLocations _EstimatePingTimeBetweenTwoLocations;
		
		#endregion
		internal int EstimatePingTimeBetweenTwoLocations( ref PingLocation location1, ref PingLocation location2 )
		{
			return _EstimatePingTimeBetweenTwoLocations( Self, ref location1, ref location2 );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FEstimatePingTimeFromLocalHost( IntPtr self, ref PingLocation remoteLocation );
		private FEstimatePingTimeFromLocalHost _EstimatePingTimeFromLocalHost;
		
		#endregion
		internal int EstimatePingTimeFromLocalHost( ref PingLocation remoteLocation )
		{
			return _EstimatePingTimeFromLocalHost( Self, ref remoteLocation );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FConvertPingLocationToString( IntPtr self, ref PingLocation location, StringBuilder pszBuf, int cchBufSize );
		private FConvertPingLocationToString _ConvertPingLocationToString;
		
		#endregion
		internal void ConvertPingLocationToString( ref PingLocation location, StringBuilder pszBuf, int cchBufSize )
		{
			_ConvertPingLocationToString( Self, ref location, pszBuf, cchBufSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FParsePingLocationString( IntPtr self, string pszString, ref PingLocation result );
		private FParsePingLocationString _ParsePingLocationString;
		
		#endregion
		internal bool ParsePingLocationString( string pszString, ref PingLocation result )
		{
			return _ParsePingLocationString( Self, pszString, ref result );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCheckPingDataUpToDate( IntPtr self, float flMaxAgeSeconds );
		private FCheckPingDataUpToDate _CheckPingDataUpToDate;
		
		#endregion
		internal bool CheckPingDataUpToDate( float flMaxAgeSeconds )
		{
			return _CheckPingDataUpToDate( Self, flMaxAgeSeconds );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsPingMeasurementInProgress( IntPtr self );
		private FIsPingMeasurementInProgress _IsPingMeasurementInProgress;
		
		#endregion
		internal bool IsPingMeasurementInProgress()
		{
			return _IsPingMeasurementInProgress( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetPingToDataCenter( IntPtr self, SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP );
		private FGetPingToDataCenter _GetPingToDataCenter;
		
		#endregion
		internal int GetPingToDataCenter( SteamNetworkingPOPID popID, ref SteamNetworkingPOPID pViaRelayPoP )
		{
			return _GetPingToDataCenter( Self, popID, ref pViaRelayPoP );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetDirectPingToPOP( IntPtr self, SteamNetworkingPOPID popID );
		private FGetDirectPingToPOP _GetDirectPingToPOP;
		
		#endregion
		internal int GetDirectPingToPOP( SteamNetworkingPOPID popID )
		{
			return _GetDirectPingToPOP( Self, popID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetPOPCount( IntPtr self );
		private FGetPOPCount _GetPOPCount;
		
		#endregion
		internal int GetPOPCount()
		{
			return _GetPOPCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetPOPList( IntPtr self, ref SteamNetworkingPOPID list, int nListSz );
		private FGetPOPList _GetPOPList;
		
		#endregion
		internal int GetPOPList( ref SteamNetworkingPOPID list, int nListSz )
		{
			return _GetPOPList( Self, ref list, nListSz );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate long FGetLocalTimestamp( IntPtr self );
		private FGetLocalTimestamp _GetLocalTimestamp;
		
		#endregion
		internal long GetLocalTimestamp()
		{
			return _GetLocalTimestamp( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetDebugOutputFunction( IntPtr self, SteamNetworkingSocketsDebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc );
		private FSetDebugOutputFunction _SetDebugOutputFunction;
		
		#endregion
		internal void SetDebugOutputFunction( SteamNetworkingSocketsDebugOutputType eDetailLevel, FSteamNetworkingSocketsDebugOutput pfnFunc )
		{
			_SetDebugOutputFunction( Self, eDetailLevel, pfnFunc );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetConfigValue( IntPtr self, NetConfig eValue, NetScope eScopeType, long scopeObj, NetConfigType eDataType, IntPtr pArg );
		private FSetConfigValue _SetConfigValue;
		
		#endregion
		internal bool SetConfigValue( NetConfig eValue, NetScope eScopeType, long scopeObj, NetConfigType eDataType, IntPtr pArg )
		{
			return _SetConfigValue( Self, eValue, eScopeType, scopeObj, eDataType, pArg );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamNetworkingGetConfigValueResult FGetConfigValue( IntPtr self, NetConfig eValue, NetScope eScopeType, long scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref ulong cbResult );
		private FGetConfigValue _GetConfigValue;
		
		#endregion
		internal SteamNetworkingGetConfigValueResult GetConfigValue( NetConfig eValue, NetScope eScopeType, long scopeObj, ref NetConfigType pOutDataType, IntPtr pResult, ref ulong cbResult )
		{
			return _GetConfigValue( Self, eValue, eScopeType, scopeObj, ref pOutDataType, pResult, ref cbResult );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetConfigValueInfo( IntPtr self, NetConfig eValue, [In,Out] string[]  pOutName, ref NetConfigType pOutDataType, [In,Out] NetScope[]  pOutScope, [In,Out] NetConfig[]  pOutNextValue );
		private FGetConfigValueInfo _GetConfigValueInfo;
		
		#endregion
		internal bool GetConfigValueInfo( NetConfig eValue, [In,Out] string[]  pOutName, ref NetConfigType pOutDataType, [In,Out] NetScope[]  pOutScope, [In,Out] NetConfig[]  pOutNextValue )
		{
			return _GetConfigValueInfo( Self, eValue, pOutName, ref pOutDataType, pOutScope, pOutNextValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate NetConfig FGetFirstConfigValue( IntPtr self );
		private FGetFirstConfigValue _GetFirstConfigValue;
		
		#endregion
		internal NetConfig GetFirstConfigValue()
		{
			return _GetFirstConfigValue( Self );
		}
		
	}
}
