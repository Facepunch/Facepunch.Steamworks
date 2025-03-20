using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamHTTP : SteamInterface
	{
		public const string Version = "STEAMHTTP_INTERFACE_VERSION003";
		
		internal ISteamHTTP( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamHTTP_v003", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamHTTP_v003();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamHTTP_v003();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamGameServerHTTP_v003", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServerHTTP_v003();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServerHTTP_v003();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_CreateHTTPRequest", CallingConvention = Platform.CC)]
		private static extern HTTPRequestHandle _CreateHTTPRequest( IntPtr self, HTTPMethod eHTTPRequestMethod, IntPtr pchAbsoluteURL );
		
		#endregion
		internal HTTPRequestHandle CreateHTTPRequest( HTTPMethod eHTTPRequestMethod, string pchAbsoluteURL )
		{
			using var str__pchAbsoluteURL = new Utf8StringToNative( pchAbsoluteURL );
			var returnValue = _CreateHTTPRequest( Self, eHTTPRequestMethod, str__pchAbsoluteURL.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestContextValue", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestContextValue( IntPtr self, HTTPRequestHandle hRequest, ulong ulContextValue );
		
		#endregion
		internal bool SetHTTPRequestContextValue( HTTPRequestHandle hRequest, ulong ulContextValue )
		{
			var returnValue = _SetHTTPRequestContextValue( Self, hRequest, ulContextValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestNetworkActivityTimeout( IntPtr self, HTTPRequestHandle hRequest, uint unTimeoutSeconds );
		
		#endregion
		internal bool SetHTTPRequestNetworkActivityTimeout( HTTPRequestHandle hRequest, uint unTimeoutSeconds )
		{
			var returnValue = _SetHTTPRequestNetworkActivityTimeout( Self, hRequest, unTimeoutSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestHeaderValue( IntPtr self, HTTPRequestHandle hRequest, IntPtr pchHeaderName, IntPtr pchHeaderValue );
		
		#endregion
		internal bool SetHTTPRequestHeaderValue( HTTPRequestHandle hRequest, string pchHeaderName, string pchHeaderValue )
		{
			using var str__pchHeaderName = new Utf8StringToNative( pchHeaderName );
			using var str__pchHeaderValue = new Utf8StringToNative( pchHeaderValue );
			var returnValue = _SetHTTPRequestHeaderValue( Self, hRequest, str__pchHeaderName.Pointer, str__pchHeaderValue.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestGetOrPostParameter( IntPtr self, HTTPRequestHandle hRequest, IntPtr pchParamName, IntPtr pchParamValue );
		
		#endregion
		internal bool SetHTTPRequestGetOrPostParameter( HTTPRequestHandle hRequest, string pchParamName, string pchParamValue )
		{
			using var str__pchParamName = new Utf8StringToNative( pchParamName );
			using var str__pchParamValue = new Utf8StringToNative( pchParamValue );
			var returnValue = _SetHTTPRequestGetOrPostParameter( Self, hRequest, str__pchParamName.Pointer, str__pchParamValue.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequest", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendHTTPRequest( IntPtr self, HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle );
		
		#endregion
		internal bool SendHTTPRequest( HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle )
		{
			var returnValue = _SendHTTPRequest( Self, hRequest, ref pCallHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendHTTPRequestAndStreamResponse( IntPtr self, HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle );
		
		#endregion
		internal bool SendHTTPRequestAndStreamResponse( HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle )
		{
			var returnValue = _SendHTTPRequestAndStreamResponse( Self, hRequest, ref pCallHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_DeferHTTPRequest", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DeferHTTPRequest( IntPtr self, HTTPRequestHandle hRequest );
		
		#endregion
		internal bool DeferHTTPRequest( HTTPRequestHandle hRequest )
		{
			var returnValue = _DeferHTTPRequest( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_PrioritizeHTTPRequest", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _PrioritizeHTTPRequest( IntPtr self, HTTPRequestHandle hRequest );
		
		#endregion
		internal bool PrioritizeHTTPRequest( HTTPRequestHandle hRequest )
		{
			var returnValue = _PrioritizeHTTPRequest( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseHeaderSize( IntPtr self, HTTPRequestHandle hRequest, IntPtr pchHeaderName, ref uint unResponseHeaderSize );
		
		#endregion
		internal bool GetHTTPResponseHeaderSize( HTTPRequestHandle hRequest, string pchHeaderName, ref uint unResponseHeaderSize )
		{
			using var str__pchHeaderName = new Utf8StringToNative( pchHeaderName );
			var returnValue = _GetHTTPResponseHeaderSize( Self, hRequest, str__pchHeaderName.Pointer, ref unResponseHeaderSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseHeaderValue( IntPtr self, HTTPRequestHandle hRequest, IntPtr pchHeaderName, ref byte pHeaderValueBuffer, uint unBufferSize );
		
		#endregion
		internal bool GetHTTPResponseHeaderValue( HTTPRequestHandle hRequest, string pchHeaderName, ref byte pHeaderValueBuffer, uint unBufferSize )
		{
			using var str__pchHeaderName = new Utf8StringToNative( pchHeaderName );
			var returnValue = _GetHTTPResponseHeaderValue( Self, hRequest, str__pchHeaderName.Pointer, ref pHeaderValueBuffer, unBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodySize", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseBodySize( IntPtr self, HTTPRequestHandle hRequest, ref uint unBodySize );
		
		#endregion
		internal bool GetHTTPResponseBodySize( HTTPRequestHandle hRequest, ref uint unBodySize )
		{
			var returnValue = _GetHTTPResponseBodySize( Self, hRequest, ref unBodySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodyData", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseBodyData( IntPtr self, HTTPRequestHandle hRequest, ref byte pBodyDataBuffer, uint unBufferSize );
		
		#endregion
		internal bool GetHTTPResponseBodyData( HTTPRequestHandle hRequest, ref byte pBodyDataBuffer, uint unBufferSize )
		{
			var returnValue = _GetHTTPResponseBodyData( Self, hRequest, ref pBodyDataBuffer, unBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPStreamingResponseBodyData( IntPtr self, HTTPRequestHandle hRequest, uint cOffset, ref byte pBodyDataBuffer, uint unBufferSize );
		
		#endregion
		internal bool GetHTTPStreamingResponseBodyData( HTTPRequestHandle hRequest, uint cOffset, ref byte pBodyDataBuffer, uint unBufferSize )
		{
			var returnValue = _GetHTTPStreamingResponseBodyData( Self, hRequest, cOffset, ref pBodyDataBuffer, unBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseHTTPRequest", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReleaseHTTPRequest( IntPtr self, HTTPRequestHandle hRequest );
		
		#endregion
		internal bool ReleaseHTTPRequest( HTTPRequestHandle hRequest )
		{
			var returnValue = _ReleaseHTTPRequest( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPDownloadProgressPct( IntPtr self, HTTPRequestHandle hRequest, ref float pflPercentOut );
		
		#endregion
		internal bool GetHTTPDownloadProgressPct( HTTPRequestHandle hRequest, ref float pflPercentOut )
		{
			var returnValue = _GetHTTPDownloadProgressPct( Self, hRequest, ref pflPercentOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestRawPostBody( IntPtr self, HTTPRequestHandle hRequest, IntPtr pchContentType, [In,Out] byte[]  pubBody, uint unBodyLen );
		
		#endregion
		internal bool SetHTTPRequestRawPostBody( HTTPRequestHandle hRequest, string pchContentType, [In,Out] byte[]  pubBody, uint unBodyLen )
		{
			using var str__pchContentType = new Utf8StringToNative( pchContentType );
			var returnValue = _SetHTTPRequestRawPostBody( Self, hRequest, str__pchContentType.Pointer, pubBody, unBodyLen );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_CreateCookieContainer", CallingConvention = Platform.CC)]
		private static extern HTTPCookieContainerHandle _CreateCookieContainer( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAllowResponsesToModify );
		
		#endregion
		internal HTTPCookieContainerHandle CreateCookieContainer( [MarshalAs( UnmanagedType.U1 )] bool bAllowResponsesToModify )
		{
			var returnValue = _CreateCookieContainer( Self, bAllowResponsesToModify );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseCookieContainer", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReleaseCookieContainer( IntPtr self, HTTPCookieContainerHandle hCookieContainer );
		
		#endregion
		internal bool ReleaseCookieContainer( HTTPCookieContainerHandle hCookieContainer )
		{
			var returnValue = _ReleaseCookieContainer( Self, hCookieContainer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetCookie", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetCookie( IntPtr self, HTTPCookieContainerHandle hCookieContainer, IntPtr pchHost, IntPtr pchUrl, IntPtr pchCookie );
		
		#endregion
		internal bool SetCookie( HTTPCookieContainerHandle hCookieContainer, string pchHost, string pchUrl, string pchCookie )
		{
			using var str__pchHost = new Utf8StringToNative( pchHost );
			using var str__pchUrl = new Utf8StringToNative( pchUrl );
			using var str__pchCookie = new Utf8StringToNative( pchCookie );
			var returnValue = _SetCookie( Self, hCookieContainer, str__pchHost.Pointer, str__pchUrl.Pointer, str__pchCookie.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestCookieContainer( IntPtr self, HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer );
		
		#endregion
		internal bool SetHTTPRequestCookieContainer( HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer )
		{
			var returnValue = _SetHTTPRequestCookieContainer( Self, hRequest, hCookieContainer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestUserAgentInfo( IntPtr self, HTTPRequestHandle hRequest, IntPtr pchUserAgentInfo );
		
		#endregion
		internal bool SetHTTPRequestUserAgentInfo( HTTPRequestHandle hRequest, string pchUserAgentInfo )
		{
			using var str__pchUserAgentInfo = new Utf8StringToNative( pchUserAgentInfo );
			var returnValue = _SetHTTPRequestUserAgentInfo( Self, hRequest, str__pchUserAgentInfo.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestRequiresVerifiedCertificate( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.U1 )] bool bRequireVerifiedCertificate );
		
		#endregion
		internal bool SetHTTPRequestRequiresVerifiedCertificate( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.U1 )] bool bRequireVerifiedCertificate )
		{
			var returnValue = _SetHTTPRequestRequiresVerifiedCertificate( Self, hRequest, bRequireVerifiedCertificate );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestAbsoluteTimeoutMS( IntPtr self, HTTPRequestHandle hRequest, uint unMilliseconds );
		
		#endregion
		internal bool SetHTTPRequestAbsoluteTimeoutMS( HTTPRequestHandle hRequest, uint unMilliseconds )
		{
			var returnValue = _SetHTTPRequestAbsoluteTimeoutMS( Self, hRequest, unMilliseconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPRequestWasTimedOut( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.U1 )] ref bool pbWasTimedOut );
		
		#endregion
		internal bool GetHTTPRequestWasTimedOut( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.U1 )] ref bool pbWasTimedOut )
		{
			var returnValue = _GetHTTPRequestWasTimedOut( Self, hRequest, ref pbWasTimedOut );
			return returnValue;
		}
		
	}
}
