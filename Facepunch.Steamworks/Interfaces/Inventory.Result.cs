using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Inventory
    {
        public class Result : IDisposable
        {
            internal Inventory inventory;

            public int Handle { get; private set; }
            public Item[] Items { get; internal set; }

            public bool IsPending
            {
                get
                {
                    if ( Items != null ) return false;
                    if ( Handle == -1 ) return false;
                    if ( Status() == Callbacks.Result.Pending ) return true;

                    TryFill();
                    return false;
                }
            }

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


            internal Result( Inventory inventory, int Handle )
            {
                this.Handle = Handle;
                this.inventory = inventory;
            }

            public bool Block( float maxWait = 5.0f )
            {
                while ( IsPending )
                {
                    System.Threading.Thread.Sleep( 5 );
                }

                return IsSuccess;
            }

            internal void TryFill()
            {
                if ( Items != null )
                    return;

                if ( !IsSuccess )
                    return;

                Valve.Steamworks.SteamItemDetails_t[] steamItems = null;
                if ( !inventory.inventory.GetResultItems( Handle, out steamItems ) )
                    return;

                if ( steamItems == null )
                {
                    throw new System.Exception( "steamItems was null" );
                }

                Items = steamItems.Select( x =>
                {
                    return new Inventory.Item()
                    {
                        Quantity = x.m_unQuantity,
                        Id = x.m_itemId,
                        DefinitionId = x.m_iDefinition,
                        TradeLocked = ( (int)x.m_unFlags & (int)Valve.Steamworks.ESteamItemFlags.k_ESteamItemNoTrade ) != 0,
                        Definition = inventory.FindDefinition( x.m_iDefinition )
                    };
                } ).ToArray();
            }

            internal unsafe byte[] Serialize()
            {
                uint size = 0;
                inventory.inventory.SerializeResult( Handle, IntPtr.Zero, out size );

                var data = new byte[size];

                fixed ( byte* ptr = data )
                {
                    if ( !inventory.inventory.SerializeResult( Handle, (IntPtr) ptr, out size ) )
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
