using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{
    public partial class ServerList
    {
        public class Request : IDisposable
        {
            internal Client client;
            internal IntPtr Id;

            private IntPtr m_pVTable;
            private GCHandle m_pGCHandle;

            public struct Server
            {
                public string Name { get; set; }
                public int Ping { get; set; }
                public string GameDir { get; set; }
                public string Map { get; set; }
                public string Description { get; set; }
                public uint AppId { get; set; }
                public int Players { get; set; }
                public int MaxPlayers { get; set; }
                public int BotPlayers { get; set; }
                public bool Passworded { get; set; }
                public bool Secure { get; set; }
                public uint LastTimePlayed { get; set; }
                public int Version { get; set; }
                public string[] Tags { get; set; }
                public ulong SteamId { get; set; }

                public uint Address { get; set; }

                public int ConnectionPort { get; set; }

                public int QueryPort { get; set; }

                public string AddressString
                {
                    get
                    {
                        return string.Format( "{0}.{1}.{2}.{3}", ( Address >> 24 ) & 0xFFul, ( Address >> 16 ) & 0xFFul, ( Address >> 8 ) & 0xFFul, Address & 0xFFul );
                    }
                }
                public string ConnectionAddress
                {
                    get
                    {
                        return string.Format( "{0}.{1}.{2}.{3}:{4}", ( Address >> 24 ) & 0xFFul, ( Address >> 16 ) & 0xFFul, ( Address >> 8 ) & 0xFFul, Address & 0xFFul, ConnectionPort );
                    }
                }

                

                internal static Server FromSteam( gameserveritem_t item )
                {
                    return new Server()
                    {
                        Address = item.m_NetAdr.m_unIP,
                        ConnectionPort = item.m_NetAdr.m_usConnectionPort,
                        QueryPort = item.m_NetAdr.m_usQueryPort,
                        Name = item.m_szServerName,
                        Ping = item.m_nPing,
                        GameDir = item.m_szGameDir,
                        Map = item.m_szMap,
                        Description = item.m_szGameDescription,
                        AppId = item.m_nAppID,
                        Players = item.m_nPlayers,
                        MaxPlayers = item.m_nMaxPlayers,
                        BotPlayers = item.m_nBotPlayers,
                        Passworded = item.m_bPassword,
                        Secure = item.m_bSecure,
                        LastTimePlayed = item.m_ulTimeLastPlayed,
                        Version = item.m_nServerVersion,
                        Tags = item.m_szGameTags.Split( ',' ),
                        SteamId = item.m_steamID
                    };
                }
            }

            /// <summary>
            /// A list of servers that responded. If you're only interested in servers that responded since you
            /// last updated, then simply clear this list.
            /// </summary>
            public List<Server> Responded = new List<Server>();

            /// <summary>
            /// A list of servers that were in the master list but didn't respond. 
            /// </summary>
            public List<Server> Unresponsive = new List<Server>();

            /// <summary>
            /// True when we have finished
            /// </summary>
            public bool Finished = false;

            internal Request()
            {
                //
                // Create a fake vtable for Steam to respond to
                //
                var vt = new VTable()
                {
                    responded = OnServerResponded,
                    nonresponsive = NonResponsive,
                    complete = Complete
                };

                m_pVTable = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( VTable ) ) );
                Marshal.StructureToPtr( vt, m_pVTable, false );
                m_pGCHandle = GCHandle.Alloc( m_pVTable, GCHandleType.Pinned );
            }

            ~Request()
            {
                Dispose();
            }

            /// <summary>
            /// Disposing will end the query
            /// </summary>
            public void Dispose()
            {
                //
                // Cancel the query if it's still running
                //
                if ( !Finished && Id != IntPtr.Zero )
                {
                    if ( client.Valid )
                        client.native.servers.CancelQuery( Id );

                    Id = IntPtr.Zero;
                }

                //
                // Release the pinned GC resources
                //
                if ( m_pVTable != IntPtr.Zero )
                {
                    Marshal.FreeHGlobal( m_pVTable );
                    m_pVTable = IntPtr.Zero;
                }

                if ( m_pGCHandle.IsAllocated )
                {
                    m_pGCHandle.Free();
                }
            }

            private void Complete( IntPtr thisptr, IntPtr RequestId, int response )
            {
                if ( RequestId != Id )
                    throw new Exception( "Request ID is invalid!" );

                Finished = true;
                Id = IntPtr.Zero;
            }

            private void NonResponsive( IntPtr thisptr, IntPtr RequestId, int iServer )
            {
                if ( RequestId != Id )
                    throw new Exception( "Request ID is invalid!" );

                var info = client.native.servers.GetServerDetails( Id, iServer );
                Unresponsive.Add( Server.FromSteam( info ) );
            }

            private void OnServerResponded( IntPtr thisptr, IntPtr RequestId, int iServer )
            {
                if ( RequestId != Id )
                    throw new Exception( "Request ID is invalid!" );

                var info = client.native.servers.GetServerDetails( Id, iServer );
                Responded.Add( Server.FromSteam( info ) );

                System.Diagnostics.Debug.WriteLine( info.m_szServerName );
            }

            internal IntPtr GetVTablePointer()
            {
                return m_pGCHandle.AddrOfPinnedObject();
            }

            [StructLayout( LayoutKind.Sequential )]
            internal class VTable
            {
                [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
                internal delegate void InternalServerResponded( IntPtr thisptr, IntPtr hRequest, int iServer );
                [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
                internal delegate void InternalServerFailedToRespond( IntPtr thisptr, IntPtr hRequest, int iServer );
                [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
                internal delegate void InternalRefreshComplete( IntPtr thisptr, IntPtr hRequest, int response );

                [NonSerialized, MarshalAs(UnmanagedType.FunctionPtr)]
                internal InternalServerResponded responded;
                [NonSerialized, MarshalAs(UnmanagedType.FunctionPtr)]
                internal InternalServerFailedToRespond nonresponsive;
                [NonSerialized, MarshalAs(UnmanagedType.FunctionPtr)]
                internal InternalRefreshComplete complete;
            }
        }
    }
}
