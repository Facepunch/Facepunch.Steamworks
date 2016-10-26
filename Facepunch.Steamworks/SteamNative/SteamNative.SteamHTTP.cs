using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamHTTP : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamHTTP( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// HTTPCookieContainerHandle
		public HTTPCookieContainerHandle CreateCookieContainer( bool bAllowResponsesToModify /*bool*/ )
		{
			return _pi.ISteamHTTP_CreateCookieContainer( bAllowResponsesToModify /*C*/ );
		}
		
		// HTTPRequestHandle
		public HTTPRequestHandle CreateHTTPRequest( HTTPMethod eHTTPRequestMethod /*EHTTPMethod*/, string pchAbsoluteURL /*const char **/ )
		{
			return _pi.ISteamHTTP_CreateHTTPRequest( eHTTPRequestMethod /*C*/, pchAbsoluteURL /*C*/ );
		}
		
		// bool
		public bool DeferHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			return _pi.ISteamHTTP_DeferHTTPRequest( hRequest.Value /*C*/ );
		}
		
		// bool
		public bool GetHTTPDownloadProgressPct( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out float pflPercentOut /*float **/ )
		{
			return _pi.ISteamHTTP_GetHTTPDownloadProgressPct( hRequest.Value /*C*/, out pflPercentOut /*B*/ );
		}
		
		// bool
		public bool GetHTTPRequestWasTimedOut( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref bool pbWasTimedOut /*bool **/ )
		{
			return _pi.ISteamHTTP_GetHTTPRequestWasTimedOut( hRequest.Value /*C*/, ref pbWasTimedOut /*A*/ );
		}
		
		// bool
		public bool GetHTTPResponseBodyData( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out byte pBodyDataBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			return _pi.ISteamHTTP_GetHTTPResponseBodyData( hRequest.Value /*C*/, out pBodyDataBuffer /*B*/, unBufferSize /*C*/ );
		}
		
		// bool
		public bool GetHTTPResponseBodySize( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out uint unBodySize /*uint32 **/ )
		{
			return _pi.ISteamHTTP_GetHTTPResponseBodySize( hRequest.Value /*C*/, out unBodySize /*B*/ );
		}
		
		// bool
		public bool GetHTTPResponseHeaderSize( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, out uint unResponseHeaderSize /*uint32 **/ )
		{
			return _pi.ISteamHTTP_GetHTTPResponseHeaderSize( hRequest.Value /*C*/, pchHeaderName /*C*/, out unResponseHeaderSize /*B*/ );
		}
		
		// bool
		public bool GetHTTPResponseHeaderValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, out byte pHeaderValueBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			return _pi.ISteamHTTP_GetHTTPResponseHeaderValue( hRequest.Value /*C*/, pchHeaderName /*C*/, out pHeaderValueBuffer /*B*/, unBufferSize /*C*/ );
		}
		
		// bool
		public bool GetHTTPStreamingResponseBodyData( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint cOffset /*uint32*/, out byte pBodyDataBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			return _pi.ISteamHTTP_GetHTTPStreamingResponseBodyData( hRequest.Value /*C*/, cOffset /*C*/, out pBodyDataBuffer /*B*/, unBufferSize /*C*/ );
		}
		
		// bool
		public bool PrioritizeHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			return _pi.ISteamHTTP_PrioritizeHTTPRequest( hRequest.Value /*C*/ );
		}
		
		// bool
		public bool ReleaseCookieContainer( HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/ )
		{
			return _pi.ISteamHTTP_ReleaseCookieContainer( hCookieContainer.Value /*C*/ );
		}
		
		// bool
		public bool ReleaseHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			return _pi.ISteamHTTP_ReleaseHTTPRequest( hRequest.Value /*C*/ );
		}
		
		// bool
		public bool SendHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref SteamAPICall_t pCallHandle /*SteamAPICall_t **/ )
		{
			return _pi.ISteamHTTP_SendHTTPRequest( hRequest.Value /*C*/, ref pCallHandle.Value /*A*/ );
		}
		
		// bool
		public bool SendHTTPRequestAndStreamResponse( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref SteamAPICall_t pCallHandle /*SteamAPICall_t **/ )
		{
			return _pi.ISteamHTTP_SendHTTPRequestAndStreamResponse( hRequest.Value /*C*/, ref pCallHandle.Value /*A*/ );
		}
		
		// bool
		public bool SetCookie( HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/, string pchHost /*const char **/, string pchUrl /*const char **/, string pchCookie /*const char **/ )
		{
			return _pi.ISteamHTTP_SetCookie( hCookieContainer.Value /*C*/, pchHost /*C*/, pchUrl /*C*/, pchCookie /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestAbsoluteTimeoutMS( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint unMilliseconds /*uint32*/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS( hRequest.Value /*C*/, unMilliseconds /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestContextValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ulong ulContextValue /*uint64*/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestContextValue( hRequest.Value /*C*/, ulContextValue /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestCookieContainer( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestCookieContainer( hRequest.Value /*C*/, hCookieContainer.Value /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestGetOrPostParameter( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchParamName /*const char **/, string pchParamValue /*const char **/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestGetOrPostParameter( hRequest.Value /*C*/, pchParamName /*C*/, pchParamValue /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestHeaderValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, string pchHeaderValue /*const char **/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestHeaderValue( hRequest.Value /*C*/, pchHeaderName /*C*/, pchHeaderValue /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestNetworkActivityTimeout( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint unTimeoutSeconds /*uint32*/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestNetworkActivityTimeout( hRequest.Value /*C*/, unTimeoutSeconds /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestRawPostBody( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchContentType /*const char **/, out byte pubBody /*uint8 **/, uint unBodyLen /*uint32*/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestRawPostBody( hRequest.Value /*C*/, pchContentType /*C*/, out pubBody /*B*/, unBodyLen /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestRequiresVerifiedCertificate( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, bool bRequireVerifiedCertificate /*bool*/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate( hRequest.Value /*C*/, bRequireVerifiedCertificate /*C*/ );
		}
		
		// bool
		public bool SetHTTPRequestUserAgentInfo( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchUserAgentInfo /*const char **/ )
		{
			return _pi.ISteamHTTP_SetHTTPRequestUserAgentInfo( hRequest.Value /*C*/, pchUserAgentInfo /*C*/ );
		}
		
	}
}
