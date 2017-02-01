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
        }
    }
}
