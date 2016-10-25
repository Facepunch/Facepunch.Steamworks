using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamHTTP
	{
		internal IntPtr _ptr;
		
		public SteamHTTP( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// HTTPCookieContainerHandle
		public HTTPCookieContainerHandle CreateCookieContainer( bool bAllowResponsesToModify /*bool*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.CreateCookieContainer( _ptr, bAllowResponsesToModify );
			else return Platform.Win64.ISteamHTTP.CreateCookieContainer( _ptr, bAllowResponsesToModify );
		}
		
		// HTTPRequestHandle
		public HTTPRequestHandle CreateHTTPRequest( HTTPMethod eHTTPRequestMethod /*EHTTPMethod*/, string pchAbsoluteURL /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.CreateHTTPRequest( _ptr, eHTTPRequestMethod, pchAbsoluteURL );
			else return Platform.Win64.ISteamHTTP.CreateHTTPRequest( _ptr, eHTTPRequestMethod, pchAbsoluteURL );
		}
		
		// bool
		public bool DeferHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.DeferHTTPRequest( _ptr, hRequest );
			else return Platform.Win64.ISteamHTTP.DeferHTTPRequest( _ptr, hRequest );
		}
		
		// bool
		public bool GetHTTPDownloadProgressPct( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out float pflPercentOut /*float **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.GetHTTPDownloadProgressPct( _ptr, hRequest, out pflPercentOut );
			else return Platform.Win64.ISteamHTTP.GetHTTPDownloadProgressPct( _ptr, hRequest, out pflPercentOut );
		}
		
		// bool
		public bool GetHTTPRequestWasTimedOut( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out bool pbWasTimedOut /*bool **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.GetHTTPRequestWasTimedOut( _ptr, hRequest, out pbWasTimedOut );
			else return Platform.Win64.ISteamHTTP.GetHTTPRequestWasTimedOut( _ptr, hRequest, out pbWasTimedOut );
		}
		
		// bool
		public bool GetHTTPResponseBodyData( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out byte pBodyDataBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.GetHTTPResponseBodyData( _ptr, hRequest, out pBodyDataBuffer, unBufferSize );
			else return Platform.Win64.ISteamHTTP.GetHTTPResponseBodyData( _ptr, hRequest, out pBodyDataBuffer, unBufferSize );
		}
		
		// bool
		public bool GetHTTPResponseBodySize( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out uint unBodySize /*uint32 **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.GetHTTPResponseBodySize( _ptr, hRequest, out unBodySize );
			else return Platform.Win64.ISteamHTTP.GetHTTPResponseBodySize( _ptr, hRequest, out unBodySize );
		}
		
		// bool
		public bool GetHTTPResponseHeaderSize( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, out uint unResponseHeaderSize /*uint32 **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.GetHTTPResponseHeaderSize( _ptr, hRequest, pchHeaderName, out unResponseHeaderSize );
			else return Platform.Win64.ISteamHTTP.GetHTTPResponseHeaderSize( _ptr, hRequest, pchHeaderName, out unResponseHeaderSize );
		}
		
		// bool
		public bool GetHTTPResponseHeaderValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, out byte pHeaderValueBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.GetHTTPResponseHeaderValue( _ptr, hRequest, pchHeaderName, out pHeaderValueBuffer, unBufferSize );
			else return Platform.Win64.ISteamHTTP.GetHTTPResponseHeaderValue( _ptr, hRequest, pchHeaderName, out pHeaderValueBuffer, unBufferSize );
		}
		
		// bool
		public bool GetHTTPStreamingResponseBodyData( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint cOffset /*uint32*/, out byte pBodyDataBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.GetHTTPStreamingResponseBodyData( _ptr, hRequest, cOffset, out pBodyDataBuffer, unBufferSize );
			else return Platform.Win64.ISteamHTTP.GetHTTPStreamingResponseBodyData( _ptr, hRequest, cOffset, out pBodyDataBuffer, unBufferSize );
		}
		
		// bool
		public bool PrioritizeHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.PrioritizeHTTPRequest( _ptr, hRequest );
			else return Platform.Win64.ISteamHTTP.PrioritizeHTTPRequest( _ptr, hRequest );
		}
		
		// bool
		public bool ReleaseCookieContainer( HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.ReleaseCookieContainer( _ptr, hCookieContainer );
			else return Platform.Win64.ISteamHTTP.ReleaseCookieContainer( _ptr, hCookieContainer );
		}
		
		// bool
		public bool ReleaseHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.ReleaseHTTPRequest( _ptr, hRequest );
			else return Platform.Win64.ISteamHTTP.ReleaseHTTPRequest( _ptr, hRequest );
		}
		
		// bool
		public bool SendHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref SteamAPICall_t pCallHandle /*SteamAPICall_t **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SendHTTPRequest( _ptr, hRequest, ref pCallHandle );
			else return Platform.Win64.ISteamHTTP.SendHTTPRequest( _ptr, hRequest, ref pCallHandle );
		}
		
		// bool
		public bool SendHTTPRequestAndStreamResponse( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref SteamAPICall_t pCallHandle /*SteamAPICall_t **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SendHTTPRequestAndStreamResponse( _ptr, hRequest, ref pCallHandle );
			else return Platform.Win64.ISteamHTTP.SendHTTPRequestAndStreamResponse( _ptr, hRequest, ref pCallHandle );
		}
		
		// bool
		public bool SetCookie( HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/, string pchHost /*const char **/, string pchUrl /*const char **/, string pchCookie /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetCookie( _ptr, hCookieContainer, pchHost, pchUrl, pchCookie );
			else return Platform.Win64.ISteamHTTP.SetCookie( _ptr, hCookieContainer, pchHost, pchUrl, pchCookie );
		}
		
		// bool
		public bool SetHTTPRequestAbsoluteTimeoutMS( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint unMilliseconds /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestAbsoluteTimeoutMS( _ptr, hRequest, unMilliseconds );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestAbsoluteTimeoutMS( _ptr, hRequest, unMilliseconds );
		}
		
		// bool
		public bool SetHTTPRequestContextValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ulong ulContextValue /*uint64*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestContextValue( _ptr, hRequest, ulContextValue );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestContextValue( _ptr, hRequest, ulContextValue );
		}
		
		// bool
		public bool SetHTTPRequestCookieContainer( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestCookieContainer( _ptr, hRequest, hCookieContainer );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestCookieContainer( _ptr, hRequest, hCookieContainer );
		}
		
		// bool
		public bool SetHTTPRequestGetOrPostParameter( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchParamName /*const char **/, string pchParamValue /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestGetOrPostParameter( _ptr, hRequest, pchParamName, pchParamValue );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestGetOrPostParameter( _ptr, hRequest, pchParamName, pchParamValue );
		}
		
		// bool
		public bool SetHTTPRequestHeaderValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, string pchHeaderValue /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestHeaderValue( _ptr, hRequest, pchHeaderName, pchHeaderValue );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestHeaderValue( _ptr, hRequest, pchHeaderName, pchHeaderValue );
		}
		
		// bool
		public bool SetHTTPRequestNetworkActivityTimeout( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint unTimeoutSeconds /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestNetworkActivityTimeout( _ptr, hRequest, unTimeoutSeconds );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestNetworkActivityTimeout( _ptr, hRequest, unTimeoutSeconds );
		}
		
		// bool
		public bool SetHTTPRequestRawPostBody( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchContentType /*const char **/, out byte pubBody /*uint8 **/, uint unBodyLen /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestRawPostBody( _ptr, hRequest, pchContentType, out pubBody, unBodyLen );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestRawPostBody( _ptr, hRequest, pchContentType, out pubBody, unBodyLen );
		}
		
		// bool
		public bool SetHTTPRequestRequiresVerifiedCertificate( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, bool bRequireVerifiedCertificate /*bool*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestRequiresVerifiedCertificate( _ptr, hRequest, bRequireVerifiedCertificate );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestRequiresVerifiedCertificate( _ptr, hRequest, bRequireVerifiedCertificate );
		}
		
		// bool
		public bool SetHTTPRequestUserAgentInfo( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchUserAgentInfo /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamHTTP.SetHTTPRequestUserAgentInfo( _ptr, hRequest, pchUserAgentInfo );
			else return Platform.Win64.ISteamHTTP.SetHTTPRequestUserAgentInfo( _ptr, hRequest, pchUserAgentInfo );
		}
		
	}
}
