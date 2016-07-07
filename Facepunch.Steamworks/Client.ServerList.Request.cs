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

                internal static Server FromSteam( gameserveritem_t item )
                {
                    return new Server()
                    {
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


            public servernetadr_t m_NetAdr;
            public int m_nPing;
            [MarshalAs(UnmanagedType.I1)]
            public bool m_bHadSuccessfulResponse;
            [MarshalAs(UnmanagedType.I1)]
            public bool m_bDoNotRefresh;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_szGameDir; //char[32]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_szMap; //char[32]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string m_szGameDescription; //char[64]
            public uint m_nAppID;
            public int m_nPlayers;
            public int m_nMaxPlayers;
            public int m_nBotPlayers;
            [MarshalAs(UnmanagedType.I1)]
            public bool m_bPassword;
            [MarshalAs(UnmanagedType.I1)]
            public bool m_bSecure;
            public uint m_ulTimeLastPlayed;
            public int m_nServerVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string m_szServerName; //char[64]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string m_szGameTags; //char[128]
            public ulong m_steamID;

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
                        client._servers.CancelQuery( Id );

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

                var info = client._servers.GetServerDetails( Id, iServer );
                Unresponsive.Add( Server.FromSteam( info ) );
            }

            private void OnServerResponded( IntPtr thisptr, IntPtr RequestId, int iServer )
            {
                if ( RequestId != Id )
                    throw new Exception( "Request ID is invalid!" );

                var info = client._servers.GetServerDetails( Id, iServer );
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
