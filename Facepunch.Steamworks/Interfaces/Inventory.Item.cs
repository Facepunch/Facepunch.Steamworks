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
        public class Item : IEquatable<Item>
        {
            internal Item( Inventory Inventory, ulong Id, int Quantity, int DefinitionId )
            {
                this.Inventory = Inventory;
                this.Id = Id;
                this.Quantity = Quantity;
                this.DefinitionId = DefinitionId;
            }

            public struct Amount
            {
                public Item Item;
                public int Quantity;
            }

            public ulong Id;
            public int Quantity;

            public int DefinitionId;

            internal Inventory Inventory;

            public Dictionary<string, string> Properties { get; internal set; }

            private Definition _cachedDefinition;

            /// <summary>
            /// Careful, this might not be available. Especially on a game server.
            /// </summary>
            public Definition Definition
            {
                get
                {
                    if ( _cachedDefinition != null )
                        return _cachedDefinition;

                    _cachedDefinition = Inventory.FindDefinition( DefinitionId );
                    return _cachedDefinition;
                }
            }

            public bool TradeLocked;

            public bool Equals(Item other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Id == other.Id;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Item)obj);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public static bool operator ==(Item left, Item right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Item left, Item right)
            {
                return !Equals(left, right);
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
                if ( !Inventory.inventory.ConsumeItem( ref resultHandle, Id, (uint)amount ) )
                    return null;

                return new Result( Inventory, resultHandle, true );
            }

            /// <summary>
            /// Split stack into two items
            /// </summary>
            public Result SplitStack( int quantity = 1 )
            {
                SteamNative.SteamInventoryResult_t resultHandle = -1;
                if ( !Inventory.inventory.TransferItemQuantity( ref resultHandle, Id, (uint)quantity, ulong.MaxValue ) )
                    return null;

                return new Result( Inventory, resultHandle, true );
            }

            SteamNative.SteamInventoryUpdateHandle_t updateHandle;

            private void UpdatingProperties()
            {
                if (!Inventory.EnableItemProperties)
                    throw new InvalidOperationException("Item properties are disabled.");

                if (updateHandle != 0) return;

                updateHandle = Inventory.inventory.StartUpdateProperties();
            }

            public bool SetProperty( string name, string value )
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Inventory.inventory.SetProperty(updateHandle, Id, name, value);
            }

            public bool SetProperty(string name, bool value)
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Inventory.inventory.SetProperty0(updateHandle, Id, name, value);
            }

            public bool SetProperty(string name, long value)
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Inventory.inventory.SetProperty1(updateHandle, Id, name, value);
            }

            public bool SetProperty(string name, float value)
            {
                UpdatingProperties();
                Properties[name] = value.ToString();
                return Inventory.inventory.SetProperty2(updateHandle, Id, name, value);
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

                    if (!Inventory.inventory.SubmitUpdateProperties(updateHandle, ref result))
                    {
                        return false;
                    }

                    Inventory.inventory.DestroyResult(result);

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
