using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamHTTP : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamHTTP( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid => platform != null && platform.IsValid;
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// HTTPCookieContainerHandle
		public HTTPCookieContainerHandle CreateCookieContainer( bool bAllowResponsesToModify /*bool*/ )
		{
			return platform.ISteamHTTP_CreateCookieContainer( bAllowResponsesToModify );
		}
		
		// HTTPRequestHandle
		public HTTPRequestHandle CreateHTTPRequest( HTTPMethod eHTTPRequestMethod /*EHTTPMethod*/, string pchAbsoluteURL /*const char **/ )
		{
			return platform.ISteamHTTP_CreateHTTPRequest( eHTTPRequestMethod, pchAbsoluteURL );
		}
		
		// bool
		public bool DeferHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			return platform.ISteamHTTP_DeferHTTPRequest( hRequest.Value );
		}
		
		// bool
		public bool GetHTTPDownloadProgressPct( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out float pflPercentOut /*float **/ )
		{
			return platform.ISteamHTTP_GetHTTPDownloadProgressPct( hRequest.Value, out pflPercentOut );
		}
		
		// bool
		public bool GetHTTPRequestWasTimedOut( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref bool pbWasTimedOut /*bool **/ )
		{
			return platform.ISteamHTTP_GetHTTPRequestWasTimedOut( hRequest.Value, ref pbWasTimedOut );
		}
		
		// bool
		public bool GetHTTPResponseBodyData( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out byte pBodyDataBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			return platform.ISteamHTTP_GetHTTPResponseBodyData( hRequest.Value, out pBodyDataBuffer, unBufferSize );
		}
		
		// bool
		public bool GetHTTPResponseBodySize( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, out uint unBodySize /*uint32 **/ )
		{
			return platform.ISteamHTTP_GetHTTPResponseBodySize( hRequest.Value, out unBodySize );
		}
		
		// bool
		public bool GetHTTPResponseHeaderSize( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, out uint unResponseHeaderSize /*uint32 **/ )
		{
			return platform.ISteamHTTP_GetHTTPResponseHeaderSize( hRequest.Value, pchHeaderName, out unResponseHeaderSize );
		}
		
		// bool
		public bool GetHTTPResponseHeaderValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, out byte pHeaderValueBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			return platform.ISteamHTTP_GetHTTPResponseHeaderValue( hRequest.Value, pchHeaderName, out pHeaderValueBuffer, unBufferSize );
		}
		
		// bool
		public bool GetHTTPStreamingResponseBodyData( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint cOffset /*uint32*/, out byte pBodyDataBuffer /*uint8 **/, uint unBufferSize /*uint32*/ )
		{
			return platform.ISteamHTTP_GetHTTPStreamingResponseBodyData( hRequest.Value, cOffset, out pBodyDataBuffer, unBufferSize );
		}
		
		// bool
		public bool PrioritizeHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			return platform.ISteamHTTP_PrioritizeHTTPRequest( hRequest.Value );
		}
		
		// bool
		public bool ReleaseCookieContainer( HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/ )
		{
			return platform.ISteamHTTP_ReleaseCookieContainer( hCookieContainer.Value );
		}
		
		// bool
		public bool ReleaseHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/ )
		{
			return platform.ISteamHTTP_ReleaseHTTPRequest( hRequest.Value );
		}
		
		// bool
		public bool SendHTTPRequest( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref SteamAPICall_t pCallHandle /*SteamAPICall_t **/ )
		{
			return platform.ISteamHTTP_SendHTTPRequest( hRequest.Value, ref pCallHandle.Value );
		}
		
		// bool
		public bool SendHTTPRequestAndStreamResponse( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ref SteamAPICall_t pCallHandle /*SteamAPICall_t **/ )
		{
			return platform.ISteamHTTP_SendHTTPRequestAndStreamResponse( hRequest.Value, ref pCallHandle.Value );
		}
		
		// bool
		public bool SetCookie( HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/, string pchHost /*const char **/, string pchUrl /*const char **/, string pchCookie /*const char **/ )
		{
			return platform.ISteamHTTP_SetCookie( hCookieContainer.Value, pchHost, pchUrl, pchCookie );
		}
		
		// bool
		public bool SetHTTPRequestAbsoluteTimeoutMS( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint unMilliseconds /*uint32*/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS( hRequest.Value, unMilliseconds );
		}
		
		// bool
		public bool SetHTTPRequestContextValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, ulong ulContextValue /*uint64*/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestContextValue( hRequest.Value, ulContextValue );
		}
		
		// bool
		public bool SetHTTPRequestCookieContainer( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, HTTPCookieContainerHandle hCookieContainer /*HTTPCookieContainerHandle*/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestCookieContainer( hRequest.Value, hCookieContainer.Value );
		}
		
		// bool
		public bool SetHTTPRequestGetOrPostParameter( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchParamName /*const char **/, string pchParamValue /*const char **/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestGetOrPostParameter( hRequest.Value, pchParamName, pchParamValue );
		}
		
		// bool
		public bool SetHTTPRequestHeaderValue( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchHeaderName /*const char **/, string pchHeaderValue /*const char **/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestHeaderValue( hRequest.Value, pchHeaderName, pchHeaderValue );
		}
		
		// bool
		public bool SetHTTPRequestNetworkActivityTimeout( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, uint unTimeoutSeconds /*uint32*/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestNetworkActivityTimeout( hRequest.Value, unTimeoutSeconds );
		}
		
		// bool
		public bool SetHTTPRequestRawPostBody( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchContentType /*const char **/, out byte pubBody /*uint8 **/, uint unBodyLen /*uint32*/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestRawPostBody( hRequest.Value, pchContentType, out pubBody, unBodyLen );
		}
		
		// bool
		public bool SetHTTPRequestRequiresVerifiedCertificate( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, bool bRequireVerifiedCertificate /*bool*/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate( hRequest.Value, bRequireVerifiedCertificate );
		}
		
		// bool
		public bool SetHTTPRequestUserAgentInfo( HTTPRequestHandle hRequest /*HTTPRequestHandle*/, string pchUserAgentInfo /*const char **/ )
		{
			return platform.ISteamHTTP_SetHTTPRequestUserAgentInfo( hRequest.Value, pchUserAgentInfo );
		}
		
	}
}
