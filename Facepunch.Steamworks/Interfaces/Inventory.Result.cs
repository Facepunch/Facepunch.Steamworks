using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public partial class Inventory
    {
        public class Result : IDisposable
        {
            internal static Dictionary< int, Result > Pending;
            internal Inventory inventory;

            private SteamNative.SteamInventoryResult_t Handle { get; set; }

            /// <summary>
            /// Called when result is successfully returned
            /// </summary>
            public Action<Result> OnResult;

            /// <summary>
            /// Items that exist, or that have been created, or changed
            /// </summary>
            public Item[] Items { get; internal set; }

            /// <summary>
            /// Items that have been removed or somehow destroyed
            /// </summary>
            public Item[] Removed { get; internal set; }

            /// <summary>
            /// Items that have been consumed, like in a craft or something
            /// </summary>
            public Item[] Consumed { get; internal set; }

            /// <summary>
            /// Returns true if this result is still pending
            /// </summary>
            public bool IsPending { get; internal set; }

            internal uint Timestamp { get; private set; }

            internal bool IsSuccess
            {
                get
                {
                    if ( Items != null ) return true;
                    if ( Handle == -1 ) return false;
                    return Status() == Callbacks.Result.OK;
                }
            }

            internal Callbacks.Result Status()
            {
                if ( Handle == -1 ) return Callbacks.Result.InvalidParam;
                return (Callbacks.Result)inventory.inventory.GetResultStatus( Handle );
            }

            internal Result( Inventory inventory, int Handle, bool pending )
            {
                if ( pending )
                {
                    Pending.Add( Handle, this );
                }

                this.Handle = Handle;
                this.inventory = inventory;
            }

            internal void Fill()
            {
                if ( Items != null )
                    return;

                this.IsPending = false;

                Timestamp = inventory.inventory.GetResultTimestamp( Handle );

                SteamNative.SteamItemDetails_t[] steamItems = inventory.inventory.GetResultItems( Handle );
                if ( steamItems == null )
                    return;

                if ( steamItems == null )
                {
                    throw new System.Exception( "steamItems was null" );
                }

                Items = steamItems.Where( x => ( (int)x.Flags & (int)SteamNative.SteamItemFlags.Removed ) != (int)SteamNative.SteamItemFlags.Removed && ( (int)x.Flags & (int)SteamNative.SteamItemFlags.Consumed ) != (int)SteamNative.SteamItemFlags.Consumed )
                .Select( x =>
                {
                    return new Inventory.Item()
                    {
                        Quantity = x.Quantity,
                        Id = x.ItemId,
                        DefinitionId = x.Definition,
                        TradeLocked = ( (int)x.Flags & (int)SteamNative.SteamItemFlags.NoTrade ) != 0,
                        Definition = inventory.FindDefinition( x.Definition )
                    };
                } ).ToArray();

                Removed = steamItems.Where( x => ( (int)x.Flags & (int)SteamNative.SteamItemFlags.Removed ) != 0 )
                .Select( x =>
                {
                    return new Inventory.Item()
                    {
                        Quantity = x.Quantity,
                        Id = x.ItemId,
                        DefinitionId = x.Definition,
                        TradeLocked = ( (int)x.Flags & (int)SteamNative.SteamItemFlags.NoTrade ) != 0,
                        Definition = inventory.FindDefinition( x.Definition )
                    };
                } ).ToArray();

                Consumed = steamItems.Where( x => ( (int)x.Flags & (int)SteamNative.SteamItemFlags.Consumed ) != 0 )
                .Select( x =>
                {
                    return new Inventory.Item()
                    {
                        Quantity = x.Quantity,
                        Id = x.ItemId,
                        DefinitionId = x.Definition,
                        TradeLocked = ( (int)x.Flags & (int)SteamNative.SteamItemFlags.NoTrade ) != 0,
                        Definition = inventory.FindDefinition( x.Definition )
                    };
                } ).ToArray();

                if ( OnResult != null )
                {
                    OnResult( this );
                }
            }

            internal void OnSteamResult( SteamInventoryResultReady_t data, bool error )
            {
                var success = data.Esult == SteamNative.Result.OK && !error;

                if ( success )
                {
                    Fill();
                }
            }

            internal unsafe byte[] Serialize()
            {
                uint size = 0;
                inventory.inventory.SerializeResult( Handle, IntPtr.Zero, out size );

                var data = new byte[size];

                fixed ( byte* ptr = data )
                {
                    if ( !inventory.inventory.SerializeResult( Handle, (IntPtr)ptr, out size ) )
                        return null;
                }

                return data;
            }

            public void Dispose()
            {
                inventory.inventory.DestroyResult( Handle );
                Handle = -1;
                inventory = null;
            }
        }
    }
}
