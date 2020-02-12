using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamHTTP : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamHTTP();
		
		
		internal ISteamHTTP()
		{
			SetupInterface();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_CreateHTTPRequest")]
		private static extern HTTPRequestHandle _CreateHTTPRequest( IntPtr self, HTTPMethod eHTTPRequestMethod, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchAbsoluteURL );
		
		#endregion
		internal HTTPRequestHandle CreateHTTPRequest( HTTPMethod eHTTPRequestMethod, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchAbsoluteURL )
		{
			var returnValue = _CreateHTTPRequest( Self, eHTTPRequestMethod, pchAbsoluteURL );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestContextValue")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestContextValue( IntPtr self, HTTPRequestHandle hRequest, ulong ulContextValue );
		
		#endregion
		internal bool SetHTTPRequestContextValue( HTTPRequestHandle hRequest, ulong ulContextValue )
		{
			var returnValue = _SetHTTPRequestContextValue( Self, hRequest, ulContextValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestNetworkActivityTimeout( IntPtr self, HTTPRequestHandle hRequest, uint unTimeoutSeconds );
		
		#endregion
		internal bool SetHTTPRequestNetworkActivityTimeout( HTTPRequestHandle hRequest, uint unTimeoutSeconds )
		{
			var returnValue = _SetHTTPRequestNetworkActivityTimeout( Self, hRequest, unTimeoutSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestHeaderValue( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderValue );
		
		#endregion
		internal bool SetHTTPRequestHeaderValue( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderValue )
		{
			var returnValue = _SetHTTPRequestHeaderValue( Self, hRequest, pchHeaderName, pchHeaderValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestGetOrPostParameter( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchParamName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchParamValue );
		
		#endregion
		internal bool SetHTTPRequestGetOrPostParameter( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchParamName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchParamValue )
		{
			var returnValue = _SetHTTPRequestGetOrPostParameter( Self, hRequest, pchParamName, pchParamValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequest")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendHTTPRequest( IntPtr self, HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle );
		
		#endregion
		internal bool SendHTTPRequest( HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle )
		{
			var returnValue = _SendHTTPRequest( Self, hRequest, ref pCallHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendHTTPRequestAndStreamResponse( IntPtr self, HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle );
		
		#endregion
		internal bool SendHTTPRequestAndStreamResponse( HTTPRequestHandle hRequest, ref SteamAPICall_t pCallHandle )
		{
			var returnValue = _SendHTTPRequestAndStreamResponse( Self, hRequest, ref pCallHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_DeferHTTPRequest")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DeferHTTPRequest( IntPtr self, HTTPRequestHandle hRequest );
		
		#endregion
		internal bool DeferHTTPRequest( HTTPRequestHandle hRequest )
		{
			var returnValue = _DeferHTTPRequest( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_PrioritizeHTTPRequest")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _PrioritizeHTTPRequest( IntPtr self, HTTPRequestHandle hRequest );
		
		#endregion
		internal bool PrioritizeHTTPRequest( HTTPRequestHandle hRequest )
		{
			var returnValue = _PrioritizeHTTPRequest( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseHeaderSize( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderName, ref uint unResponseHeaderSize );
		
		#endregion
		internal bool GetHTTPResponseHeaderSize( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderName, ref uint unResponseHeaderSize )
		{
			var returnValue = _GetHTTPResponseHeaderSize( Self, hRequest, pchHeaderName, ref unResponseHeaderSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseHeaderValue( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderName, ref byte pHeaderValueBuffer, uint unBufferSize );
		
		#endregion
		internal bool GetHTTPResponseHeaderValue( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHeaderName, ref byte pHeaderValueBuffer, uint unBufferSize )
		{
			var returnValue = _GetHTTPResponseHeaderValue( Self, hRequest, pchHeaderName, ref pHeaderValueBuffer, unBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodySize")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseBodySize( IntPtr self, HTTPRequestHandle hRequest, ref uint unBodySize );
		
		#endregion
		internal bool GetHTTPResponseBodySize( HTTPRequestHandle hRequest, ref uint unBodySize )
		{
			var returnValue = _GetHTTPResponseBodySize( Self, hRequest, ref unBodySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodyData")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPResponseBodyData( IntPtr self, HTTPRequestHandle hRequest, ref byte pBodyDataBuffer, uint unBufferSize );
		
		#endregion
		internal bool GetHTTPResponseBodyData( HTTPRequestHandle hRequest, ref byte pBodyDataBuffer, uint unBufferSize )
		{
			var returnValue = _GetHTTPResponseBodyData( Self, hRequest, ref pBodyDataBuffer, unBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPStreamingResponseBodyData( IntPtr self, HTTPRequestHandle hRequest, uint cOffset, ref byte pBodyDataBuffer, uint unBufferSize );
		
		#endregion
		internal bool GetHTTPStreamingResponseBodyData( HTTPRequestHandle hRequest, uint cOffset, ref byte pBodyDataBuffer, uint unBufferSize )
		{
			var returnValue = _GetHTTPStreamingResponseBodyData( Self, hRequest, cOffset, ref pBodyDataBuffer, unBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseHTTPRequest")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReleaseHTTPRequest( IntPtr self, HTTPRequestHandle hRequest );
		
		#endregion
		internal bool ReleaseHTTPRequest( HTTPRequestHandle hRequest )
		{
			var returnValue = _ReleaseHTTPRequest( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetHTTPDownloadProgressPct( IntPtr self, HTTPRequestHandle hRequest, ref float pflPercentOut );
		
		#endregion
		internal bool GetHTTPDownloadProgressPct( HTTPRequestHandle hRequest, ref float pflPercentOut )
		{
			var returnValue = _GetHTTPDownloadProgressPct( Self, hRequest, ref pflPercentOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestRawPostBody( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchContentType, [In,Out] byte[]  pubBody, uint unBodyLen );
		
		#endregion
		internal bool SetHTTPRequestRawPostBody( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchContentType, [In,Out] byte[]  pubBody, uint unBodyLen )
		{
			var returnValue = _SetHTTPRequestRawPostBody( Self, hRequest, pchContentType, pubBody, unBodyLen );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_CreateCookieContainer")]
		private static extern HTTPCookieContainerHandle _CreateCookieContainer( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAllowResponsesToModify );
		
		#endregion
		internal HTTPCookieContainerHandle CreateCookieContainer( [MarshalAs( UnmanagedType.U1 )] bool bAllowResponsesToModify )
		{
			var returnValue = _CreateCookieContainer( Self, bAllowResponsesToModify );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseCookieContainer")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReleaseCookieContainer( IntPtr self, HTTPCookieContainerHandle hCookieContainer );
		
		#endregion
		internal bool ReleaseCookieContainer( HTTPCookieContainerHandle hCookieContainer )
		{
			var returnValue = _ReleaseCookieContainer( Self, hCookieContainer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetCookie")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetCookie( IntPtr self, HTTPCookieContainerHandle hCookieContainer, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHost, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUrl, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchCookie );
		
		#endregion
		internal bool SetCookie( HTTPCookieContainerHandle hCookieContainer, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchHost, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUrl, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchCookie )
		{
			var returnValue = _SetCookie( Self, hCookieContainer, pchHost, pchUrl, pchCookie );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestCookieContainer( IntPtr self, HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer );
		
		#endregion
		internal bool SetHTTPRequestCookieContainer( HTTPRequestHandle hRequest, HTTPCookieContainerHandle hCookieContainer )
		{
			var returnValue = _SetHTTPRequestCookieContainer( Self, hRequest, hCookieContainer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestUserAgentInfo( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUserAgentInfo );
		
		#endregion
		internal bool SetHTTPRequestUserAgentInfo( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchUserAgentInfo )
		{
			var returnValue = _SetHTTPRequestUserAgentInfo( Self, hRequest, pchUserAgentInfo );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestRequiresVerifiedCertificate( IntPtr self, HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.U1 )] bool bRequireVerifiedCertificate );
		
		#endregion
		internal bool SetHTTPRequestRequiresVerifiedCertificate( HTTPRequestHandle hRequest, [MarshalAs( UnmanagedType.U1 )] bool bRequireVerifiedCertificate )
		{
			var returnValue = _SetHTTPRequestRequiresVerifiedCertificate( Self, hRequest, bRequireVerifiedCertificate );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetHTTPRequestAbsoluteTimeoutMS( IntPtr self, HTTPRequestHandle hRequest, uint unMilliseconds );
		
		#endregion
		internal bool SetHTTPRequestAbsoluteTimeoutMS( HTTPRequestHandle hRequest, uint unMilliseconds )
		{
			var returnValue = _SetHTTPRequestAbsoluteTimeoutMS( Self, hRequest, unMilliseconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut")]
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
