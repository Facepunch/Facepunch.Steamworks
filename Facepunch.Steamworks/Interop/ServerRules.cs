using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks.Interop
{
    class ServerRules : IDisposable
    {
        // Pins and pointers for the created vtable
        private GCHandle vTablePin;
        private IntPtr vTablePtr;

        // Pins for the functions
        private GCHandle RulesRespondPin;
        private GCHandle FailedRespondPin;
        private GCHandle CompletePin;

        // The server that called us
        private ServerList.Server Server;

        public ServerRules( ServerList.Server server, IPAddress address, int queryPort )
        {
            Server = server;

            //
            // Create a fake VTable to pass to c++
            //
            InstallVTable();

            //
            // Ask Steam to get the server rules, respond to our fake vtable
            //
            Server.Client.native.servers.ServerRules( Utility.IpToInt32( address ), (ushort)queryPort, GetPtr() );
        }

        public void Dispose()
        {
            if ( vTablePtr != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( vTablePtr );
                vTablePtr = IntPtr.Zero;
            }

            if ( vTablePin.IsAllocated )
                vTablePin.Free();

            if ( RulesRespondPin.IsAllocated )
                RulesRespondPin.Free();

            if ( FailedRespondPin.IsAllocated )
                FailedRespondPin.Free();

            if ( CompletePin.IsAllocated )
                CompletePin.Free();
        }

        void InstallVTable()
        {

            //
            // Depending on platform, we either use ThisCall or stdcall.
            // This is a bit of a fuckabout but you need to define Client.UseThisCall
            //

            if ( Config.UseThisCall )
            {
                ThisVTable.InternalRulesResponded da = ( _, k, v ) => InternalOnRulesResponded( k, v );
                ThisVTable.InternalRulesFailedToRespond db = ( _ ) => InternalOnRulesFailedToRespond();
                ThisVTable.InternalRulesRefreshComplete dc = ( _ ) => InternalOnRulesRefreshComplete();

                RulesRespondPin = GCHandle.Alloc( da );
                FailedRespondPin = GCHandle.Alloc( db );
                CompletePin = GCHandle.Alloc( dc );

                var t = new ThisVTable()
                {
                    m_VTRulesResponded = da,
                    m_VTRulesFailedToRespond = db,
                    m_VTRulesRefreshComplete = dc,
                };
                vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( ThisVTable ) ) );
                Marshal.StructureToPtr( t, vTablePtr, false );

                vTablePin = GCHandle.Alloc( vTablePtr, GCHandleType.Pinned );
                
            }
            else
            {
                StdVTable.InternalRulesResponded da = InternalOnRulesResponded;
                StdVTable.InternalRulesFailedToRespond db = InternalOnRulesFailedToRespond;
                StdVTable.InternalRulesRefreshComplete dc = InternalOnRulesRefreshComplete;

                RulesRespondPin = GCHandle.Alloc( da );
                FailedRespondPin = GCHandle.Alloc( db );
                CompletePin = GCHandle.Alloc( dc );

                var t = new StdVTable()
                {
                    m_VTRulesResponded = da,
                    m_VTRulesFailedToRespond = db,
                    m_VTRulesRefreshComplete = dc
                };
                vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( StdVTable ) ) );
                Marshal.StructureToPtr( t, vTablePtr, false );

                vTablePin = GCHandle.Alloc( vTablePtr, GCHandleType.Pinned );
            }
        }

        private void InternalOnRulesResponded( string k, string v )
        {
           Server.Rules.Add( k, v );
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
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesResponded m_VTRulesResponded;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesFailedToRespond m_VTRulesFailedToRespond;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesRefreshComplete m_VTRulesRefreshComplete;

            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate void InternalRulesResponded( string pchRule, string pchValue );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate void InternalRulesFailedToRespond();
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]
            public delegate void InternalRulesRefreshComplete();
        }

        [StructLayout( LayoutKind.Sequential )]
        private class ThisVTable
        {
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesResponded m_VTRulesResponded;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesFailedToRespond m_VTRulesFailedToRespond;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public InternalRulesRefreshComplete m_VTRulesRefreshComplete;

            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
            public delegate void InternalRulesResponded( IntPtr thisptr, string pchRule, string pchValue );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
            public delegate void InternalRulesFailedToRespond( IntPtr thisptr );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
            public delegate void InternalRulesRefreshComplete( IntPtr thisptr );
        }

        public System.IntPtr GetPtr()
        {
            return vTablePin.AddrOfPinnedObject();
        }
    };
}
