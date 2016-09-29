using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks.Interop
{
    class ServerRules
    {
        private GCHandle thisPin;
        private IntPtr vTablePtr;
        private GCHandle vTableHandle;

        private ServerList.Server Server;

        public ServerRules( ServerList.Server server, uint address, int queryPort )
        {
            Server = server;

            InstallVTable();

            Valve.Interop.NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_ServerRules( Server.Client.native.servers.GetIntPtr(), address, (short) queryPort, GetPtr() );
        }

        void InstallVTable()
        {
            GC.KeepAlive( Server );
            GC.SuppressFinalize( Server );

            if ( Server.Client.UseThisCall )
            {
                var t = new ThisVTable()
                {
                    m_VTRulesResponded = ( _, k, v ) => InternalOnRulesResponded( k, v ),
                    m_VTRulesFailedToRespond = ( _ ) => InternalOnRulesFailedToRespond(),
                    m_VTRulesRefreshComplete = ( _ ) => InternalOnRulesRefreshComplete(),
                };
                vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( ThisVTable ) ) );
                Marshal.StructureToPtr( t, vTablePtr, false );

                vTableHandle = GCHandle.Alloc( vTablePtr, GCHandleType.Pinned );
                
            }
            else
            {
                var t = new StdVTable()
                {
                    m_VTRulesResponded = InternalOnRulesResponded,
                    m_VTRulesFailedToRespond = InternalOnRulesFailedToRespond,
                    m_VTRulesRefreshComplete = InternalOnRulesRefreshComplete
                };
                vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( StdVTable ) ) );
                Marshal.StructureToPtr( t, vTablePtr, false );

                vTableHandle = GCHandle.Alloc( vTablePtr, GCHandleType.Pinned );
            }
        }

        void Unpin()
        {
            if ( vTablePtr != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( vTablePtr );
            }

            if ( vTableHandle.IsAllocated )
            {
                vTableHandle.Free();
            }

            if ( thisPin.IsAllocated )
            {
                thisPin.Free();
            }
        }

        private void InternalOnRulesResponded( IntPtr k, IntPtr v )
        {
           // Server.Rules.Add( k, v );
        }
        private void InternalOnRulesFailedToRespond()
        {
            Server.OnServerRulesReceiveFinished( false );
        }
        private void InternalOnRulesRefreshComplete()
        {
            Server.OnServerRulesReceiveFinished( true );
        }

        [StructLayout( LayoutKind.Sequential )]
        private class StdVTable
        {
            [NonSerialized]
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesResponded m_VTRulesResponded;

            [NonSerialized]
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesFailedToRespond m_VTRulesFailedToRespond;

            [NonSerialized]
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesRefreshComplete m_VTRulesRefreshComplete;

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate void InternalRulesResponded( IntPtr pchRule, IntPtr pchValue );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate void InternalRulesFailedToRespond();
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate void InternalRulesRefreshComplete();
        }

        [StructLayout( LayoutKind.Sequential )]
        private class ThisVTable
        {
            [NonSerialized]
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesResponded m_VTRulesResponded;

            [NonSerialized]
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesFailedToRespond m_VTRulesFailedToRespond;

            [NonSerialized]
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesRefreshComplete m_VTRulesRefreshComplete;

            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
            public delegate void InternalRulesResponded( IntPtr thisptr, IntPtr pchRule, IntPtr pchValue );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
            public delegate void InternalRulesFailedToRespond( IntPtr thisptr );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
            public delegate void InternalRulesRefreshComplete( IntPtr thisptr );
        }

        public System.IntPtr GetPtr()
        {
            return vTableHandle.AddrOfPinnedObject();
        }
    };
}
