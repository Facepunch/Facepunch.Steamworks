using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
    [UnmanagedFunctionPointer( Platform.CC )]
    internal delegate void NetDebugFunc( [In] NetDebugOutput nType, [In] IntPtr pszMsg );

    [UnmanagedFunctionPointer( Platform.CC )]
    internal unsafe delegate void FnSteamNetConnectionStatusChanged( ref SteamNetConnectionStatusChangedCallback_t arg );

    [UnmanagedFunctionPointer( Platform.CC )]
    internal delegate void FnSteamNetAuthenticationStatusChanged( ref SteamNetAuthenticationStatus_t arg );

    [UnmanagedFunctionPointer( Platform.CC )]
    internal delegate void FnSteamRelayNetworkStatusChanged( ref SteamRelayNetworkStatus_t arg );

    [UnmanagedFunctionPointer( Platform.CC )]
    internal delegate void FnSteamNetworkingMessagesSessionRequest( ref SteamNetworkingMessagesSessionRequest_t arg );

    [UnmanagedFunctionPointer( Platform.CC )]
    internal delegate void FnSteamNetworkingMessagesSessionFailed( ref SteamNetworkingMessagesSessionFailed_t arg );

    [UnmanagedFunctionPointer( Platform.CC )]
    internal delegate void FnSteamNetworkingFakeIPResult( ref SteamNetworkingFakeIPResult_t arg );
}
