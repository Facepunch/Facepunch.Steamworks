using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Inventory
    {
        /// <summary>
        /// An item in your inventory.
        /// </summary>
        public struct Item : IEquatable<Item>
        {
            public struct Amount
            {
                public Item Item;
                public int Quantity;
            }

            public ulong Id;
            public int Quantity;

            public int DefinitionId;

            public Dictionary<string, string> Properties { get; internal set; }

            /// <summary>
            /// Careful, this might not be available. Especially on a game server.
            /// </summary>
            public Definition Definition;

            public bool TradeLocked;

            public bool Equals( Item other )
            {
                return Equals( other, this );
            }

            public override bool Equals( object obj )
            {
                if ( obj == null || GetType() != obj.GetType() )
                {
                    return false;
                }

                return ((Item)obj).Id == Id;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }


            public static bool operator ==( Item c1, Item c2 )
            {
                return c1.Equals( c2 );
            }

            public static bool operator !=( Item c1, Item c2 )
            {
                return !c1.Equals( c2 );
            }

            /// <summary>
            /// Consumes items from a user's inventory. If the quantity of the given item goes to zero, it is permanently removed.
            /// Once an item is removed it cannot be recovered.This is not for the faint of heart - if your game implements item removal at all, 
            /// a high-friction UI confirmation process is highly recommended.ConsumeItem can be restricted to certain item definitions or fully
            /// blocked via the Steamworks website to minimize support/abuse issues such as the classic "my brother borrowed my laptop and deleted all of my rare items".
            /// </summary>
            public Result Consume( int amount = 1 )
            {
                SteamNative.SteamInventoryResult_t resultHandle = -1;
                if ( !Definition.inventory.inventory.ConsumeItem( ref resultHandle, Id, (uint)amount ) )
                    return null;

                return new Result( Definition.inventory, resultHandle, true );
            }

            /// <summary>
            /// Split stack into two items
            /// </summary>
            public Result SplitStack( int quantity = 1 )
            {
                SteamNative.SteamInventoryResult_t resultHandle = -1;
                if ( !Definition.inventory.inventory.TransferItemQuantity( ref resultHandle, Id, (uint)quantity, ulong.MaxValue ) )
                    return null;

                return new Result( Definition.inventory, resultHandle, true );
            }

            SteamNative.SteamInventoryUpdateHandle_t updateHandle;

            private void UpdatingProperties()
            {
                if (updateHandle != 0) return;

                updateHandle = Definition.inventory.inventory.StartUpdateProperties();
            }

            public bool SetProperty( string name, string value )
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Definition.inventory.inventory.SetProperty(updateHandle, Id, name, value);
            }

            public bool SetProperty(string name, bool value)
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Definition.inventory.inventory.SetProperty0(updateHandle, Id, name, value);
            }

            public bool SetProperty(string name, long value)
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Definition.inventory.inventory.SetProperty1(updateHandle, Id, name, value);
            }

            public bool SetProperty(string name, float value)
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Definition.inventory.inventory.SetProperty2(updateHandle, Id, name, value);
            }

            /// <summary>
            /// Called to finalize any changes made using SetProperty
            /// </summary>
            public bool SubmitProperties()
            {
                if (updateHandle == 0)
                    throw new Exception("SubmitProperties called without updating properties");

                try
                {
                    SteamNative.SteamInventoryResult_t result = -1;

                    if (!Definition.inventory.inventory.SubmitUpdateProperties(updateHandle, ref result))
                    {
                        return false;
                    }

                    Definition.inventory.inventory.DestroyResult(result);

                    return true;
                }
                finally
                {
                    updateHandle = 0;
                }
            }
        }
    }
}
